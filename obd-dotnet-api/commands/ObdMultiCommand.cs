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


using System.Collections.Generic;
using System.IO;
using System.Text;

namespace obd_dotnet_api.commands
{
    /// <summary>
    /// Container for multiple <see cref="ObdCommand"/> instances
    /// </summary>
    public class ObdMultiCommand
    {
        private readonly List<ObdCommand> _commands;

        /// <summary>Default ctor.</summary>
        public ObdMultiCommand()
        {
            _commands = new List<ObdCommand>();
        }

        /// <summary>Add ObdCommand to list of ObdCommands.</summary>
        /// <param name="command"></param>
        public void Add(ObdCommand command)
        {
            _commands.Add(command);
        }

        /// <summary>Removes ObdCommand from the list of ObdCommands.</summary>
        /// <param name="command"></param>
        public void Remove(ObdCommand command)
        {
            _commands.Remove(command);
        }

        /// <summary>Iterate all commands, send them and read response.</summary>
        /// <param name="inputStream"></param>
        /// <param name="outputStream"></param>
        public void SendCommands(Stream inputStream, Stream outputStream)
        {
            foreach (var command in _commands)
            {
                command.Run(inputStream, outputStream);
            }
        }

        /// <summary>a formatted command response in string representation.</summary>
        public string FormattedResult
        {
            get
            {
                var res = new StringBuilder();
                foreach (var command in _commands)
                {
                    res.Append(command.FormattedResult).Append(",");
                }

                return res.ToString();
            }
        }
    }
}