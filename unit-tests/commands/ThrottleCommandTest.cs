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
 * Tests for ThrottlePositionCommand class.
 */

using System.IO;
using System.Text;
using obd_dotnet_api.commands.engine;
using Xunit;

namespace unit_tests.commands
{
    public class ThrottleCommandTest : ThrottlePositionCommand
    {

        /**
         * Test for valid InputStream read, maximum value of 100%
         *
         * @throws IOException
         */
        [Fact]
        public void TestMaxThrottlePositionValue()
        {
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 11 FF>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(100f, Percentage);
        }

        /**
         * Test for valid InputStream read, 58.4%
         *
         * @throws IOException
         */
        [Fact]
        public void TestSomeThrottlePositionValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 11 95>"));
            mockIn.Flush();
            mockIn.Position = 0;


            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(58.431374f, Percentage);
        }

        /**
         * Test for valid InputStream read, minimum value 0%
         *
         * @throws IOException
         */
        [Fact]
        public void TestMinThrottlePositionValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 11 00>"));
            mockIn.Flush();
            mockIn.Position = 0;
            
            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(0f, Percentage);
        }
    }
}