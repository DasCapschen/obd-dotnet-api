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

using System.Text.RegularExpressions;
using obd_dotnet_api.enums;

namespace obd_dotnet_api.commands.control
{
    /// <summary>
    /// It is not needed no know how many DTC are stored.
    /// Because when no DTC are stored response will be NO DATA
    /// And where are more messages it will be stored in frames that have 7 bytes.
    /// In one frame are stored 3 DTC.
    /// If we find out DTC P0000 that mean no message are we can end.
    /// </summary>
    public class PendingTroubleCodesCommand : TroubleCodesCommand
    {
        /// <summary>default ctor</summary>
        public PendingTroubleCodesCommand()
            : base("07")
        {
        }

        /// <summary>copy ctor</summary>
        /// <param name="other"></param>
        public PendingTroubleCodesCommand(PendingTroubleCodesCommand other)
            : base(other)
        {
        }

        //this is the only part in "PerformCalculations" that differs between the 3 commands
        //this, just override this, and not the whole function
        ///<inheritdoc/>
        protected override string RegexReplace(string input)
        {
            return Regex.Replace(input, "^47|[\r\n]47|[\r\n]", "");
        }

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.PendingTroubleCodes.Name;
    }
}