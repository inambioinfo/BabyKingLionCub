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
        public static List<InputListItems> inputs = new List<InputListItems>();
        public static List<string> aggregateValuesForPairsTable = new List<string>();

        public MakeCombinations(List<InputListItems> inputs)
        {
            MakeCombinations.inputs = inputs; // make combinations of all possible file/scan pairs
            MakeAllCombinations();
        }

        public void MakeAllCombinations()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            int count = 0;
            for (int i = 0; i < inputs.Count - 1; i++)// prsmFile used to be a List<PrSmAndFile>
            {
                for (int j = i + 1; j < inputs.Count; j++)
                {
                    count++;
                    if (count%50000000 == 0) // only output every 50 million lines
                    {
                        Console.WriteLine("Processed: " + count + ", Elapsed Time: " + time.ElapsedMilliseconds);
                    }
                    var left = inputs[i]; // this is one object
                    var right = inputs[j]; // this is the second object for comparison
                    //add a length calculator for the peptides, if difference over 2, don't bother computing Lev distance
                    if (Math.Abs(left.peptide.Length - right.peptide.Length) < 3)
                    {
                        if (left.charge == right.charge)
                        {
                            var tableAppendix = ComputeLevenshteinDistance.LevenshteinDistance(left.peptide,
                                right.peptide);
                            string valuesForPairsTable = "('" + left.id + "','" + right.id + "','" + tableAppendix +
                                                         "')";
                            if (tableAppendix < 3)
                            {
                                aggregateValuesForPairsTable.Add(valuesForPairsTable);
                            }

                            if (aggregateValuesForPairsTable.Count >= 800 && aggregateValuesForPairsTable.Count > 0)
                            {
                                SQLiteConnector.FillDatabase(string.Join(",", aggregateValuesForPairsTable.ToArray()));
                                aggregateValuesForPairsTable = new List<string>();
                            }
                        }
                    }
                }
            }
        }
    }
}
  


