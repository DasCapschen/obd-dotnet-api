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
 * Tests for ObdSpeedCommand class.
 */

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class SpeedCommandTest : SpeedCommand
    {
        /**
         * Test for valid InputStream read, 64km/h
         *
         * @throws IOException
         */
        [Fact]
        public void TestValidSpeedMetric()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0D 40>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            //FormattedResult;
            Assert.Equal(64, MetricSpeed);
        }

        /**
         * Test for valid InputStream read, 42.87mph
         *
         * @throws IOException
         */
        [Fact]
        public void TestValidSpeedImperial()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0D 45>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            UseImperialUnits = true;
            //FormattedResult;
            Assert.Equal(42.874615f, ImperialSpeed);
        }

        /**
         * Test for valid InputStream read, 0km/h
         *
         * @throws IOException
         */
        [Fact]
        public void TestZeroSpeedMetric()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0D 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(0, MetricSpeed);
        }
    }
}