using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EditDistanceFinder
{
    public class MakeCombinations
    {
        private List<SpectrumWithSortedMz> prsmFile;


        public MakeCombinations(List<SpectrumWithSortedMz> prsmFile)
        {
            this.prsmFile = prsmFile;
            MakeAllCombinations();
        }

        public void MakeAllCombinations()
        { Stopwatch time = new Stopwatch();
            time.Start();
            int count = 0;
            SQLiteConnector.SQLiteConnectorClass(); // create the datbase to hold the output data
            for (int i = 0; i < prsmFile.Count-1; i++)
            {
                for (int j = i + 1; j < prsmFile.Count; j++)
                {
                    count ++;
                    if (count%1000 == 0) // only output every 1000 lines
                    {
                        Console.WriteLine("Processed: " + count + ", Elapsed Time: " + time.ElapsedMilliseconds);
                    }
                    var left = prsmFile[i]; // this is one spectrum
                    var right = prsmFile[j]; // this is the second spectrum for comparison
                    var tableAppendix = ComputeLevenshteinDistance.LevenshteinDistance(left.peptide, right.peptide); //this will call the calculator and then store to db with org1_org2 table name
                    string[] peptidePair = {left.peptide, right.peptide};
                    SQLiteConnector.Add(new SpectrumPair(left, right, peptidePair), tableAppendix); // here we are adding a new spectrumPair to a table of only that edit distance
                }
            }
            SQLiteConnector.Finish();
        }
    }
}


