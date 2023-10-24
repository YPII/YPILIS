using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace YellowstonePathology.Business.Rules
{
	public class RuleManager
	{
        const string RuleFilePath = @"C:\SVN\YPII\LIS\Business\Rules\";        

        public static void ExecuteRuleSet(System.Workflow.Activities.Rules.RuleSet ruleSet, object thisObject)
        {
            System.Workflow.Activities.Rules.RuleValidation validation = new System.Workflow.Activities.Rules.RuleValidation(thisObject.GetType(), null);
            ruleSet.Validate(validation);

            System.Workflow.ComponentModel.Compiler.ValidationErrorCollection errors = validation.Errors;
            if (errors.Count > 0)
            {
                System.Windows.MessageBox.Show("There were Errors in the validation");
            }

            System.Workflow.Activities.Rules.RuleExecution execution = new System.Workflow.Activities.Rules.RuleExecution(validation, thisObject);
            ruleSet.Execute(execution);       
        }       

        public static void EditRuleSet(string ruleSetFileName, Type objectType)
        {
            System.Workflow.Activities.Rules.RuleSet ruleSet = DeserializeRuleSet(ruleSetFileName);            
            System.Workflow.Activities.Rules.Design.RuleSetDialog ruleSetDialog = new System.Workflow.Activities.Rules.Design.RuleSetDialog(objectType, null, ruleSet);
            ruleSetDialog.ShowDialog();
            ruleSet = ruleSetDialog.RuleSet;
            SerializeRuleSet(ruleSetFileName, ruleSet);            
        }

        public static void CreateRuleSet(string ruleSetFileName, Type objectType)
        {
            string fullFileName = RuleFilePath + ruleSetFileName;
            if (System.IO.File.Exists(fullFileName) == false)
            {
                System.Workflow.Activities.Rules.RuleSet ruleSet = null;
                System.Workflow.Activities.Rules.Design.RuleSetDialog ruleSetDialog = new System.Workflow.Activities.Rules.Design.RuleSetDialog(objectType, null, ruleSet);
                ruleSetDialog.ShowDialog();
                ruleSet = ruleSetDialog.RuleSet;
                SerializeRuleSet(ruleSetFileName, ruleSet);
            }
            else
            {
                System.Windows.MessageBox.Show("Can't create rule. The rule name already exists!");
            }
        }

        public static System.Workflow.Activities.Rules.RuleSet GetRuleSet(string ruleSetFileName)
        {
            return DeserializeRuleSet(ruleSetFileName);
        }

        private static void SerializeRuleSet(string ruleSetFileName, System.Workflow.Activities.Rules.RuleSet ruleSet)
        {
            System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer serializerWrite = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer();
            XmlWriter rulesWriter = XmlWriter.Create(RuleFilePath + ruleSetFileName);
            serializerWrite.Serialize(rulesWriter, ruleSet);
            rulesWriter.Close();
        }

        private static System.Workflow.Activities.Rules.RuleSet DeserializeRuleSet(string ruleSetFileName)
        {
            XmlTextReader rulesReader = new XmlTextReader(RuleFilePath + ruleSetFileName);
            System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer serializerRead = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer();
            System.Workflow.Activities.Rules.RuleSet ruleSet = (System.Workflow.Activities.Rules.RuleSet)serializerRead.Deserialize(rulesReader);
            rulesReader.Close();
            return ruleSet;
        }
	}
}
