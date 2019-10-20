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

namespace obd_dotnet_api.commands.protocol
{
    public class ResetTroubleCodesCommand : ObdCommand
    {
        /// <summary>Constructor</summary>
        public ResetTroubleCodesCommand()
            : base("04")
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
        }

        ///<inheritdoc/>
        public override string FormattedResult => Result;
        ///<inheritdoc/>
        public override string CalculatedResult => Result;
        ///<inheritdoc/>
        public override string Name => Result;
    }
}