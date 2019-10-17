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

using System.IO;
using System.Text;
using obd_dotnet_api.commands.engine;
using Xunit;

namespace unit_tests.commands
{
    public class RuntimeCommandTest : RuntimeCommand
    {

        /**
     * Test for valid InputStream read, 65535 seconds.
     *
     * @throws IOException
     */
        [Fact]
        public void TestMaxRuntimeValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 1F FF FF>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal("18:12:15", FormattedResult);
        }

        /**
         * Test for valid InputStream read, 67 seconds
         *
         * @throws IOException
         */
        [Fact]
        public void TestSomeRuntimeValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 1F 45 43>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal("04:55:31", FormattedResult);
        }

        /**
         * Test for valid InputStream read, 0 seconds.
         *
         * @throws IOException
         */
        [Fact]
        public void TestMinRuntimeValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 1F 00 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal("00:00:00", FormattedResult);
        }
    }

}
