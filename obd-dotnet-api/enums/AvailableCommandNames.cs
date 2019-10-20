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

using System.Collections.Generic;

namespace obd_dotnet_api.enums
{
    /// <summary>
    /// An "ENUM" of all available OBD Commands
    /// </summary>
    public class AvailableCommandNames
    {
        public static readonly AvailableCommandNames AirIntakeTemp
            = new AvailableCommandNames("Air Intake Temperature");

        public static readonly AvailableCommandNames AmbientAirTemp
            = new AvailableCommandNames("Ambient Air Temperature");

        public static readonly AvailableCommandNames EngineCoolantTemp =
            new AvailableCommandNames("Engine Coolant Temperature");

        public static readonly AvailableCommandNames BarometricPressure
            = new AvailableCommandNames("Barometric Pressure");

        public static readonly AvailableCommandNames FuelPressure
            = new AvailableCommandNames("Fuel Pressure");

        public static readonly AvailableCommandNames IntakeManifoldPressure =
            new AvailableCommandNames("Intake Manifold Pressure");

        public static readonly AvailableCommandNames EngineLoad
            = new AvailableCommandNames("Engine Load");

        public static readonly AvailableCommandNames EngineRuntime
            = new AvailableCommandNames("Engine Runtime");

        public static readonly AvailableCommandNames EngineRpm
            = new AvailableCommandNames("Engine RPM");

        public static readonly AvailableCommandNames Speed
            = new AvailableCommandNames("Vehicle Speed");

        public static readonly AvailableCommandNames Maf
            = new AvailableCommandNames("Mass Air Flow");

        public static readonly AvailableCommandNames ThrottlePos
            = new AvailableCommandNames("Throttle Position");

        public static readonly AvailableCommandNames TroubleCodes
            = new AvailableCommandNames("Trouble Codes");

        public static readonly AvailableCommandNames PendingTroubleCodes =
            new AvailableCommandNames("Pending Trouble Codes");

        public static readonly AvailableCommandNames PermanentTroubleCodes =
            new AvailableCommandNames("Permanent Trouble Codes");

        public static readonly AvailableCommandNames FuelLevel
            = new AvailableCommandNames("Fuel Level");

        public static readonly AvailableCommandNames FuelType
            = new AvailableCommandNames("Fuel Type");

        public static readonly AvailableCommandNames FuelConsumptionRate =
            new AvailableCommandNames("Fuel Consumption Rate");

        public static readonly AvailableCommandNames TimingAdvance
            = new AvailableCommandNames("Timing Advance");

        public static readonly AvailableCommandNames DtcNumber
            = new AvailableCommandNames("Diagnostic Trouble Codes");

        public static readonly AvailableCommandNames EquivRatio
            = new AvailableCommandNames("Command Equivalence Ratio");

        public static readonly AvailableCommandNames DistanceTraveledAfterCodesCleared =
            new AvailableCommandNames("Distance since codes cleared");

        public static readonly AvailableCommandNames ControlModuleVoltage =
            new AvailableCommandNames("Control Module Power Supply");

        public static readonly AvailableCommandNames EngineFuelRate
            = new AvailableCommandNames("Engine Fuel Rate");

        public static readonly AvailableCommandNames FuelRailPressure
            = new AvailableCommandNames("Fuel Rail Pressure");

        public static readonly AvailableCommandNames Vin
            = new AvailableCommandNames("Vehicle Identification Number (VIN)");

        public static readonly AvailableCommandNames DistanceTraveledMilOn =
            new AvailableCommandNames("Distance traveled with MIL on");

        public static readonly AvailableCommandNames TimeTraveledMilOn =
            new AvailableCommandNames("Time run with MIL on");

        public static readonly AvailableCommandNames TimeSinceTcCleared =
            new AvailableCommandNames("Time since trouble codes cleared");

        public static readonly AvailableCommandNames RelThrottlePos =
            new AvailableCommandNames("Relative throttle position");

        public static readonly AvailableCommandNames Pids01_20
            = new AvailableCommandNames("Available PIDs 01-20");

        public static readonly AvailableCommandNames Pids21_40
            = new AvailableCommandNames("Available PIDs 21-40");

        public static readonly AvailableCommandNames Pids41_60
            = new AvailableCommandNames("Available PIDs 41-60");

        public static readonly AvailableCommandNames AbsLoad
            = new AvailableCommandNames("Absolute load");

        public static readonly AvailableCommandNames EngineOilTemp
            = new AvailableCommandNames("Engine oil temperature");

        public static readonly AvailableCommandNames AirFuelRatio
            = new AvailableCommandNames("Air/Fuel Ratio");

        public static readonly AvailableCommandNames WidebandAirFuelRatio
            = new AvailableCommandNames("Wideband Air/Fuel Ratio");

        public static readonly AvailableCommandNames DescribeProtocol
            = new AvailableCommandNames("Describe protocol");

        public static readonly AvailableCommandNames DescribeProtocolNumber
            = new AvailableCommandNames("Describe protocol number");

        public static readonly AvailableCommandNames IgnitionMonitor
            = new AvailableCommandNames("Ignition monitor");

        /// <summary>
        /// All Commands in some, fixed, order
        /// </summary>
        public static IEnumerable<AvailableCommandNames> Values
        {
            get
            {
                yield return AirIntakeTemp;
                yield return AmbientAirTemp;
                yield return EngineCoolantTemp;
                yield return BarometricPressure;
                yield return FuelPressure;
                yield return IntakeManifoldPressure;
                yield return EngineLoad;
                yield return EngineRuntime;
                yield return EngineRpm;
                yield return Speed;
                yield return Maf;
                yield return ThrottlePos;
                yield return TroubleCodes;
                yield return PendingTroubleCodes;
                yield return PermanentTroubleCodes;
                yield return FuelLevel;
                yield return FuelType;
                yield return FuelConsumptionRate;
                yield return TimingAdvance;
                yield return DtcNumber;
                yield return EquivRatio;
                yield return DistanceTraveledAfterCodesCleared;
                yield return ControlModuleVoltage;
                yield return EngineFuelRate;
                yield return FuelRailPressure;
                yield return Vin;
                yield return DistanceTraveledMilOn;
                yield return TimeTraveledMilOn;
                yield return TimeSinceTcCleared;
                yield return RelThrottlePos;
                yield return Pids01_20;
                yield return Pids21_40;
                yield return Pids41_60;
                yield return AbsLoad;
                yield return EngineOilTemp;
                yield return AirFuelRatio;
                yield return WidebandAirFuelRatio;
                yield return DescribeProtocol;
                yield return DescribeProtocolNumber;
                yield return IgnitionMonitor;
            }
        }

        /// <summary>
        /// The human readable name of this command
        /// </summary>
        public string Name { get; private set; }

        private AvailableCommandNames(string value) => Name = value;
    }
}