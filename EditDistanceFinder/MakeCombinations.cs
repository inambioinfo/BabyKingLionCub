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
        public bool filterForPeptideLength(string leftPeptide, string rightPeptide)//if the length of the two peptides is greater than 2, don't bother sending to Lev
        {
            if (Math.Abs(leftPeptide.Length - rightPeptide.Length) < 3) { return true; }
            else return false;
        }
        public bool filterForSameCharge(string leftCharge, string rightCharge)//if the charges are not equal, don't send to Levenshtein
        {
            if (leftCharge == rightCharge) { return true; }
            else return false;
        }
        public bool filterForBeginningOrEndMatch(string leftPeptide, string rightPeptide)//check to see that at least either the first 4 values are the same or that the last 4 are the same
        {
            if (leftPeptide.Length >= 4 && rightPeptide.Length >= 4 && (leftPeptide.Substring(0, 4) == rightPeptide.Substring(0, 4)) || leftPeptide.Substring(leftPeptide.Length - 4, 4) == rightPeptide.Substring(rightPeptide.Length - 4, 4)) { return true; }
            else return false;
        }
        public bool filterForFilenameMatch(string leftCodex, string rightCodex) // determine if the codexes are the same -- later use actual org name, not codex
        {
            if (leftCodex == rightCodex) { return true; }
            else return false;
        }
        public void MakeAllCombinations()
        {
            Stopwatch time = new Stopwatch();
            time.Start();
            ulong count = 0;
            for (int i = 0; i < inputs.Count - 1; i++)// prsmFile used to be a List<PrSmAndFile>
            {
                for (int j = i + 1; j < inputs.Count; j++)
                {
                    count++;
                    if (count % 100000000 == 0) // only output every 100 million lines
                    {
                        Console.WriteLine("Processed: " + count + ", Elapsed Time: " + time.ElapsedMilliseconds);
                    }
                    var left = inputs[i]; // this is one object
                    var right = inputs[j]; // this is the second object for comparison

                    if (filterForPeptideLength(left.peptide, right.peptide) != true) { continue; }
                    if (filterForSameCharge(left.charge, right.charge) != true) { continue; }
                    if (filterForBeginningOrEndMatch(left.peptide, right.peptide) != true) { continue; }
                    if (filterForFilenameMatch(left.codex, right.codex) != true) { continue; }//later use actual org name, not codex

                    var tableAppendix = ComputeLevenshteinDistance.LevenshteinDistance(left.peptide, right.peptide);
                    string valuesForPairsTable = "('" + left.id + "','" + right.id + "','" + tableAppendix + "')";
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

