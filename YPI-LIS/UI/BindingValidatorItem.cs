using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace YellowstonePathology.UI
{
    public class BindingValidatorItem
    {
        private BindingExpression m_BindingExpression;
        private Control m_Control;
        private string m_ErrorMessage;

        public BindingValidatorItem(Control control, DependencyProperty dependencyProperty, string ErrorMessage)
        {
            this.m_Control = control;
            this.m_BindingExpression = this.m_Control.GetBindingExpression(dependencyProperty);
            this.m_ErrorMessage = ErrorMessage;            
        }

        public BindingExpression BindingExpression
        {
            get { return this.m_BindingExpression; }
            set { this.m_BindingExpression = value; }
        }

        public Control Control
        {
            get { return this.m_Control; }
            set { this.m_Control = value; }
        }

        public string ErrorMessage
        {
            get { return this.m_ErrorMessage; }
            set { this.m_ErrorMessage = value; }
        }
    }
}
