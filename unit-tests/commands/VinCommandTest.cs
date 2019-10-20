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
 * Tests for VinCommand class.
 */

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands.control;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class VinCommandTest : VinCommand //need to inherit to test protected function!
    {
        /**
         * Test VIN CAN (ISO-15765) format
         *
         * @throws IOException
         */
        [Fact]
        public void VinCanFormatTest()
        {
            var mockIn = new MemoryStream();

            //write data to stream
            mockIn.Write(Encoding.ASCII.GetBytes(
                "014\n"
                + "0: 49 02 01 57 50 30\n"
                + "1: 5A 5A 5A 39 39 5A 54\n"
                + "2: 53 33 39 32 31 32 34>"
            ));
            mockIn.Flush();
            //rewind stream so it can be read
            mockIn.Position = 0;

            var res = "WP0ZZZ99ZTS392124";

            // call the method to test
            // we inherit VinCommand ; xUnit instantiates the test class to execute the test
            // thus we have already instantiated a VinCommand (this), run method!
            ReadResult(mockIn);

            Assert.Equal(res, FormattedResult);
        }

        /**
         * Test VIN ISO9141-2, KWP2000 Fast and KWP2000 5Kbps (ISO15031) format
         *
         * @throws IOException
         */
        [Fact]
        public void Vin()
        {
            var mockIn = new MemoryStream();

            //write data to stream
            mockIn.Write(Encoding.ASCII.GetBytes(
                "49 02 01 00 00 00 57\n"
                + "49 02 02 50 30 5A 5A\n"
                + "49 02 03 5A 39 39 5A\n"
                + "49 02 04 54 53 33 39\n"
                + "49 02 05 32 31 32 34>"
            ));
            mockIn.Flush();
            //rewind stream so it can be read
            mockIn.Position = 0;

            var res = "WP0ZZZ99ZTS392124";

            // call the method to test
            ReadResult(mockIn);

            Assert.Equal(res, FormattedResult);
        }
    }
}