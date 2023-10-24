using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.PNH
{
	public class PNHCellLine
	{
		string m_Name;
		decimal m_Threshold;
		decimal m_SmallLimit;
        decimal m_RareLimit;
		decimal m_Type1;
		decimal m_Type2;
		decimal m_Type3;

		public PNHCellLine(string name, decimal threshold, decimal rareLimit, decimal smallLimit)
		{
            this.m_Name = name;
            this.m_RareLimit = rareLimit;
			this.m_Threshold = threshold;
			this.m_SmallLimit = smallLimit;
			this.m_Type1 = 0.0m;
			this.m_Type2 = 0.0m;
			this.m_Type3 = 0.0m;
		}

		public string Name
		{
			get { return this.m_Name; }
			set { this.m_Name = value; }
		}

        public decimal RareLimit
        {
            get { return this.m_RareLimit; }
            set { this.m_RareLimit = value; }
        }

        public decimal Threshold
		{
			get { return this.m_Threshold; }
			set { this.m_Threshold = value; }
		}

		public decimal SmallLimit
		{
			get { return this.m_SmallLimit; }
			set { this.m_SmallLimit = value; }
		}

		public decimal CellLineValue
		{
			get { return this.Type2 + this.Type3; }
		}

		public decimal Type1
		{
			get { return this.m_Type1; }
			set { this.m_Type1 = value; }
		}

		public decimal Type2
		{
			get { return this.m_Type2; }
			set { this.m_Type2 = value; }
		}

		public decimal Type3
		{
			get { return this.m_Type3; }
			set { this.m_Type3 = value; }
		}

        public bool IsAboveRare
        {
            get { return this.CellLineValue >= this.RareLimit; }
        }

        public bool IsAboveSmall
        {
            get { return this.CellLineValue >= this.m_SmallLimit; }
        }

        public bool IsAboveThreshold
		{
			get { return this.CellLineValue >= this.Threshold; }
		}		
	}
}
