/**
 * Licensed under the Apache License, Version 2.0 (the "License"); you may not
 * use this file except in compliance with the License. You may obtain a copy of
 * the License at
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 */


using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using obd_dotnet_api.exceptions;

namespace obd_dotnet_api.commands
{
    /// <summary>base obd command</summary>
    public abstract class ObdCommand
    {
        /// <summary>Error classes to be tested in order</summary>
        private readonly Type[] _errorClasses =
        {
            typeof(UnableToConnectException),
            typeof(BusInitException),
            typeof(MisunderstoodCommandException),
            typeof(NoDataException),
            typeof(StoppedException),
            typeof(UnknownErrorException),
            typeof(UnsupportedCommandException)
        };


        //fields
        protected readonly string Cmd;
        protected string RawData = null;
        protected int ResponseDelayInMs = 0;

        //properties
        protected List<int> Buffer { get; set; }
        protected bool UseImperialUnits { get; set; }
        
        /// <summary>Pid of the Command</summary>
        public string CommandPid => Cmd.Substring(3);
        
        /// <summary>Raw Data Result</summary>
        public string Result => RawData;

        /// <summary>Start</summary>
        public long Start { get; set; }
        
        /// <summary>End</summary>
        public long End { get; set; }

        /// <summary> Time in ms the command waits before returning from #sendCommand() </summary>
        public int ResponseTimeDelay
        {
            get => ResponseDelayInMs;
            set => ResponseDelayInMs = value;
        }

        /// <summary>Command Mode</summary>
        public string CommandMode => Cmd.Length >= 2 ? Cmd.Substring(0, 2) : Cmd;

        /// <summary>The unit of the result, as used in <see cref="FormattedResult"/></summary>
        /// <returns>a string representing the unit or "", never null</returns>
        public virtual string ResultUnit => "";

        /// <summary>a formatted command response in string representation.</summary>
        public abstract string FormattedResult { get; }

        /// <summary>the command response in string representation, without formatting.</summary>
        public abstract string CalculatedResult { get; }

        /// <summary>the OBD command name.</summary>
        public abstract string Name { get; }

        /// <summary> Default ctor to use </summary>
        /// <param name="command"> command the command to send </param>
        public ObdCommand(string command)
        {
            Cmd = command;
            Buffer = new List<int>();
        }

        /// <summary> Prevent empty instantiation </summary>
        private ObdCommand()
        {
        }

        /// <summary>Copy ctor</summary>
        /// <param name="other">ObdCommand to Copy</param>
        public ObdCommand(ObdCommand other)
            : this(other.Cmd)
        {
        }

