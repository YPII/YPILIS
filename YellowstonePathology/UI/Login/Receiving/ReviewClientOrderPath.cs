using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.UI.Login.Receiving
{
    public class ReviewClientOrderPath
    {
        public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
        public event ReturnEventHandler Return;

        public delegate void BackEventHandler(object sender, EventArgs e);
        public event BackEventHandler Back;

        public delegate void FinishEventHandler(object sender, EventArgs e);
        public event FinishEventHandler Finish;

		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		
		private YellowstonePathology.Business.Domain.OrderCommentLogCollection m_OrderCommentLogCollection;
		private YellowstonePathology.Business.Rules.Surgical.PatientRecentAccessions m_PatientRecentAccessions;

        private ClientOrderReceivingHandler m_ClientOrderReceivingHandler;
        private object m_Writer;

		public ReviewClientOrderPath(YellowstonePathology.UI.Navigation.PageNavigator pageNavigator, ClientOrderReceivingHandler clientOrderReceivingHandler, object writer)
		{
			this.m_PageNavigator = pageNavigator;
            this.m_ClientOrderReceivingHandler = clientOrderReceivingHandler;
            this.m_Writer = writer;
            this.m_OrderCommentLogCollection = new Business.Domain.OrderCommentLogCollection();            
		}

        public void Start()
        {            
            this.m_PatientRecentAccessions = new YellowstonePathology.Business.Rules.Surgical.PatientRecentAccessions();
            this.m_PatientRecentAccessions.GetPatientRecentAccessionsByLastNameFirstName(this.m_ClientOrderReceivingHandler.ClientOrder.PLastName, this.m_ClientOrderReceivingHandler.ClientOrder.PFirstName);
			this.ShowReviewClientOrderPage(this.m_PatientRecentAccessions);            
        }

		private void ShowReviewClientOrderPage(YellowstonePathology.Business.Rules.Surgical.PatientRecentAccessions patientRecentAccessions)
        {
			ReviewClientOrderPage reviewClientOrderPage = new ReviewClientOrderPage(patientRecentAccessions, this.m_ClientOrderReceivingHandler.ClientOrder, this.m_PageNavigator);
			reviewClientOrderPage.Back += new ReviewClientOrderPage.BackEventHandler(ReviewClientOrderPage_Back);
			reviewClientOrderPage.ViewAccessionOrder += new ReviewClientOrderPage.ViewAccessionOrderEventHandler(ReviewClientOrderPage_ViewAccessionOrder);
			reviewClientOrderPage.CreateNewAccessionOrder += new ReviewClientOrderPage.CreateNewAccessionEventHandler(ReviewClientOrderPage_CreateNewAccessionOrder);
            reviewClientOrderPage.Next += new ReviewClientOrderPage.NextEventHandler(ReviewClientOrderPage_Next);
			this.m_PageNavigator.Navigate(reviewClientOrderPage);            
        }

        private void ReviewClientOrderPage_Next(object sender, EventArgs e)
        {
            if (this.m_ClientOrderReceivingHandler.AccessionOrder != null)
            {
                if (this.m_ClientOrderReceivingHandler.AccessionOrder.MasterAccessionNo != this.m_ClientOrderReceivingHandler.ClientOrder.MasterAccessionNo)
                {
                    this.m_ClientOrderReceivingHandler.UseThisMasterAccessionNoToGetTheAccessionOrder(this.m_ClientOrderReceivingHandler.ClientOrder.MasterAccessionNo);
                }
            }
            else
            {
                this.m_ClientOrderReceivingHandler.UseThisMasterAccessionNoToGetTheAccessionOrder(this.m_ClientOrderReceivingHandler.ClientOrder.MasterAccessionNo);
            }

            this.m_ClientOrderReceivingHandler.AccessionClientOrder();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
            this.StartAccessionOrderPath();
        }

		private void ReviewClientOrderPage_Back(object sender, EventArgs e)
		{
			if (this.Back != null) this.Back(this, e);
		}

		private void ReviewClientOrderPage_CreateNewAccessionOrder(object sender, EventArgs e)
        {
			if (this.m_ClientOrderReceivingHandler.AnAccessionOrderHasBeenAquired == false)
			{
				this.m_ClientOrderReceivingHandler.CreateNewAccessionOrder(Business.Test.AccessionTypeEnum.Surgical);
				this.m_ClientOrderReceivingHandler.AccessionClientOrder();
				this.SendAcknowledgements();
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
			}
			else
			{
				System.Windows.MessageBox.Show("The current order has been accessioned.  The Master Accession Number is " + this.m_ClientOrderReceivingHandler.AccessionOrder.MasterAccessionNo);
			}
        }

		private void ReviewClientOrderPage_ViewAccessionOrder(object sender, CustomEventArgs.MasterAccessionNoReturnEventArgs e)
        {
            this.ShowViewAccessionPage(e.MasterAccessionNo);
        }

        private void ShowViewAccessionPage(string masterAccessionNo)
        {
			YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(masterAccessionNo, this.m_Writer);
            ViewAccessionOrderPage viewAccessionOrderPage = new ViewAccessionOrderPage(accessionOrder);
            viewAccessionOrderPage.UseThisAccessionOrder += new ViewAccessionOrderPage.UseThisAccessionOrderEventHandler(ViewAccessionOrderPage_UseThisAccessionOrder);
            viewAccessionOrderPage.Back += new ViewAccessionOrderPage.BackEventHandler(ViewAccessionOrderPage_Back);
            this.m_PageNavigator.Navigate(viewAccessionOrderPage);
        }

        private void ViewAccessionOrderPage_Back(object sender, EventArgs e)
        {
			this.ShowReviewClientOrderPage(this.m_PatientRecentAccessions);
        }

        private void ViewAccessionOrderPage_UseThisAccessionOrder(object sender, CustomEventArgs.AccessionOrderReturnEventArgs e)
        {
            this.m_ClientOrderReceivingHandler.UseThisMasterAccessionNoToGetTheAccessionOrder(e.AccessionOrder.MasterAccessionNo);
            this.m_ClientOrderReceivingHandler.AccessionClientOrder();
            this.SendAcknowledgements();  
        }

        private void SendAcknowledgements()
        {			
            if (this.m_ClientOrderReceivingHandler.ClientOrder.SystemInitiatingOrder == "EPIC")
            {
                if (this.m_ClientOrderReceivingHandler.ClientOrder.Acknowledged == false)
                {
                    YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection universalServiceIdCollection = YellowstonePathology.Business.ClientOrder.Model.UniversalServiceCollection.GetAll();
                    YellowstonePathology.Business.ClientOrder.Model.UniversalService universalServiceId = universalServiceIdCollection.GetByUniversalServiceId(this.m_ClientOrderReceivingHandler.ClientOrder.UniversalServiceId);

                    YellowstonePathology.Business.HL7View.EPIC.EPICStatusMessage statusMessage = new Business.HL7View.EPIC.EPICStatusMessage(this.m_ClientOrderReceivingHandler.ClientOrder, YellowstonePathology.Business.HL7View.OrderStatusEnum.InProcess, universalServiceId);
					YellowstonePathology.Business.Rules.MethodResult result = statusMessage.Send();

					if (result.Success == false)
					{
                        YellowstonePathology.Business.Logging.EmailExceptionHandler.HandleException(result.Message);                        
					}
					else
					{
                        this.m_ClientOrderReceivingHandler.ClientOrder.Acknowledged = true;
                        this.m_ClientOrderReceivingHandler.ClientOrder.AcknowledgedById = YellowstonePathology.Business.User.SystemIdentity.Instance.User.UserId;
                        this.m_ClientOrderReceivingHandler.ClientOrder.AcknowledgedDate = DateTime.Now;
					}
                }
            }

            this.m_ClientOrderReceivingHandler.Save(false);
            this.StartAccessionOrderPath();
        }

		private void StartAccessionOrderPath()
		{
            this.m_ClientOrderReceivingHandler.Save(false);
			AccessionOrderPath accessionOrderPath = new AccessionOrderPath(this.m_ClientOrderReceivingHandler, this.m_PageNavigator, PageNavigationModeEnum.Inline);
			accessionOrderPath.Back += new AccessionOrderPath.BackEventHandler(AccessionOrderPath_Back);
			accessionOrderPath.Return += new AccessionOrderPath.ReturnEventHandler(AccessionOrderPath_Return);
			accessionOrderPath.Finish += new AccessionOrderPath.FinishEventHandler(AccessionOrderPath_Finish);
			accessionOrderPath.Start();
		}

		private void AccessionOrderPath_Finish(object sender, EventArgs e)
		{
			this.Finish(this, new EventArgs());
		}

		private void AccessionOrderPath_Return(object sender, Navigation.PageNavigationReturnEventArgs e)
		{
			switch (e.PageNavigationDirectionEnum)
			{
				case UI.Navigation.PageNavigationDirectionEnum.Back:
					this.ShowReviewClientOrderPage(this.m_PatientRecentAccessions);
					break;
				case UI.Navigation.PageNavigationDirectionEnum.Finish:
					this.Return(this, e);
					break;
			}
		}

		private void AccessionOrderPath_Back(object sender, EventArgs e)
		{
			this.ShowReviewClientOrderPage(this.m_PatientRecentAccessions);
		}
    }
}
