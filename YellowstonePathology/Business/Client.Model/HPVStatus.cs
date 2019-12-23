using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Client.Model
{
    public class HPVStatus
    {
        private string m_MasterAccessionNo;
        private string m_HPVStandingOrderCode;
        private string m_HPV1618StandingOrderCode;
        private string m_ClientName;
        private string m_PhysicianName;
        private bool m_HPVRequired;
        private bool m_HPVOrdered;
        private bool m_HPV1618Required;
        private bool m_HPV1618Ordered;

        public HPVStatus()
        { }

        public HPVStatus(HPVStatus input)
        {
            this.m_MasterAccessionNo = input.m_MasterAccessionNo;
            this.m_HPVStandingOrderCode = input.HPVStandingOrderCode;
            this.m_HPV1618StandingOrderCode = input.HPV1618StandingOrderCode;
            this.m_ClientName = input.ClientName;
            this.m_PhysicianName = input.PhysicianName;
            this.m_HPVRequired = input.HPVRequired;
            this.m_HPVOrdered = input.HPVOrdered;
            this.m_HPV1618Required = input.HPV1618Required;
            this.m_HPV1618Ordered = input.HPV1618Ordered;
        }

        [PersistentProperty()]
        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set { this.m_MasterAccessionNo = value; }
        }

        [PersistentProperty()]
        public string HPVStandingOrderCode
        {
            get { return this.m_HPVStandingOrderCode; }
            set { this.m_HPVStandingOrderCode = value; }
        }

        [PersistentProperty()]
        public string HPV1618StandingOrderCode
        {
            get { return this.m_HPV1618StandingOrderCode; }
            set { this.m_HPV1618StandingOrderCode = value; }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set { this.m_ClientName = value; }
        }

        [PersistentProperty()]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set { this.m_PhysicianName = value; }
        }

        public bool HPVRequired
        {
            get { return this.m_HPVRequired; }
            set { this.m_HPVRequired = value; }
        }

        public bool HPVOrdered
        {
            get { return this.m_HPVOrdered; }
            set { this.m_HPVOrdered = value; }
        }

        public bool HPV1618Required
        {
            get { return this.m_HPV1618Required; }
            set { this.m_HPV1618Required = value; }
        }

        public bool HPV1618Ordered
        {
            get { return this.m_HPV1618Ordered; }
            set { this.m_HPV1618Ordered = value; }
        }

        public void SetRequiredAndOrdered(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.SetHPVRequiredAndOrdered(accessionOrder);
            this.SetHPV1618RequiredAndOrdered(accessionOrder);
        }

        private void SetHPVRequiredAndOrdered(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Test.HPV.HPVTest hpvTest = new Business.Test.HPV.HPVTest();
            YellowstonePathology.Business.Client.Model.StandingOrder standingOrder = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(this.HPVStandingOrderCode);
            if (standingOrder.IsRequired(accessionOrder) == true)
            {
                this.m_HPVRequired = true;
            }
            else
            {
                this.m_HPVRequired = false;
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(hpvTest.PanelSetId) == true)
            {
                this.m_HPVOrdered = true;
            }
            else
            {
                this.m_HPVOrdered = false;
            }
        }

        private void SetHPV1618RequiredAndOrdered(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            YellowstonePathology.Business.Test.HPV1618.HPV1618Test hpv1618Test = new Business.Test.HPV1618.HPV1618Test();
            YellowstonePathology.Business.Client.Model.StandingOrder standingOrder = YellowstonePathology.Business.Client.Model.StandingOrderCollection.GetByStandingOrderCode(this.HPV1618StandingOrderCode);
            if (standingOrder.IsRequired(accessionOrder) == true)
            {
                this.m_HPV1618Required = true;
            }
            else
            {
                this.m_HPV1618Required = false;
            }

            if (accessionOrder.PanelSetOrderCollection.Exists(hpv1618Test.PanelSetId) == true)
            {
                this.m_HPV1618Ordered = true;
            }
            else
            {
                this.m_HPV1618Ordered = false;
            }
        }
    }
}
