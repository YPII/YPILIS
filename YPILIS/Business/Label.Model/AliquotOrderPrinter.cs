using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class AliquotOrderPrinter
    {
        private BlockLabelPrinter m_BlockLabelPrinter;
        private CassettePrinter m_CassettePrinter;
        private SlideLabelPrinter m_SlideLabelPrinter;
        private SpecimenLabelPrinter m_SpecimenLabelPrinter;
        private AliquotLabelPrinter m_AliquotLabelPrinter;

        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public AliquotOrderPrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrderCollection = aliquotOrderCollection;
            this.m_AccessionOrder = accessionOrder;
            
            this.m_BlockLabelPrinter = new BlockLabelPrinter(this.m_AliquotOrderCollection, this.m_AccessionOrder);

            Business.Label.Model.CassettePrinterCollection cassettePrinterCollection = new CassettePrinterCollection();
            this.m_CassettePrinter = cassettePrinterCollection.GetPrinter(Business.User.UserPreferenceInstance.Instance.UserPreference.CassettePrinter);

            this.m_SlideLabelPrinter = new SlideLabelPrinter(this.m_AliquotOrderCollection, this.m_AccessionOrder);
            this.m_SpecimenLabelPrinter = new SpecimenLabelPrinter(this.m_AliquotOrderCollection, this.m_AccessionOrder);
            this.m_AliquotLabelPrinter = new AliquotLabelPrinter(this.m_AliquotOrderCollection, this.m_AccessionOrder);
        }

        public void Print()
        {
            if (this.m_BlockLabelPrinter.HasItemsToPrint() == true) this.m_BlockLabelPrinter.Print();
            this.m_CassettePrinter.Print(this.m_AliquotOrderCollection, this.m_AccessionOrder);
            if (this.m_SlideLabelPrinter.HasItemsToPrint() == true) this.m_SlideLabelPrinter.Print();
            if (this.m_SpecimenLabelPrinter.HasItemsToPrint() == true) this.m_SpecimenLabelPrinter.Print();
            if (this.m_AliquotLabelPrinter.HasItemsToPrint() == true) this.m_AliquotLabelPrinter.Print();
        }        
    }
}
