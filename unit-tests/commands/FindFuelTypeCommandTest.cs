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
 * Tests for FindFuelTypeCommand class.
 */

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands.fuel;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class FindFuelTypeCommandTest : FindFuelTypeCommand
    {
        /**
     * Test for valid InputStream read, Gasoline
     *
     * @throws IOException
     */
        [Fact]
        public void TestFindGasoline()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 51 01>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal("Gasoline", FormattedResult);
        }

        /**
     * Test for valid InputStream read, Diesel
     *
     * @throws IOException
     */
        [Fact]
        public void TestDiesel()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 51 04>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal("Diesel", FormattedResult);
        }

        /**
     * Test for valid InputStream read, Ethanol
     *
     * @throws IOException
     */
        [Fact]
        public void TestHybridEthanol()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 51 12>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal("Hybrid Ethanol", FormattedResult);
        }
    }
}