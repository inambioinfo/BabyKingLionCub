using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDistanceFinder
{
    public class PrsmAndFile
    {
        public PrsmAndFile(List<PrSm> prsms, string fileName)// this is likely where the codex for each file will be set
        {
            prsmsHolder = prsms;
            fileNameHolder = fileName;
        }

        public List<PrSm> prsmsHolder { get; set; }
        public string fileNameHolder { get; set; }

    }
}
