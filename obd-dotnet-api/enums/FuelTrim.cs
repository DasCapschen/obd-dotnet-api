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

using System.Collections.Generic;
using System.Linq;

namespace obd_dotnet_api.enums
{
    /// <summary>
    /// An "ENUM" of FuelTrims
    /// </summary>
    public class FuelTrim
    {
        public static readonly FuelTrim ShortTermBank1 = new FuelTrim(0x06, "Short Term Fuel Trim Bank 1");
        public static readonly FuelTrim LongTermBank1 = new FuelTrim(0x07, "Long Term Fuel Trim Bank 1");
        public static readonly FuelTrim ShortTermBank2 = new FuelTrim(0x08, "Short Term Fuel Trim Bank 2");
        public static readonly FuelTrim LongTermBank2 = new FuelTrim(0x09, "Long Term Fuel Trim Bank 2");

        /// <summary>
        /// All Fueltrims in numeric order (6-9)
        /// </summary>
        public static IEnumerable<FuelTrim> Values
        {
            get
            {
                yield return ShortTermBank1;
                yield return LongTermBank1;
                yield return ShortTermBank2;
                yield return LongTermBank2;
            }
        }

        /// <summary>
        /// numeric value of this fueltrim
        /// </summary>
        public int Value { get; private set; }

        /// <summary>
        /// human readable name of this fueltrim
        /// </summary>
        public string Bank { get; private set; }

        private FuelTrim(int value, string bank) => (Value, Bank) = (value, bank);

        /// <summary>
        /// get fueltrim object from numeric value
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        public static FuelTrim FromValue(int val)
        {
            return Values.First(item => item.Value == val);
        }

        /// <summary>
        /// builds an obd command for this fueltrim
        /// </summary>
        /// <returns>obd command as string (ex: 01 07)</returns>
        public string BuildObdCommand()
        {
            return "01 0" + Value;
        }
    }
}