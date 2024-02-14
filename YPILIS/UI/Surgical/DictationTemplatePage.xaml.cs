using System;
using System.Collections.Generic;
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
using System.Drawing.Imaging;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Surgical
{
    public partial class DictationTemplatePage : UserControl, INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

        private Business.Test.Surgical.SurgicalTestOrder m_SurgicalTestOrder;
        private UI.Gross.DictationTemplate m_DictationTemplate;        
        private Business.Test.AccessionOrder m_AccessionOrder;
        private Business.User.SystemIdentity m_SystemIdentity;
        private Business.User.SystemUserCollection m_PathologistUsers;
        private Business.User.UserPreference m_UserPreference;
        private string m_GrossDescription;
        private System.Drawing.Image m_GrossCapture;

        public DictationTemplatePage(YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
		{
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            this.m_SurgicalTestOrder = (YellowstonePathology.Business.Test.Surgical.SurgicalTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetSurgical();
			this.m_PathologistUsers = Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetUsersByRole(YellowstonePathology.Business.User.SystemUserRoleDescriptionEnum.Pathologist, true);
			this.m_UserPreference = Business.User.UserPreferenceInstance.Instance.UserPreference;

			InitializeComponent();
            this.Loaded += DictationTemplatePage_Loaded;

			DataContext = this;
		}

        public Business.Test.Surgical.SurgicalTestOrder SurgicalTestOrder
        {
            get { return this.m_SurgicalTestOrder; }
        }


        public YellowstonePathology.Business.User.SystemUserCollection PathologistUsers
		{
			get { return this.m_PathologistUsers; }
		}
        
        public YellowstonePathology.Business.User.UserPreference UserPreference
		{
			get { return this.m_UserPreference; }
		}

        private void DictationTemplatePage_Loaded(object sender, RoutedEventArgs e)
        {
            if(this.m_AccessionOrder.SpecimenOrderCollection.Count != 0)
            {
                this.ListBoxSpecimenOrders.SelectedIndex = 0;
            }
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
        }

        public YellowstonePathology.UI.Gross.DictationTemplate DictationTemplate
        {
            get { return this.m_DictationTemplate; }            
        }  
        
        public string GrossDescription
        {
            get { return this.m_GrossDescription; }
            set
            {
                if(this.m_GrossDescription != value)
                {
                    this.m_GrossDescription = value;
                    this.NotifyPropertyChanged("GrossDescription");
                }
            }
        }      	                        

        private void ListBoxSpecimen_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.m_GrossDescription = null;
            if(this.ListBoxSpecimenOrders.SelectedItem != null)
            {                                                              
                YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder = (YellowstonePathology.Business.Specimen.Model.SpecimenOrder)this.ListBoxSpecimenOrders.SelectedItem;                    
                if(string.IsNullOrEmpty(specimenOrder.SpecimenId) == false)
                {
                    this.m_DictationTemplate = Gross.DictationTemplateCollection.Instance.GetClone(specimenOrder);

                    if (this.m_DictationTemplate.TemplateName != "Template Not Found.")
                    {
	                    this.m_GrossDescription = this.m_DictationTemplate.BuildResultText(specimenOrder, this.m_AccessionOrder, this.m_SystemIdentity);	                    	                    
	                    this.NotifyPropertyChanged(string.Empty);
	                    this.TextBoxGrossDescription.Focus();
	                    this.SelectNextInput(0);
                    }
                }                
            }

            this.NotifyPropertyChanged(string.Empty);
        }   
        
        private bool SelectNextInput(int startingPosition)
        {
            bool result = false;   
            if(startingPosition != this.TextBoxGrossDescription.Text.Length)
            {
                int positionOfNextLeftBracket = this.TextBoxGrossDescription.Text.IndexOf("[", startingPosition + 1);
                if (positionOfNextLeftBracket != -1)
                {
                    int positionOfNextRightBracket = this.TextBoxGrossDescription.Text.IndexOf("]", positionOfNextLeftBracket);
                    this.TextBoxGrossDescription.SelectionStart = positionOfNextLeftBracket;
                    this.TextBoxGrossDescription.SelectionLength = positionOfNextRightBracket - positionOfNextLeftBracket + 1;
                    result = true;
                }
            }                           
            return result;
        }    
        
        private void HyperLinkAddDicationToGross_Click(object sender, RoutedEventArgs e)
        {
            if (this.m_AccessionOrder.PanelSetOrderCollection.HasSurgical() == true)
            {
                this.AddToSurgical();
            }
            else if(this.m_AccessionOrder.PanelSetOrderCollection.Exists(238) == true)
            {
                AddToGrossOnly();
            }
        }

        private void AddToSurgical()
        {
            if (this.m_SurgicalTestOrder.GrossX == "???") this.m_SurgicalTestOrder.GrossX = null;
            if (string.IsNullOrEmpty(this.m_SurgicalTestOrder.GrossX) == true)
            {
                this.m_SurgicalTestOrder.GrossX = this.m_GrossDescription;
            }
            else
            {
                this.m_SurgicalTestOrder.GrossX = this.m_SurgicalTestOrder.GrossX + Environment.NewLine + Environment.NewLine + this.m_GrossDescription;
            }

            this.m_GrossDescription = null;
            this.NotifyPropertyChanged("GrossDescription");

            if (this.ListBoxSpecimenOrders.SelectedIndex != this.ListBoxSpecimenOrders.Items.Count - 1)
            {
                this.ListBoxSpecimenOrders.SelectedIndex = this.ListBoxSpecimenOrders.SelectedIndex + 1;
            }
        }

        private void AddToGrossOnly()
        {
            Business.Test.GrossOnly.GrossOnlyTestOrder grossOnlyTestOrder = (Business.Test.GrossOnly.GrossOnlyTestOrder)this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(238);
            if (string.IsNullOrEmpty(grossOnlyTestOrder.GrossX) == true)
            {
                grossOnlyTestOrder.GrossX = this.m_GrossDescription;
            }
            else
            {
                grossOnlyTestOrder.GrossX = grossOnlyTestOrder.GrossX + Environment.NewLine + Environment.NewLine + this.m_GrossDescription;
            }

            this.m_GrossDescription = null;
            this.NotifyPropertyChanged("GrossDescription");

            if (this.ListBoxSpecimenOrders.SelectedIndex != this.ListBoxSpecimenOrders.Items.Count - 1)
            {
                this.ListBoxSpecimenOrders.SelectedIndex = this.ListBoxSpecimenOrders.SelectedIndex + 1;
            }
        }

        private void TextBoxGrossDescription_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            TextBox tbx = sender as TextBox;
            if (e.Key == Key.Tab)
            {
                e.Handled = true;
                this.HandleTextBoxGrossDescriptionTab();
            }
            else if(e.Key == Key.Enter)
            {
                if(this.TextBoxGrossDescription.SelectedText.Length > 0)
                {
                    if (this.TextBoxGrossDescription.SelectedText.Substring(0, 2) == "[?")
                    {
                        e.Handled = true;

                        int selectedTextStart = this.TextBoxGrossDescription.SelectionStart;
                        int selectedTextLength = this.TextBoxGrossDescription.SelectionLength;
                        string selectedText = this.TextBoxGrossDescription.Text.Substring(selectedTextStart, selectedTextLength);
                        this.TextBoxGrossDescription.Text = this.TextBoxGrossDescription.Text.Remove(selectedTextStart, selectedTextLength);

                        selectedText = selectedText.Replace("[?", "");
                        selectedText = selectedText.Replace("?]", "");

                        this.TextBoxGrossDescription.Text = this.TextBoxGrossDescription.Text.Insert(selectedTextStart, selectedText);

                        this.TextBoxGrossDescription.SelectionStart = selectedTextStart;
                        this.TextBoxGrossDescription.SelectionLength = selectedTextLength - 4;
                    }
                }                
            }
            else if(e.Key == Key.Delete)
            {
                StringBuilder text = new StringBuilder(this.TextBoxGrossDescription.Text);                
                int cursorPosition = this.TextBoxGrossDescription.SelectionStart;
                int selectedTextLength = this.TextBoxGrossDescription.SelectionLength;                
                text.Remove(cursorPosition, selectedTextLength);

                if (text.Length > cursorPosition && text.ToString(cursorPosition, 1) == "." && text.ToString(cursorPosition - 1, 1) == " ")
                {
                    text.Remove(cursorPosition - 1, 1);
                }

                this.TextBoxGrossDescription.Text = text.ToString();
                this.TextBoxGrossDescription.SelectionStart = cursorPosition;
                e.Handled = true;
            }
        }        

        private void HandleTextBoxGrossDescriptionTab()
        {
            int startingPosition = this.TextBoxGrossDescription.SelectionStart;
            if (string.IsNullOrEmpty(this.TextBoxGrossDescription.SelectedText) == false)
            {
                startingPosition = this.TextBoxGrossDescription.SelectionStart;
            }

            if (startingPosition == 0)
            {
                SelectNextInput(startingPosition);
            }
            else
            {
                if (SelectNextInput(startingPosition) == false)
                {
                    SelectNextInput(0);
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void HyperLinkShowCaseReport_Click(object sender, RoutedEventArgs e)
        {
            //YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder = this.m_AccessionOrder.PanelSetOrderCollection.GetPanelSetOrder(this.m_LoginUI.ReportNo);
            //YellowstonePathology.Business.Interface.ICaseDocument caseDocument = Business.Document.DocumentFactory.GetDocument(this.m_AccessionOrder, this.m_SurgicalTestOrder, Business.Document.ReportSaveModeEnum.Draft);
            //caseDocument.Render();
            //YellowstonePathology.Business.Document.CaseDocument.OpenWordDocumentWithWord(caseDocument.SaveFileName);

            WriteToPng(this.TextBoxGross);
        }

        public void WriteToPng(UIElement element)
        {
            var rect = new Rect(element.RenderSize);
            var visual = new DrawingVisual();

            using (var dc = visual.RenderOpen())
            {
                dc.DrawRectangle(new VisualBrush(element), null, rect);
            }

            var bitmap = new RenderTargetBitmap(
                (int)rect.Width, (int)rect.Height, 96, 96, PixelFormats.Default);
            bitmap.Render(visual);

            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));

            //using (var file = System.IO.File.OpenWrite(filename))
            //{
            //    encoder.Save(file);
            //}

            System.IO.MemoryStream stream = new System.IO.MemoryStream();
            BitmapEncoder bEncoder = new BmpBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            encoder.Save(stream);

            stream.Position = 0;
            //this.m_GrossCapture = System.Drawing.Image.FromFile(@"d:\testing\qqq.png");
            this.m_GrossCapture = System.Drawing.Image.FromStream(stream);
            //int newX = Convert.ToInt32(Math.Round((this.m_GrossCapture.Width * .9), 0));
            //int newY = Convert.ToInt32(Math.Round((this.m_GrossCapture.Height * .9), 0));
            //System.Drawing.Size newSize = new System.Drawing.Size(newX, newY);
            //this.m_GrossCapture = new Bitmap(this.m_GrossCapture, newSize);
            
            //this.m_GrossCapture.Save(@"d:\testing\qqq.png");

            System.Drawing.Printing.PrintDocument printDoc = new System.Drawing.Printing.PrintDocument();            
            printDoc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintDoc_PrintPage);
            printDoc.Print();

            //PrintPreviewDialog dlg = new PrintPreviewDialog();
            //dlg.Document = printDoc as IDocumentPaginatorSource;
            //dlg.ShowDialog();
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {            
            System.Drawing.Point ulCorner = new System.Drawing.Point(10, 10);
            e.Graphics.DrawImage(this.m_GrossCapture, ulCorner);           
        }

        private void HyperLinkPrintGrossText_Click(object sender, RoutedEventArgs e)
        {
            FlowDocument doc = new FlowDocument(new Paragraph(new Run(this.m_SurgicalTestOrder.GrossX)));
            //doc.Name = "FlowDoc.";
            IDocumentPaginatorSource idpSource = doc;
            PrintDialog printDlg = new PrintDialog();
            printDlg.PrintDocument(idpSource.DocumentPaginator, "Gross Description");
        }
    }
}
