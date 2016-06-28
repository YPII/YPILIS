using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class WHPHoldList : ObservableCollection<int>
    {
        public WHPHoldList()
        {
            this.Add(1004); //Sironi
			this.Add(3107); //Carlson
			this.Add(617); //Ana Earl
			this.Add(3747); //Chad Abbey
			this.Add(997); //Chimene Dahl
			this.Add(45); //Hal forseth
			this.Add(99); //Doug Neuhoff
			this.Add(3538); //Julianna Papez
			this.Add(3277); //Pam Templeton
			this.Add(690); //Aimee Brown
			this.Add(425); //Kris Miller
			this.Add(2986); //Nero
            this.Add(9); //Ieva bailey
        }

		public bool Exists(int physicianId)
        {
            bool result = false;
			foreach (int i in this)
            {
                if (i == physicianId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
    }
}
