using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDistanceFinder
{
    public class InputListItems
    {
        public InputListItems(string id, string peptide, string charge)
        {   
            this.id = id;
            this.peptide = peptide;
            this.charge = charge;
        }

        public string id{ get;set; }
        public string peptide { get; set; }
        public string charge { get; set; }
    }
}
