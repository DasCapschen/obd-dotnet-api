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
 * This command will for now read MIL (check engine light) state and number of
 * diagnostic trouble codes currently flagged in the ECU.
 * <p>
 * Perhaps in the future we'll extend this to read the 3rd, 4th and 5th bytes of
 * the response in order to store information about the availability and
 * completeness of certain on-board tests.
 *
 */

using obd_dotnet_api.commands.control;

namespace obd_dotnet_api.commands.control
{
    public class DtcNumberCommand : ObdCommand
    {

        private int _codeCount = 0;
        private bool _milOn = false;


        public DtcNumberCommand()
            : base("01 01")
        {
        }

        public DtcNumberCommand(DtcNumberCommand other)
            : base(other)
        {
        }

        public override void PerformCalculations()
        {
            var mil = Buffer[2];
            _milOn = (mil & 0x80) == 128;
            _codeCount = mil & 0x7F;
        }

        public override string FormattedResult => (_milOn ? "MIL is ON" : "MIL is OFF") + _codeCount + " codes";
        public override string CalculatedResult => _codeCount.ToString();

        public int TotalAvailableCodes => _codeCount;
        public bool MilOn => _milOn;

        public override string Name => AvailableCommandNames.DtcNumber.Value;
    }
}