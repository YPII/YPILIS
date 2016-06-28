using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace YellowstonePathology.UI.Surgical
{
    public class TemplateWriter
    {
        TextBox m_TextBox;

        string m_Text = "Gross: ";

        public TemplateWriter(TextBox textBox)
        {
            this.m_TextBox = textBox;
        }

        public void Start()
        {
            this.m_TextBox.Text = this.m_Text;
        }
    }
}
