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
using obd_dotnet_api.commands.control;
using obd_dotnet_api.exceptions;
using Xunit;

namespace unit_tests.commands
{
    public class PermanentTroubleCodesCommandTest : PermanentTroubleCodesCommand
    {

        /**
     * Test for two frames with four dtc
     *
     * @throws java.io.IOException
     */
        [Fact]
        public void TwoFramesWithFourDtc()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            var _13 = Encoding.ASCII.GetString(new byte[]{13});
            mockIn.Write(Encoding.ASCII.GetBytes($"4A 00 03 51 04 A1 AB{_13}4A F1 06 00 00 00 00>"));
            mockIn.Flush();
            mockIn.Position = 0;
        
            var expected = "P0003\n";
            expected += "C1104\n";
            expected += "B21AB\n";
            expected += "U3106\n";

            // call the method to test
            ReadResult(mockIn);

            Assert.Equal(expected, FormattedResult);
        }

        /**
     * Test for one frame with three dtc
     *
     * @throws java.io.IOException
     */
        [Fact]
        public void OneFrameWithThreeDtc() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"4A 01 03 01 04 01 05>"));
            mockIn.Flush();
            mockIn.Position = 0;

            var expected = "P0103\n";
            expected += "P0104\n";
            expected += "P0105\n";

            // call the method to test
            ReadResult(mockIn);

            Assert.Equal(expected, FormattedResult);
        }

        /**
     * Test for one frame with two dtc
     *
     * @throws java.io.IOException
     */
        [Fact]
        public void OneFrameWithTwoDtc()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"4A 01 03 01 04 00 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            var expected = "P0103\n";
            expected += "P0104\n";

            // call the method to test
            ReadResult(mockIn);

            Assert.Equal(expected, FormattedResult);
        }

        /**
     * Test for two frames with four dtc CAN (ISO-15765) format
     *
     * @throws IOException
     */
        [Fact]
        public void TwoFramesWithFourDtcCan()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            var _13 = Encoding.ASCII.GetString(new byte[]{13});
            mockIn.Write(Encoding.ASCII.GetBytes($"00A{_13}0:4A 04 01 08 01 18{_13}1:01 19 01 20 00 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            var expected = "P0108\n";
            expected += "P0118\n";
            expected += "P0119\n";
            expected += "P0120\n";

            // call the method to test
            ReadResult(mockIn);

            Assert.Equal(expected, FormattedResult);
        }

        /**
     * Test for one frames with two dtc CAN (ISO-15765) format
     *
     * @throws IOException
     */
        [Fact]
        public void OneFrameWithTwoDtcCan() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"4A 02 01 20 01 21>"));
            mockIn.Flush();
            mockIn.Position = 0;

            var expected = "P0120\n";
            expected += "P0121\n";

            // call the method to test
            ReadResult(mockIn);

            Assert.Equal(expected, FormattedResult);
        }

        /**
     * Test for no data
     *
     * @throws java.io.IOException
     */
        //@Test(expectedExceptions = NoDataException.class)
        [Fact]
        public void NoData() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"4A NO DATA>"));
            mockIn.Flush();
            mockIn.Position = 0;

            Assert.Throws<NoDataException>(()=>ReadResult(mockIn));
        }
    }
}