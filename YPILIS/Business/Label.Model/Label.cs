using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class Label
    {        
        public Label()
        {

        }        

        public virtual void DrawLabel(int x, int y, System.Drawing.Printing.PrintPageEventArgs e)
        {
            throw new Exception("Not implemented here.");
        }

        public string TruncateString(string text, int width)
        {
            string result = string.Empty;
            if (string.IsNullOrEmpty(text) == false)
            {
                if (text.Length > width)
                {
                    result = text.Substring(0, width);
                }
                else
                {
                    result = text;
                }
            }
            return result;
        }
    }
}
