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
 * Displays the current engine revolutions per minute (RPM).
 *
 */
namespace obd_dotnet_api.commands.engine
{
    public class RpmCommand : ObdCommand 
    {
        private int _rpm = -1;

        public RpmCommand() 
            : base("01 0C")
        {
        }

        public RpmCommand(RpmCommand other) 
            : base(other)
        {
        }

        /** {@inheritDoc} */
        public override void PerformCalculations()
        {
            _rpm = (Buffer[2] * 256 + Buffer[3]) / 4;
        }

        public override string FormattedResult => $"{_rpm:D}{ResultUnit}";
        public override string CalculatedResult => _rpm.ToString();

        public override string ResultUnit => "RPM";

        public override string Name => AvailableCommandNames.EngineLoad.Value;

        public int Rpm => _rpm;
    }
}
