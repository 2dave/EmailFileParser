using SharpRazor;
using System;
using System.Collections.Generic;
using System.Text;

namespace EmailApi
{
    public class EmailRenderer
    {
        private Razorizer renderer = new Razorizer();
        private const string Template = "<html><head><title>@Model.From</title></head><body><p>@Model.Body</p></body></html>";

        //public string Render(EmailMessage message) => renderer.Parse(Template, message);

        public string Render(EmailMessage message)
        {
            return renderer.Parse(Template, message);
        }

    }
}
