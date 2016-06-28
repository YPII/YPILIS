using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace YellowstonePathology.Business
{	
	public partial class ReportNo : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        private const string ReportNoLetterPattern = @"^\d\d-\d+.(?<ReportNoLetter>[AaBbFfMmPpRrSsTtYyQq])\d*";
        private const string LegacyReportNoLetterPattern = @"(?<ReportNoPrefix>[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*)\d\d-\d+";

        private string m_Value;
        private string m_MasterAccessionNo;

		public ReportNo()
        {

        }

		public ReportNo(string reportNo)
		{
            this.m_Value = reportNo;			
		}

        public string Value
        {
            get { return this.m_Value; }
            set
            {
                if (this.m_Value != value)
                {
                    this.m_Value = value;
                    this.NotifyPropertyChanged("Value");
                }
            }
        }

        public string MasterAccessionNo
        {
            get { return this.m_MasterAccessionNo; }
            set
            {
                if (this.m_MasterAccessionNo != value)
                {
                    this.m_MasterAccessionNo = value;
                    this.NotifyPropertyChanged("MasterAccessionNo");
                }
            }
        }

        public string GetLetter()
        {
            string result = null;

            System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoLetterPattern);
            System.Text.RegularExpressions.Match match = regex.Match(this.m_Value);
            if (match.Captures.Count != 0) 
            {
                result = match.Groups["ReportNoLetter"].Captures[0].Value;
            }

            return result;
        }

        public ReportNo Parse(string reportNo)
        {
            ReportNo result = null;
            result.Value = reportNo;
            string[] dotSplit = reportNo.Split('.');
            if (dotSplit.Length == 2)
            {
                result.MasterAccessionNo = dotSplit[1];
            }
            return result;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        public void WriteProperties(YellowstonePathology.Business.Domain.Persistence.IPropertyWriter propertyWriter)
        {
            this.m_Value = propertyWriter.WriteString("Value");
        }
	}
}
