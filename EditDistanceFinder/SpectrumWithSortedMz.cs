using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformedProteomics.Backend.Data.Spectrometry;
using InformedProteomics.Backend.MassSpecData;

namespace EditDistanceFinder
{
    public class SpectrumWithSortedMz 
    {
        public int ScanNum { get; private set; }
        public string path { get; private set; }
        private const int topNElements = 20; // this variable stores the number of items that will be stored into the array of mz values used for comparison
        public string peptide { get; private set; }

        public SpectrumWithSortedMz(Spectrum s, string path, string peptide)
        {
            this.path = path;
            ScanNum = s.ScanNum;
            combinedString = getOrderedMZFromSpectrum(s.Peaks);
            this.peptide = peptide;
        }

        // Property accessors
        public string combinedString { get; set; }

        public static string getOrderedMZFromSpectrum(Peak[] Peaks)
        {   //after sorting by intensity
            var sorted = Peaks.OrderBy(c => -c.Intensity).Take(topNElements); // the minus sign indicates descending order
            var orderedMz = sorted.OrderBy(c => c.Mz).Select(p => p.Mz.ToString()); // this will sort in ascending order (0 -> largest)
            return string.Join(",", orderedMz);
        }

    }
}


