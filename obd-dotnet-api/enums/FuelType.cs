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
 * MODE 1 PID 0x51 will return one of the following values to identify the fuel
 * type of the vehicle.
 *
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace obd_dotnet_api.enums
{
    public class FuelType
    {
        public static readonly FuelType Gasoline = new FuelType(0x01, "Gasoline");
        public static readonly FuelType Methanol = new FuelType(0x02, "Methanol");
        public static readonly FuelType Ethanol = new FuelType(0x03, "Ethanol");
        public static readonly FuelType Diesel = new FuelType(0x04, "Diesel");
        public static readonly FuelType Lpg = new FuelType(0x05, "GPL/LGP");
        public static readonly FuelType Cng = new FuelType(0x06, "Natural Gas");
        public static readonly FuelType Propane = new FuelType(0x07, "Propane");
        public static readonly FuelType Electric = new FuelType(0x08, "Electric");
        public static readonly FuelType BifuelGasoline = new FuelType(0x09, "Biodiesel + Gasoline");
        public static readonly FuelType BifuelMethanol = new FuelType(0x0A, "Biodiesel + Methanol");
        public static readonly FuelType BifuelEthanol = new FuelType(0x0B, "Biodiesel + Ethanol");
        public static readonly FuelType BifuelLpg = new FuelType(0x0C, "Biodiesel + GPL/LGP");
        public static readonly FuelType BifuelCng = new FuelType(0x0D, "Biodiesel + Natural Gas");
        public static readonly FuelType BifuelPropane = new FuelType(0x0E, "Biodiesel + Propane");
        public static readonly FuelType BifuelElectric = new FuelType(0x0F, "Biodiesel + Electric");
        public static readonly FuelType BifuelGasolineElectric = new FuelType(0x10, "Biodiesel + Gasoline/Electric");
        public static readonly FuelType HybridGasoline = new FuelType(0x11, "Hybrid Gasoline");
        public static readonly FuelType HybridEthanol = new FuelType(0x12, "Hybrid Ethanol");
        public static readonly FuelType HybridDiesel = new FuelType(0x13, "Hybrid Diesel");
        public static readonly FuelType HybridElectric = new FuelType(0x14, "Hybrid Electric");
        public static readonly FuelType HybridMixed = new FuelType(0x15, "Hybrid Mixed");
        public static readonly FuelType HybridRegenerative = new FuelType(0x16, "Hybrid Regenerative");
        
        public static IEnumerable<FuelType> Values 
        {
            get
            {
                yield return Gasoline;
                yield return Methanol;
                yield return Ethanol;
                yield return Diesel;
                yield return Lpg;
                yield return Cng;
                yield return Propane;
                yield return Electric;
                yield return BifuelGasoline;
                yield return BifuelMethanol;
                yield return BifuelEthanol;
                yield return BifuelLpg;
                yield return BifuelCng;
                yield return BifuelPropane;
                yield return BifuelElectric;
                yield return BifuelGasolineElectric;
                yield return HybridGasoline;
                yield return HybridEthanol;
                yield return HybridDiesel;
                yield return HybridElectric;
                yield return HybridMixed;
                yield return HybridRegenerative;
            }
        }

        public static FuelType FromValue(int val)
        {
            return Values.First(item => item.Value == val);
        }

        public int Value { get; private set; } 
        public string Description { get; private set; }
        private FuelType(int value, string desc) => (Value, Description) = (value, desc);
    }
}