﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.UI.Login.FinalizeAccession
{	
    public partial class AliquotAndStainOrderPage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		public delegate void ReturnEventHandler(object sender, UI.Navigation.PageNavigationReturnEventArgs e);
		public event ReturnEventHandler Return;

        public delegate void ShowSpecimenMappingPageEventHandler(object sender, EventArgs e);
        public event ShowSpecimenMappingPageEventHandler ShowSpecimenMappingPage;

		public delegate void ShowTaskOrderPageEventHandler(object sender, CustomEventArgs.AcknowledgeStainOrderEventArgs e);
		public event ShowTaskOrderPageEventHandler ShowTaskOrderPage;

		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;		
		private string m_PageHeaderText = "Aliquots and Stains";

        private Nullable<int> m_Aliquots;
        private YellowstonePathology.Business.Specimen.Model.Aliquot m_Aliquot;
        private Nullable<int> m_PassNumber;
		private object m_Test;        

		private ObservableCollection<object> m_TestCollection;

		private YellowstonePathology.Business.Visitor.StainAcknowledgementTaskOrderVisitor m_StainAcknowledgementTaskOrderVisitor;

        private AliquotAndStainOrderView m_AliquotAndStainOrderView;        
		private YellowstonePathology.Business.Test.PanelSetOrder m_PanelSetOrder;

        private YellowstonePathology.Business.Specimen.Model.EmbeddingInstructionList m_EmbeddingInstructionList;

		public AliquotAndStainOrderPage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder,
			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder)
		{
            this.m_AccessionOrder = accessionOrder;
			this.m_PanelSetOrder = panelSetOrder;

            this.m_EmbeddingInstructionList = new Business.Specimen.Model.EmbeddingInstructionList();

			this.m_AliquotAndStainOrderView = new AliquotAndStainOrderView(accessionOrder, panelSetOrder);			
			
			this.m_PageHeaderText = this.m_AccessionOrder.MasterAccessionNo + ": " + this.m_AccessionOrder.PFirstName + " " + this.m_AccessionOrder.PLastName;

			this.m_StainAcknowledgementTaskOrderVisitor = new Business.Visitor.StainAcknowledgementTaskOrderVisitor(this.m_PanelSetOrder);

			InitializeComponent();

			this.DataContext = this;
			this.Loaded += new RoutedEventHandler(StainOrderPage_Loaded);
            Unloaded += AliquotAndStainOrderPage_Unloaded;
		}

        private void StainOrderPage_Loaded(object sender, RoutedEventArgs e)
        {             
            int selectedIndex = -1;
            foreach (YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder in this.ListBoxPanelSetOrders.Items)
            {
                selectedIndex++;
                if (panelSetOrder.ReportNo == this.m_PanelSetOrder.ReportNo)
                {
                    break;
                }
            }
            this.ListBoxPanelSetOrders.SelectedIndex = selectedIndex;
            this.m_AliquotAndStainOrderView.SetSpecimenChecks(true);
        }

        private void AliquotAndStainOrderPage_Unloaded(object sender, RoutedEventArgs e)
        {
            this.m_AliquotAndStainOrderView.SetEmbeddingComments();
        }

        public YellowstonePathology.Business.Specimen.Model.EmbeddingInstructionList EmbeddingInstructionList
        {
            get { return this.m_EmbeddingInstructionList;  }
        }                    

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.Business.Test.PanelSetOrder PanelSetOrder
        {
            get { return this.m_PanelSetOrder; }
        }

        public AliquotAndStainOrderView AliquotAndStainOrderView
        {
            get { return this.m_AliquotAndStainOrderView; }
        }

        public Nullable<int> PassNumber
        {
            get { return this.m_PassNumber; }
            set { this.m_PassNumber = value; }
        }

		public ObservableCollection<object> TestCollection
        {
            get { return this.m_TestCollection; }
        }        

		public string PageHeaderText
		{
			get { return this.m_PageHeaderText; }
		}        

        public Nullable<int> Aliquots
        {
            get { return this.m_Aliquots; }
            set 
            { 
                this.m_Aliquots = value;
                this.NotifyPropertyChanged("Aliquots");
            }
        }

        public YellowstonePathology.Business.Specimen.Model.Aliquot Aliquot
        {
            get { return this.m_Aliquot; }
            set 
            { 
                this.m_Aliquot = value;
                this.NotifyPropertyChanged("Aliquot");
            }
        }

		public object Test
        {
            get { return this.m_Test; }
            set 
            { 
                this.m_Test = value;
                this.NotifyPropertyChanged("Test");
            }
        }				

		private void ButtonBack_Click(object sender, RoutedEventArgs e)
		{
			if (this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderStainAcknowlegedment != null)
			{
				if (this.ShowTaskOrderPage != null)
				{
					CustomEventArgs.AcknowledgeStainOrderEventArgs args = new CustomEventArgs.AcknowledgeStainOrderEventArgs(this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderStainAcknowlegedment);
					this.ShowTaskOrderPage(this, args);
				}
			}
			else
			{
				UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Back, this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderStainAcknowlegedment);
				this.Return(this, args);
			}
		}

		private void ButtonNext_Click(object sender, RoutedEventArgs e)
		{
			YellowstonePathology.Business.Audit.Model.AliquotAndStainOrderAuditCollection aliquotAndStainOrderAuditCollection = new YellowstonePathology.Business.Audit.Model.AliquotAndStainOrderAuditCollection(this.m_AccessionOrder, this.m_AliquotAndStainOrderView.GetAliquotCollection());
			YellowstonePathology.Business.Audit.Model.AuditResult auditResult = aliquotAndStainOrderAuditCollection.Run2();
			
			if(auditResult.Status == Business.Audit.Model.AuditStatusEnum.Failure)
			{
				if(aliquotAndStainOrderAuditCollection.FNAHasIntraOpAudit.Status == Business.Audit.Model.AuditStatusEnum.Failure)
				{
					MessageBoxResult answer = MessageBox.Show(aliquotAndStainOrderAuditCollection.FNAHasIntraOpAudit.Message.ToString() + "  Do you want to continue without ordering.", "Intraoperative Consultation", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
					if(answer == MessageBoxResult.No)
					{
						return;
					}
				}
				else
				{
                    MessageBox.Show(auditResult.Message);
                    return;
				}
			}					                

			if (this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderStainAcknowlegedment != null)
			{
				if (this.ShowTaskOrderPage != null)
				{
                    YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                    CustomEventArgs.AcknowledgeStainOrderEventArgs args = new CustomEventArgs.AcknowledgeStainOrderEventArgs(this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderStainAcknowlegedment);
					this.ShowTaskOrderPage(this, args);
				}
			}
			else
			{
                YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Save();
                UI.Navigation.PageNavigationReturnEventArgs args = new UI.Navigation.PageNavigationReturnEventArgs(UI.Navigation.PageNavigationDirectionEnum.Next, this.m_StainAcknowledgementTaskOrderVisitor.TaskOrderStainAcknowlegedment);
				this.Return(this, args);
			}			
		}        

		private bool TestHasBeenOrdered(string testId)
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this.m_PanelSetOrder.PanelOrderCollection)
			{
				if(panelOrder.TestOrderCollection.ExistsByTestId(testId))
				{
					result = true;
				}
			}
			return result;
		}

		private void ButtonClose_Click(object sender, RoutedEventArgs e)
		{
            Window.GetWindow(this).Close();			
		}		

        private void ButtonNumberPad_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string tag = button.Tag.ToString();
            if (tag == "Clear")
            {
                this.Aliquots = null;
            }
            else
            {
                string aliquotString = this.m_Aliquots.ToString() + tag.ToString();                
                int result = 0;
                if (Int32.TryParse(aliquotString, out result) == true)
                {
                    if (result <= 50)
                    {
                        this.Aliquots = result;
                    }
                    else
                    {
                        MessageBox.Show("The number is to large.");
                        this.Aliquots = null;
                    }
                }                
            }

            this.TextBoxAliquots.Focus();
            this.TextBoxAliquots.SelectAll();
        }

        private void ButtonOrderHEWithSlide_Click(object sender, RoutedEventArgs e)
        {
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("49"); //H&E            
        }   

        private void ButtonOrderHEBlock_Click(object sender, RoutedEventArgs e)
        {
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("49"); //H&E
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Block(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);            
        }

        private void ButtonAddHESlide_Click(object sender, RoutedEventArgs e)
        {
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("49"); //H&E
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Block(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);            
        }

        private void ButtonOrderHEBlockPaperLabel_Click(object sender, RoutedEventArgs e)
        {
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("49"); //H&E
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Block(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);            
        }        

		private void ButtonOrderFrozenBlock_Click(object sender, RoutedEventArgs e)
		{
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("45"); //Intraoperative Consultation with Frozen
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.FrozenBlock(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);            
		}

		private void ButtonOrderHECellBlock_Click(object sender, RoutedEventArgs e)
		{
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("49"); //Cell Block
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.CellBlock(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);            
		}

		private void ButtonOrderGrossOnlySpecimen_Click(object sender, RoutedEventArgs e)
		{
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("48"); //Gross Only
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.SpecimenAliquot(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);            
		}

		private void ButtonOrderhPyloriBlock_Click(object sender, RoutedEventArgs e)
		{
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("107"); //Helicobacter pylori			
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Block(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);            
		}

		private void ButtonOrderIronBlock_Click(object sender, RoutedEventArgs e)
		{
			Business.Test.Model.Test test = Business.Test.Model.TestCollectionInstance.GetClone("115"); //Iron
            test.UseWetProtocol = true;
            this.Test = test;
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Block(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);            
		}

		private void ButtonOrderWrightsStainSlide_Click(object sender, RoutedEventArgs e)
		{
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("205"); //Wrights Stain
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Slide(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);            
		}
        
		private void ButtonOrderNonGynSlide_Click(object sender, RoutedEventArgs e)
		{
            this.Test = Business.Test.Model.TestCollectionInstance.GetClone("206"); //NonGyn
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Slide(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);            
		}

		private void ButtonOrderIronSlide_Click(object sender, RoutedEventArgs e)
		{
            Business.Test.Model.Test test = Business.Test.Model.TestCollectionInstance.GetClone("115"); //Iron
            test.UseWetProtocol = true;
            this.Test = test;
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Slide(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);            
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

		private void CheckBoxSpecimen_Checked(object sender, RoutedEventArgs e)
		{
            this.m_AliquotAndStainOrderView.SetSpecimenChecks(true);
		}

		private void CheckBoxSpecimen_Unchecked(object sender, RoutedEventArgs e)
		{
            this.m_AliquotAndStainOrderView.SetSpecimenChecks(false);
		}		

		private void CheckBoxAliquots_Checked(object sender, RoutedEventArgs e)
		{
            this.m_AliquotAndStainOrderView.SetAliquotChecks(true);
		}

		private void CheckBoxAliquots_Unchecked(object sender, RoutedEventArgs e)
		{
            this.m_AliquotAndStainOrderView.SetAliquotChecks(false);
		}
	
		private void CheckBoxTests_Checked(object sender, RoutedEventArgs e)
		{
            this.m_AliquotAndStainOrderView.SetTestChecks(true);
		}

		private void CheckBoxTests_Unchecked(object sender, RoutedEventArgs e)
		{
            this.m_AliquotAndStainOrderView.SetTestChecks(false);
		}        

        private void ButtonOrder_Click(object sender, RoutedEventArgs e)
		{                        
            OrderStain(false);            
		}

        private void OrderStain(bool useWetProtocol)
        {
            if (this.m_PanelSetOrder.PanelSetId == 216)
            {
                MessageBox.Show("Warning: Stains should only be added to Informal Consults when adding client accessioned blocks/slide/stains.");
            }
            if (this.m_PanelSetOrder.PanelSetId == 197)  //Peer Review
            {
                MessageBox.Show("Sorry, I can't let you add stains to a Peer Review.");
                return;
            }

            bool isDualStain = false;
            YellowstonePathology.Business.Test.Model.Test test = null;
            YellowstonePathology.Business.Test.Model.DualStain dualStain = null;

            if (this.m_Test is YellowstonePathology.Business.Test.Model.Test)
            {
                test = (YellowstonePathology.Business.Test.Model.Test)this.m_Test;
            }
            else if (this.m_Test is YellowstonePathology.Business.Test.Model.DualStain)
            {
                dualStain = (YellowstonePathology.Business.Test.Model.DualStain)this.m_Test;
                isDualStain = true;
            }

            if (this.m_Aliquot != null && this.m_Aliquot.AliquotType == "Wash")
            {
                AddWash();
            }

            if (this.m_Aliquots.HasValue == true && this.m_Aliquot != null && this.m_Aliquot.AliquotType == "FNASLD")
            {
                if (this.m_PassNumber.HasValue == true)
                {
                    this.AddFNASlide(true, this.m_PassNumber.Value);
                }
                else
                {
                    this.AddFNASlide(false, 0);
                }
            }
            else if (this.m_Aliquots.HasValue == true && this.m_Aliquot != null && this.m_Aliquot.AliquotType == "NGYNSLD")
            {
                this.AddNGYNSlide();
            }
            else if (this.m_Aliquots.HasValue == true && this.m_Aliquot != null && this.m_Aliquot.AliquotType == "CESLD")
            {
                this.AddCESlide();
            }
            else if (this.m_Aliquots.HasValue == true && this.m_Aliquot != null && this.m_Aliquot.AliquotType == "CSSLD")
            {
                this.AddCSSlide();
            }
            else if (this.m_Aliquots.HasValue)
            {
                if (this.m_Aliquot != null)
                {
                    if (this.Test != null)
                    {
                        if (isDualStain == false)
                        {
                            this.OrderAliquotsAndTestOnSelectedSpecimen(test, isDualStain);
                        }
                        else
                        {
                            this.OrderAliquotsAndTestOnSelectedSpecimen(dualStain.FirstTest, isDualStain);
                            this.OrderAliquotsAndTestOnSelectedSpecimen(dualStain.SecondTest, isDualStain);
                        }
                    }
                }
            }
            else if (this.Test != null)
            {
                if (isDualStain == false)
                {
                    this.OrderTestOnSelectedAliquots(test, isDualStain);
                }
                else
                {
                    this.OrderTestOnSelectedAliquots(dualStain.FirstTest, isDualStain);
                    this.OrderTestOnSelectedAliquots(dualStain.SecondTest, isDualStain);
                }
            }

            this.m_AccessionOrder.TakeATrip(this.m_StainAcknowledgementTaskOrderVisitor);
            this.m_AliquotAndStainOrderView.Refresh(true, this.m_PanelSetOrder);
            this.NotifyPropertyChanged("AliquotAndStainOrderView");
            this.Aliquots = null;
            this.Test = null;
            this.Aliquot = null;
        }

        private void AddFNASlide(bool isPass, int passNumber)
        {
			YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection selectedSpecimen = this.m_AliquotAndStainOrderView.GetSelectedSpecimen();
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in selectedSpecimen)
            {
                int slideNumber = 0;                
                for (int i = 0; i < this.m_Aliquots; i++)
                {
                    slideNumber += 1;
                    if (isPass == true)
                    {
                        specimenOrder.AliquotOrderCollection.AddFNASlide(specimenOrder, passNumber, slideNumber, this.m_AccessionOrder.AccessionDate.Value);
                    }
                    else
                    {
                        specimenOrder.AliquotOrderCollection.AddFNASlide(specimenOrder, slideNumber, this.m_AccessionOrder.AccessionDate.Value);
                    }
                }                
            }
        }

        private void AddWash()
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection selectedSpecimen = this.m_AliquotAndStainOrderView.GetSelectedSpecimen();
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in selectedSpecimen)
            {                
                specimenOrder.AliquotOrderCollection.AddWash(specimenOrder, this.m_AccessionOrder.AccessionDate.Value);                
            }
        }

        private void AddNGYNSlide()
        {
			YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection selectedSpecimen = this.m_AliquotAndStainOrderView.GetSelectedSpecimen();
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in selectedSpecimen)
            {                
                for (int i = 0; i < this.m_Aliquots; i++)
                {
                    specimenOrder.AliquotOrderCollection.AddNGYNSlide(specimenOrder, this.m_AccessionOrder.AccessionDate.Value);             
                }
            }
        }

        private void AddCESlide()
        {
			YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection selectedSpecimen = this.m_AliquotAndStainOrderView.GetSelectedSpecimen();
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in selectedSpecimen)
            {
                for (int i = 0; i < this.m_Aliquots; i++)
                {
                    specimenOrder.AliquotOrderCollection.AddCESlide(specimenOrder, this.m_AccessionOrder.AccessionDate.Value);
                }
            }
        }

        private void AddCSSlide()
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection selectedSpecimen = this.m_AliquotAndStainOrderView.GetSelectedSpecimen();
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in selectedSpecimen)
            {
                for (int i = 0; i < this.m_Aliquots; i++)
                {
                    specimenOrder.AliquotOrderCollection.AddCSSlide(specimenOrder, this.m_AccessionOrder.AccessionDate.Value);

                    string taskOrderId = Business.OrderIdParser.GetNextTaskOrderId(this.m_AccessionOrder.TaskOrderCollection, this.m_AccessionOrder.MasterAccessionNo);
                    string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();

                    Business.Task.Model.Task task = new Business.Task.Model.Task(YellowstonePathology.Business.Task.Model.TaskAssignment.Cytology, "Perform Cytospin.");

                    YellowstonePathology.Business.Task.Model.TaskOrder taskOrder = null;
                    if (this.m_AccessionOrder.TaskOrderCollection.ReportNoExists(this.m_PanelSetOrder.ReportNo) == false)
                    {
                        taskOrder = new Business.Task.Model.TaskOrder(taskOrderId, objectId, this.m_AccessionOrder.MasterAccessionNo,
                        this.m_PanelSetOrder.ReportNo, task, specimenOrder, "Cytospin", YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.Immediate);
                        this.m_AccessionOrder.TaskOrderCollection.Add(taskOrder);
                    }
                    else
                    {
                        taskOrder = this.m_AccessionOrder.TaskOrderCollection.GetTaskOrderByReportNo(this.m_PanelSetOrder.ReportNo);
                    }
                                        
                    string taskOrderDetailId = Business.OrderIdParser.GetNextTaskOrderDetailId(taskOrder.TaskOrderDetailCollection, taskOrder.TaskOrderId);                    
                    YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail = new Business.Task.Model.TaskOrderDetail(taskOrderDetailId, taskOrder.TaskOrderId, objectId, task, this.m_AccessionOrder.ClientId);
                    taskOrder.TaskOrderDetailCollection.Add(taskOrderDetail);                    
                }
            }
        }

        private void ButtonPutBlockOnHold_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots = this.m_AliquotAndStainOrderView.GetSelectedAliquots();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in selectedAliquots)
            {
                if (aliquotOrder.Status == "Hold")
                {
                    aliquotOrder.Status = "Created";
                    aliquotOrder.ReadyToProcess = true;
                    aliquotOrder.ProcessorStartDate = DateTime.Now;
                }
                else
                {
                    aliquotOrder.Status = "Hold";
                    aliquotOrder.ReadyToProcess = false;
                    aliquotOrder.ProcessorStartDate = null;
                }
            }

            this.m_AliquotAndStainOrderView.Refresh(true, this.m_PanelSetOrder);
            this.NotifyPropertyChanged("AliquotAndStainOrderView");
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
		{
            List<string> selectedSlideOrderIds = this.m_AliquotAndStainOrderView.GetSelectedSlideOrderIds();
            foreach (string slideOrderId in selectedSlideOrderIds)
            {
                YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSlideOrder(slideOrderId);
                YellowstonePathology.Business.Visitor.RemoveSlideOrderVisitor removeSlideOrderVisitor = new Business.Visitor.RemoveSlideOrderVisitor(slideOrder);
                this.m_AccessionOrder.TakeATrip(removeSlideOrderVisitor);
            }

            YellowstonePathology.Business.Test.Model.TestOrderCollection selectedTestOrders = this.m_AliquotAndStainOrderView.GetSelectedTestOrders();
            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in selectedTestOrders)
            {
                YellowstonePathology.Business.Visitor.RemoveTestOrderVisitor removeTestOrderVisitor = new Business.Visitor.RemoveTestOrderVisitor(testOrder.TestOrderId);
                this.m_AccessionOrder.TakeATrip(removeTestOrderVisitor);

				YellowstonePathology.Business.Test.Model.Test test = Business.Test.Model.TestCollectionInstance.GetClone(testOrder.TestId);
				if (test.NeedsAcknowledgement == true)
				{
					this.m_StainAcknowledgementTaskOrderVisitor.RemoveTestOrder(testOrder);
				}
			}
			this.m_AccessionOrder.TakeATrip(this.m_StainAcknowledgementTaskOrderVisitor);

            YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots = this.m_AliquotAndStainOrderView.GetSelectedAliquots();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in selectedAliquots)
            {
                YellowstonePathology.Business.Visitor.RemoveAliquotOrderVisitor removeAliquotOrderVisitor = new Business.Visitor.RemoveAliquotOrderVisitor(aliquotOrder);
                this.m_AccessionOrder.TakeATrip(removeAliquotOrderVisitor);
                this.m_AccessionOrder.SpecimenOrderCollection.SetAliquotRequestCount();
            }
            
            this.m_AliquotAndStainOrderView.Refresh(true, this.m_PanelSetOrder);            
			this.NotifyPropertyChanged("AliquotAndStainOrderView");
		}        		
		
		private void OrderAliquotsAndTestOnSelectedSpecimen(YellowstonePathology.Business.Test.Model.Test test, bool orderedAsDual)
		{
			YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection selectedSpecimen = this.m_AliquotAndStainOrderView.GetSelectedSpecimen();
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in selectedSpecimen)
			{				
				for (int i = 0; i < this.m_Aliquots.Value; i++)
				{
					YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = null;

					switch (this.m_Aliquot.AliquotType)
					{
						case "Block":
                            aliquotOrder = specimenOrder.AliquotOrderCollection.AddBlock(specimenOrder, this.m_Aliquot.IdentificationType, this.m_AccessionOrder.AccessionDate.Value);
							break;
						case "FrozenBlock":
                            aliquotOrder = specimenOrder.AliquotOrderCollection.AddFrozenBlock(specimenOrder, this.m_Aliquot.IdentificationType, this.m_AccessionOrder.AccessionDate.Value);
							break;
						case "CellBlock":
                            aliquotOrder = specimenOrder.AliquotOrderCollection.AddCellBlock(specimenOrder, this.m_Aliquot.IdentificationType, this.m_AccessionOrder.AccessionDate.Value);
							break;
						case "Slide":
                            aliquotOrder = specimenOrder.AliquotOrderCollection.AddSlide(specimenOrder, this.m_Aliquot.IdentificationType, this.m_AccessionOrder.AccessionDate.Value);
							break;
						case "Specimen":
                            aliquotOrder = specimenOrder.AliquotOrderCollection.AddSpecimen(specimenOrder, this.m_Aliquot.IdentificationType, this.m_AccessionOrder.AccessionDate.Value);
							break;                        
					}								

					specimenOrder.AliquotRequestCount = specimenOrder.AliquotOrderCollection.Count;
					if (this.Test != null)
					{
						YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, null, false, aliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
                        this.m_AccessionOrder.TakeATrip(orderTestVisitor);

						if ((aliquotOrder.AliquotType == "Block" ||
							aliquotOrder.AliquotType == "FrozenBlock" ||
							aliquotOrder.AliquotType == "CellBlock"))
						{
							YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(aliquotOrder, orderTestVisitor.TestOrder);
							this.m_AccessionOrder.TakeATrip(addSlideOrderVisitor);
						}

                        if (test.NeedsAcknowledgement == true)
                        {
                            this.m_StainAcknowledgementTaskOrderVisitor.AddTestOrder(orderTestVisitor.TestOrder);
                        }

                        this.m_AliquotAndStainOrderView = new AliquotAndStainOrderView(this.m_AccessionOrder, this.m_PanelSetOrder);
					}
				}
			}            				
		}

		private void OrderTestOnSelectedAliquots(YellowstonePathology.Business.Test.Model.Test test, bool orderedAsDual)
		{
            YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots = this.m_AliquotAndStainOrderView.GetSelectedAliquots();
            foreach(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in selectedAliquots)
            {
				YellowstonePathology.Business.Visitor.OrderTestVisitor orderTestVisitor = new Business.Visitor.OrderTestVisitor(this.m_PanelSetOrder.ReportNo, test, test.OrderComment, null, orderedAsDual, aliquotOrder, false, false, this.m_AccessionOrder.TaskOrderCollection);
                this.m_AccessionOrder.TakeATrip(orderTestVisitor);

				if ((aliquotOrder.AliquotType == "Block" ||
					aliquotOrder.AliquotType == "FrozenBlock" ||
					aliquotOrder.AliquotType == "CellBlock"))
				{
					YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(aliquotOrder, orderTestVisitor.TestOrder);
					this.m_AccessionOrder.TakeATrip(addSlideOrderVisitor);
				}

                if (test.NeedsAcknowledgement == true)
                {
                    this.m_StainAcknowledgementTaskOrderVisitor.AddTestOrder(orderTestVisitor.TestOrder);
                }
			}            
		}        

		private void HyperlinkTestBeginsWith_Click(object sender, RoutedEventArgs e)
		{			
			Hyperlink hyperlink = (Hyperlink)sender;
			string initial = hyperlink.Tag.ToString();
			
			this.m_TestCollection = Business.Test.Model.TestCollectionInstance.GetTestsStartingWithToObjectCollection(initial, true);            
            this.NotifyPropertyChanged("TestCollection");            
		}                

		private void ButtonAliquotTypeBlock_Click(object sender, RoutedEventArgs e)
		{
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Block(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);
		}

        private void ButtonAliquotTypeFrozen_Click(object sender, RoutedEventArgs e)
        {
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.FrozenBlock(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);
        }

        private void ButtonAliquotTypeCellBlock_Click(object sender, RoutedEventArgs e)
        {
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.CellBlock(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint);
        }

        private void ButtonAliquotTypeSpecimen_Click(object sender, RoutedEventArgs e)
        {
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.SpecimenAliquot(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);
        }

        private void ButtonAliquotTypeWash_Click(object sender, RoutedEventArgs e)
        {
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.WashAliquot(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);
        }

        private void ButtonAliquotTypeSlide_Click(object sender, RoutedEventArgs e)
        {
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.Slide(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);
        }        

        private void ButtonAliquotTypeFNASlide_Click(object sender, RoutedEventArgs e)
        {
            this.Aliquot = new YellowstonePathology.Business.Specimen.Model.FNASlide(YellowstonePathology.Business.Specimen.Model.AliquotLabelType.PaperLabel);
        }                 

        private void HyperlinkTestItem_Click(object sender, RoutedEventArgs e)
        {            
            Hyperlink textBlock = (Hyperlink)sender;			
		    this.Test = textBlock.Tag;			
        }		

        private void HyperlinkDualStains_Click(object sender, RoutedEventArgs e)
        {
            this.m_TestCollection = Business.Test.Model.DualStainCollection.GetAllAsObjectCollection();
			this.NotifyPropertyChanged("TestCollection");         
		}        		

		public void UpdateBindingSources()
		{

		}

        private void ListBoxPanelSetOrders_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.ListBoxPanelSetOrders.SelectedItem != null)
            {
				this.m_PanelSetOrder = (YellowstonePathology.Business.Test.PanelSetOrder)this.ListBoxPanelSetOrders.SelectedItem;
                this.m_AliquotAndStainOrderView.Refresh(true, this.m_PanelSetOrder);
                this.NotifyPropertyChanged("AliquotAndStainOrderView");
            }
        }

        private void ButtonPrintLabels_Click(object sender, RoutedEventArgs e)
        {
            this.m_AliquotAndStainOrderView.SetEmbeddingComments();
            YellowstonePathology.Business.Test.AliquotOrderCollection selectedAliquots = this.m_AliquotAndStainOrderView.GetSelectedAliquots();
            YellowstonePathology.Business.Label.Model.AliquotOrderPrinter aliquotOrderPrinter = new Business.Label.Model.AliquotOrderPrinter(selectedAliquots, this.m_AccessionOrder);
                        
            if (this.m_AccessionOrder.SpecimenOrderCollection.HasBlocks() == true && string.IsNullOrEmpty(this.m_AccessionOrder.CassetteColor))
            {
                MessageBox.Show("You must select the Cassette Color before printing.");
                return;
            }

            if (this.m_AccessionOrder.SpecimenOrderCollection.HasCellBlocks() == true && string.IsNullOrEmpty(this.m_AccessionOrder.CassetteColor))
            {
                MessageBox.Show("You must select the Cassette Color before printing.");
                return;
            }

            aliquotOrderPrinter.Print();            
            this.m_AliquotAndStainOrderView.SetAliquotChecks(false);

            this.PrintSelectedSlides();

            MessageBox.Show("The selected items have been submitted to the printer.");
        }

        private void PrintSelectedSlides()
        {            
            YellowstonePathology.Business.Slide.Model.SlideOrderCollection slideOrderCollection = this.m_AliquotAndStainOrderView.GetSelectedSlideOrders();
            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in slideOrderCollection)
            {
                YellowstonePathology.Business.Label.Model.HistologySlidePaperLabel paperLabel = new Business.Label.Model.HistologySlidePaperLabel(slideOrder.SlideOrderId,
                                        this.m_AccessionOrder.MasterAccessionNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestAbbreviation, slideOrder.AccessioningFacility);

                YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter printer = new Business.Label.Model.HistologySlidePaperLabelPrinter();
                printer.Queue.Enqueue(paperLabel);
                printer.Print();
            }            
        }

        private void PrintRowOfSlides(Queue<Business.Label.Model.HistologySlidePaperZPLLabelV2> queue)
        {
            StringBuilder result = new StringBuilder();
            int xOffset = 0;

            result.Append("^XA");
            for (int i = 0; i < 4; i++)
            {
                if (queue.Count != 0)
                {
                    Business.Label.Model.HistologySlidePaperZPLLabelV2 label = queue.Dequeue();                    
                    result.Append(label.GetCommandWithOffset(xOffset));
                    xOffset += 325;
                }
            }
            result.Append("^XZ");

            string facilityId = Business.User.UserPreferenceInstance.Instance.UserPreference.FacilityId;
            Business.Label.Model.ZPLPrinterTCP printer = new Business.Label.Model.ZPLPrinterTCP("10.1.1.21");
            if (facilityId == "YPICDY" || facilityId == "YPCDY")
            {
                printer = new Business.Label.Model.ZPLPrinterTCP("10.33.33.56");
            }

            printer.Print(result.ToString());
        }

        private void ButtonAddSlideOrder_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection = this.m_AliquotAndStainOrderView.GetSelectedTestOrders();
            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in testOrderCollection)
            {
                YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(testOrder.AliquotOrder.AliquotOrderId);
                YellowstonePathology.Business.Visitor.AddSlideOrderVisitor addSlideOrderVisitor = new Business.Visitor.AddSlideOrderVisitor(aliquotOrder, testOrder);
                this.m_AccessionOrder.TakeATrip(addSlideOrderVisitor);
            }

            this.m_AliquotAndStainOrderView = new AliquotAndStainOrderView(this.m_AccessionOrder, this.m_PanelSetOrder);
            this.NotifyPropertyChanged("AliquotAndStainOrderView");
        }

        private void ButtonMapping_Click(object sender, RoutedEventArgs e)
        {
            this.ShowSpecimenMappingPage(this, new EventArgs());
        }

        private void CheckBoxSpecimenClientAccessioned_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string specimenOrderId = checkBox.Tag.ToString();
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(specimenOrderId);
            specimenOrder.ClientAccessioned = true;
        }

        private void CheckBoxSpecimenClientAccessioned_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string specimenOrderId = checkBox.Tag.ToString();
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrder(specimenOrderId);
            specimenOrder.ClientAccessioned = false;
        }

        private void CheckBoxAliquotClientAccessioned_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string aliquotOrderId = checkBox.Tag.ToString();
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotOrderId);
            aliquotOrder.ClientAccessioned = true;
        }

        private void CheckBoxAliquotClientAccessioned_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string aliquotOrderId = checkBox.Tag.ToString();
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotOrderId);
            aliquotOrder.ClientAccessioned = false;
        }

        private void CheckBoxSlideClientAccessioned_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string slideOrderId = checkBox.Tag.ToString();
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSlideOrder(slideOrderId);
            slideOrder.ClientAccessioned = true;
        }   

        private void CheckBoxSlideClientAccessioned_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            string slideOrderId = checkBox.Tag.ToString();
            YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSlideOrder(slideOrderId);
            slideOrder.ClientAccessioned = false;
        }

        private void MenuItemPrintPaperLabel_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;            
            XElement xElement = XElement.Parse(menuItem.Tag.ToString());
            string aliquotOrderId = xElement.Element("AliquotOrderId").Value;            
            YellowstonePathology.Business.Test.AliquotOrder aliquotOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetAliquotOrder(aliquotOrderId);            
            YellowstonePathology.Business.Label.Model.BlockLabelPrinter blockLabelPrinter = new Business.Label.Model.BlockLabelPrinter(aliquotOrderId, aliquotOrder.Label, this.m_AccessionOrder.MasterAccessionNo, this.m_AccessionOrder.PLastName, this.m_AccessionOrder.PFirstName);
            blockLabelPrinter.Print();            
        }        

        private void MenuItemShowVentanaOrder_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            XElement xElement = XElement.Parse(menuItem.Tag.ToString());
            Business.HL7View.VentanaStainOrder ventanaStainOrder = new Business.HL7View.VentanaStainOrder();
            string testOrderId = xElement.Element("TestOrderId").Value;
            string slideOrderId = xElement.Element("SlideOrder").Element("SlideOrderId").Value;
            string result = ventanaStainOrder.Build(this.m_AccessionOrder, testOrderId, slideOrderId);            

            MessageBox.Show(result);
        }

        private void HyperLinkUpdateSlides_Click(object sender, RoutedEventArgs e)
        {
            this.m_AccessionOrder.SpecimenOrderCollection.UpdateSlideInfo(this.m_AccessionOrder);
            this.m_AliquotAndStainOrderView.Refresh(true, this.m_PanelSetOrder);
            this.NotifyPropertyChanged("AliquotAndStainOrderView");
            MessageBox.Show("The patient information has been updated for all existing slides.");
        }

        private void HyperLinkPrintAllPaperSlides_Click(object sender, RoutedEventArgs e)
        {
            foreach(Business.Specimen.Model.SpecimenOrder specimenOrder in this.m_AccessionOrder.SpecimenOrderCollection)
            {
                foreach(Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    foreach(Business.Slide.Model.SlideOrder slideOrder in aliquotOrder.SlideOrderCollection)
                    {
                        System.Windows.Controls.PrintDialog printDialog = new System.Windows.Controls.PrintDialog();
                        System.Printing.PrintServer printServer = new System.Printing.LocalPrintServer();

                        System.Printing.PrintQueue printQueue = printServer.GetPrintQueue(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HistologySlideLabelPrinter);
                        printDialog.PrintQueue = printQueue;

                        YellowstonePathology.Business.Label.Model.HistologySlidePaperLabel histologySlidePaperLabel = new Business.Label.Model.HistologySlidePaperLabel(slideOrder.SlideOrderId,
                            slideOrder.ReportNo, slideOrder.Label, slideOrder.PatientLastName, slideOrder.TestAbbreviation, slideOrder.AccessioningFacility);
                        YellowstonePathology.Business.Label.Model.HistologySlidePaperLabelPrinter histologySlidePaperLabelPrinter = new Business.Label.Model.HistologySlidePaperLabelPrinter();
                        histologySlidePaperLabelPrinter.Queue.Enqueue(histologySlidePaperLabel);
                        histologySlidePaperLabelPrinter.Print();
                    }
                }
            }            
        }

        private void MenuItemSendVentanaOrder_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            XElement xElement = XElement.Parse(menuItem.Tag.ToString());

            string slideOrderId = xElement.Element("SlideOrderId").Value;           
            Business.OrderIdParser orderIdParser = new Business.OrderIdParser(slideOrderId);

            Business.Test.AccessionOrder ao = Business.Persistence.DocumentGateway.Instance.GetAccessionOrderByMasterAccessionNo(orderIdParser.MasterAccessionNo);
            Business.Slide.Model.SlideOrder slideOrder = ao.SpecimenOrderCollection.GetSlideOrder(slideOrderId);
            Business.HL7View.VentanaStainOrder vo = new Business.HL7View.VentanaStainOrder();
            vo.HandleOrder(ao, slideOrder);

            MessageBox.Show("The order has been sent.");
        }

        private void HyperLinkPrintCovidLabel_Click(object sender, RoutedEventArgs e)
        {            
            if(this.m_PanelSetOrder.PanelSetId == 415)
            {
                if(string.IsNullOrEmpty(Business.User.UserPreferenceInstance.Instance.UserPreference.PantherCOVIDLabelPrinter) == false)
                {
                    YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = this.m_AccessionOrder.SpecimenOrderCollection.GetSpecimenOrderById(this.m_PanelSetOrder.OrderedOnId);
                    string zplCommands = Business.Label.Model.PantherZPLLabel.GetCommands(specimenOrder.SpecimenOrderId, this.m_AccessionOrder.PBirthdate.Value, this.m_AccessionOrder.PatientDisplayName, specimenOrder.Description);

                    Business.Label.Model.ZPLPrinterUSB zplPrinter = new Business.Label.Model.ZPLPrinterUSB(Business.User.UserPreferenceInstance.Instance.UserPreference.PantherCOVIDLabelPrinter);
                    zplPrinter.Print(zplCommands);
                }                
                else
                {
                    MessageBox.Show("Please select the Panther COVID printer to use in User Preferences.");
                }
            }
            else
            {
                Business.Label.Model.ReportNoLabel reportNoLabel = new Business.Label.Model.ReportNoLabel(this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, this.m_PanelSetOrder.ReportNo, this.m_AccessionOrder.HighPriority);
                Business.Label.Model.ReportNoLabelPrinter.Print(reportNoLabel, Business.User.UserPreferenceInstance.Instance.UserPreference.CytologySlideLabelPrinter, 2);
            }            
        }

        private void ButtonAliquotTypeCSSlide_Click(object sender, RoutedEventArgs e)
        {
            this.Aliquot = new Business.Specimen.Model.CSSlide(Business.Specimen.Model.AliquotLabelType.PaperLabel);
        }

        private void ButtonAliquotTypeCESlide_Click(object sender, RoutedEventArgs e)
        {
            this.Aliquot = new Business.Specimen.Model.CESlide(Business.Specimen.Model.AliquotLabelType.PaperLabel);
        }

        private void MenuItemPrintZPLSlideLabel_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            XElement xElement = XElement.Parse(menuItem.Tag.ToString());

            string slideOrderId = xElement.Element("SlideOrderId").Value;
            string [] dotSplit = slideOrderId.Split('.');
            
            Business.Label.Model.HistologySlidePaperZPLLabelV3 label = new Business.Label.Model.HistologySlidePaperZPLLabelV3(slideOrderId, this.m_PanelSetOrder.ReportNo, 
                this.m_AccessionOrder.PFirstName, this.m_AccessionOrder.PLastName, "H&E", dotSplit[1], "YPI Blgs, Mt", false, false);
            string xx = label.GetCommandWithOffset(0);
            string result = $"^XA{xx}^XZ";

            Business.Label.Model.ZPLPrinterTCP printer = new Business.Label.Model.ZPLPrinterTCP("10.1.1.21");
            printer.Print(result);
        }
    }
}
