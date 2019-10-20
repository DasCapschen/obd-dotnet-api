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

namespace obd_dotnet_api.commands
{
    /// <summary>
    /// Current speed.
    /// </summary>
    public class SpeedCommand : ObdCommand, ISystemOfUnits
    {
        /// <summary>the speed in metric units.</summary>
        public int MetricSpeed { get; private set; }

        /// <summary>the speed in imperial units.</summary>
        public float ImperialSpeed => GetImperialUnit();

        /// <inheritdoc/>
        public override string FormattedResult =>
            UseImperialUnits
                ? $"{ImperialSpeed:2F}{ResultUnit}"
                : $"{MetricSpeed:D}{ResultUnit}";

        /// <inheritdoc/>
        public override string CalculatedResult =>
            UseImperialUnits
                ? ImperialSpeed.ToString()
                : MetricSpeed.ToString();

        /// <inheritdoc/>
        public override string ResultUnit => UseImperialUnits ? "mph" : "km/h";

        /// <inheritdoc/>
        public override string Name => AvailableCommandNames.Speed.Name;

        /// <summary>default ctor</summary>
        public SpeedCommand()
            : base("01 0D")
        {
        }

        /// <summary>copy constructor</summary>
        /// <param name="other"></param>
        public SpeedCommand(SpeedCommand other)
            : base(other)
        {
        }

        /// <inheritdoc/>
        public override void PerformCalculations()
        {
            // Ignore first two bytes [hh hh] of the response.
            MetricSpeed = Buffer[2];
        }


        /// <summary>
        /// Convert from km/h to mph
        /// </summary>
        /// <returns>speed in mph, float</returns>
        public float GetImperialUnit() => MetricSpeed * 0.621371192F;
    }
}