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
 * Test results with echo on and off.
 */

using System.IO;
using System.Text;
using obd_dotnet_api.commands;
using Xunit;

namespace unit_tests.commands
{
    public class ExtraMessagesTest : SpeedCommand
    {
        /**
     * Test for valid InputStream read with echo
     *
     * @throws java.io.IOException
     */
        [Fact]
        public void TestValidSpeedMetricWithMessage() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            var _13 = Encoding.ASCII.GetString(new byte[] {13});
            mockIn.Write(Encoding.ASCII.GetBytes($"BUS INIT...{_13}41 0D 40>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(64, MetricSpeed);
        }

        /**
     * Test for valid InputStream read with echo
     *
     * @throws java.io.IOException
     */
        [Fact]
        public void TestValidSpeedMetricWithoutMessage() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0D 40>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(64, MetricSpeed);
        }

    }
}
