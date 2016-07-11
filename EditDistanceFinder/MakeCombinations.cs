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
            MakeCombinations.inputs = inputs;
            MakeAllCombinations();
        }

        public void MakeAllCombinations()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            int count = 0;
            //SQLiteConnector.SQLiteConnectorClass(); // create the datbase to hold the output data
            for (int i = 0; i < inputs.Count - 1; i++)// prsmFile used to be a List<PrSmAndFile>
            {
                for (int j = i + 1; j < inputs.Count; j++)
                {
                    count++;
                    if (count%1000 == 0) // only output every 1000 lines
                    {
                        Console.WriteLine("Processed: " + count + ", Elapsed Time: " + time.ElapsedMilliseconds);
                    }
                    var left = inputs[i]; // this is one object
                    var right = inputs[j]; // this is the second object for comparison
                    var tableAppendix = ComputeLevenshteinDistance.LevenshteinDistance(left.peptide, right.peptide);
                    string valuesForPairsTable = "('" + left.id + "','" + right.id + "','" + tableAppendix + "')";
                    if (tableAppendix <5)
                    {
                        aggregateValuesForPairsTable.Add(valuesForPairsTable);
                    }
                }
            }
            SQLiteConnector.FillDatabase(string.Join(",", aggregateValuesForPairsTable.ToArray()));
            //SQLiteConnector.FillDatabase(string.Join(",", filesFromDir.Select(x => x.toDatabaseFile()).ToArray())aggregateValuesForPairsTable);
            // SQLiteConnector.Finish();
        }
    }
}
  


