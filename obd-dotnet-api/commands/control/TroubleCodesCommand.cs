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
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using obd_dotnet_api.enums;

namespace obd_dotnet_api.commands.control
{
    /// <summary>
    /// It is not needed no know how many DTC are stored.
    /// Because when no DTC are stored response will be NO DATA
    /// And where are more messages it will be stored in frames that have 7 bytes.
    /// In one frame are stored 3 DTC.
    /// If we find out DTC P0000 that mean no message are we can end.
    /// </summary>
    public class TroubleCodesCommand : ObdCommand
    {
        /// <summary>Constant <code>dtcLetters={'P', 'C', 'B', 'U'}</code></summary>
        protected readonly static char[] DtcLetters = {'P', 'C', 'B', 'U'};

        ///<summary>Constant <code>hexArray="0123456789ABCDEF".toCharArray()</code></summary>
        protected readonly static char[] HexArray = "0123456789ABCDEF".ToCharArray();

        protected readonly StringBuilder Codes;

        ///<summary>Constructor for TroubleCodesCommand</summary>
        public TroubleCodesCommand()
            : base("03")
        {
            Codes = new StringBuilder();
        }

        /// <summary>
        /// constructor to be used by inherited commands
        /// See <see cref="PendingTroubleCodesCommand"/>, and <see cref="PermanentTroubleCodesCommand"/>
        /// </summary>
        /// <param name="command"></param>
        protected TroubleCodesCommand(string command)
            : base(command)
        {
            Codes = new StringBuilder();
        }

        /// <summary>Copy ctor.</summary>
        /// <param name="other"></param>
        public TroubleCodesCommand(TroubleCodesCommand other)
            : base(other)
        {
            Codes = new StringBuilder();
        }

        ///<inheritdoc/>
        protected override void FillBuffer()
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            var result = Result;
            string workingData;
            var startIndex = 0; //Header size.

            var canOneFrame = Regex.Replace(result, "[\r\n]", "");
            var canOneFrameLength = canOneFrame.Length;

            if (canOneFrameLength <= 16 && canOneFrameLength % 4 == 0) //CAN(ISO-15765) protocol one frame.
            {
                workingData = canOneFrame; //43yy{codes}
                startIndex = 4; //Header is 43yy, yy showing the number of data items.
            }
            else if (result.Contains(":")) //CAN(ISO-15765) protocol two and more frames.
            {
                workingData = Regex.Replace(result, "[\r\n].:", ""); //xxx43yy{codes}
                startIndex = 7; //Header is xxx43yy, xxx is bytes of information to follow, yy showing the number of data items.
            }
            else //ISO9141-2, KWP2000 Fast and KWP2000 5Kbps (ISO15031) protocols.
            {
                workingData = RegexReplace(result);
            }

            for (var begin = startIndex; begin < workingData.Length; begin += 4)
            {
                var dtc = "";
                var b1 = HexStringToByteArray(workingData[begin]);
                var ch1 = ((b1 & 0xC0) >> 6);
                var ch2 = ((b1 & 0x30) >> 4);
                dtc += DtcLetters[ch1];
                dtc += HexArray[ch2];
                dtc += workingData.Substring(begin + 1, 3);
                if (dtc == "P0000")
                {
                    return;
                }

                Codes.Append(dtc);
                Codes.Append('\n');
            }
        }

        //this is the only part in "PerformCalculations" that differs between the 3 commands
        //this, just override this, and not the whole function
        protected virtual string RegexReplace(string input)
        {
            return Regex.Replace(input, "^43|[\r\n]43|[\r\n]", "");
        }

        private byte HexStringToByteArray(char s)
        {
            // emulate Javas "Character.Digit()" function (non-existent in C#)
            const int radix = 16;
            int digit;

            if (s >= '0' && s <= '9')
                digit = s - '0';
            else if (s >= 'a' && s < 'a' + radix - 10)
                digit = s - 'a' + 10;
            else if (s >= 'A' && s < 'A' + radix - 10)
                digit = s - 'A' + 10;
            else
                digit = -1;

            //original call was `(byte)(Character.Digit(s, 16) << 4)`
            return (byte) (digit << 4);
        }

        ///<inheritdoc/>
        public override string FormattedResult => Codes.ToString();
        ///<inheritdoc/>
        public override string CalculatedResult => Codes.ToString();
        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.TroubleCodes.Name;

        ///<inheritdoc/>
        protected override void ReadRawData(Stream inputStream)
        {
            int b;
            char c;
            var res = new StringBuilder();

            //wait for data to become available, thus initial long timeout
            //might throw TimeoutException, catch in calling function!
            b = ReadByteTimeout(inputStream, 1500);
            
            // read until '>' arrives OR end of stream reached (and skip ' ')
            while(b > -1)
            {
                c = (char) b;
                if (c == '>') break; // read until '>' arrives
                if (c != ' ') res.Append(c); // skip ' '
                
                try
                {
                    //read new byte, or time out rather quickly
                    b = ReadByteTimeout(inputStream, 250);
                }
                catch (TimeoutException)
                {
                    //timeout? the '>' was missing, stop!
                    break;
                }
            }

            RawData = res.ToString().Trim();
        }
    }
}