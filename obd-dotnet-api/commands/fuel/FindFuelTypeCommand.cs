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
 * This command is intended to determine the vehicle fuel type.
 *
 */

using System;
using obd_dotnet_api.enums;

namespace obd_dotnet_api.commands.fuel
{
    public class FindFuelTypeCommand : ObdCommand 
    {

        private int _fuelType = 0;

        public FindFuelTypeCommand() 
            : base("01 51")
        {
        }

        public FindFuelTypeCommand(FindFuelTypeCommand other) 
            : base(other)
        {
        }

        public override void PerformCalculations()
        {
            _fuelType = Buffer[2];
        }

        public override string FormattedResult {
            get
            {
                try
                {
                    return FuelType.FromValue(_fuelType).Description;
                }
                catch (Exception e)
                {
                    return "-";
                }
            }
        }

        public override string CalculatedResult => _fuelType.ToString();
        public override string Name => AvailableCommandNames.FuelType.Value;
    }
}
