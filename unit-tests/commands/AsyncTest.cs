using System;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using obd_dotnet_api.commands.temperature;
using Xunit;

namespace unit_tests.commands
{
    internal class MockStream : MemoryStream
    {
        //memory stream sends "end of stream" if no data ; a socket stream (networkstream) would not!
        //I'm working with bluetooth obd, so it would be a socket, not sure how a cable connection would handle this
        
        public override int Read(byte[] buffer, int offset, int count)
        {
            while (GetBuffer().Length == 0) ;
            while(GetBuffer()[0] == 0x00) ;
            return base.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            //we may not change position because we wanna read it in a second
            base.Write(buffer, offset, count);
            Position = 0;
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            //doing just while(){} here strangely blocks the test... I thought this already runs async?
            await Task.Run(() =>
            {
                while (GetBuffer().Length == 0) ;
            });

            return await base.ReadAsync(buffer, offset, count, cancellationToken);
        }
    }

    public class AsyncTest
    {
        [Fact]
        public async void TestAsyncCalls()
        {
            var mockIn = new MockStream();
            var mockOut = new MemoryStream();

            var cmd = new AirIntakeTemperatureCommand();

            //calls SendCommandAsync and ReadResultAsync
            //which in turn call other async stuff ; ends up testing pretty much all async methods
            var task = cmd.RunAsync(mockIn, mockOut);

            //RunAsync has sent its command and should now be waiting for data
            //to make sure it actually does wait, let's delay
            await Task.Delay(500);

            //data finally arrives in the command
            mockIn.Write(Encoding.ASCII.GetBytes($"41 0F 40>"));
            mockIn.Flush();

            //lets wait for the task to finish reading and processing the data
            await task;

            //and if everything worked well, we get the proper result!
            Assert.Equal(24f, cmd.Temperature);
        }
    }
}