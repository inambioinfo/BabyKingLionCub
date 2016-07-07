using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InformedProteomics.Backend.Data.Spectrometry;

namespace EditDistanceFinder
{
    public class SQLiteConnector
    {
        private static List<SpectrumPair> zeroPair = new List<SpectrumPair>();
        private static List<SpectrumPair> onePair = new List<SpectrumPair>();
        private static List<SpectrumPair> twoPair = new List<SpectrumPair>();
        private static List<SpectrumPair> threePair = new List<SpectrumPair>();
        private static List<SpectrumPair> fourPair = new List<SpectrumPair>();
        private static List<SpectrumPair> fivePair = new List<SpectrumPair>();
        private static List<SpectrumPair> nonPair = new List<SpectrumPair>();

        public static void SQLiteConnectorClass()
        {
            SQLiteConnection.CreateFile("MatchingPairsWithMzList.db3");
                // Create the file which will be hosting our database
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=MatchingPairsWithMzList.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {

                    con.Open(); // Open the connection to the database
                    Console.WriteLine("DatabaseCreated");
                    for (int i = 0; i < 7; i++)
                    {
                        createTables(tableNameCreation(i), con);
                    }
                    con.Close();
                }
            }
        }

        public static string tableNameCreation(int tableAppendix)
        {
            string tableName = "";
            if (tableAppendix == 0)
            {
                tableName = "MatchDataWithEditDistZero";

            }
            else if (tableAppendix == 1)
            {
                tableName = "MatchDataWithEditDistOne";

            }
            else if (tableAppendix == 2)
            {
                tableName = "MatchDataWithEditDistTwo";

            }
            else if (tableAppendix == 3)
            {
                tableName = "MatchDataWithEditDistThree";

            }
            else if (tableAppendix == 4)
            {
                tableName = "MatchDataWithEditDistFour";

            }
            else if (tableAppendix == 5)
            {
                tableName = "MatchDataWithEditDistFive";

            }
            else
            {
                tableName = "NonMatchingPairs";

            }
            return tableName;
        }

