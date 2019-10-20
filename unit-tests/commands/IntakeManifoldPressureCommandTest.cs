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

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands.pressure;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class IntakeManifoldPressureCommandTest : IntakeManifoldPressureCommand
    {
        /**
     * Test for valid InputStream read, 100kPa
     *
     * @throws IOException
     */
        [Fact]
        public void TestValidPressureMetric()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0B 64>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            UseImperialUnits = false;
            Assert.Equal(100, MetricUnit);
        }

        /**
     * Test for valid InputStream read, 14.50psi
     *
     * @throws IOException
     */
        [Fact]
        public void TestValidPressureImperial()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0B 64>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            UseImperialUnits = true;
            Assert.Equal(14.503774f, GetImperialUnit());
        }
    }
}