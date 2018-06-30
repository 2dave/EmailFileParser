using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EmailApi;

namespace EmailTests
{
    public class EmailMessageFixture
    {
        [Fact]
        public void BeginMessageTest()
        {
            var input = "From Not Header stuff\r\n more header \r\n\r\n body stuff \r\n\r\n more body \r\n";

            EmailFileParser parser = new EmailFileParser();
            EmailMessage message = parser.ParseEmailMessage(input);

            Assert.Equal("Not Header stuff\r\n", message.From);
            Assert.Equal(" more header \r\n", message.Header);
            Assert.Equal(" body stuff \r\n\r\n more body \r\n", message.Body);
        }

        //two messages in one input 1st

        //one message split across two inputs

        //two messages across three inputs

    }
}
