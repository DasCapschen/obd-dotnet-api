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
 * Tests for DtcNumberCommand class.
 */

using System.IO;
using System.Text;
using obd_dotnet_api.commands.control;
using Xunit;

namespace unit_tests.commands
{
    public class DtcNumberCommandTest : DtcNumberCommand
    {

        /**
     * Test for valid InputStream read, MIL on.
     *
     * @throws IOException
     */
        [Fact]
        public void TestMilOn()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 01 9F>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);

            Assert.True(MilOn);
            Assert.Equal(31, TotalAvailableCodes);
        }

        /**
     * Test for valid InputStream read, MIL off.
     *
     * @throws IOException
     */
        [Fact]
        public void TestMilOff() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 01 0F>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);

            Assert.False(MilOn);
            Assert.Equal(15, TotalAvailableCodes);
        }
    }
}
