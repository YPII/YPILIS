using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace YellowstonePathology.Business.Validation
{
    class ValidationMethods
    {
		public static void AreCodesSet(YellowstonePathology.Business.Billing.Model.CptBillingCodeItemCollection cptBillingCodes, BrokenRuleCollection brokenRules)
        {            
            if (cptBillingCodes.Count == 0)
            {
                Validation.BrokenRuleItem item = new BrokenRuleItem();
                item.RuleName = "Codes Not Set";
                item.Severity = "Critical";
                item.Description = "Please Set Codes.";
                brokenRules.Add(item);
            }
        }

        public static void IsDateValid(string value, BrokenRuleCollection brokenRules)
        {            
            DateTime result;
            if (DateTime.TryParse(value, out result) == false)
            {                
                Validation.BrokenRuleItem item = new BrokenRuleItem();
                item.RuleName = "Date Not Valid";
                item.Severity = "Critical";
                item.Description = "Please enter a valid date.";                
                brokenRules.Add(item);
            }            
        }

        public static void IsBirthdateValid(Nullable<DateTime> value, BrokenRuleCollection brokenRules)
        {
            if (value.HasValue == false)
            {
                Validation.BrokenRuleItem item = new BrokenRuleItem();
                item.RuleName = "Blank Birthdate";
                item.Severity = "Critical";
                item.Description = "Please enter a birthdate.";
                brokenRules.Add(item);
            }
        }

        public static void IsClientIdValid(long value, BrokenRuleCollection brokenRules)
        {
            if (value == 0)
            {
                Validation.BrokenRuleItem item = new BrokenRuleItem();
                item.RuleName = "Blank Client Id";
                item.Severity = "Critical";
                item.Description = "Please select a Client.";
                brokenRules.Add(item);
            }
        }

        public static void IsPhyscianIdValid(int value, BrokenRuleCollection brokenRules)
        {
            if (value == 0)
            {
                Validation.BrokenRuleItem item = new BrokenRuleItem();
                item.RuleName = "Blank Physician Id";
                item.Severity = "Critical";
                item.Description = "Please select a Physician.";
                brokenRules.Add(item);
            }
        }

        public static void IsPatientIdValid(string value, BrokenRuleCollection brokenRules)
        {
            if (value == "0" || value == string.Empty)
            {
                Validation.BrokenRuleItem item = new BrokenRuleItem();
                item.RuleName = "Blank Patient Id";
                item.Severity = "Critical";
                item.Description = "Please select a link this case.";
                brokenRules.Add(item);
            }
        }

        public static void IsDistributionValid(YellowstonePathology.Business.ReportDistribution.Model.TestOrderReportDistributionCollection testOrderReportDistributionCollection, BrokenRuleCollection brokenRules)
        {
            if (testOrderReportDistributionCollection.Count == 0)
            {
                Validation.BrokenRuleItem item = new BrokenRuleItem();
                item.RuleName = "Report Distribution not set.";
                item.Severity = "Critical";
                item.Description = "Please set the report distribution.";
                brokenRules.Add(item);
            }
        }        
    }
}
