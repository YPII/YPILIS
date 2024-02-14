using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Validation
{
    class ValidatePatient
    {
        public void Validate(Patient.Model.Patient patient, BrokenRuleCollection brokenRules)
        {
            if (patient.PatientId == "0" | patient.PatientId == string.Empty)
            {
                Validation.BrokenRuleItem brokenRule = new BrokenRuleItem();
                brokenRule.RuleName = "Patient Not Linked.";
                brokenRule.Description = "Please link this patient.";
                brokenRule.Severity = "Critical";
                brokenRules.Add(brokenRule);
            }

            if (patient.LastName == string.Empty)
            {
                BrokenRuleItem brokenRule = new BrokenRuleItem();
                brokenRule.RuleName = "No Patient Last Name";
                brokenRule.Description = "Please enter the patients last name.";
                brokenRule.Severity = "Critical";
                brokenRules.Add(brokenRule);
            }
            if (patient.FirstName == string.Empty)
            {
                BrokenRuleItem brokenRule = new BrokenRuleItem();
                brokenRule.RuleName = "No Patient First Name";
                brokenRule.Description = "Please enter the patients first name.";
                brokenRule.Severity = "Critical";
                brokenRules.Add(brokenRule);
            }
            if (patient.Birthdate.ToString() == string.Empty)
            {
                BrokenRuleItem brokenRule = new BrokenRuleItem();
                brokenRule.RuleName = "No Patient Birthdate";
                brokenRule.Description = "Please enter the patients birthdate.";
                brokenRule.Severity = "Critical";
                brokenRules.Add(brokenRule);
            }            
        }
    }
}
