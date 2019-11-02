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
    /// <summary>
    /// Fuel Consumption Rate per hour.
    /// </summary>
    public class ConsumptionRateCommand : ObdCommand
    {
        private float _fuelRate = -1.0f;

        /// <summary>
        /// ctor
        /// </summary>
        public ConsumptionRateCommand()
            : base("01 5E")
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public ConsumptionRateCommand(ConsumptionRateCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            _fuelRate = (Buffer[2] * 256 + Buffer[3]) / 20f;
        }

        ///<inheritdoc/>
        public override string FormattedResult => $"{_fuelRate:F1}{ResultUnit}";

        ///<inheritdoc/>
        public override string CalculatedResult => _fuelRate.ToString("F1");

        ///<inheritdoc/>
        public override string ResultUnit => "L/h";

        /// <summary>
        /// fuel consumption in Litres per Hour
        /// </summary>
        public float LitersPerHour => _fuelRate;

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.FuelConsumptionRate.Name;
    }
}