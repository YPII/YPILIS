using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.XPSDocument.Result.Data
{
    public class AccessionOrderDataSheetDataSpecimenOrder
    {
        private string m_Description;
        private string m_CollectionTime;
        private string m_DateReceived;
        private string m_ReceivedIn;
        private string m_ProcessedIn;
        private string m_AccessionTime;
        private string m_Verified;
        private string m_VerifiedById;
        private string m_VerifiedDate;

        private string m_ClientDescription;
        private string m_AccessionedAsDescription;
        private string m_AccessionedAsNumberedDescription;
        private string m_SpecialInstructions;
        private string m_OrderType;
        private string m_CallbackNumber;
        private string m_CollectionDate;
        private string m_SpecimenNumberMatchStatus;
        private string m_SpecimenDescriptionMatchStatus;
        private string m_OrderedBy;
        private string m_OrderTime;
        private string m_Accessioned;

        public AccessionOrderDataSheetDataSpecimenOrder(Specimen.Model.SpecimenOrder specimenOrder, ClientOrder.Model.ClientOrderCollection clientOrderCollection)
        {
            string specimenNumber = specimenOrder.SpecimenNumber.ToString();
            string specimenDescription = specimenOrder.Description;
            this.m_Description = specimenNumber + ". " + specimenDescription;

            this.m_CollectionTime = string.Empty;
            if (specimenOrder.CollectionDate.HasValue == true)
            {
                YellowstonePathology.Business.Helper.DateTimeJoiner dateTimeJoiner = new Business.Helper.DateTimeJoiner(specimenOrder.CollectionDate.Value, specimenOrder.CollectionTime);
                this.m_CollectionTime = dateTimeJoiner.DisplayString;
            }
            else if (specimenOrder.CollectionTime.HasValue == true)
            {
                YellowstonePathology.Business.Helper.DateTimeJoiner dateTimeJoiner = new Business.Helper.DateTimeJoiner(specimenOrder.CollectionTime.Value, specimenOrder.CollectionTime);
                this.m_CollectionTime = dateTimeJoiner.DisplayString;
            }
            
            this.m_DateReceived = Business.Helper.DateTimeExtensions.DateAndTimeStringFromNullable(specimenOrder.DateReceived);                        
            this.m_ReceivedIn = string.IsNullOrEmpty(specimenOrder.ClientFixation) == false ? specimenOrder.ClientFixation : string.Empty;
            this.m_ProcessedIn = string.IsNullOrEmpty(specimenOrder.LabFixation) == false ? specimenOrder.LabFixation : string.Empty;
            this.m_AccessionTime = specimenOrder.AccessionTime.HasValue ? specimenOrder.AccessionTime.Value.ToShortDateString() + " " + specimenOrder.AccessionTime.Value.ToShortTimeString() : string.Empty;
            this.m_Verified = specimenOrder.Verified.ToString();
            this.m_VerifiedById = specimenOrder.VerifiedById.ToString();
            this.m_VerifiedDate = specimenOrder.VerifiedDate.HasValue ? specimenOrder.VerifiedDate.Value.ToShortDateString() + " " + specimenOrder.VerifiedDate.Value.ToShortTimeString() : string.Empty;

            List<ClientOrder.Model.ClientOrderDetail> clientOrderDetails = (from co in clientOrderCollection
                                                                            from cod in co.ClientOrderDetailCollection
                                                                            where cod.ContainerId == specimenOrder.ContainerId
                                                                            select cod).ToList<ClientOrder.Model.ClientOrderDetail>();
            if (clientOrderDetails.Count > 0)
            {
                ClientOrder.Model.ClientOrderDetail clientOrderDetail = clientOrderDetails[0];
                string clientSpecimenNumber = clientOrderDetail.SpecimenNumber.ToString();
                string clientDescription = clientOrderDetail.Description;
                this.m_ClientDescription = clientSpecimenNumber + ". " + clientDescription;

                this.m_AccessionedAsDescription = clientOrderDetail.DescriptionToAccession;
                this.m_AccessionedAsNumberedDescription = clientSpecimenNumber + ". " + this.m_AccessionedAsDescription;
                this.m_SpecialInstructions = string.IsNullOrEmpty(clientOrderDetail.SpecialInstructions) == false ? clientOrderDetail.SpecialInstructions : string.Empty;

                //this.m_SpecialInstructions = clientOrderDetail.OrderType;
                this.m_OrderType = string.IsNullOrEmpty(clientOrderDetail.OrderType) == false ? clientOrderDetail.OrderType : string.Empty;
                this.m_CallbackNumber = string.IsNullOrEmpty(clientOrderDetail.CallbackNumber) == false ? clientOrderDetail.CallbackNumber : string.Empty;

                if (clientOrderDetail.CollectionDate.HasValue)
                {
                    this.m_CollectionDate = clientOrderDetail.CollectionDate.HasValue ? clientOrderDetail.CollectionDate.Value.ToShortDateString() + " " + clientOrderDetail.CollectionDate.Value.ToShortTimeString() : string.Empty;
                }

                this.m_SpecimenNumberMatchStatus = string.IsNullOrEmpty(clientOrderDetail.SpecimenNumberMatchStatus) == false ? clientOrderDetail.SpecimenNumberMatchStatus : string.Empty;
                this.m_SpecimenDescriptionMatchStatus = string.IsNullOrEmpty(clientOrderDetail.SpecimenDescriptionMatchStatus) == false ? clientOrderDetail.SpecimenDescriptionMatchStatus : string.Empty;
                this.m_OrderedBy = clientOrderDetail.OrderedBy;
                this.m_OrderTime = clientOrderDetail.OrderTime.HasValue ? clientOrderDetail.OrderTime.Value.ToShortDateString() + " " + clientOrderDetail.OrderTime.Value.ToShortTimeString() : string.Empty;
                this.m_Accessioned = clientOrderDetail.Accessioned.ToString();
            }
            else
            {
                this.m_Description = "None";
                this.m_AccessionedAsDescription = string.Empty;
                this.m_AccessionedAsNumberedDescription = string.Empty;
                this.m_SpecialInstructions = string.Empty;
                this.m_OrderType = string.Empty;
                this.m_CallbackNumber = string.Empty;
                this.m_CollectionDate = string.Empty;
                this.m_SpecimenNumberMatchStatus = string.Empty;
                this.m_SpecimenDescriptionMatchStatus = string.Empty;
                this.m_OrderedBy = string.Empty;
                this.m_OrderTime = string.Empty;
                this.m_Accessioned = string.Empty;
            }
        }

        public string Description
        {
            get { return this.m_Description; }
        }

        public string CollectionTime
        {
            get { return this.m_CollectionTime; }
        }

        public string DateReceived
        {
            get { return this.m_DateReceived; }
        }

        public string ReceivedIn
        {
            get { return this.m_ReceivedIn; }
        }

        public string ProcessedIn
        {
            get { return this.m_ProcessedIn; }
        }

        public string AccessionTime
        {
            get { return this.m_AccessionTime; }
        }

        public string Verified
        {
            get { return this.m_Verified; }
        }

        public string VerifiedById
        {
            get { return this.m_VerifiedById; }
        }

        public string VerifiedDate
        {
            get { return this.m_VerifiedDate; }
        }

        public string ClientDescription
        {
            get { return this.m_ClientDescription; }
        }

        public string AccessionedAsDescription
        {
            get { return this.m_AccessionedAsDescription; }
        }

        public string AccessionedAsNumberedDescription
        {
            get { return this.m_AccessionedAsNumberedDescription; }
        }

        public string SpecialInstructions
        {
            get { return this.m_SpecialInstructions; }
        }

        public string OrderType
        {
            get { return this.m_OrderType; }
        }

        public string CallbackNumber
        {
            get { return this.m_CallbackNumber; }
        }

        public string CollectionDate
        {
            get { return this.m_CollectionDate; }
        }

        public string SpecimenNumberMatchStatus
        {
            get { return this.m_SpecimenNumberMatchStatus; }
        }

        public string SpecimenDescriptionMatchStatus
        {
            get { return this.m_SpecimenDescriptionMatchStatus; }
        }

        public string OrderedBy
        {
            get { return this.m_OrderedBy; }
        }

        public string OrderTime
        {
            get { return this.m_OrderTime; }
        }

        public string Accessioned
        {
            get { return this.m_Accessioned; }
        }
    }
}
