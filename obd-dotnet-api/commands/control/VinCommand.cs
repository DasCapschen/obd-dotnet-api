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

using System;
using System.Text;
using System.Text.RegularExpressions;
using obd_dotnet_api.enums;

namespace obd_dotnet_api.commands.control
{
    public class VinCommand : ObdCommand
    {
        private string _vin = "";

        /// <summary>
        /// ctor
        /// </summary>
        public VinCommand()
            : base("09 02")
        {
        }

        /// <summary>
        /// copy ctor
        /// </summary>
        /// <param name="other"></param>
        public VinCommand(VinCommand other)
            : base(other)
        {
        }

        ///<inheritdoc/>
        public override void PerformCalculations()
        {
            var result = Result;
            string workingData;
            if (result.Contains(":")) //CAN(ISO-15765) protocol.
            {
                workingData =
                    RemoveAll(".:", result).Substring(9); //9 is xxx490201, xxx is bytes of information to follow.

                //translating from java, it used "CASE INSENSITIVE"... update pattern instead
                const string pattern = "[^A-Za-z0-9 ]";
                var m = Regex.Matches(ConvertHexToString(workingData), pattern);

                if (m.Count > 0)
                {
                    workingData = RemoveAll("0:49", result);
                    workingData = RemoveAll(".:", workingData);
                }
            }
            else //ISO9141-2, KWP2000 Fast and KWP2000 5Kbps (ISO15031) protocols.
            {
                workingData = RemoveAll("49020.", result);
            }

            _vin = RemoveAll("[\u0000-\u001f]", ConvertHexToString(workingData));
            //_vin = Regex.Replace(ConvertHexToString(workingData), "[\u0000-\u001f]", "");
        }

        /// <inheritdoc/>
        public override string FormattedResult => _vin;

        /// <inheritdoc/>
        public override string Name => AvailableCommandNames.Vin.Name;

        /// <inheritdoc/>
        public override string CalculatedResult => _vin;

        /// <inheritdoc/>
        protected override void FillBuffer()
        {
        }

        /// <summary>
        /// converts a hex number (as string) to a ascii representation
        /// </summary>
        /// <param name="hex">hex number, ex: 49204c6f...</param>
        /// <returns></returns>
        public string ConvertHexToString(string hex)
        {
            var sb = new StringBuilder();

            //49204c6f7665204a617661 split into two characters 49, 20, 4c...
            for (var i = 0; i < hex.Length - 1; i += 2)
            {
                //grab the hex in pairs
                var output = hex.Substring(i, 2);

                //convert hex to decimal
                var dec = Convert.ToInt32(output, 16);

                //convert the decimal to character
                sb.Append((char) dec);
            }

            return sb.ToString();
        }
    }
}