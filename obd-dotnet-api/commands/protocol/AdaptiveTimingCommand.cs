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
    /// <summary>
    /// By default, Adaptive Timing option 1 (AT1) is enabled, and is the recommended setting.
    /// AT0 is used to disable Adaptive timing (so the timeout is always as set by AT ST),
    /// while AT2 is a more aggressive version of AT1 (the effect is more noticeable for very
    /// slow connections – you may not see much difference with faster OBD systems.
    /// </summary>
    public class AdaptiveTimingCommand : ObdProtocolCommand
    {
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mode"></param>
        public AdaptiveTimingCommand(int mode)
            : base("AT AT" + mode)
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public AdaptiveTimingCommand(AdaptiveTimingCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override string Name => "Adaptive Timing Set";
    }
}