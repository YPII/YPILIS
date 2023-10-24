using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class FixationType : ObservableCollection<string>
    {
        public const string Fresh = "Fresh";
        public const string Formalin = "Formalin";
        public const string BPlusFixative = "B+ Fixative";
        public const string Cytolyt = "Cytolyt";
        public const string PreservCyt = "PreservCyt";
        public const string NotApplicable = "Not Applicable";
        public const string NinetyFivePercentIPA = "95% IPA";
        public const string EDTA = "EDTA";
        public const string SodiumHeparin = "Sodium Heparin";
        public const string ACD = "ACD";
        public const string FreshInRPMI = "Fresh in RPMI";
        public const string MichelsTransport = "Michels Transport";


        public static ObservableCollection<string> GetFixationTypeCollection()
        {
            ObservableCollection<string> result = new ObservableCollection<string>();
            result.Add(Fresh);
            result.Add(Formalin);
            result.Add(BPlusFixative);
            result.Add(Cytolyt);
            result.Add(PreservCyt);
            result.Add(NotApplicable);
            result.Add(NinetyFivePercentIPA);
            result.Add(EDTA);
            result.Add(SodiumHeparin);
            result.Add(ACD);
            result.Add(FreshInRPMI);
            result.Add(MichelsTransport);
            return result;
        }
    }
}
