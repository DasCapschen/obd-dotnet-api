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
    public class IntakeManifoldPressureCommand : PressureCommand
    {
        /// <summary>default ctor</summary>
        public IntakeManifoldPressureCommand()
            : base("01 0B")
        {
        }

        /// <summary>copy ctor</summary>
        /// <param name="other"></param>
        public IntakeManifoldPressureCommand(IntakeManifoldPressureCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.IntakeManifoldPressure.Name;
    }
}