using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class LabelFactory
    {
        public static Label GetMolecularLabel(LabelFormatEnum labelFormat, string masterAccessionNo, string patientFirstName, string patientLastName, string specimenDescription, 
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet, bool includeTestAbbreviation)
        {
            Label result = null;
            switch (labelFormat)
            {
                case LabelFormatEnum.DYMO:
                    result = new MolecularLabelDymo(masterAccessionNo, patientFirstName, patientLastName, specimenDescription, panelSet, includeTestAbbreviation);
                    break;
                case LabelFormatEnum.ZEBRA:
                    result = new MolecularLabelZebra(masterAccessionNo, patientFirstName, patientLastName, specimenDescription, panelSet);
                    break;
            }
            return result;
        }
    }
}
