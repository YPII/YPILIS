using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class AccessionOrderDataSheetDataV2
    {
        private AccessionOrderDataSheetDataAccessionOrder m_AccessionOrderDataSheetDataAccessionOrder;
        private List<AccessionOrderDataSheetDataClientOrder> m_AccessionOrderDataSheetDataClientOrders;
        private List<AccessionOrderDataSheetDataSpecimenOrder> m_AccessionOrderDataSheetDataSpecimenOrders;
        private List<AccessionOrderDataSheetDataCommentLog> m_AccessionOrderDataSheetDataCommentLogs;

        public AccessionOrderDataSheetDataV2(Test.AccessionOrder accessionOrder, ClientOrder.Model.ClientOrderCollection clientOrderCollection, Domain.OrderCommentLogCollection orderCommentLogCollection)
        {
            this.m_AccessionOrderDataSheetDataAccessionOrder = new AccessionOrderDataSheetDataAccessionOrder(accessionOrder, clientOrderCollection);
            this.m_AccessionOrderDataSheetDataClientOrders = new List<AccessionOrderDataSheetDataClientOrder>();
            this.m_AccessionOrderDataSheetDataSpecimenOrders = new List<AccessionOrderDataSheetDataSpecimenOrder>();
            this.m_AccessionOrderDataSheetDataCommentLogs = new List<AccessionOrderDataSheetDataCommentLog>();

            foreach (Business.ClientOrder.Model.ClientOrder clientOrder in clientOrderCollection)
            {
                AccessionOrderDataSheetDataClientOrder accessionOrderDataSheetDataClientOrder = new AccessionOrderDataSheetDataClientOrder(clientOrder);
                this.m_AccessionOrderDataSheetDataClientOrders.Add(accessionOrderDataSheetDataClientOrder);
            }

            foreach (Specimen.Model.SpecimenOrder specimenOrder in accessionOrder.SpecimenOrderCollection)
            {
                AccessionOrderDataSheetDataSpecimenOrder accessionOrderDataSheetDataSpecimenOrder = new AccessionOrderDataSheetDataSpecimenOrder(specimenOrder, clientOrderCollection);
                this.m_AccessionOrderDataSheetDataSpecimenOrders.Add(accessionOrderDataSheetDataSpecimenOrder);
            }

            foreach (Domain.OrderCommentLog orderCommentLog in orderCommentLogCollection)
            {
                AccessionOrderDataSheetDataCommentLog accessionOrderDataSheetDataCommentLog = new AccessionOrderDataSheetDataCommentLog(orderCommentLog);
                this.m_AccessionOrderDataSheetDataCommentLogs.Add(accessionOrderDataSheetDataCommentLog);
            }
        }

        public AccessionOrderDataSheetDataAccessionOrder AccessionOrderDataSheetDataAccessionOrder
        {
            get { return this.m_AccessionOrderDataSheetDataAccessionOrder; }
        }

        public List<AccessionOrderDataSheetDataClientOrder> AccessionOrderDataSheetDataClientOrders
        {
            get { return this.m_AccessionOrderDataSheetDataClientOrders; }
        }

        public List<AccessionOrderDataSheetDataSpecimenOrder> AccessionOrderDataSheetDataSpecimenOrders
        {
            get { return this.m_AccessionOrderDataSheetDataSpecimenOrders; }
        }

        public List<AccessionOrderDataSheetDataCommentLog> AccessionOrderDataSheetDataCommentLogs
        {
            get { return this.m_AccessionOrderDataSheetDataCommentLogs; }
        }
    }
}
