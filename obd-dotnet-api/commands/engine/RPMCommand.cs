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
    /// Displays the current engine revolutions per minute (RPM).
    /// </summary>
    public class RpmCommand : ObdCommand
    {
        private int _rpm = -1;

        /// <summary>
        /// ctor
        /// </summary>
        public RpmCommand()
            : base("01 0C")
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public RpmCommand(RpmCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            _rpm = (Buffer[2] * 256 + Buffer[3]) / 4;
        }

        ///<inheritdoc/>
        public override string FormattedResult => $"{_rpm:D}{ResultUnit}";
        ///<inheritdoc/>
        public override string CalculatedResult => _rpm.ToString();

        ///<inheritdoc/>
        public override string ResultUnit => "RPM";

        ///<inheritdoc/>
        public override string Name => AvailableCommandNames.EngineLoad.Name;

        /// <summary>
        /// revolutions per minute
        /// </summary>
        public int Rpm => _rpm;
    }
}