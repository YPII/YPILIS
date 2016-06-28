using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login.Receiving
{
	public class ReportOrderPath
	{
		public delegate void FinishEventHandler(object sender, CustomEventArgs.TestOrderInfoEventArgs e);
		public event FinishEventHandler Finish;		

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
		private YellowstonePathology.Business.ClientOrder.Model.ClientOrder m_ClientOrder;
		private YellowstonePathology.UI.Navigation.PageNavigator m_PageNavigator;
		private PageNavigationModeEnum m_PageNavigationMode;
        private YellowstonePathology.Business.Test.TestOrderInfo m_TestOrderInfo;
        private System.Windows.Window m_Writer;

		public ReportOrderPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			PageNavigationModeEnum pageNavigationMode,
            System.Windows.Window writer)
		{
			this.m_AccessionOrder = accessionOrder;						
			this.m_PageNavigator = pageNavigator;
			this.m_PageNavigationMode = pageNavigationMode;
            this.m_Writer = writer;
		}

		public ReportOrderPath(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder,
			YellowstonePathology.UI.Navigation.PageNavigator pageNavigator,
			PageNavigationModeEnum pageNavigationMode,
            System.Windows.Window writer)
		{
			this.m_AccessionOrder = accessionOrder;
			this.m_ClientOrder = clientOrder;			
			this.m_PageNavigator = pageNavigator;
			this.m_PageNavigationMode = pageNavigationMode;
            this.m_Writer = writer;
		}							

		public void Start(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
		{
             
            this.m_TestOrderInfo = testOrderInfo;
            if (this.IsOkToOrder() == true)
            {
                if (this.m_TestOrderInfo.PanelSet.HasNoOrderTarget == false)
                {                    
                    if (this.m_TestOrderInfo.OrderTargetIsKnown == false)
                    {
                        this.m_TestOrderInfo.AttemptSetOrderTarget(this.m_AccessionOrder.SpecimenOrderCollection);
                        if (this.m_TestOrderInfo.OrderTargetIsKnown == false)
                        {
                            this.ShowOrderTargetSelectionPage(this.m_TestOrderInfo);
                        }
                        else
                        {
                            this.OrderTheTest(this.m_TestOrderInfo);
                        }
                    }
                    else
                    {
                        this.OrderTheTest(this.m_TestOrderInfo);
                    }
                }
                else
                {
                    this.OrderTheTest(this.m_TestOrderInfo);
                }
            }            
		}        

		private void ShowOrderTargetSelectionPage(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
		{
            SpecimenSelectionPage specimenSelectionPage = new SpecimenSelectionPage(this.m_AccessionOrder, testOrderInfo);
			specimenSelectionPage.Back += new SpecimenSelectionPage.BackEventHandler(SpecimenSelectionPage_Back);
			specimenSelectionPage.TargetSelected += new SpecimenSelectionPage.TargetSelectedEventHandler(OrderTargetSelectionPage_TargetSelected);
			this.m_PageNavigator.Navigate(specimenSelectionPage);
		}

		private void OrderTargetSelectionPage_TargetSelected(object sender, CustomEventArgs.TestOrderInfoEventArgs e)
		{
			this.OrderTheTest(e.TestOrderInfo);
		}

        private bool ShowResultPage(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
        {
            bool result = false;

            if (testOrderInfo.PanelSet.ShowResultPageOnOrder == true)
            {
                YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_TestOrderInfo.PanelSetOrder;

                YellowstonePathology.UI.Test.ResultPathFactory resultPathFactory = new Test.ResultPathFactory();
                resultPathFactory.Finished += new Test.ResultPathFactory.FinishedEventHandler(ResultPathFactory_Finished);
                resultPathFactory.Start(panelSetOrder, this.m_AccessionOrder, this.m_PageNavigator, this.m_Writer, System.Windows.Visibility.Collapsed);

                result = true;
            }

            return result;
        }

        private void ResultPathFactory_Finished(object sender, EventArgs e)
        {
            if (this.ShowTaskOrderPage(this.m_TestOrderInfo) == false)
            {
                if (this.ShowAdditionalTestingEMailPage() == false)
                {                    
                    CustomEventArgs.TestOrderInfoEventArgs eventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
                    if (this.Finish != null) this.Finish(this, eventArgs);
                }
            }
        }

		private bool ShowTaskOrderPage(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
		{
			bool result = false;

            if (testOrderInfo.PanelSet.TaskCollection.Count != 0)
			{
				YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = this.m_AccessionOrder.CreateTask(testOrderInfo);
                this.m_AccessionOrder.TaskOrderCollection.Add(taskOrder);
                    
				TaskOrderPath taskOrderPath = new TaskOrderPath(this.m_AccessionOrder, taskOrder, this.m_PageNavigator, PageNavigationModeEnum.Inline);
				taskOrderPath.Next += new TaskOrderPath.NextEventHandler(TaskOrderPath_Next);
				taskOrderPath.Start();
				result = true;
			}
			
			return result;
		}

		private void TaskOrderPath_Next(object sender, EventArgs e)
		{
            if (this.m_TestOrderInfo.PanelSetOrder is YellowstonePathology.Business.Interface.ISolidTumorTesting)
            {
                TumorNucleiPercentageEntryPage tumorNucleiPercentagePage = new TumorNucleiPercentageEntryPage((YellowstonePathology.Business.Interface.ISolidTumorTesting)this.m_TestOrderInfo.PanelSetOrder,
                    this.m_AccessionOrder);
                tumorNucleiPercentagePage.Back += new TumorNucleiPercentageEntryPage.BackEventHandler(TumorNucleiPercentagePage_Back);
                tumorNucleiPercentagePage.Next += new TumorNucleiPercentageEntryPage.NextEventHandler(TumorNucleiPercentagePage_Next);
                this.m_PageNavigator.Navigate(tumorNucleiPercentagePage);
            }
            else if (this.ShowAdditionalTestingEMailPage() == false)
            {                
                CustomEventArgs.TestOrderInfoEventArgs eventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
                if (this.Finish != null) this.Finish(this, eventArgs);
            }
		}

        private void TumorNucleiPercentagePage_Next(object sender, EventArgs e)
        {
            if (this.ShowAdditionalTestingEMailPage() == false)
            {             
                CustomEventArgs.TestOrderInfoEventArgs eventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
                if (this.Finish != null) this.Finish(this, eventArgs);
            }
        }

        private void TumorNucleiPercentagePage_Back(object sender, EventArgs e)
        {
            this.ShowTaskOrderPage(this.m_TestOrderInfo);
        }

		public void OrderTheTest(YellowstonePathology.Business.Test.TestOrderInfo testOrderInfo)
		{
            YellowstonePathology.Business.Visitor.OrderTestOrderVisitor orderTestOrderVisitor = new Business.Visitor.OrderTestOrderVisitor(testOrderInfo);
            this.m_AccessionOrder.TakeATrip(orderTestOrderVisitor);
            this.m_TestOrderInfo.PanelSetOrder = testOrderInfo.PanelSetOrder;                       

            if (this.ShowResultPage(testOrderInfo) == false)
            {
                if (this.ShowTaskOrderPage(testOrderInfo) == false)
                {
                    if (this.ShowAdditionalTestingEMailPage() == false)
                    {                        
                        CustomEventArgs.TestOrderInfoEventArgs eventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
                        if (this.Finish != null) this.Finish(this, eventArgs);
                    }
                }          
            }                      
		}					

		private void SpecimenSelectionPage_Back(object sender, EventArgs e)
		{            
            CustomEventArgs.TestOrderInfoEventArgs eventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
            if (this.Finish != null) this.Finish(this, eventArgs);
		}									

        private bool IsOkToOrder()
        {
            bool result = true;            

            if (this.m_TestOrderInfo.PanelSet.OrderTargetTypeCollectionRestrictions.Count != 0)
            {
                if (this.m_TestOrderInfo.PanelSet.CanHaveMultipleOrderTargets == false)
                {
                    if (this.m_TestOrderInfo.PanelSet.OrderTargetTypeCollectionRestrictions.Count != 0)
                    {                        
                        if (this.m_AccessionOrder.SpecimenOrderCollection.Exists(this.m_TestOrderInfo.PanelSet.OrderTargetTypeCollectionRestrictions) == false)
                        {
                            System.Windows.MessageBox.Show("Cannot order this test because the correct order target cannot be found.");
                            result = false;
                        }                     
                    }
                }                
            }

            if (this.m_TestOrderInfo.PanelSet.AllowMultiplePerAccession == false)
            {
                if (this.m_AccessionOrder.PanelSetOrderCollection.Exists(this.m_TestOrderInfo.PanelSet.PanelSetId) == true)
                {
                    System.Windows.MessageBox.Show("Only one " + this.m_TestOrderInfo.PanelSet.PanelSetName + " can be ordered and one already exists.");
                    result = false;
                }
            }            

            if(this.m_AccessionOrder.ClientId == 278 && this.m_TestOrderInfo.PanelSet.PanelSetId == 13) //St. James Healthcare - Pathology
            {                
                System.Windows.MessageBoxResult messageBoxResultShannon = System.Windows.MessageBox.Show("If this is a Dr. Shannon case you may need to order an ER/PR.  Are you sure you want to order Surgical Pathology?", "Order Warning!", System.Windows.MessageBoxButton.YesNo, System.Windows.MessageBoxImage.Question, System.Windows.MessageBoxResult.No);
                if (messageBoxResultShannon == System.Windows.MessageBoxResult.No)
                {
                    result = false;
                }                
            }                       

            return result;
        }							   		    		       

        private bool ShowAdditionalTestingEMailPage()
        {
            bool result = false;
            if (this.m_TestOrderInfo.PanelSet.HasTechnicalComponent == true)
            {
                if (this.m_TestOrderInfo.PanelSet.TechnicalComponentFacility.GetType() != typeof(YellowstonePathology.Business.Facility.Model.YellowstonePathologyInstituteBillings))
                {
                    if (this.m_AccessionOrder.PanelSetOrderCollection.Count > 1 && this.m_AccessionOrder.PhysicianId != 0)
                    {
                        result = true;
                        AdditionalTestingEMailPage additionalTestingEMailPage = new AdditionalTestingEMailPage(this.m_TestOrderInfo.PanelSetOrder, this.m_AccessionOrder);
                        additionalTestingEMailPage.Next += AdditionalTestingEMailPage_Next;
                        additionalTestingEMailPage.Back += AdditionalTestingEMailPage_Back;
                        this.m_PageNavigator.Navigate(additionalTestingEMailPage);
                    }
                }
            }
            return result;
        }

        private void AdditionalTestingEMailPage_Next(object sender, EventArgs e)
        {            
            CustomEventArgs.TestOrderInfoEventArgs eventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
            if (this.Finish != null) this.Finish(this, eventArgs);
        }

        private void AdditionalTestingEMailPage_Back(object sender, EventArgs e)
        {         
            CustomEventArgs.TestOrderInfoEventArgs eventArgs = new CustomEventArgs.TestOrderInfoEventArgs(this.m_TestOrderInfo);
            if (this.Finish != null) this.Finish(this, eventArgs);
        }
    }
}