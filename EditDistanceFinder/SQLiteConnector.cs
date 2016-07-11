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
        //property accessors
        public static List<InputListItems> inputs = new List<InputListItems>();
        public static string inputDir { get; set; }
        
        //constructor
        public SQLiteConnector(ParseCommandLine options)
        {
            inputDir = options.InputDir;
            GetItemsFromDatabase();
        }
     
        public static void GetItemsFromDatabase()
        {   Console.WriteLine("made it to the getItemsFromDatabase");
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=" + inputDir + ".db3"))
            {
                con.Open();

                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {
                    com.CommandText = "SELECT id,peptide FROM Spectrum";
                    using (System.Data.SQLite.SQLiteDataReader reader = com.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            //store id and peptide into an object holding those values
                            //holderForPairs.Add(reader["id"].ToString() + "\t" + reader["peptide"].ToString());
                            inputs.Add(new InputListItems(reader["id"].ToString(), reader["peptide"].ToString()));
                        }
                    }

                }
                con.Close(); // Close the connection to the database
            }
            MakeCombinations something = new MakeCombinations(inputs);
        }

        public static void FillDatabase(string valuesForPairs)
        {
            using (
                System.Data.SQLite.SQLiteConnection con =
                    new System.Data.SQLite.SQLiteConnection("data source=" + inputDir + ".db3"))// you need to add path
            {
                con.Open();

                using (System.Data.SQLite.SQLiteCommand com = new System.Data.SQLite.SQLiteCommand(con))
                {   Console.WriteLine("filling database");
                    com.CommandText = "INSERT INTO Pairs (id_scan1, id_scan2, editDist) Values " + valuesForPairs;
                    com.ExecuteNonQuery(); // Execute the query

                }
                con.Close(); // Close the connection to the database
            }
        }
    }
}
