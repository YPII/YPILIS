using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI.Gross
{
    public class DictationTemplate : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;        

        protected string m_TemplateName;        
        protected string m_Text;                               
        protected YellowstonePathology.Business.Specimen.Model.SpecimenCollection m_SpecimenCollection;
        protected int m_FontSize;
        protected bool m_UseAppendInitials;

        public DictationTemplate()
        {            
            this.m_SpecimenCollection = new YellowstonePathology.Business.Specimen.Model.SpecimenCollection();
            this.m_FontSize = 20;
        }

        [PersistentCollection()]
        public YellowstonePathology.Business.Specimen.Model.SpecimenCollection SpecimenCollection
        {
            get { return this.m_SpecimenCollection; }
            set
            {
                if (this.m_SpecimenCollection != value)
                {
                    this.m_SpecimenCollection = value;
                    this.NotifyPropertyChanged("SpecimenCollection");
                }
            }
        }

        public string RemoveLine(string text, int lineIndex)
        {
            string result = text;
            string[] lines = result.Split('\n');
            if(lines.Length >= lineIndex)
            {
                string lineToRemove = lines[lineIndex] + '\n';
                result = result.Replace(lineToRemove, string.Empty);
            }
            return result;
        }

        public int FontSize
        {
            get { return this.m_FontSize; }
            set
            {
                if (this.m_FontSize != value)
                {
                    this.m_FontSize = value;
                    this.NotifyPropertyChanged("FontSize");
                }
            }
        }

        [PersistentPrimaryKeyProperty(false)]
        public string TemplateName
        {
            get { return this.m_TemplateName; }
            set
            {
                if (this.m_TemplateName != value)
                {
                    this.m_TemplateName = value;
                    this.NotifyPropertyChanged("TemplateName");
                }
            }
        }

        [PersistentProperty()]
        public string Text
        {
            get { return this.m_Text; }
            set 
            {
                if (this.m_Text != value)
                {
                    this.m_Text = value;
                    this.NotifyPropertyChanged("Text");
                }
            }
        }

        [PersistentProperty()]
        public bool UseAppendInitials
        {
            get { return this.m_UseAppendInitials; }
            set
            {
                if (this.m_UseAppendInitials != value)
                {
                    this.m_UseAppendInitials = value;
                    this.NotifyPropertyChanged("UseAppendInitials");
                }
            }
        }

        public string BuildResultText(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = this.m_Text;
            if(result.Contains("[identifier]") == true)
            {
                result = this.ReplaceIdentifier(result, specimenOrder, accessionOrder);
            }
            if(result.Contains("[identifiernodescription]") == true)
            {
                result = this.ReplaceIdentifierNoDescription(this.m_Text, specimenOrder, accessionOrder);
            }
            if (result.Contains("[cassettelabel]") == true)
            {
                result = this.ReplaceCassetteLabel(result, specimenOrder);
            }
            if (result.Contains("[cassettesummary]") == true)
            {
                result = this.ReplaceCassetteSummary(result, specimenOrder);
            }
            if (result.Contains("[representativesectionsagerestricted]") == true)
            {
                result = this.ReplaceRepresentativeSectionsAgeRestricted(result, specimenOrder, accessionOrder);
            }
            if (result.Contains("[representativesections]") == true)
            {
                result = this.ReplaceRepresentativeSections(result, specimenOrder);
            }
            if (result.Contains("[submitted]") == true)
            {
                result = this.ReplaceSubmitted(result, specimenOrder);
            }
            if (result.Contains("[tipssubmitted]") == true)
            {
                result = this.ReplaceTipsSubmitted(result, specimenOrder);
            }
            if (result.Contains("[tipssubmittedwithcurettings]") == true)
            {
                result = this.ReplaceTipsSubmittedWithCurettings(result, specimenOrder);
            }
            if (result.Contains("[curettingssubmitted]") == true)
            {
                result = this.ReplaceCurettingsSubmitted(result, specimenOrder);
            }
            if (result.Contains("[summarysubmission]") == true)
            {
                result = this.ReplaceSummarySubmission(result, specimenOrder);
            }
            if (result.Contains("[Specimen]") == true)
            {
                result = this.ReplaceSpecimen(result, specimenOrder);
            }
            if (result.Contains("[patientname]") == true)
            {
                result = this.ReplacePatientName(result, accessionOrder);
            }
            if (result.Contains("[cellblock]") == true)
            {
                result = this.ReplaceCellBlock(result, specimenOrder);
            }
            if (result.Contains("[physicianname]") == true)
            {
                result = this.ReplacePhysicianName(result, accessionOrder);
            }
            if(result.Contains("[clientaccession]") == true)
            {
                result = this.ReplaceClientAccession(result, accessionOrder);
            }
            if (result.Contains("[blockcount]") == true)
            {
                result = this.ReplaceBlockCount(result, specimenOrder);
            }
            if (result.Contains("[slidecount]") == true)
            {
                result = this.ReplaceSlideCount(result, specimenOrder);
            }
            if (result.Contains("[clientaccessionedslidecount]") == true)
            {
                result = this.ReplaceClientAccessionedSlideCount(result, specimenOrder);
            }
            if (result.Contains("[clientname]") || result.Contains("[clientcitystate]") == true)
            {
                result = this.ReplaceClientNameAddress(result, accessionOrder);
            }
            if(result.Contains("tonsils") == true)
            {
                result = this.ReplaceTonsils(result, accessionOrder, specimenOrder);
            }

            if (this.m_UseAppendInitials == true)
            {
                result = this.AppendInitials(result, specimenOrder, accessionOrder, systemIdentity);
            }

            return result;            
        }

        protected string ReplaceIdentifier(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string identifier = "Specimen " + specimenOrder.SpecimenNumber + " ";
            if (string.IsNullOrEmpty(specimenOrder.ClientFixation) == false && specimenOrder.ClientFixation != YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
            {                
                identifier += "is received in a " + specimenOrder.ClientFixation + " filled container labeled \"" + accessionOrder.PatientDisplayName + " - [description]\"";
            }
            else if (specimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
            {
                identifier += "is received fresh in a container labeled \"" + accessionOrder.PatientDisplayName + " - [description]\"";
            }

            identifier = identifier.Replace("Formalin", "formalin");
            identifier = identifier.Replace("B+ Fixative", "B+ fixative");
            return text.Replace("[identifier]", identifier);
        }

        protected string ReplaceIdentifierNoDescription(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string identifier = "Specimen " + specimenOrder.SpecimenNumber + " ";
            if (string.IsNullOrEmpty(specimenOrder.ClientFixation) == false && specimenOrder.ClientFixation != YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
            {
                identifier += "is received in a " + specimenOrder.ClientFixation + " filled container labeled \"" + accessionOrder.PatientDisplayName + "\"";
            }
            else if (specimenOrder.ClientFixation == YellowstonePathology.Business.Specimen.Model.FixationType.Fresh)
            {
                identifier += "is received fresh in a container labeled \"" + accessionOrder.PatientDisplayName + "\"";
            }

            identifier = identifier.Replace("Formalin", "formalin");
            identifier = identifier.Replace("B+ Fixative", "B+ fixative");
            return text.Replace("[identifiernodescription]", identifier);
        }

        protected string ReplaceCassetteLabel(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[cassettelabel]", "\"" + specimenOrder.SpecimenNumber.ToString() + "A\"");
        }

        protected string ReplaceCassetteSummary(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {            
            return text.Replace("[cassettesummary]", specimenOrder.GetGrossSubmittedInString());
        }

        protected string ReplaceRepresentativeSectionsAgeRestricted(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            string result = null;
            if (accessionOrder.PBirthdate.HasValue == true && YellowstonePathology.Business.Helper.PatientHelper.GetAge(accessionOrder.PBirthdate.Value) >= 13)
            {
                result = specimenOrder.GetRepresentativeSectionsSubmittedIn();
            }
            else
            {
                result = "No sections are submitted for microscopic examination";
            }
            result = text.Replace("[representativesectionsagerestricted]", result);
            return result;
        }

        protected string ReplaceRepresentativeSections(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[representativesections]", specimenOrder.GetRepresentativeSectionsSubmittedIn());
        }

        protected string ReplaceSubmitted(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            string submittedStatement = "[procedure] and " + specimenOrder.GetGrossSubmittedInString();
            return text.Replace("[submitted]", submittedStatement);
        }

        protected string ReplaceTipsSubmitted(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            string statement = null;
            if (specimenOrder.AliquotOrderCollection.Count == 1)
            {
                statement = "Entirely submitted in cassette \"" + specimenOrder.AliquotOrderCollection[0].Label + "\"";
            }
            else
            {
                statement = "Tips submitted in cassette \"" + specimenOrder.SpecimenNumber + "A\" and remainder " + specimenOrder.GetGrossRemainderSubmittedInString();
            }
            return text.Replace("[tipssubmitted]", statement);

        }

        protected string ReplaceTipsSubmittedWithCurettings(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            string statement = null;
            if (specimenOrder.AliquotOrderCollection.Count == 1)
            {
                statement = "Entirely submitted in cassette \"" + specimenOrder.AliquotOrderCollection[0].Label + "\"";
            }
            else
            {
                statement = "Tips submitted in cassette \"" + specimenOrder.SpecimenNumber + "A\", ";
                if (specimenOrder.AliquotOrderCollection.Count >= 2)
                {
                    statement += "remainder of excision in cassettes" + specimenOrder.GetGrossMiddleCassettesSubmittedInString() + ". ";
                    if (specimenOrder.AliquotOrderCollection.Count >= 3)
                    {
                        statement += " The curettings are filtered through a fine mesh bag and entirely submitted in cassette \"" + specimenOrder.AliquotOrderCollection.GetLastBlock().Label + "\"";
                    }                    
                }                
            }
            return text.Replace("[tipssubmittedwithcurettings]", statement);
        }

        protected string ReplaceCurettingsSubmitted(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            string result = text;
            if (specimenOrder.AliquotOrderCollection.Count == 1)
            {
                result = result.Replace("[curettingssubmitted]", "Entirely submitted in cassette \"" + specimenOrder.AliquotOrderCollection[0].Label + "\"");
            }
            else if (specimenOrder.AliquotOrderCollection.Count == 2)
            {
                result = result.Replace("[curettingssubmitted]", "Shave [procedure] and submitted in cassette \"" + specimenOrder.SpecimenNumber + "A\".  " + "The curettings are filtered through a fine mesh bag and entirely submitted in cassette \"" + specimenOrder.AliquotOrderCollection.GetLastBlock().Label + "\"");
            }
            else
            {
                result = "This template only works with 2 blocks.";
            }
            return result;
        }

        protected string ReplaceSummarySubmission(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[summarysubmission]", specimenOrder.GetSummarySubmissionString());
        }

        protected string ReplaceSpecimen(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[Specimen]", "Specimen " + specimenOrder.SpecimenNumber.ToString());
        }

        protected string ReplacePatientName(string text, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return text.Replace("[patientname]", accessionOrder.PatientDisplayName);
        }

        protected string ReplaceCellBlock(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            string result = null;
            if (specimenOrder.AliquotOrderCollection.HasCellBlock() == true)
            {
                result = text.Replace("[cellblock]"," A cell block was made.");
            }
            else
            {
                result = text.Replace("[cellblock]", string.Empty);
            }
            return result;
        }

        protected string ReplacePhysicianName(string text, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return text.Replace("[physicianname]", accessionOrder.PhysicianName);
        }

        protected string ReplaceClientAccession(string text, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            return text.Replace("[clientaccession]", accessionOrder.ClientAccessionNo);
        }

        protected string ReplaceBlockCount(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[blockcount]", specimenOrder.AliquotOrderCollection.GetBlockCountString().ToString());
        }

        protected string ReplaceSlideCount(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[slidecount]", specimenOrder.AliquotOrderCollection.GetSlideCount().ToString());
        }

        protected string ReplaceClientAccessionedSlideCount(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            return text.Replace("[clientaccessionedslidecount]", specimenOrder.AliquotOrderCollection.GetClientAccessionedSlideOrderCountString().ToString());
        }

        protected string ReplaceClientNameAddress(string text, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {            
            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);
            string result = text.Replace("[clientname]", client.ClientName);
            result = result.Replace("[clientcitystate]", client.City + ", " + client.State);
            return result;
        }

        protected string ReplaceTonsils(string text, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder)
        {
            string result = text;
            if (accessionOrder.SpecimenOrderCollection.SpecimenIdCount(specimenOrder.SpecimenId) != 1)
            {
                result = result.Replace("[quantity]", "One");
                result = result.Replace("[tonsils]", "tonsil");
                result = result.Replace("[measurementstring]", "Measurement:  [measurement]");
            }
            else
            {
                string measurementString = "Measurement Tonsil 1:  [measurement]" + Environment.NewLine + "Measurement Tonsil 2:  [measurement]";
                result = result.Replace("[tonsils]", "tonsils");
                result = result.Replace("[quantity]", "Two");
                result = result.Replace("[measurementstring]", measurementString);
            }
            return result;
        }

        protected string AppendInitials(string text, YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = text;
            string initials = null;
            
            if (specimenOrder.AliquotOrderCollection.Count != 0)
            {
                if (accessionOrder.SpecimenOrderCollection.IsLastNonPAPSpecimen(specimenOrder.SpecimenOrderId) == true)
                {
                    int grossVerifiedById = 0;
                    if(specimenOrder.AliquotOrderCollection[0].GrossVerifiedById.HasValue == true)
                    {
                        grossVerifiedById = specimenOrder.AliquotOrderCollection[0].GrossVerifiedById.Value;
                    }
                    string grossedByInitials = "[??]";

                    if (grossVerifiedById != 0)
                    {
                        YellowstonePathology.Business.User.SystemUser grossedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(grossVerifiedById);
                        grossedByInitials = grossedBy.Initials.ToUpper();
                    }

                    string supervisedByInitials = "[??]";         
                    if(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.GPathologistId.HasValue == true)
                    {
                        YellowstonePathology.Business.User.SystemUser supervisedBy = YellowstonePathology.Business.User.SystemUserCollectionInstance.Instance.SystemUserCollection.GetSystemUserById(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.GPathologistId.Value);
                        supervisedByInitials = supervisedBy.Initials.ToUpper();
                    }                               

                    string typedByInitials = systemIdentity.User.Initials.ToLower();

                    if(grossedByInitials != supervisedByInitials)
                    {
                        initials = grossedByInitials + "/" + supervisedByInitials + "/" + typedByInitials;
                    }
                    else
                    {
                        initials = grossedByInitials + "/" + typedByInitials;
                    }
                    
                    result = result + initials;
                }
            }            
            return result;
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }                
    }
}
