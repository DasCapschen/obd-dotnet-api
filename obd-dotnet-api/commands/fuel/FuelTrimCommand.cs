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

namespace obd_dotnet_api.commands.fuel
{
    public class FuelTrimCommand : PercentageObdCommand
    {
        private readonly FuelTrim _bank;

        /// <summary>
        /// Default ctor.
        /// Will read the bank from parameters and construct the command accordingly.
        /// Please, see <see cref="FuelTrim"/> for more details.
        /// </summary>
        /// <param name="bank"></param>
        public FuelTrimCommand(FuelTrim bank)
            : base(bank.BuildObdCommand())
        {
            this._bank = bank;
        }

        /// <summary>
        /// calls <see cref="FuelTrimCommand(FuelTrim)"/> with ShortTermBank1
        /// </summary>
        public FuelTrimCommand()
            : this(FuelTrim.ShortTermBank1)
        {
        }

        private float PrepareTempValue(int value)
        {
            return (value - 128) * (100.0F / 128);
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            // ignore first two bytes [hh hh] of the response
            Percentage = PrepareTempValue(Buffer[2]);
        }

        /// <summary>the read Fuel Trim percentage value.</summary>
        public float Value => Percentage;

        /// <summary>the name of the bank in string representation.</summary>
        public string Bank => _bank.Bank;

        ///<inheritdoc/>
        public override string Name => _bank.Bank;
    }
}