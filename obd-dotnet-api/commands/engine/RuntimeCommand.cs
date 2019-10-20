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

namespace obd_dotnet_api.commands.engine
{
    /// <summary>
    /// Engine Runtime
    /// </summary>
    public class RuntimeCommand : ObdCommand
    {
        private int _value = 0;

        /// <summary>
        /// ctor
        /// </summary>
        public RuntimeCommand()
            : base("01 1F")
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public RuntimeCommand(RuntimeCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            // ignore first two bytes [01 0C] of the response
            _value = Buffer[2] * 256 + Buffer[3];
        }

        ///<inheritdoc/>
        public override string FormattedResult => $"{_value / 3600:D2}:{(_value % 3600) / 60:D2}:{_value % 60:D2}";

        ///<inheritdoc/>
        public override string CalculatedResult => _value.ToString();

        ///<inheritdoc/>
        public override string ResultUnit => "s";

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.EngineRuntime.Name;
    }
}