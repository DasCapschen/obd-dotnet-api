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
    /// As per https://www.elmelectronics.com/help/obd/tips/#327_Commands:
    /// <p/>
    /// If a connection is lost, you will need to tell the ELM327 to ‘close’
    /// the current connection, with a Protocol Close command (AT PC).
    /// This will ensure that the ELM327 starts from the beginning when
    /// the next request is made. This is particularly important for the
    /// ISO 9141 and ISO 14230 protocols, as they need to send a special
    /// initiation sequence.
    /// <p/>
    /// Once the protocol has been closed, it can be re-opened by making a
    /// request such as 01 00 (do not send ATZ or AT SP0, as many do).
    /// </summary>
    public class CloseCommand : ObdProtocolCommand
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CloseCommand()
            : base("AT PC")
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public CloseCommand(CloseCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override string Name => "Protocol Close";
    }
}