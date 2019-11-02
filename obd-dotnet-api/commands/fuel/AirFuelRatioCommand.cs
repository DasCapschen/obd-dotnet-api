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

namespace obd_dotnet_api.commands.fuel
{
    public class AirFuelRatioCommand : ObdCommand
    {
        private float _afr = 0;

        /// <summary>
        /// ctor
        /// </summary>
        public AirFuelRatioCommand()
            : base("01 44")
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            float A = Buffer[2];
            float B = Buffer[3];
            //TODO: where does that * 14.7f come from!?
            _afr = (((A * 256) + B) / 32768) * 14.7f; //((A*256)+B)/32768
        }

        ///<inheritdoc/>
        public override string FormattedResult => $"{AirFuelRatio:F2}:1 AFR";

        ///<inheritdoc/>
        public override string CalculatedResult => AirFuelRatio.ToString("F2");

        public double AirFuelRatio => _afr;

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.AirFuelRatio.Name;
    }
}