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

namespace obd_dotnet_api.commands.protocol
{
    /// <summary>
    /// This class allows for an unspecified command to be sent.
    /// </summary>
    public class ObdRawCommand : ObdProtocolCommand
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="command">Command to run (ex: 00)</param>
        public ObdRawCommand(string command)
            : base(command)
        {
        }

        ///<inheritdoc/>
        public override string Name => "Custom command " + CommandPid;
    }
}