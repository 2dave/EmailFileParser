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
            //At the moment if you don't add a \r\n somewhere in the message then ParseEmailMessage 
            //will not parse anything because ParseLine will be null
            var input = "From junk stuff no header no body just random junk\r\n other garbage";

            EmailFileParser parser = new EmailFileParser();
            EmailMessage message = parser.ParseEmailMessage(input, out input); 

            Assert.Equal("junk stuff no header no body just random junk\r\n", message.From);
            Assert.Null(message.Header);
            Assert.Null(message.Body);
            Assert.Equal(" other garbage", input);
        }

       
    }
}
