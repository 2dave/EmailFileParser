using System;
using System.Collections.Generic;
using System.Text;

namespace EmailApi
{
    public class EmailMessage
    {
        public string From { get; set; }
        //break header into a key value pair
        public string Header { get; set; }        
        public string Body { get; set; }
    }
}
