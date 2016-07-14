using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDistanceFinder
{
    public class InputListItems
    {
        public InputListItems(string id, string peptide)
        {   
            this.id = id;
            this.peptide = peptide;
        }

        public string id{ get;set; }
        public string peptide { get; set; }
    }
}
