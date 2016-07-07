using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace EditDistanceFinder
{
    class MainProgram
    {
        //create constructors
        static void Main(string[] args)
        {
            var options = new ParseCommandLine();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {   Console.WriteLine(options.InputDir.GetType());
                // Values are available here
                Console.WriteLine("Starting Sort");
                //new SortPairs(options.InputDir).ToFile(); // this creates a new SortPairs and outputs the info to file
                new SortPairs(options).ToFile(); // this creates a new SortPairs and outputs the info to file/
                Console.WriteLine("done");
            }
        }

    }

}
