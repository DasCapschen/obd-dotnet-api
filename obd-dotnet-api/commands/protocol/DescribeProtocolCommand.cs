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
    /// Describe the current Protocol.
    /// If a protocol is chosen and the automatic option is
    /// also selected, AT DP will show the word 'AUTO' before
    /// the protocol description. Note that the description
    /// shows the actual protocol names, not the numbers
    /// used by the protocol setting commands.
    /// </summary>
    public class DescribeProtocolCommand : ObdProtocolCommand
    {
        /// <summary>
        /// ctor
        /// </summary>
        public DescribeProtocolCommand()
            : base("AT DP")
        {
        }

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.DescribeProtocol.Name;
    }
}