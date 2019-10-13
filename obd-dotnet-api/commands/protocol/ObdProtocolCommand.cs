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
 * <p>Abstract ObdProtocolCommand class.</p>
 */
namespace obd_dotnet_api.commands.protocol
{
    public abstract class ObdProtocolCommand : ObdCommand 
    {

        public ObdProtocolCommand(string command)
            : base(command)
        {
        }

        public ObdProtocolCommand(ObdProtocolCommand other) 
            : this(other.Cmd)
        {
        }

        public override void PerformCalculations()
        {
            // ignore
        }

        protected override void FillBuffer()
        {
            // settings commands don't return a value appropriate to place into the
            // buffer, so do nothing
        }

        public override string FormattedResult => Result;
        public override string CalculatedResult => Result;
    }
}
