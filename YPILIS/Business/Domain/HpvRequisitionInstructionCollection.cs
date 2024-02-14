using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class HpvRequisitionInstructionCollection : ObservableCollection<HpvRequisitionInstruction>
	{
		public HpvRequisitionInstructionCollection()
		{
            HpvRequisitionInstruction nullValue = new HpvRequisitionInstruction(-1, "Null Value");
            HpvRequisitionInstruction notSpecified = new HpvRequisitionInstruction(0, "Not Specified");
            HpvRequisitionInstruction reflexIfASCUS = new HpvRequisitionInstruction(1, "Reflex If ASCUS");
            HpvRequisitionInstruction routine = new HpvRequisitionInstruction(2, "Routine");
            HpvRequisitionInstruction doNotOrder = new HpvRequisitionInstruction(3, "Do Not Order");
            HpvRequisitionInstruction addOnOrder = new HpvRequisitionInstruction(4, "Add On Order");

            this.Add(nullValue);
            this.Add(notSpecified);
            this.Add(reflexIfASCUS);
            this.Add(routine);
            this.Add(doNotOrder);
            this.Add(addOnOrder);
		}
	}
}
