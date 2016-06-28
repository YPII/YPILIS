using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Controls;

namespace YellowstonePathology.UI
{
    public class BindingValidator
    {
        List<BindingValidatorItem> m_BindingValidatorItemList;
   
        public BindingValidator()
        {
            this.m_BindingValidatorItemList = new List<BindingValidatorItem>();
        }

        public void Add(Control ctrl, DependencyProperty depencyProperty, string errorMessage)
        {                        
            this.m_BindingValidatorItemList.Add(new BindingValidatorItem(ctrl, depencyProperty, errorMessage));
        }

        public bool HasErrors()
        {
            bool result = false;
            foreach (BindingValidatorItem bindingValidatorItem in this.m_BindingValidatorItemList)
            {
                if (bindingValidatorItem.BindingExpression.HasError == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public string GetErrorMessage()
        {
            StringBuilder result = new StringBuilder();
            foreach (BindingValidatorItem bindingValidatorItem in this.m_BindingValidatorItemList)
            {
                if (bindingValidatorItem.BindingExpression.HasError == true)
                {
                    result.AppendLine(bindingValidatorItem.ErrorMessage);
                }
            }
            return result.ToString();
        }
    }
}
