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

namespace obd_dotnet_api.commands.control
{
    /// <summary>
    /// Fuel systems that use conventional oxygen sensor display the commanded open
    /// loop equivalence ratio while the system is in open loop. Should report 100%
    /// when in closed loop fuel.
    /// <p/>
    /// To obtain the actual air/fuel ratio being commanded, multiply the
    /// stoichiometric A/F ratio by the equivalence ratio. For example, gasoline,
    /// stoichiometric is 14.64:1 ratio. If the fuel control system was commanded an
    /// equivalence ratio of 0.95, the commanded A/F ratio to the engine would be
    /// 14.64 * 0.95 = 13.9 A/F.
    /// </summary>
    public class EquivalentRatioCommand : PercentageObdCommand
    {
        /// <summary>default ctor</summary>
        public EquivalentRatioCommand()
            : base("01 44")
        {
        }

        /// <summary>copy ctor</summary>
        /// <param name="other"></param>
        public EquivalentRatioCommand(EquivalentRatioCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            var a = Buffer[2];
            var b = Buffer[3];
            Percentage = (a * 256 + b) / 32768.0f;
        }

        /// <summary>
        /// Ratio, see <see cref="PercentageObdCommand.Percentage"/>
        /// </summary>
        public double Ratio => Percentage;

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.EquivRatio.Name;
    }
}