using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Text;

//This does all of the work that does not interact with the user
namespace EmailApi
{  
    public enum EmailParseStates
    {
        Start,
        From,
        Header,
        Body,
    }

    public class EmailFileParser
    {
        private EmailParseStates State { get; set; }

        private string Remainder { get; set; }

        public static string ParseLine(string input, out string remainder)
        {
            if (input == null)
            {
                remainder = null;
                return null;
            }

            string line = null;

            var crsearch = input.IndexOf('\r');
            var lfsearch = input.IndexOf('\n');

            if (lfsearch >= 0 && crsearch >= 0)
            {
                //line = input.Remove(lfsearch + 1);
                line = input.Substring(0, lfsearch + 1);
            }

            remainder = input.Substring(lfsearch + 1);

            return line;
        }

        //make emailmessage a partial? or partial email into a state?
        public EmailMessage ParseEmailMessage (string input)
        {            
            EmailMessage message = new EmailMessage();

            var line = ParseLine(input, out string remainder);

            while (line != null)
            {
                if (this.State == EmailParseStates.Start)
                {
                    if (line.StartsWith("From "))
                    {
                        this.State = EmailParseStates.From;
                        const string removestring = "From ";
                        message.From = line.Substring(removestring.Length);
                    }
                }
                else if (this.State == EmailParseStates.From) 
                {
                    if (line != "\r\n")
                    {
                        message.Header += line;

                        this.State = EmailParseStates.Header;
                    }
                    else
                    {
                        this.State = EmailParseStates.Body;
                    }
                }
                else if (this.State == EmailParseStates.Header)
                {
                    if (line != "\r\n")
                    {
                        message.Header += line;
                    }
                    else
                    {
                        this.State = EmailParseStates.Body;
                    }
                }
                else if (this.State == EmailParseStates.Body)
                {
                    message.Body += line;
                }

                line = ParseLine(remainder, out remainder);

            }

            this.Remainder = remainder;

            return message;
        }

    }
}
