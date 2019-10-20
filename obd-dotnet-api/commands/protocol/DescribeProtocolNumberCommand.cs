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
    /// Describe the Protocol by Number.
    /// It returns a number which represents the current
    /// obdProtocol. If the automatic search function is also
    /// enabled, the number will be preceded with the letter
    /// ‘A’. The number is the same one that is used with the
    /// set obdProtocol and test obdProtocol commands.
    /// </summary>
    public class DescribeProtocolNumberCommand : ObdCommand
    {
        private ObdProtocols _obdProtocol = ObdProtocols.Auto;

        /// <summary>
        /// ctor
        /// </summary>
        public DescribeProtocolNumberCommand()
            : base("AT DPN")
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            var result = Result;
            char protocolNumber;

            if (result.Length == 2) //the obdProtocol was set automatic and its format A#
            {
                protocolNumber = result[1];
            }
            else protocolNumber = result[0];


            foreach (var protocol in ObdProtocols.Values)
            {
                if (protocol.Value != protocolNumber) continue;

                _obdProtocol = protocol;
                break;
            }
        }

        ///<inheritdoc/>
        public override string FormattedResult => Result;
        ///<inheritdoc/>
        public override string CalculatedResult => _obdProtocol.Name;

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.DescribeProtocolNumber.Name;

        /// <summary>
        /// The ObdProtocols Enum Entry for this Protocol Number
        /// </summary>
        public ObdProtocols ObdProtocol => _obdProtocol;
    }
}