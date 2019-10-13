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
 * Fuel Consumption Rate per hour.
 *
 */
namespace obd_dotnet_api.commands.fuel
{
    public class ConsumptionRateCommand : ObdCommand 
    {

        private float _fuelRate = -1.0f;

        public ConsumptionRateCommand() 
            : base("01 5E")
        {
        }

        public ConsumptionRateCommand(ConsumptionRateCommand other) 
            : base(other)
        {
        }

        public override void PerformCalculations()
        {
            _fuelRate = (Buffer[2] * 256 + Buffer[3]) * 0.05f;
        }

        public override string FormattedResult => $"{_fuelRate:1F}{ResultUnit}";

        public override string CalculatedResult => _fuelRate.ToString();

        public override string ResultUnit => "L/h";

        public float LitersPerHour => _fuelRate;

        public override string Name => AvailableCommandNames.FuelConsumptionRate.Value;
    }
}