        /// <summary>
        /// Sends the OBD-II request and deals with the response.
        /// This method CAN be overriden in fake commands.
        /// </summary>
        /// <param name="inputStream">stream we read the result from</param>
        /// <param name="outputStream">stream we write the command to</param>
        [MethodImpl(MethodImplOptions.Synchronized)] //Only one command can write and read a data in one time.
        public virtual void Run(Stream inputStream, Stream outputStream)
        {
            Start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            SendCommand(outputStream);
            ReadResult(inputStream);
            End = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Async variant of <see cref="Run(Stream, Stream)"/>
        /// </summary>
        /// <param name="inputStream">stream we read the result from</param>
        /// <param name="outputStream">stream we write the command to</param>
        /// <returns>async task</returns>
        public virtual async Task RunAsync(Stream inputStream, Stream outputStream)
        {
            Start = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            await SendCommandAsync(outputStream); //wait for command to finish sending before trying to read
            await ReadResultAsync(inputStream);   //wait for reading of result to finish
            End = DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

        /// <summary>
        /// Sends the OBD-II request.
        /// This method may be overriden in subclasses, such as ObMultiCommand or TroubleCodesCommand
        /// </summary>
        /// <param name="outputStream">stream to write commands into</param>
        protected void SendCommand(Stream outputStream)
        {
            // write to OutputStream (i.e.: a BluetoothSocket) with an added
            // Carriage return

            //ascii encoding works because cmd is something like "01 00"
            var buffer = Encoding.ASCII.GetBytes(Cmd + "\r");
            outputStream.Write(buffer, 0, buffer.Length);
            outputStream.Flush();

            if (ResponseDelayInMs > 0)
            {
                Thread.Sleep(ResponseDelayInMs);
            }
        }

        /// <summary>
        /// Async variant of <see cref="SendCommand(Stream)"/>
        /// </summary>
        /// <param name="outputStream">stream to write commands into</param>
        /// <returns>async task</returns>
        protected async Task SendCommandAsync(Stream outputStream) //cannot await void
        {
            var buffer = Encoding.ASCII.GetBytes(Cmd + "\r");
            await outputStream.WriteAsync(buffer, 0, buffer.Length);
            await outputStream.FlushAsync();
            if (ResponseDelayInMs > 0)
            {
                await Task.Delay(ResponseDelayInMs);
            }
        }

        /// <summary>
        /// Resends this command.
        /// </summary>
        /// <param name="outputStream">stream to write commands into</param>
        protected void ResendCommand(Stream outputStream)
        {
            var buffer = Encoding.ASCII.GetBytes("\r");
            outputStream.Write(buffer, 0, buffer.Length);
            outputStream.Flush();
            if (ResponseDelayInMs > 0)
            {
                Thread.Sleep(ResponseDelayInMs);
            }
        }

        /// <summary>
        /// Async variant of <see cref="ResendCommand(Stream)"/>
        /// </summary>
        /// <param name="outputStream">stream to write commands into</param>
        /// <returns>async task</returns>
        protected async Task ResendCommandAsync(Stream outputStream)
        {
            var buffer = Encoding.ASCII.GetBytes("\r");
            await outputStream.WriteAsync(buffer, 0, buffer.Length);
            await outputStream.FlushAsync();
            if (ResponseDelayInMs > 0)
            {
                await Task.Delay(ResponseDelayInMs);
            }
        }


        /// <summary>
        /// Reads the OBD-II response.
        /// This method may be overriden in subclasses, such as ObdMultiCommand.
        /// </summary>
        /// <param name="inputStream">stream to read result from</param>
        protected virtual void ReadResult(Stream inputStream)
        {
            ReadRawData(inputStream);
            CheckForErrors();
            FillBuffer();
            PerformCalculations();
        }

        /// <summary>
        /// Async variant of <see cref="ReadResult(Stream)"/>
        /// </summary>
        /// <param name="inputStream">stream to read result from</param>
        /// <returns>async task</returns>
        protected virtual async Task ReadResultAsync(Stream inputStream)
        {
            await ReadRawDataAsync(inputStream);   //use await for I/O bound code
            await Task.Run(CheckForErrors);
            await Task.Run(FillBuffer);            //use Task.Run() for CPU bound code
            await Task.Run(PerformCalculations);      
        }

        /// <summary>
        /// This method exists so that for each command, there must be a method that is called only once to perform calculations.
        /// </summary>
        public abstract void PerformCalculations();

        //regex patterns
        private static readonly string WhitespacePattern = "\\s";
        private static readonly string BusinitPattern = "(BUS INIT)|(BUSINIT)|(\\.)";
        private static readonly string SearchingPattern = "SEARCHING";
        private static readonly string DigitsLettersPattern = "([0-9A-F])+";

        /// <summary>
        /// Regex Replaces all occurences of pattern in input with replacement
        /// </summary>
        /// <param name="pattern">regex pattern</param>
        /// <param name="input">original string</param>
        /// <param name="replacement">replace pattern with this</param>
        /// <returns>new string</returns>
        protected string ReplaceAll(string pattern, string input, string replacement)
        {
            return Regex.Replace(input, pattern, replacement);
        }

        /// <summary>
        /// <see cref="ReplaceAll"/> with empty string as replacement
        /// </summary>
        /// <param name="pattern">regex pattern</param>
        /// <param name="input">original string</param>
        /// <returns>new string</returns>
        protected string RemoveAll(string pattern, string input)
        {
            return Regex.Replace(input, pattern, "");
        }


        /// <summary>fill buffer</summary>
        /// <exception cref="NonNumericResponseException"></exception>
        protected virtual void FillBuffer()
        {
            RawData = RemoveAll(WhitespacePattern, RawData); //removes all [ \t\n\x0B\f\r]
            RawData = RemoveAll(BusinitPattern, RawData);

            if (!Regex.IsMatch(RawData, DigitsLettersPattern))
            {
                throw new NonNumericResponseException(RawData);
            }

            // read string each two chars
            Buffer.Clear();
            var begin = 0;
            const int length = 2;
            while ((begin + length) <= RawData.Length)
            {
                var hex = "0x" + RawData.Substring(begin, length);
                Buffer.Add(Convert.ToInt32(hex, 16));
                begin += length;
            }
        }


        /// <summary>readRawData</summary>
        /// <param name="inputStream">stream to read data from</param>
        protected virtual void ReadRawData(Stream inputStream)
        {
            int b = 0;
            var res = new StringBuilder();

            // read until '>' arrives OR end of stream reached
            char c;

            //wait for data to become available (read blocks until there is at least 1 byte)
            //unless it is actually end of stream! then it returns 0!
            //a network socket (as I am testing with) would only return end of stream if closed though!
            var buf = new byte[1];
            inputStream.Read(buf, 0, 1);

            b = buf[0];
            
            //now we can read 1 byte at a time
            do
            {
                c = (char) b;
                if (c == '>') // read until '>' arrives
                {
                    break;
                }
                res.Append(c);
            } while ((b = inputStream.ReadByte()) > -1);

            /*
             * Imagine the following response 41 0c 00 0d.
             *
             * ELM sends strings!! So, ELM puts spaces between each "byte". And pay
             * attention to the fact that I've put the word byte in quotes, because 41
             * is actually TWO bytes (two chars) in the socket. So, we must do some more
             * processing..
             */
            RawData = RemoveAll(SearchingPattern, res.ToString());

            /*
             * Data may have echo or informative text like "INIT BUS..." or similar.
             * The response ends with two carriage return characters. So we need to take
             * everything from the last carriage return before those two (trimmed above).
             */
            //kills multiline.. rawData = rawData.substring(rawData.lastIndexOf(13) + 1);
            RawData = RemoveAll(WhitespacePattern, RawData); //removes all [ \t\n\x0B\f\r]
        }

        /// <summary>
        /// Async variant of <see cref="ReadRawData(Stream)"/>
        /// </summary>
        /// <param name="inputStream">stream to read data from</param>
        /// <returns>async task</returns>
        protected virtual async Task ReadRawDataAsync(Stream inputStream)
        {
            int b = 0;
            char c;
            var res = new StringBuilder();

            var buffer = new byte[1];
            
            //wait for data to become available
            await inputStream.ReadAsync(buffer, 0, 1);

            b = (int)buffer[0];

            //from now on, we can just read one byte at a time
            do
            {
                c = (char) b;
                if (c == '>') // read until '>' arrives
                {
                    break;
                }
                res.Append(c);
            } while ((b = inputStream.ReadByte()) > -1);

            RawData = RemoveAll(SearchingPattern, res.ToString());
            RawData = RemoveAll(WhitespacePattern, RawData);
        }

        private void CheckForErrors()
        {
            foreach (var errorClass in _errorClasses)
            {
                ResponseException messageError;

                messageError = (ResponseException) Activator.CreateInstance(errorClass);
                messageError.Command = Cmd;

                if (messageError.IsError(RawData))
                {
                    throw messageError;
                }
            }
        }

        public static bool operator ==(ObdCommand cmd1, ObdCommand cmd2)
        {
            if (ReferenceEquals(cmd1, null)) return ReferenceEquals(cmd2, null);
            if (ReferenceEquals(cmd2, null)) return false;

            return cmd1.Cmd == cmd2.Cmd;
        }

        public static bool operator !=(ObdCommand cmd1, ObdCommand cmd2)
        {
            return !(cmd1 == cmd2);
        }

        public override int GetHashCode()
        {
            return Cmd != null ? Cmd.GetHashCode() : 0;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, this)) return true;
            if (obj == null || obj.GetType() != GetType()) return false;

            var cmd = (ObdCommand) obj;

            if (Cmd != null) return Cmd == cmd.Cmd;
            return cmd.Cmd == null;
        }
    }
}