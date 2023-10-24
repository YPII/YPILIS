using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Slide : Aliquot
    {
        public Slide(string identificationType) : base("Slide", "Slide", identificationType)
        {

        }

        public static string GetSlideNumber(string slideLabel)
        {
            string result = slideLabel;
            if (string.IsNullOrEmpty(slideLabel) == false)
            {
                if(slideLabel.Contains(".") == true)
                {
                    string[] dotSplit = slideLabel.Split('.');
                    if(dotSplit.Length == 2)
                    {
                        result = dotSplit[0];
                    }                    
                }
                else
                {
                    string pattern = "([1-9]+)([A-Z]+)([0-9]+)";
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                    System.Text.RegularExpressions.Match match = regex.Match(slideLabel);
                    if (match.Captures.Count != 0)
                    {
                        result = match.Groups[3].Value;
                    }
                }                
            }
            return result;
        }
    }
}
