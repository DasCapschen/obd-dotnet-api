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
 * <p>ModuleVoltageCommand class.</p>
 *
 */
namespace obd_dotnet_api.commands.control
{
    public class ModuleVoltageCommand : ObdCommand 
    {

        // Equivalent ratio (V)
        private double _voltage = 0.00;

        /// <summary>default ctor</summary>
        public ModuleVoltageCommand() 
            : base("01 42")
        {
        }

        /// <summary>copy ctor</summary>
        /// <param name="other"></param>
        public ModuleVoltageCommand(ModuleVoltageCommand other)
            : base(other)
        {
        }

        public override void PerformCalculations()
        {
            var a = Buffer[2];
            var b = Buffer[3];
            _voltage = (a * 256 + b) / 1000.0;
        }

        public override string FormattedResult => $"{_voltage:1F}{ResultUnit}";
        public override string CalculatedResult => _voltage.ToString();
        public override string Name => AvailableCommandNames.ControlModuleVoltage.Value;
        public override string ResultUnit => "V";

        public double Voltage => _voltage;
    }
}