        public static void createTables(string tableName, System.Data.SQLite.SQLiteConnection con)
        {
            //using (
                //System.Data.SQLite.SQLiteConnection con =
                //    new System.Data.SQLite.SQLiteConnection("data source=MatchingPairsWithMzList.db3"))
            {
                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    string createTableQuery = @"CREATE TABLE IF NOT EXISTS [" + tableName +
                                              "] ( [ID] INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,[path1] NVARCHAR(2048) NULL,[scan1] NVARCHAR(2048)  NULL,[path2] NVARCHAR(2048) NULL, [scan2] NVARCHAR(2048)  NULL,[MzList1] NVARCHAR(2048)  NULL,[MzList2] NVARCHAR(2048)  NULL,[peptide] NVARCHAR(2048) NULL)";
                    com.CommandText = createTableQuery; // Set CommandText to our query that will create the table
                    com.ExecuteNonQuery();
                }
            }
        }

        public static void Add(SpectrumPair pair, int editDist)
        {
            Console.WriteLine("The edit distance is: "+ editDist);
            switch (@editDist)
            {
                case 0:
                    zeroPair.Add(pair);
                    if (zeroPair.Count >= 100)
                    {
                        FillDatabase(tableNameCreation(editDist),
                            string.Join(",", zeroPair.Select(x => x.toDatabase()).ToArray()));
                        zeroPair = new List<SpectrumPair>(); // look into reassignment
                    }
                    break;
                case 1:
                    onePair.Add(pair);
                    if (onePair.Count >= 100)
                    {
                        FillDatabase(tableNameCreation(editDist),
                            string.Join(",", onePair.Select(x => x.toDatabase()).ToArray()));
                        onePair = new List<SpectrumPair>(); // look into reassignment
                    }
                    break;
                case 2:
                    twoPair.Add(pair);
                    if (twoPair.Count >= 100)
                    {
                        FillDatabase(tableNameCreation(editDist),
                            string.Join(",", twoPair.Select(x => x.toDatabase()).ToArray()));
                        twoPair = new List<SpectrumPair>(); // look into reassignment
                    }
                    break;
                case 3:
                    threePair.Add(pair);
                    if (threePair.Count >= 100)
                    {
                        FillDatabase(tableNameCreation(editDist),
                            string.Join(",", threePair.Select(x => x.toDatabase()).ToArray()));
                        threePair = new List<SpectrumPair>(); // look into reassignment
                    }
                    break;
                case 4:
                    fourPair.Add(pair);
                    if (fourPair.Count >= 100)
                    {
                        FillDatabase(tableNameCreation(editDist),
                            string.Join(",", fourPair.Select(x => x.toDatabase()).ToArray()));
                        fourPair = new List<SpectrumPair>(); // look into reassignment
                    }
                    break;
                case 5:
                    fivePair.Add(pair);
                    if (fivePair.Count >= 100)
                    {
                        FillDatabase(tableNameCreation(editDist),
                            string.Join(",", fivePair.Select(x => x.toDatabase()).ToArray()));
                        fivePair = new List<SpectrumPair>(); // look into reassignment
                    }
                    break;
                default: // this will take anything not listed above
                    nonPair.Add(pair);
                    if (nonPair.Count >= 100)
                    {
                        FillDatabase(tableNameCreation(6),
                            string.Join(",", nonPair.Select(x => x.toDatabase()).ToArray()));
                        nonPair = new List<SpectrumPair>(); // look into reassignment
                    }
                    break;

            }
            //create switch that calls the 
            // call tableNameCreation

        }

        public static void FillDatabase(string tableName, string values)
        {
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=MatchingPairsWithMzList.db3"))
            {
                con.Open();

                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    Console.WriteLine("Inserted into: " + tableName);
                    com.CommandText = "INSERT INTO " + tableName + " (path1,scan1,path2,scan2,MzList1,MzList2,peptide) Values " + values;
                    com.ExecuteNonQuery(); // Execute the query

                }
                con.Close(); // Close the connection to the database
            }
        }

        public static void Finish()
        {
            if (zeroPair.Count > 0)
            {
                FillDatabase(tableNameCreation(0), string.Join(",", zeroPair.Select(x => x.toDatabase()).ToArray()));
                zeroPair = new List<SpectrumPair>(); // look into reassignment }
            }
            if (onePair.Count > 0)
            {
                FillDatabase(tableNameCreation(1), string.Join(",", onePair.Select(x => x.toDatabase()).ToArray()));
                onePair = new List<SpectrumPair>(); // look into reassignment }
            }
            if (twoPair.Count > 0)
            {
                FillDatabase(tableNameCreation(2), string.Join(",", twoPair.Select(x => x.toDatabase()).ToArray()));
                twoPair = new List<SpectrumPair>(); // look into reassignment }
            }
            if (threePair.Count > 0)
            {
                FillDatabase(tableNameCreation(3), string.Join(",", threePair.Select(x => x.toDatabase()).ToArray()));
                threePair = new List<SpectrumPair>(); // look into reassignment }
            }
            if (fourPair.Count > 0)
            {
                FillDatabase(tableNameCreation(4), string.Join(",", fourPair.Select(x => x.toDatabase()).ToArray()));
                fourPair = new List<SpectrumPair>(); // look into reassignment }
            }
            if (fivePair.Count > 0)
            {
                FillDatabase(tableNameCreation(5), string.Join(",", fivePair.Select(x => x.toDatabase()).ToArray()));
                fivePair = new List<SpectrumPair>(); // look into reassignment }
            }
            if (nonPair.Count > 0)
            {
                FillDatabase(tableNameCreation(6), string.Join(",", nonPair.Select(x => x.toDatabase()).ToArray()));
                nonPair = new List<SpectrumPair>(); // look into reassignment }
            }

        }

    }
}
