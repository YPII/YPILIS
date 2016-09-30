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
        

        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;        
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

        public AliquotOrderPrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrderCollection = aliquotOrderCollection;
            this.m_AccessionOrder = accessionOrder;
            
            this.m_BlockLabelPrinter = new BlockLabelPrinter(this.m_AliquotOrderCollection, this.m_AccessionOrder);
            this.m_CassettePrinter = new CassettePrinter(this.m_AliquotOrderCollection, this.m_AccessionOrder);
            this.m_SlideLabelPrinter = new SlideLabelPrinter(this.m_AliquotOrderCollection, this.m_AccessionOrder);
            this.m_SpecimenLabelPrinter = new SpecimenLabelPrinter(this.m_AliquotOrderCollection, this.m_AccessionOrder);
        }

        public void Print()
        {
            if (this.m_BlockLabelPrinter.HasItemsToPrint() == true) this.m_BlockLabelPrinter.Print();
            if (this.m_CassettePrinter.HasItemsToPrint() == true) this.m_CassettePrinter.Print();
            if (this.m_SlideLabelPrinter.HasItemsToPrint() == true) this.m_SlideLabelPrinter.Print();
            if (this.m_SpecimenLabelPrinter.HasItemsToPrint() == true) this.m_SpecimenLabelPrinter.Print();
        }

        public bool HasCassettesToPrint()
        {
            bool result = false;
            if (this.m_CassettePrinter.HasItemsToPrint() == true) result = true;
            return result;
        }
    }
}
