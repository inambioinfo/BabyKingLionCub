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
    public class SQLiteConnectorForSpectrum
    {
        public static void SQLiteConnectorClass()
        {
            //SQLiteConnection.CreateFile("MatchingPairsDB.db3");
            //// Create the file which will be hosting our database
            //using (
            //    System.Data.SQLite.SQLiteConnection con =
            //        new System.Data.SQLite.SQLiteConnection("data source=MatchingPairsDB.db3"))
            //{
            //    using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
            //    {

            //        con.Open(); // Open the connection to the database
            //        Console.WriteLine("DatabaseCreated");
            //        for (int i = 0; i < 7; i++)
            //        {
            //            createTables(tableNameCreation(i), con);
            //        }
            //        con.Close();
            //    }
            //}
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

        public static void Add(SpectrumPair pair, int editDist)// here we will need to add in the SpectrumWithSortedMz instead of the pair, no edit distance
        {
            //Console.WriteLine("The edit distance is: " + editDist);
            //switch (@editDist)
            //{
            //    case 0:
            //        zeroPair.Add(pair);
            //        if (zeroPair.Count >= 100)
            //        {
            //            FillDatabase(tableNameCreation(editDist),
            //                string.Join(",", zeroPair.Select(x => x.toDatabase()).ToArray()));
            //            zeroPair = new List<SpectrumPair>(); // look into reassignment
            //        }
            //        break;
            //    case 1:
            //        onePair.Add(pair);
            //        if (onePair.Count >= 100)
            //        {
            //            FillDatabase(tableNameCreation(editDist),
            //                string.Join(",", onePair.Select(x => x.toDatabase()).ToArray()));
            //            onePair = new List<SpectrumPair>(); // look into reassignment
            //        }
            //        break;
            //    case 2:
            //        twoPair.Add(pair);
            //        if (twoPair.Count >= 100)
            //        {
            //            FillDatabase(tableNameCreation(editDist),
            //                string.Join(",", twoPair.Select(x => x.toDatabase()).ToArray()));
            //            twoPair = new List<SpectrumPair>(); // look into reassignment
            //        }
            //        break;
            //}
        }

        public static void FillDatabase(string tableName, string values)
        {
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=MatchingPairsDB.db3"))
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
            //if (zeroPair.Count > 0)
            //{
            //    FillDatabase(tableNameCreation(0), string.Join(",", zeroPair.Select(x => x.toDatabase()).ToArray()));
            //    zeroPair = new List<SpectrumPair>(); // look into reassignment }
            //}
            //if (onePair.Count > 0)
            //{
            //    FillDatabase(tableNameCreation(1), string.Join(",", onePair.Select(x => x.toDatabase()).ToArray()));
            //    onePair = new List<SpectrumPair>(); // look into reassignment }
            //}
        }
    }
}
