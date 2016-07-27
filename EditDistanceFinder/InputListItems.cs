using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace EditDistanceFinder
{
    public class InputListItems
    {
        public InputListItems(string id, string peptide, string charge, string path)
        {   
            this.id = id;
            this.peptide = peptide;
            this.charge = charge;
            this.organismName = GetOrganism(path);
        }

        public string id{ get;set; }
        public string peptide { get; set; }
        public string charge { get; set; }
        public string organismName { get; set; }

        public static string GetOrganism(string path)
        {
            string filenameNoExtension = Path.GetFileNameWithoutExtension(path);
            string[] lines = Regex.Split(filenameNoExtension, "_");
            Console.WriteLine("org is: " + lines[1] + "_" + lines[2].Substring(0, 4));
            return lines[1] + "_" + lines[2].Substring(0, 4);
        }
    }
}
