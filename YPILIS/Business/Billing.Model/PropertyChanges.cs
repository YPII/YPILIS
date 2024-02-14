using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Billing.Model
{
    public class PropertyChanges
    {
        private string m_ReportNo;
        private string m_PropertyName;
        private string m_CurrentValue;
        private string m_OriginalValue;      

        public PropertyChanges(string reportNo, string propertyName, string currentValue, string originalValue)
        {
            this.m_ReportNo = reportNo;
            this.m_PropertyName = propertyName;
            this.m_CurrentValue = currentValue;
            this.m_OriginalValue = originalValue;
        }

        public string ReportNo
        {
            get { return this.m_ReportNo; }
        }

        public string PropertyName
        {
            get { return this.m_PropertyName; }
        }

        public string CurrentValue
        {
            get { return this.m_CurrentValue; }
        }

        public string OriginalValue
        {
            get { return this.m_OriginalValue; }
        }
    }
}
