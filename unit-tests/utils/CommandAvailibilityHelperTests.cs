using System;
using obd_dotnet_api.commands.engine;
using obd_dotnet_api.utils;
using Xunit;

namespace unit_tests.utils
{
    public class CommandAvailibilityHelperTests
    {
        private int Parse2(string s) => Convert.ToInt32(s, 2);
        
        [Fact]
        public void TestDigestAvailabilityString()
        {
            var expected = new[]{
                Parse2("10111110"), Parse2("00011111"), 
                Parse2("10101000"), Parse2("00010011")
            };
            var result = CommandAvailabilityHelper.DigestAvailabilityString("BE1FA813");
            Assert.Equal(expected, result);

            //Now with 16 characters
            expected = new[]{
                Parse2("10111110"), Parse2("00011111"),
                Parse2("10101000"), Parse2("00010011"),
                Parse2("10111110"), Parse2("00011111"),
                Parse2("10101000"), Parse2("00010011")
            };

            result = CommandAvailabilityHelper.DigestAvailabilityString("BE1FA813BE1FA813");
            Assert.Equal(expected, result);
        }
        
        [Fact]
        public void TestIsAvailable() 
        {
            Assert.False(CommandAvailabilityHelper.IsAvailable("02", "BE1FA813"));
            Assert.True(CommandAvailabilityHelper.IsAvailable("07", "BE1FA813"));
            Assert.True(CommandAvailabilityHelper.IsAvailable(new ThrottlePositionCommand().CommandPid /*11*/, "BE1FA813"));
            Assert.False(CommandAvailabilityHelper.IsAvailable("1A", "BE1FA813"));
            Assert.False(CommandAvailabilityHelper.IsAvailable("1D", "BE1FA813"));
            Assert.True(CommandAvailabilityHelper.IsAvailable("1F", "BE1FA813"));
            Assert.False(CommandAvailabilityHelper.IsAvailable("22", "BE1FA813BE1FA813"));
            Assert.True(CommandAvailabilityHelper.IsAvailable("27", "BE1FA813BE1FA813"));
            Assert.False(CommandAvailabilityHelper.IsAvailable("3A", "BE1FA813BE1FA813"));
            Assert.False(CommandAvailabilityHelper.IsAvailable("3D", "BE1FA813BE1FA813"));
            Assert.True(CommandAvailabilityHelper.IsAvailable("3F", "BE1FA813BE1FA813"));
        }
        
        [Fact]
        public void TestThrowExceptions()
        {
            Assert.Throws<ArgumentException>(() => CommandAvailabilityHelper.DigestAvailabilityString("AAA"));

            Assert.Throws<ArgumentException>(() => CommandAvailabilityHelper.DigestAvailabilityString("AAAAAAAR"));

            Assert.Throws<ArgumentException>(() => CommandAvailabilityHelper.IsAvailable("2F", "BE1FA813"));
        }
    }
}