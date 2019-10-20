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
 * Tests for RPMCommand class.
 */

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands.engine;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class RpmCommandTest : RpmCommand
    {
        /**
         * Test for valid InputStream read, max RPM
         *
         * @throws IOException
         */
        [Fact]
        public void TestMaximumRpmValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0C FF FF>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(16383, Rpm);
        }

        /**
         * Test for valid InputStream read
         *
         * @throws IOException
         */
        [Fact]
        public void TestHighRpm()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0C 28 3C>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(2575, Rpm);
        }

        /**
         * Test for valid InputStream read
         *
         * @throws IOException
         */
        [Fact]
        public void TestLowRpm()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0C 0A 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(640, Rpm);
        }
    }
}