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
        static void Main(string[] args)
        {
            var options = new ParseCommandLine();
            if (CommandLine.Parser.Default.ParseArguments(args, options))
            {   Console.WriteLine("Starting Program.");
                SQLiteConnector newConnection = new SQLiteConnector(options);//.ToFile(); // this creates a new SortPairs and outputs the info to file/
                Console.WriteLine("Finished. Press any key to exit");
                Console.ReadKey();
            }
        }
    }
}
