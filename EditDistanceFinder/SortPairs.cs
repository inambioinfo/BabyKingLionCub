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
    public class SortPairs 
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

        //temporarily method for creating codex values stored here, will most likely move to SQLiteConnector class
        public static void createCodexValues(List<string> fileHolderList)
        {
            List<string> filesTableValues = new List<string>();
            //List<string> codexValues = new List<string> { 'a','b','c' };
            int lengthOfDir = fileHolderList.Count;
            for (int i = 0; i < lengthOfDir; i++)
            {   // here add the values of the alphabet and assign to each of the path names
                //filesTableValues.Add(codexValues[i]);
                // you will then send to a command that fills these to the database as a bulk inser
            }

        }
        private PrSms GetInputDir(string InputDir, string MzMLDir) //unit testing of this method requires public static 
        {
            DirectoryInfo d = new DirectoryInfo(InputDir); //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("*.mzid"); //Getting Text files
            List<string> FileHolderList = new List<string>();
           
            foreach (FileInfo file in Files)
            {
                //need to find a way to trim or remove the _msgfplus off, .Remove(15) and .Trim(15) cannot be used with FileInfo
                FileHolderList.Add(InputDir + "\\" + file);
            }
            //here send the entire FileHolderList over to Prsms
            //here pass the whole FileHolderList over to a new method which will assign a codex value to each file and create and send to table 
           return new PrSms(FileHolderList, MzMLDir, InputDir);
        }

        private void SortPairs2(string stringOne, string stringTwo) // this is never called - check to make sure before deleting
        {
            int editDist = ComputeLevenshteinDistance.LevenshteinDistance(stringOne, stringTwo);
        }    
    }
}
