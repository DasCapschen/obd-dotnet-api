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
 * Tests for DescribeProtocolNumberCommand class.
 */

#region

using System.IO;
using System.Text;
using obd_dotnet_api.commands.protocol;
using obd_dotnet_api.enums;
using Xunit;

#endregion

namespace unit_tests.commands
{
    public class DescribeProtocolNumberCommandTest : DescribeProtocolNumberCommand
    {
        [Fact]
        public void TestGetCalculatedResult()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"A3>2>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal("ISO_9141_2", CalculatedResult); //AUTO ISO_9141_2

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal("SAE_J1850_VPW", CalculatedResult); //SAE_J1850_VPW
        }

        [Fact]
        public void TestGetProtocol()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"A6>7>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(ObdProtocols.Iso157654Can, ObdProtocol); //AUTO ISO_15765_4_CAN

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(ObdProtocols.Iso157654CanB, ObdProtocol); //ISO_15765_4_CAN_B
        }
    }
}