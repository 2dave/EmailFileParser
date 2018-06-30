using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EmailApi;

namespace EmailTests
{
    public class EmailLineFixture
    {
        [Fact]
        public void TestParseEmailLine()
        {
            var teststring = "From Some email address \r\n Some other stuff";

            var line = EmailFileParser.ParseLine(teststring, out var remainder);

            Assert.Equal("From Some email address \r\n", line);
            Assert.Equal(" Some other stuff", remainder);
        }

        [Fact]
        public void TestAgainParseEmailLine()
        {
            var teststring = "From Something\r\n Some other stuff\r\n and all other stuff";

            var line = EmailFileParser.ParseLine(teststring, out var remainder);

            Assert.Equal("From Something\r\n", line);
            Assert.Equal(" Some other stuff\r\n and all other stuff", remainder);
        }
    }
}
