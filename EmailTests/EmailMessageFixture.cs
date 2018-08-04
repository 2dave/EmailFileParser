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
            EmailMessage message;
            EmailMessage message2;

            //loop through until input = ""
            do
            {
                //just putting the input back in instead of using a remainder variable!
                message = parser.ParseEmailMessage(input, out input);
                message2 = parser.ParseEmailMessage(input, out input);

            } while (input != "");

            Assert.Equal("Not Header stuff\r\n", message.From);
            Assert.Equal(" more header \r\n", message.Header);
            Assert.Equal(" body stuff \r\n\r\n more body \r\n", message.Body);

            Assert.Equal("Not Header again\r\n", message2.From);
            Assert.Equal(" again more header \r\n", message2.Header);
            Assert.Equal(" again body stuff \r\n\r\n more body \r\n", message2.Body);

            //remainder should be the next message (or everything else)
            //Assert.Equal("", input);
        }

        [Fact]
        public void ThreeMessagesAndIncompleteMessageOneInput()
        {
            var input =
                "From Not Header stuff\r\n more header \r\n\r\n body stuff \r\n\r\n more body \r\n" +
                "From Not Header again\r\n again more header \r\n\r\n again body stuff \r\n\r\n more body \r\n" +
                "From Not Header thrice\r\n third more header \r\n\r\n third body stuff \r\n\r\n more body \r\n" +
                "From junk stuff no header no body just randmon junk";

            EmailFileParser parser = new EmailFileParser();
            EmailMessage message;
            EmailMessage message2;
            EmailMessage message3;

            do
            {
                //just putting the input back in instead of using a remainder variable!
                message = parser.ParseEmailMessage(input, out input);
                message2 = parser.ParseEmailMessage(input, out input);
                message3 = parser.ParseEmailMessage(input, out input);

                //this loop needs to be rethought some - the loop logic could go into the email parser
            } while (input != "From junk stuff no header no body just randmon junk");

            Assert.Equal("Not Header stuff\r\n", message.From);
            Assert.Equal(" more header \r\n", message.Header);
            Assert.Equal(" body stuff \r\n\r\n more body \r\n", message.Body);

            Assert.Equal("Not Header again\r\n", message2.From);
            Assert.Equal(" again more header \r\n", message2.Header);
            Assert.Equal(" again body stuff \r\n\r\n more body \r\n", message2.Body);

            Assert.Equal("Not Header thrice\r\n", message3.From);
            Assert.Equal(" third more header \r\n", message3.Header);
            Assert.Equal(" third body stuff \r\n\r\n more body \r\n", message3.Body);

            Assert.Equal("From junk stuff no header no body just randmon junk", input);
        }

        //one message split across two inputs
        //[Fact]
        //public void TwoMessagesTwoInputs()
        //{

        //}

        //two messages across three inputs

    }
}
