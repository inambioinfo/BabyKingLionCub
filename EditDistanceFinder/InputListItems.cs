using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDistanceFinder
{
    public class InputListItems
    {
        public InputListItems(string id, string peptide, string charge, string codex)
        {   
            this.id = id;
            this.peptide = peptide;
            this.charge = charge;
            this.codex = codex;
        }

        public string id{ get;set; }
        public string peptide { get; set; }
        public string charge { get; set; }
        public string codex { get; set; }
    }
}
