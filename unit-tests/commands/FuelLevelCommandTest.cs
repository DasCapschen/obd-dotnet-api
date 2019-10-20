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
 * Tests for FuelLevelCommand class.
 */

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands.fuel;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class FuelLevelCommandTest : FuelLevelCommand
    {
        /**
         * Test for valid InputStream read, full tank
         *
         * @throws IOException
         */
        [Fact]
        public void TestFullTank()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 2F FF>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(100f, FuelLevel);
        }

        /**
         * Test for valid InputStream read. 78.4%
         *
         * @throws IOException
         */
        [Fact]
        public void TestSomeValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 2F C8>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(78.43137f, FuelLevel);
        }

        /**
         * Test for valid InputStream read, empty tank
         *
         * @throws IOException
         */
        [Fact]
        public void TestEmptyTank()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 2F 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(0f, FuelLevel);
        }
    }
}