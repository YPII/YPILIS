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
using System.IO;
using System.IO.Packaging;
using System.Windows.Markup;
using System.Reflection;
using System.Windows.Threading;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Xml.Linq;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Document
{
	public class ADTInsuranceDocument
	{
        private Business.HL7View.ADTMessages m_ADTMessages;        
        private string m_DisplayText;
		private FixedDocument m_Document;
		private System.Windows.Threading.DispatcherPriority m_DispatcherPriority;
		private const string TemplatePath = "YellowstonePathology.Business.Document.ADTInsuranceDocument.xaml";

        public ADTInsuranceDocument(Business.HL7View.ADTMessages adtMessages)
		{
            this.m_ADTMessages = adtMessages;            
            this.SetDisplayText();
                  
			this.m_DispatcherPriority = DispatcherPriority.SystemIdle;
			this.LoadTemplate();
			this.InjectData();
		}        

        private void SetDisplayText()
        {
            if(m_ADTMessages.Messages.Count > 0)
            {
                StringBuilder result = new StringBuilder();
                result.AppendLine("Last Name: " + this.m_ADTMessages.Messages[0].PLastName);
                result.AppendLine("First Name: " + this.m_ADTMessages.Messages[0].PFirstName);
                result.AppendLine("Birthdate: " + this.m_ADTMessages.Messages[0].PBirthdate.ToShortDateString());
                result.AppendLine("Home Phone: " + this.m_ADTMessages.Messages[0].PIDSegment.HomePhoneNumber);
                result.AppendLine();

                Business.Patient.Model.Address address = this.m_ADTMessages.GetPatientAddress();
                result.Append(address.DisplayString());

                result.AppendLine();

                if (this.m_ADTMessages.Messages[0].IN2Segments.Count > 0)
                {
                    result.AppendLine(this.m_ADTMessages.Messages[0].IN2Segments[0].DisplayString);
                }                

                List<Business.HL7View.IN1> in1Segments = this.m_ADTMessages.GetUniqueIN1Segments();
                for(int i=0; i<4; i++)
                {
                    if(i < in1Segments.Count)
                    {
                        result.AppendLine(in1Segments[i].DisplayString);
                        result.AppendLine();
                    }
                    else
                    {
                        break;
                    }
                }                                            
                
                this.m_DisplayText = result.ToString();
            }            
        }
        
        public string DisplayText
        {
            get { return this.m_DisplayText; }
        }     

		private void LoadTemplate()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			m_Document = (FixedDocument)XamlReader.Load(assembly.GetManifestResourceStream(TemplatePath));
		}

		private void InjectData()
		{
			this.m_Document.DataContext = this;
			var dispatcher = Dispatcher.CurrentDispatcher;
			dispatcher.Invoke(this.m_DispatcherPriority, new DispatcherOperationCallback(delegate { return null; }), null);
		}

		public void SaveAsTIF(YellowstonePathology.Business.OrderIdParser orderIdParser)
        {
			string filename = Business.Document.CaseDocumentPath.GetPath(orderIdParser);
            filename += orderIdParser.MasterAccessionNo + ".Insurance.tif";
            YellowstonePathology.Business.Helper.FileConversionHelper.SaveFixedDocumentAsTiff(this.Document, filename);
        }

		public FixedDocument Document
		{
			get { return this.m_Document; }
		}
	}
}
