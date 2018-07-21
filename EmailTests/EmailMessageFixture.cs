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

            //Now that I'm using an out variable in ParseEmailMessage I need to use "out string remainder" again
            EmailMessage message = parser.ParseEmailMessage(input, out string remainder);

            Assert.Equal("Not Header stuff\r\n", message.From);
            Assert.Equal(" more header \r\n", message.Header);
            Assert.Equal(" body stuff \r\n\r\n more body \r\n", message.Body);
        }

        //two messages in one input 1st
        [Fact]
        public void TwoMessagesOneInput()
        {
            var input = 
                "From Not Header stuff\r\n more header \r\n\r\n body stuff \r\n\r\n more body \r\n" +
                "From Not Header again\r\n again more header \r\n\r\n again body stuff \r\n\r\n more body \r\n";

            EmailFileParser parser = new EmailFileParser();
            EmailMessage message = parser.ParseEmailMessage(input, out string remainder);

            Assert.Equal("Not Header stuff\r\n", message.From);
            Assert.Equal(" more header \r\n", message.Header);
            Assert.Equal(" body stuff \r\n\r\n more body \r\n", message.Body);
            //remainder should be the next message (or everything else)
            Assert.Equal("From Not Header again\r\n again more header \r\n\r\n again body stuff \r\n\r\n more body \r\n", remainder);

        }

        //one message split across two inputs

        //two messages across three inputs

    }
}
