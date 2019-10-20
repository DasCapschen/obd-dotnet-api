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

namespace obd_dotnet_api.commands.pressure
{
    public class FuelPressureCommand : PressureCommand
    {
        /// <summary>
        /// ctor
        /// </summary>
        public FuelPressureCommand()
            : base("01 0A")
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public FuelPressureCommand(FuelPressureCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        protected override int PreparePressureValue()
        {
            return Buffer[2] * 3;
        }

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.FuelPressure.Name;
    }
}