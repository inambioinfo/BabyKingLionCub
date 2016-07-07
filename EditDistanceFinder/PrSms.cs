using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDBFramework.Algorithms;
using MTDBFramework.Data;
using MTDBFramework.IO;

// **** UNSORTED: NEED TO EDIT TO ONLY GRAB SOME EVALUES STILL ****
namespace EditDistanceFinder
{
    public class PrSms
    {  
        private List<PrsmAndFile> prsmAndFile = new List<PrsmAndFile>();

        public PrSms(List<string> filesFromDir, string MzMLdir, string inputDir)//, int inputDirLength
        {
            ParseMzid(filesFromDir, MzMLdir, inputDir);//inputDirLength as well
        }
        public List<PrsmAndFile> prsmFile
        {
            get
            {
            return prsmAndFile;
            }
        }
        private void ParseMzid(List<string> filesFromDir, string MzMLDir, string inputDir)//, int inputDirLength
        {
            

            var options = new Options
            {
                MsgfQValue = 0.01, // can change this to other values, such as 0.01 
                MaxMsgfSpecProb = 1.0,
                //MaxLogEValForMsAlignAlignment = 1e-10, // this option does not seem to actually specify a cutoff for the Evalue.  Program continued grabbing values outside
                //of range of the cutoff 
                //MaxLogEValForXTandemAlignment = 1e-10, // this also did not change the output -- need to find one that does
                TargetFilterType = TargetWorkflowType.BOTTOM_UP
            };

            foreach (string filename in filesFromDir)
            {
                List<PrSm> prsms = new List<PrSm>();
                int filenameLength = filename.Length;
                //add here and mzident goes here
                var mzIdentMlReader = new MTDBFramework.IO.MzIdentMlReader(options);
                var dataSet = mzIdentMlReader.Read(filename);
                var evidences = dataSet.Evidences;
                
                foreach (var evidence in evidences)
                { 
                    prsms.Add(new PrSm(evidence));
                }
                //prsms holds all the values from the whole file, so now send that to make combinations

                Console.WriteLine("the filename for mzml is: "+ filename.Replace(inputDir, MzMLDir).Replace("mzid", "mzML"));
                prsmAndFile.Add( new PrsmAndFile(prsms, filename.Replace(inputDir, MzMLDir).Replace("mzid", "mzML"))); // here strip the .mzid replace with .mzML and strip inputdir and add mzmldir
                // MzMLDir + filename.Remove(0,20).Remove(filename.Length-5,5) + ".mzML")
            }
        }
 
    }
}
