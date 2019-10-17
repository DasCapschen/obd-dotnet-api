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
 * Tests for FuelTrimCommand class.
 * <p>
 * TODO replace integer values in expected values with strings, like in other
 * tests.
 */

using System.IO;
using System.Text;
using obd_dotnet_api.commands.fuel;
using Xunit;

namespace unit_tests.commands
{
    public class FuelTrimCommandTest : FuelTrimCommand
    {

        /**
     * Test for valid InputStream read, 99.22%
     *
     * @throws IOException
     */
        [Fact]
        public void TestMaxFuelTrimValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
        
            //0x07 (2nd byte) == FuelTrim.LongTermBank1
            var bytes = new byte[] {0x41, 0x07, 0xFF, 0x3E};
            mockIn.Write(Encoding.ASCII.GetBytes("41 07 FF>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            
            //Equals for floats is pretty dumb
            Assert.Equal(99.21875f, Value);
        }

        /**
         * Test for valid InputStream read, 56.25%
         *
         * @throws IOException
         */
        [Fact]
        public void TestSomeValue() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();

            var bytes = new byte[] { 0x41, 0x20, 0x07, 0x20, 0xC8, 0x20, 0x3E};
        
            mockIn.Write(Encoding.ASCII.GetBytes("41 07 C8>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(56.25f, Value);
        }

        /**
     * Test for valid InputStream read, -100.00%
     *
     * @throws IOException
     */
        [Fact]
        public void TestMinFuelTrimValue() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();

            var bytes = new byte[] { 0x41, 0x20, 0x07, 0x20, 0x00, 0x20, 0x3E };
        
            mockIn.Write(Encoding.ASCII.GetBytes("41 07 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(-100f, Value);
        }
    }
}
