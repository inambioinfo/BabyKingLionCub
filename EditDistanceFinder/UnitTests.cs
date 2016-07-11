using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace EditDistanceFinder
{
    class UnitTests 
    {
        public static List<InputListItems> inputs = new List<InputListItems>();

        [Test]
        public static void testLevenshteinDistanceCalculator()// tests to see that calculator is giving correct values
        {
            int actualDistance = 1; // the peptides below are one edit distance apart
            string stringOne = "LKAEEQAADQVAYQQAVQAIKDKQFVLEADQVIFKR";
            string stringTwo = "LKAEEQAADQVAYQQAVQAIKDKQFVLEADAVIFKR";
            int distanceCalculated = ComputeLevenshteinDistance.LevenshteinDistance(stringOne, stringTwo);
            Assert.That(distanceCalculated, Is.EqualTo(actualDistance));
        }

        [Test]
        public static void testLevenshteinDistanceCalculatorForAsterick()// tests to see that calculator can handle asterisk
        {
            int actualDistance = 2; // the peptides below have an edit distance of two
            string stringOne = "LKAEEQAADQVAYQQAVQAIKDKQFVLEADQVI*FKR";
            string stringTwo = "LKAEEQAADQVAYQQAVQAIKDKQFVLEADAVIKFKR";
            int distanceCalculated = ComputeLevenshteinDistance.LevenshteinDistance(stringOne, stringTwo);
            Assert.That(distanceCalculated, Is.EqualTo(actualDistance));
        }

        [Test]
        public static void testLevenshteinDistanceCalculatorForNonAlpha()//tests to see that calculator can handle non-alpha values
        {
            int actualDistance = 3; // the peptides below have an edit distance of three
            string stringOne = "LKAEEQAADQVA*QQAVQAIKDKQFVLEADQVI!FKR";
            string stringTwo = "LKAEEQAADQVAYQQAVQAIKDKQFVLEADAVIKFKR";
            int distanceCalculated = ComputeLevenshteinDistance.LevenshteinDistance(stringOne, stringTwo);
            Assert.That(distanceCalculated, Is.EqualTo(actualDistance));
        }

        //[Test]
        //public static void testDatabaseInsertion() 
        //{
        //    SQLiteConnector.GetItemsFromDatabase();
        //    // need to call the database MatchingPairsDB.db3 to ensure a specific value is retrieved
        //}
    }
}
