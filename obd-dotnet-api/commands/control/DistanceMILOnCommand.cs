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
 * <p>DistanceMILOnCommand class.</p>
 *
 */
namespace obd_dotnet_api.commands.control
{
    public class DistanceMilOnCommand : ObdCommand, ISystemOfUnits 
    {
        private int _km = 0;

        public DistanceMilOnCommand() 
            : base("01 21")
        {
        }

        public DistanceMilOnCommand(DistanceMilOnCommand other) 
            : base(other)
        {
        }

        public override void PerformCalculations()
        {
            _km = Buffer[2] * 256 + Buffer[3];
        }

        public override string FormattedResult => UseImperialUnits
            ? $"{GetImperialUnit():2F}{ResultUnit}"
            : $"{_km:D}{ResultUnit}";

        public override string CalculatedResult => UseImperialUnits ? GetImperialUnit().ToString() : _km.ToString();

        public override string ResultUnit => UseImperialUnits ? "m" : "km"; //m == miles

        public float GetImperialUnit()
        {
            return _km * 0.621371192F;
        }

        public int DistanceKm => _km;

        public override string Name => AvailableCommandNames.DistanceTraveledMilOn.Value;
    }
}
