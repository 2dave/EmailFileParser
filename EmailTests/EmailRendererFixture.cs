using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using EmailApi;

namespace EmailTests
{
    public class EmailRendererFixture
    {
        [Fact]
        public void CanRender()
        {
            EmailRenderer renderer = new EmailRenderer();
            EmailMessage message = new EmailMessage();

            message.From = "The beginning of the message";
            message.Header = "the header";
            message.Body = "the body";

            var result = renderer.Render(message);
        }
    }
}
