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
 * Tests for TemperatureCommand sub-classes.
 */

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands.temperature;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class AirIntakeTempCommandTest : AirIntakeTemperatureCommand
    {
        /**
     * Test for valid InputStream read, 24ºC
     *
     * @throws IOException
     */
        [Fact]
        public void TestValidTemperatureCelsius()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0F 40>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(24f, Temperature);
        }

        /**
     * Test for valid InputStream read, 75.2F
     *
     * @throws IOException
     */
        [Fact]
        public void TestValidTemperatureFahrenheit()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0F 45>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            UseImperialUnits = true;
            Assert.Equal(84.2f, GetImperialUnit());
        }

        /**
     * Test for valid InputStream read, 0ºC
     *
     * @throws IOException
     */
        [Fact]
        public void TestValidTemperatureZeroCelsius()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0F 28>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(0f, Temperature);
        }
    }
}