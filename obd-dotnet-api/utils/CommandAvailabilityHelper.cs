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
using System.Globalization;

namespace obd_dotnet_api.utils
{
/**
 * <p>Abstract CommandAvailabilityHelper class.</p>
 *
 * @since 1.0-RC12
 */
    public abstract class CommandAvailabilityHelper {

        /*
        All around this class, integers are used where unsigned bytes should be used. Too bad they don't exist in Java.
        Thank you Oracle.
         */

        /**
         * Digests the given string into an array of integers which can be used to check for command availability
         *
         * @param availabilityString An 8*n (where n is an integer) character string containing only numbers and uppercase letters from A to F
         * @return An integer array containing the digested information
         * @throws java.lang.ArgumentException if any.
         */
        public static int[] DigestAvailabilityString(string availabilityString)
        {
            //The string must have 8*n characters, n being an integer
            if (availabilityString.Length % 8 != 0) {
                throw new ArgumentException("Invalid length for Availability string supplied: " + availabilityString);
            }

            //Each two characters of the string will be digested into one byte, thus the resulting array will
            //have half the elements the string has
            int[] availabilityArray = new int[availabilityString.Length / 2];

            for (int i = 0, a = 0; i < availabilityArray.Length; ++i, a += 2) {
                //First character is more significant
                availabilityArray[i] = 16 * ParseHexChar(availabilityString[a]) + ParseHexChar(availabilityString[a + 1]);
            }

            return availabilityArray;
        }

        private static int ParseHexChar(char hexChar)
        {
            return hexChar switch
            {
                '0' => 0,
                '1' => 1,
                '2' => 2,
                '3' => 3,
                '4' => 4,
                '5' => 5,
                '6' => 6,
                '7' => 7,
                '8' => 8,
                '9' => 9,
                'A' => 10,
                'B' => 11,
                'C' => 12,
                'D' => 13,
                'E' => 14,
                'F' => 15,
                _ => throw new ArgumentException("Invalid character [" + hexChar + "] supplied")
            };
        }

        /**
         * Implementation of {@link #isAvailable(string, int[])} isAvailable} which returns the specified safetyReturn boolean instead of
         * throwing and exception in the event of supplying an availabilityString which doesn't include information about the specified command
         *
         * This is a direct call to {@link #isAvailable(string, int[], boolean)} with built-in string digestion
         *
         * @param commandPid a {@link java.lang.string} object.
         * @param availabilityString a {@link java.lang.string} object.
         * @param safetyReturn a boolean.
         * @return a boolean.
         */
        public static bool IsAvailable(string commandPid, string availabilityString, bool safetyReturn) 
        {
            return IsAvailable(commandPid, DigestAvailabilityString(availabilityString), safetyReturn);
        }

        /**
         * Checks whether the command identified by commandPid is available, as noted by availabilityString.
         *
         * This is a direct call to {@link com.github.pires.obd.utils.CommandAvailabilityHelper#isAvailable(string, int[])} with built-in string digestion
         *
         * @param commandPid a {@link java.lang.string} object.
         * @param availabilityString a {@link java.lang.string} object.
         * @return a boolean.
         * @throws java.lang.ArgumentException if any.
         */
        public static bool IsAvailable(string commandPid, string availabilityString) 
        {
            return IsAvailable(commandPid, DigestAvailabilityString(availabilityString));
        }

        /**
         * Implementation of {@link #isAvailable(string, int[])} isAvailable} which returns the specified safetyReturn boolean instead of
         * throwing and exception in the event of supplying an availabilityString which doesn't include information about the specified command
         *
         * @param commandPid a {@link java.lang.string} object.
         * @param availabilityArray an array of int.
         * @param safetyReturn a boolean.
         * @return a boolean.
         */
        public static bool IsAvailable(string commandPid, int[] availabilityArray, bool safetyReturn) 
        {
            try 
            {
                return IsAvailable(commandPid, availabilityArray);
            } 
            catch (ArgumentException e) 
            {
                return safetyReturn;
            }
        }

        /**
         * Checks whether the command identified by commandPid is available, as noted by availabilityArray
         *
         * @param commandPid a {@link java.lang.string} object.
         * @param availabilityArray an array of int.
         * @return a boolean.
         * @throws java.lang.ArgumentException if any.
         */
        public static bool IsAvailable(string commandPid, int[] availabilityArray)
        {
            //Command 00 is always supported
            if (commandPid == "00")
                return true;

            //Which byte from the array contains the info we want?
            
            var cmdNumber = Convert.ToInt32(commandPid, 16);
            var arrayIndex = (cmdNumber - 1) / 8; //the -1 corrects the command code offset, as 00, 20, 40 are not the first commands in each response to be evaluated

            if (arrayIndex > availabilityArray.Length - 1)
                throw new ArgumentException("availabilityArray does not contain enough entries to check for command " + commandPid);

            //Subtract 8 from cmdNumber until we have it in the 1-8 range
            while (cmdNumber > 8) 
            {
                cmdNumber -= 8;
            }

            var requestedAvailability = cmdNumber switch
            {
                1 => 128,
                2 => 64,
                3 => 32,
                4 => 16,
                5 => 8,
                6 => 4,
                7 => 2,
                8 => 1,
                _ => throw new Exception("This is not supposed to happen.")
            };

            return requestedAvailability == (requestedAvailability & availabilityArray[arrayIndex]);
        }
    }
}
