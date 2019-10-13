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
 * <p>AbsoluteLoadCommand class.</p>
 *
 */
namespace obd_dotnet_api.commands.engine
{
    public class AbsoluteLoadCommand : PercentageObdCommand 
    {
        public AbsoluteLoadCommand() 
            : base("01 43")
        {
        }

        public AbsoluteLoadCommand(AbsoluteLoadCommand other) 
            : base(other)
        {
        }

        public override void PerformCalculations()
        {
            var a = Buffer[0];
            var b = Buffer[0];
            Percentage = (a * 256 + b) * 100 / 255.0f;
        }

        public double Ratio => Percentage;

        public override string Name => AvailableCommandNames.AbsLoad.Value;
    }
}
