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

using System.IO;
using System.Text;
using obd_dotnet_api.commands.engine;
using Xunit;

namespace unit_tests.commands
{
    public class MassAirFlowCommandTest : MassAirFlowCommand
    {


        /**
     * Test for valid InputStream read, maximum value of 655.35g/s
     *
     * @throws IOException
     */
        [Fact]
        public void TestMaxMafValue() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 10 FF FF>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(655.3499755859375d, Maf);
        }

        /**
     * Test for valid InputStream read, 381.61g/s
     *
     * @throws IOException
     */
        [Fact]
        public void TestSomeMafValue() 
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 10 95 11>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(381.6099853515625d, Maf);
        }

        /**
     * Test for valid InputStream read, minimum value 0g/s
     *
     * @throws IOException
     */
        [Fact]
        public void TestMinMafValue()
        {
            // mock InputStream read
            var mockIn = new MemoryStream();
            mockIn.Write(Encoding.ASCII.GetBytes($"41 10 00 00>"));
            mockIn.Flush();
            mockIn.Position = 0;

            // call the method to test
            ReadResult(mockIn);
            Assert.Equal(0d, Maf);
        }

    }
}
