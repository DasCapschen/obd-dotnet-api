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

namespace obd_dotnet_api.commands.temperature
{
    /// <summary>
    /// Abstract temperature command.
    /// </summary>
    public abstract class TemperatureCommand : ObdCommand, ISystemOfUnits
    {
        /// <summary>the temperature in Celsius.</summary>
        public float Temperature { get; private set; }

        ///<inheritdoc/>
        public override string FormattedResult => UseImperialUnits
            ? $"{GetImperialUnit():F}{ResultUnit}"
            : $"{Temperature:F}{ResultUnit}";

        ///<inheritdoc/>
        public override string CalculatedResult =>
            UseImperialUnits
                ? GetImperialUnit().ToString("F")
                : Temperature.ToString("F");

        ///<inheritdoc/>
        public override string ResultUnit => UseImperialUnits ? "F" : "C";


        /// <summary>default ctor</summary>
        /// <param name="cmd"></param>
        public TemperatureCommand(string cmd)
            : base(cmd)
        {
        }

        /// <summary>copy ctor</summary>
        /// <param name="other"></param>
        public TemperatureCommand(TemperatureCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            Temperature = Buffer[2] - 40;
        }

        /// <summary></summary>
        /// <returns>the temperature in Fahrenheit</returns>
        public float GetImperialUnit()
        {
            return Temperature * 1.8f + 32;
        }

        /// <summary>get the temperature in kelvin</summary>
        /// <returns>the temperature in Kelvin</returns>
        public float GetKelvin()
        {
            return Temperature + 273.15f;
        }
    }
}