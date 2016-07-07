using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTDBFramework.Data;

namespace EditDistanceFinder
{
    public class PrSm
    {
        private short _charge;
        private double _qValue;
        private int _scan;
        private double _score;
        private string _sequenceText;

        //Property accessors
        public short Charge { get { return _charge; } }
        public double QValue { get { return _qValue; } }
        public int Scan { get { return _scan; } }
        public double Score { get { return _score; } }
        public string SequenceText { get { return _sequenceText; } }

        //constructor
        public PrSm(Evidence evidence)
        { // if Evidence is null then throw error - null pointer exception
            this._charge = evidence.Charge;
            this._qValue = ((MsgfPlusResult) evidence).QValue; // casting evidence as msgplusresult
            this._scan = evidence.Scan;
            this._score = ((MsgfPlusResult) evidence).SpecEValue;
            this._sequenceText = evidence.SeqWithNumericMods.Substring(2, evidence.SeqWithNumericMods.Length - 4);
        }
    }
}
