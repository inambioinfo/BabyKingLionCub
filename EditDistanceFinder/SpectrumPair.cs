using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformedProteomics.Backend.Data.Spectrometry;
using InformedProteomics.Backend.Utils;



namespace EditDistanceFinder

{
    public class SpectrumPair
    {   //you would only pass in the two Spectra and the peptidePair and then call methods for comparison
        public SpectrumPair(SpectrumWithSortedMz spectrum1, SpectrumWithSortedMz spectrum2, string[] peptidePair)//, string path1, string path2
        {
            spectrumOne = spectrum1;
            spectrumTwo = spectrum2;
            specOneMzArr = spectrumOne.combinedString;//spectrumOne.orderedPeaks
            specTwoMzArr = spectrumTwo.combinedString;//spectrumTwo.orderedPeaks
            pathOne = spectrumOne.path;
            pathTwo = spectrumTwo.path;
            pairOfPeptides = peptidePair;
        }

        // Property accessors
        public SpectrumWithSortedMz spectrumOne { get; set; }
        public SpectrumWithSortedMz spectrumTwo { get; set; }
        public string specOneMzArr { get; set; } // these are the ordered Mz arrays for each spectrum
        public string specTwoMzArr { get; set; }
        public string[] pairOfPeptides { get; set; }// this was of type string[], but changed it to string for testing.  Will change back to string[]
        public string pathOne { get; set; }
        public string pathTwo { get; set; }
       // private double tolerance = 0.2;
        

        public string toDatabase()
        {
      
            return "('" + pathOne + "','" + spectrumOne.ScanNum + "','" + pathTwo + "','" + spectrumTwo.ScanNum + "','" + spectrumOne.combinedString + "','" + spectrumTwo.combinedString +
                   "','" + string.Join(",", pairOfPeptides) + "')";
        }
    }
}
