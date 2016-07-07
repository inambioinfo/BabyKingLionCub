using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDistanceFinder
{
    public class PrsmAndFile
    {
        public PrsmAndFile(List<PrSm> prsms, string fileName)
        {
            prsmsHolder = prsms;
            fileNameHolder = fileName;
        }

        public List<PrSm> prsmsHolder { get; set; }
        public string fileNameHolder { get; set; }

    }
}
