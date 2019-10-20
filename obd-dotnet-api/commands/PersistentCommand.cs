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

namespace obd_dotnet_api.commands
{
    /// <summary>
    /// Baseclass for persistent OBD commands
    /// </summary>
    public abstract class PersistentCommand : ObdCommand
    {
        private static Dictionary<string, string> _knownValues = new Dictionary<string, string>();
        private static Dictionary<string, List<int>> _knownBuffers = new Dictionary<string, List<int>>();

        /// <summary>default ctor</summary>
        /// <param name="command"></param>
        public PersistentCommand(string command)
            : base(command)
        {
        }

        /// <summary>copy ctor</summary>
        /// <param name="other"></param>
        public PersistentCommand(ObdCommand other)
            : base(other)
        {
        }

        /// <summary>reset</summary>
        public static void Reset()
        {
            _knownValues = new Dictionary<string, string>();
            _knownBuffers = new Dictionary<string, List<int>>();
        }

        /// <summary>Contains Key</summary>
        /// <param name="cmd">the command to check for</param>
        /// <returns>true if command contained in known values</returns>
        public static bool Knows(Type cmd)
        {
            var key = cmd.Name;
            return _knownValues.ContainsKey(key);
        }

        /// <inheritdoc/>
        protected override void ReadResult(Stream inputStream)
        {
            base.ReadResult(inputStream);
            var key = GetType().Name;
            _knownValues[key] = RawData;
            _knownBuffers[key] = new List<int>(Buffer);
        }

        /// <inheritdoc/>
        public override void Run(Stream inputStream, Stream outputStream)
        {
            var key = GetType().Name;
            if (_knownValues.ContainsKey(key))
            {
                RawData = _knownValues[key];
                Buffer = _knownBuffers[key];
                PerformCalculations();
            }
            else
            {
                base.Run(inputStream, outputStream);
            }
        }
    }
}