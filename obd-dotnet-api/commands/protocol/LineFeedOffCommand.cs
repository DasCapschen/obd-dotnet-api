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
 * Turns off line-feed.
 *
 */
namespace obd_dotnet_api.commands.protocol
{
    public class LineFeedOffCommand : ObdProtocolCommand 
    {

        public LineFeedOffCommand() 
            : base("AT L0")
        {
        }

        public LineFeedOffCommand(LineFeedOffCommand other) 
            : base(other)
        {
        }

        public override string Name => "Line Feed Off";
    }
}
