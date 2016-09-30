using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
    public class SoundexGenerator
    {
        public static string GetSoundex(string word) 
        { 
            StringBuilder result = new StringBuilder();
            if (word != null && word.Length > 0) 
            { 
                string previousCode = "", currentCode = "", currentLetter = "";
                result.Append(word.Substring(0, 1));
                for (int i = 1; i < word.Length; i++) 
                {
                    currentLetter = word.Substring(i, 1).ToLower(); 
                    currentCode = ""; 
                    if ("bfpv".IndexOf(currentLetter) > -1)                     
                        currentCode = "1"; else if ("cgjkqsxz".IndexOf(currentLetter) > -1)                     
                        currentCode = "2"; else if ("dt".IndexOf(currentLetter) > -1)                     
                        currentCode = "3"; else if (currentLetter == "l")                     
                        currentCode = "4"; else if ("mn".IndexOf(currentLetter) > -1)                     
                        currentCode = "5"; else if (currentLetter == "r")                     
                        currentCode = "6"; if (currentCode != previousCode)                     
                            result.Append(currentCode); 
                    if (result.Length == 4) break; if (currentCode != "")                     
                                previousCode = currentCode; 
                } 
            } 

            if (result.Length < 4)             
                result.Append(new String('0', 4 - result.Length)); 
            return result.ToString().ToUpper(); 
        } 
    }
}
