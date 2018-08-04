using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EmailApi;

namespace EmailTests
{
    public class EmailFailureFixture
    {
        //partial messages and bad data
        [Fact]
        public void OnePartialFromMessage()
        {
            var input = "From junk stuff no header no body just randmon junk";

            EmailFileParser parser = new EmailFileParser();
            EmailMessage message;

            do
            {
                message = parser.ParseEmailMessage(input, out input);

            } while (input != "From junk stuff no header no body just random junk");

            Assert.Equal("", message.From);
            Assert.Equal("", message.Header);
            Assert.Equal("", message.Body);
            Assert.Equal("From junk stuff no header no body just randmon junk", input);
        }
       
    }
}
