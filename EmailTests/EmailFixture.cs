using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.Reflection;
using EmailApi;


namespace EmailTests
{
    public class EmailFixture
    {
        [Fact]
        public void ReadFileCheck()
        {
            string inputfile = FindDir("smallsample.txt");
            bool fileexists;

            if (File.Exists(inputfile))
            {
                fileexists = true;
            }
            else
            {
                fileexists = false;
            }

            Assert.True(fileexists);
        }

        [Fact]
        public void ReadFileSyntaxCheck()
        {
            var inputfile = FindDir("smallsample.txt");
              
            //When working with these potentially very large files this is not going to work
            //var filecontents = File.ReadAllText(inputfile);
            //var filetoarray = File.ReadAllLines(inputfile);

            int counter = 0;
            string line;

            using (var file = new StreamReader(inputfile))
            {
                while ((line = file.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    counter++;
                }
            }

            Assert.Equal(1356, counter);
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