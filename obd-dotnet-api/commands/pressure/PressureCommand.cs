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
 * Abstract pressure command.
 *
 */
namespace obd_dotnet_api.commands.pressure
{
    public abstract class PressureCommand : ObdCommand, ISystemOfUnits 
    {

        protected int TempValue = 0;
        protected int Pressure = 0;


        /// <summary>default ctor</summary>
        /// <param name="cmd"></param>
        public PressureCommand(string cmd)
            : base(cmd)
        {
        }


        /// <summary>copy ctor</summary>
        /// <param name="other"></param>
        public PressureCommand(PressureCommand other) 
            : base(other)
        {
        }
        
        /// <summary>
        /// Some PressureCommand subclasses will need to implement this method in order to determine the final kPa value.
        /// *NEED* to read tempValue
        /// </summary>
        /// <returns></returns>
        protected virtual int PreparePressureValue()
        {
            return Buffer[2];
        }
        
        public override void PerformCalculations() 
        {
            // ignore first two bytes [hh hh] of the response
            Pressure = PreparePressureValue();
        }

        public override string FormattedResult => UseImperialUnits 
            ? $"{GetImperialUnit():F1}{ResultUnit}"
            : $"{MetricUnit:F1}{ResultUnit}";

        
        public int MetricUnit => Pressure;

        public float GetImperialUnit() 
        {
            return Pressure * 0.145037738F;
        }

        public override string CalculatedResult => UseImperialUnits ? GetImperialUnit().ToString() : Pressure.ToString();

        public override string ResultUnit => UseImperialUnits ? "psi" : "kPa";
    }
}
