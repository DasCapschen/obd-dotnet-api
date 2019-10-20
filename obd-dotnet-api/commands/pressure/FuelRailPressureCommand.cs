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
    public class FuelRailPressureCommand : PressureCommand
    {
        /// <summary>default ctor</summary>
        public FuelRailPressureCommand()
            : base("01 23")
        {
        }

        /// <summary>copy ctor</summary>
        /// <param name="other"></param>
        public FuelRailPressureCommand(FuelRailPressureCommand other)
            : base(other)
        {
        }

        /// <summary></summary>
        /// <returns></returns>
        protected override int PreparePressureValue()
        {
            var a = Buffer[2];
            var b = Buffer[3];
            return ((a * 256) + b) * 10;
        }

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.FuelRailPressure.Name;
    }
}