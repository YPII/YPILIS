using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;

namespace YellowstonePathology.Business.Validation
{
    public class DataValidatorItem
    {
		private ValidationResult m_DataTypeValidationResult;        
        private BindingExpression m_BindingExpression;
		private Func<ValidationResult> m_DomainValidationMethod;
		private ValidationResult m_DomainValidationResult;

		public DataValidatorItem(ValidationResult dataTypeValidationResult, BindingExpression bindingExpression, Func<ValidationResult> domainValidationMethod)
        {
            this.m_DataTypeValidationResult = dataTypeValidationResult;            
            this.m_BindingExpression = bindingExpression;
            this.m_DomainValidationMethod = domainValidationMethod;
        }

		public ValidationResult DataTypeValidationResult
        {
            get { return this.m_DataTypeValidationResult; }
            set {this.m_DataTypeValidationResult = value;}
        }        

        public BindingExpression BindingExpression
        {
            get { return this.m_BindingExpression; }
            set { this.m_BindingExpression = value; }
        }

		public Func<ValidationResult> DomainValidationMethod
        {
            get { return this.m_DomainValidationMethod; }
            set { this.m_DomainValidationMethod = value; }
        }

		public ValidationResult DomainValidationResult
        {
            get { return this.m_DomainValidationResult; }
            set { this.m_DomainValidationResult = value; }
        }        
    }
}
