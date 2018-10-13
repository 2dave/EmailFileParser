using System;
using System.IO;
using System.Reflection;
using EmailApi;
using HtmlAgilityPack;

namespace EmailProject
{
    class Program
    {
        static void Main(string[] args)
        {
            //spit out an error for empty args
            //would like to look at this further

            //var inputfile = FindDir("smallsample.txt");
            //Make a folder for data like this - take out of git
            var inputfile = args[0];

            EmailRenderer renderer = new EmailRenderer();
            EmailFileParser parser = new EmailFileParser();

            using (var file = new StreamReader(inputfile))
            {
                var input = file.ReadToEnd(); //solve later :)

                //save result to file
                var path = @".\outputhtml.html";

                if (File.Exists(@".\outputhtml.html")) // (File.Exists(path))?
                {
                    try
                    {
                        File.Delete(@".\outputhtml.html");
                    }
                    catch (System.IO.IOException e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }

                while (!string.IsNullOrEmpty(input))
                {
                    var message = parser.ParseEmailMessage(input, out input);

                    //Console.WriteLine("---FROM -----------\n {0}", message.From);
                    //Console.WriteLine("---HEADER ---------\n {0}", message.Header);
                    //Console.WriteLine("---BODY -----------\n {0}", message.Body);
                                        
                    //render the html messages - might no longer be relevant
                    var result = renderer.Render(message);
                    File.AppendAllText(path, result); 

                    //use the html agility pack
                    var htmldoc = new HtmlDocument();
                    htmldoc.LoadHtml(message.Body);
                                        
                    var htmlnodes = htmldoc.DocumentNode.SelectNodes("//body");

                    if (htmlnodes != null)
                    {
                        foreach (var node in htmlnodes)
                        {
                            Console.WriteLine(node.InnerText);
                            //Console.WriteLine(node.XPath);
                            //Console.WriteLine(node.InnerHtml);
                        }
                    }                
                }
            }
        }

        public static string FindDir(string filename)
        {
            //var root = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var root = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
            var inputfile = Path.Combine(root, filename);

            return inputfile;
        }
    }
}
