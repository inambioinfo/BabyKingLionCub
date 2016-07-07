using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformedProteomics.Backend.Data.Spectrometry;
using InformedProteomics.Backend.MassSpecData;

namespace EditDistanceFinder
{
    public class SortPairs // maybe make into static class with two public methods - one will call from main and the other from Console_Sim
        /* this class is just going to call the levenshteinDistance computer and  
        *  then store the output to files based on those scores - nothing else 
        */
        //main calls this program and this creates all the objects needed
        //ReadFilesInDir will be a method inside of this class
        //create prsm and the prsmandfile    
        //call make combos
    {
        private MakeCombinations combinations;
        //constructor
        public SortPairs(ParseCommandLine options)
        {
            Console.WriteLine("Creating PrSmAndFile List");
            PrSms combinationFiller = GetInputDir(options.InputDir, options.MzMLDir);
            Console.WriteLine("Making combinations");
            //need to fill in here to make the combinatons
            combinations = new MakeCombinations(createASpectrum(combinationFiller.prsmFile));//create a new spectrum

        }
        //public SortPairs(string input)
        //{   Console.WriteLine("Creating PrSmAndFile List");
        //    PrSms combinationFiller = GetInputDir(input);
        //    Console.WriteLine("Making combinations");
        //    //need to fill in here to make the combinatons
        //    combinations = new MakeCombinations(createASpectrum(combinationFiller.prsmFile));//create a new spectrum
        //}

        internal void ToFile()
        {
            throw new NotImplementedException();
        }

        public static List<SpectrumWithSortedMz> createASpectrum(List<PrsmAndFile> p) //Currently reading MGF files
        {
            // receives as input a path to the file and the scan number -- outputs a new Spectrum
            List<SpectrumWithSortedMz> results = new List<SpectrumWithSortedMz>();
            for (int j = 0; j < p.Count; j++)
            {
                LcMsRun myRun = PbfLcMsRun.GetLcMsRun(p[j].fileNameHolder);
                //This is a time bottleneck if you do not run the CreateAllPbfs first
                //IList<int> scans = myRun.GetScanNumbers(2); //2 for ms2 spectra - returns list of scan numbers   
                //here iterate over the scans and retreive all spectra -- do not use line below        
                for (int k = 0; k < p[j].prsmsHolder.Count; k++)
                {   
                        Spectrum inf = myRun.GetSpectrum(p[j].prsmsHolder[k].Scan);
                        results.Add(new SpectrumWithSortedMz(inf, p[j].fileNameHolder, p[j].prsmsHolder[k].SequenceText));
                }       
            }
            return results;
        }
        private PrSms GetInputDir(string InputDir, string MzMLDir) 
        {
            DirectoryInfo d = new DirectoryInfo(InputDir); //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.mzid"); //Getting Text files
            List<string> FileHolderList = new List<string>();
           
            foreach (FileInfo file in Files)
            {
                //need to find a way to trim or remove the _msgfplus off, .Remove(15) and .Trim(15) cannot be used with FileInfo
                FileHolderList.Add(InputDir + "\\" + file);
            }
            //here send the entire FileHolderList over to Prsm
           return new PrSms(FileHolderList, MzMLDir, InputDir);//, inputDirLength
        }

        private void SortPairs2(string stringOne, string stringTwo) // this is never called - check to make sure before deleting
        {
            int editDist = ComputeLevenshteinDistance.LevenshteinDistance(stringOne, stringTwo);
        }    
    }
}
