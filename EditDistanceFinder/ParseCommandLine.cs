using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using CommandLine.Text;

namespace EditDistanceFinder
{   // Define a class to receive parsed values
    //example: 
    public class ParseCommandLine
    {
            [Option('r', "read", Required = true,
              HelpText = "Path to directory where your database is stored.")]
            public string InputDir { get; set; }

            [Option('v', "verbose", DefaultValue = true,
              HelpText = "Prints all messages to standard output.")]
            public bool Verbose { get; set; }

            [Option('e', "eVal", DefaultValue = 1e-10,
                  HelpText = "Specifies the minimum cutoff for e-values.")]
            public double SpecEValCutoff { get; set; }

            [Option('o', "outputFileName", DefaultValue = "RenameYourOutput.csv",
                      HelpText = "The name of your output file.")]
            public string outputFileName { get; set; }

           //example of the proper format for command line - as below



        [ParserState]
            public IParserState LastParserState { get; set; }

            [HelpOption]
            public string GetUsage()
            {
                return HelpText.AutoBuild(this,
                  (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
            }
     
        
    }
}
