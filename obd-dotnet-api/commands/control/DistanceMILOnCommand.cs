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
    /// Distance travelled with Check Engine Light on
    /// </summary>
    public class DistanceMilOnCommand : ObdCommand, ISystemOfUnits
    {
        private int _km = 0;

        /// <summary>
        /// ctor
        /// </summary>
        public DistanceMilOnCommand()
            : base("01 21")
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public DistanceMilOnCommand(DistanceMilOnCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            _km = Buffer[2] * 256 + Buffer[3];
        }

        ///<inheritdoc/>
        public override string FormattedResult => UseImperialUnits
            ? $"{GetImperialUnit():2F}{ResultUnit}"
            : $"{_km:D}{ResultUnit}";

        ///<inheritdoc/>
        public override string CalculatedResult => UseImperialUnits ? GetImperialUnit().ToString() : _km.ToString();

        ///<inheritdoc/>
        public override string ResultUnit => UseImperialUnits ? "m" : "km"; //m == miles

        /// <summary>
        /// Distance in Miles
        /// </summary>
        /// <returns></returns>
        public float GetImperialUnit()
        {
            return _km * 0.621371192F;
        }

        /// <summary>
        /// Distance in Kilometers
        /// </summary>
        public int DistanceKm => _km;

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.DistanceTraveledMilOn.Name;
    }
}