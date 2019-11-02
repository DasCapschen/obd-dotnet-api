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

namespace obd_dotnet_api.commands
{
    /// <summary>
    /// Abstract class for percentage commands.
    /// </summary>
    public abstract class PercentageObdCommand : ObdCommand
    {
        /// <summary>The Percentage Value</summary>
        public float Percentage { get; protected set; }

        /// <summary>Constructor for PercentageObdCommand</summary>
        /// <param name="command"></param>
        public PercentageObdCommand(string command)
            : base(command)
        {
        }

        /// <summary>Copy constructor</summary>
        /// <param name="other">Command to copy</param>
        public PercentageObdCommand(PercentageObdCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            Percentage = (Buffer[2] * 100.0f) / 255.0f;
        }

        ///<inheritdoc/>
        public override string FormattedResult => $"{Percentage:F1}{ResultUnit}";
        
        ///<inheritdoc/>
        public override string CalculatedResult => Percentage.ToString("F1");

        ///<inheritdoc/>
        public override string ResultUnit => "%";
    }
}