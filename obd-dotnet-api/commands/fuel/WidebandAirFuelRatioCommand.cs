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


/**
 * Wideband AFR
 *
 */
namespace obd_dotnet_api.commands.fuel
{
    public class WidebandAirFuelRatioCommand : ObdCommand 
    {

        private float _wafr = 0;

        public WidebandAirFuelRatioCommand() 
            : base("01 34")
        {
        }

        public override void PerformCalculations()
        {
            float A = Buffer[2];
            float B = Buffer[3];
            _wafr = (((A * 256) + B) / 32768) * 14.7f;//((A*256)+B)/32768
        }

        public override string FormattedResult => $"{WidebandAirFuelRatio:2F}:1 AFR";

        public override string CalculatedResult => WidebandAirFuelRatio.ToString();

        public double WidebandAirFuelRatio => _wafr;

        /** {@inheritDoc} */
        public override string Name => AvailableCommandNames.WidebandAirFuelRatio.Value;
    }
}
