using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;

namespace YellowstonePathology.Business.Rules
{
	public class BaseRules 
	{        
        public static string m_RuleFilePath = @"C:\SVN\YPII\LIS\Business\Rules\";
                
        protected System.Workflow.Activities.Rules.RuleValidation m_RuleValidation;                
        protected Type m_InheritingObjectType;
        System.Workflow.Activities.Rules.RuleSet m_RuleSet;
        
        protected RuleExecutionStatus m_RuleExecutionStatus;        
		protected YellowstonePathology.Business.User.SystemUserRoleDescriptionList m_PermissionList;

        protected BaseRules(Type inheritingObjectType)
        {            
            this.m_PermissionList = new YellowstonePathology.Business.User.SystemUserRoleDescriptionList();
			this.m_InheritingObjectType = inheritingObjectType;            
            this.Deserialize();
		}

        public virtual void Run(YellowstonePathology.Business.Rules.RuleExecutionStatus ruleExecutionStatus)
        {
            this.m_RuleExecutionStatus = ruleExecutionStatus;
            this.Execute();
        }

        public bool UserHasPermission
        {
            get
            {                
                return YellowstonePathology.Business.User.SystemIdentity.Instance.User.IsUserInRole(this.m_PermissionList);
			}
        }

        public RuleExecutionStatus RuleExecutionStatus
        {
            get { return this.m_RuleExecutionStatus; }            
        }

        protected System.IO.Stream RulesFileStream
        {
            get
            {
                System.Reflection.Assembly assembly = this.GetType().Assembly;
                string[] names = assembly.GetManifestResourceNames();
                System.IO.Stream stream = assembly.GetManifestResourceStream(this.RuleResourceName);                
                return stream;
            }
        }

        protected string RuleResourceName
        {
            get { return this.m_InheritingObjectType.FullName + ".xml";  }
        }

        protected string RuleResourceFileName
        {
            get
            {                
                string fileName = this.RuleResourceName.Replace(".", "\\");
                fileName = fileName.Replace("YellowstonePathology", @"C:\SVN\YPII\LIS\");    
                fileName = fileName.Replace(@"\xml", ".xml");
                return fileName;
            }
        }

        public virtual RuleExecutionStatus Execute()
        {            
            if (this.m_RuleValidation == null)
            {
                this.Validate(this.m_InheritingObjectType);
            }
                        
            System.Workflow.Activities.Rules.RuleExecution ruleExecution = new System.Workflow.Activities.Rules.RuleExecution(this.m_RuleValidation, this);            
            this.m_RuleSet.Execute(ruleExecution);
            return this.RuleExecutionStatus;
        }

        public void AddToStatusList(string description, bool executionHalted)
        {
            RuleExecutionStatusItem ruleStatus = new RuleExecutionStatusItem();
            ruleStatus.Description = description;
            ruleStatus.ExecutionHalted = executionHalted;
            this.RuleExecutionStatus.RuleExecutionStatusList.Add(ruleStatus);
        }

        protected void Validate(Type type)
        {            
            this.m_RuleValidation = new System.Workflow.Activities.Rules.RuleValidation(type, null);
            this.m_RuleSet.Validate(this.m_RuleValidation);

            System.Workflow.ComponentModel.Compiler.ValidationErrorCollection errors = this.m_RuleValidation.Errors;
            if (errors.Count > 0)
            {
                throw new Exception("There were Errors in the validation");
            }            
        }

        protected void Deserialize()
        {            
            XmlTextReader rulesReader = new XmlTextReader(this.RulesFileStream);
            System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer serializerRead = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer();
            System.Workflow.Activities.Rules.RuleSet ruleSet = (System.Workflow.Activities.Rules.RuleSet)serializerRead.Deserialize(rulesReader);
            rulesReader.Close();
            this.m_RuleSet = ruleSet;
        }

        public void EditRuleSet()
        {            
            System.Workflow.Activities.Rules.Design.RuleSetDialog ruleSetDialog = new System.Workflow.Activities.Rules.Design.RuleSetDialog(this.m_InheritingObjectType, null, this.m_RuleSet);
            ruleSetDialog.ShowDialog();
            this.m_RuleSet = ruleSetDialog.RuleSet;
            SerializeRuleSet();
        }

        private void SerializeRuleSet()
        {
            System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer serializerWrite = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer();
            XmlWriter rulesWriter = XmlWriter.Create(this.RuleResourceFileName);
            serializerWrite.Serialize(rulesWriter, this.m_RuleSet);
            rulesWriter.Close();
        }

        public static void CreateRuleSetFile(Type objectType)
        {
            System.Workflow.Activities.Rules.RuleSet ruleSet = new System.Workflow.Activities.Rules.RuleSet();

            string objectPath = objectType.FullName.Replace("YellowstonePathology.Business.Rules", string.Empty);
            string fileName = m_RuleFilePath + objectPath.Replace(".", "\\") + ".xml";

            if (System.IO.File.Exists(fileName) == false)
            {                
                System.Workflow.Activities.Rules.Design.RuleSetDialog ruleSetDialog = new System.Workflow.Activities.Rules.Design.RuleSetDialog(objectType, null, ruleSet);
                ruleSetDialog.ShowDialog();
                ruleSet = ruleSetDialog.RuleSet;
                System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer serializerWrite = new System.Workflow.ComponentModel.Serialization.WorkflowMarkupSerializer();                
                XmlWriter rulesWriter = XmlWriter.Create(fileName);
                serializerWrite.Serialize(rulesWriter, ruleSet);
                rulesWriter.Close();
            }
            else
            {
                System.Windows.MessageBox.Show("Can't create rule. The rule name already exists!");
            }
        }

        public static YellowstonePathology.Business.Rules.BaseRules CreateRuleSetInstance(Type type)
        {
            PropertyInfo propertyInfo = type.GetProperty("Instance");
            YellowstonePathology.Business.Rules.BaseRules baseRules = (YellowstonePathology.Business.Rules.BaseRules)propertyInfo.GetValue(null, null);
            return baseRules;
        }        
	}
}
