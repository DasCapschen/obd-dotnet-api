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

using obd_dotnet_api.enums;

namespace obd_dotnet_api.commands.protocol
{
    /// <summary>
    /// Retrieve available PIDs ranging from 41 to 60.
    /// </summary>
    public class AvailablePidsCommand_41_60 : AvailablePidsCommand
    {
        /// <summary>
        /// ctor
        /// </summary>
        public AvailablePidsCommand_41_60()
            : base("01 40")
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public AvailablePidsCommand_41_60(AvailablePidsCommand_41_60 other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.Pids41_60.Name;
    }
}