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
 * Runtime since engine start in seconds, with a maximum value of 65535.
 */

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands.control;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class DistanceSinceCcCommandTest : DistanceSinceCcCommand
    {
        /**
     * Test for valid InputStream read, 65535 km.
     *
     * @throws IOException
     */
        [Fact]
        public void TestMaxDistanceValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 31 FF FF>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(65535, DistanceKm);
        }

        /**
     * Test for valid InputStream read, 17731 kms
     *
     * @throws IOException
     */
        [Fact]
        public void TestSomeRuntimeValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 31 45 43>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(17731, DistanceKm);
        }

        /**
     * Test for valid InputStream read, 0 km.
     *
     * @throws IOException
     */
        [Fact]
        public void TestMinRuntimeValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 31 00 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(0, DistanceKm);
        }
    }
}