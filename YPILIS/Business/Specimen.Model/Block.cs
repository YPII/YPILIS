using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Block : Aliquot
    {
        public Block(string identificationType) : base("Block", "Block", identificationType)
        {
            
        }

        public static string GetBlockLetter(string blockLabel)
        {
            string result = blockLabel;            
            if(string.IsNullOrEmpty(blockLabel) == false)
            {
                if(blockLabel.Contains(".") == true)
                {
                    string[] dotSplit = blockLabel.Split('.');
                    result = dotSplit[1];
                }
                else
                {
                    string pattern = "([A-Z]+)";
                    System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(pattern);
                    System.Text.RegularExpressions.Match match = regex.Match(blockLabel);
                    if (match.Captures.Count != 0)
                    {
                        result = match.Value;
                    }
                }                
            }
            return result;
        }
    }    
}
