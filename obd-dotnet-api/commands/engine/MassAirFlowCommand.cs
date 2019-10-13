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
 * Mass Air Flow (MAF)
 *
 */
namespace obd_dotnet_api.commands.engine
{
    public class MassAirFlowCommand : ObdCommand
    {
        private float _maf = -1.0f;

        public MassAirFlowCommand()
            : base("01 10")
        {
        }

        public MassAirFlowCommand(MassAirFlowCommand other)
            : base(other)
        {
        }

        public override void PerformCalculations()
        {
            _maf = (Buffer[2] * 256 + Buffer[3]) / 100.0f;
        }

        public override string FormattedResult => $"{_maf:2F}{ResultUnit}";

        public override string CalculatedResult => _maf.ToString();

        public override string ResultUnit => "g/s";


        public double Maf => _maf;

        public override string Name => AvailableCommandNames.Maf.Value;
    }
}
