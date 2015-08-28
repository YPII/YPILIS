using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Gross
{
    public class TemplateNotFound : DictationTemplate
    {
        public TemplateNotFound()
        {
            this.m_TemplateName = "Template Not Found.";            
        }
    }

    public class ProstateNeedleCoreTemplate : DictationTemplate
    {
        public ProstateNeedleCoreTemplate()
        {
            this.m_TemplateName = "Prostate Specimen";
            this.m_Text = "Specimen *SPECIMENNUMBER* is received in a B-plus-filled container labeled \"*PATIENTNAME*\"" +
                " and consists of *NUMBEROFFRAGMENTS* tan-pink cylindrical tissue fragments *MEASURING/AGGREGATING*. " +
                "The specimen is submitted entirely in *COLOR* cassette \"*CASSETTENUMBER*\".";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", "*SPECIMENNUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Patient Name", "*PATIENTNAME*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number Of Fragments", "*NUMBEROFFRAGMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measuring/Agregating", "*MEASURING/AGGREGATING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));
            
            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy nb = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateNeedleBiopsy();                        
            this.m_SpecimenCollection.Add(nb);            
        }
    }

    public class ProstateTURTemplate : DictationTemplate
    {
        public ProstateTURTemplate()
        {
            this.m_TemplateName = "Prostate Specimen";
            this.m_Text = "Specimen *SPECIMENNUMBER* is received fresh container labeled \"*PATIENTNAME*\"" +
                " and consists of (*NUMBEROFFRAGMENTS*) tan-pink irregularly shaped, rough and ragged tissue fragments(s) weighing in aggregate *WEIGHING*, and measuring/aggregating to *MEASURING/AGGREGATING*. " +
                "The specimen is submitted *ENTIRELY/PARTIALLY* in (*COLOR*) cassette \"*CASSETTENUMBER*\"";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", "*SPECIMENNUMBER*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Patient Name", "*PATIENTNAME*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Weight Of Fragments", "*WEIGHING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number Of Fragments", "*NUMBEROFFRAGMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measuring/Agregating", "*MEASURING/AGGREGATING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ProstateTUR tur = new Business.Specimen.Model.SpecimenDefinition.ProstateTUR();
            this.m_SpecimenCollection.Add(tur);
        }
    }

    public class GITemplate : DictationTemplate
    {
        public GITemplate()
        {
            this.m_TemplateName = "GI Specimen";
            this.m_Text = "The specimen *SPECIMENNUMBER* is received in one formalin filled container labeled \"*PATIENTNAME*\"" +
                " and consists of *NUMBEROFFRAGMENTS* fragments of tan-pink tissue measuring in aggregate *MEASURING/AGGREGATING*. " +
                "The specimen is filtered through a fine mesh bag and entirely submitted in *COLOR* cassette \"*CASSETTENUMBER*\".";
            
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number Of Fragments", "*NUMBEROFFRAGMENTS*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measuring/Agregating", "*MEASURING/AGGREGATING*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", "*COLOR*"));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", "*CASSETTENUMBER*"));
            
            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GIBiopsy gi = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GIBiopsy();                        
            this.m_SpecimenCollection.Add(gi);            
        }
    }

    public class AppendixExcisionTemplate : DictationTemplate
    {
        public AppendixExcisionTemplate()
        {
            this.m_TemplateName = "Appendix Excision";
            this.m_Text = "Specimen number: *SPECIMENNUMBER* Received: *FORMALIN/FRESH* Labeled: \"*PATIENTNAME*\"" + Environment.NewLine +
                          "Condition: *OPENED/UNOPENED* Measuring: *MEASUREMENTS*" + Environment.NewLine +
                          "Serosal Surface: *DESCRIPTION*" + Environment.NewLine +
                          "Mesoappendix: *DESCRIPTION/MEASUREMENTS*" + Environment.NewLine +
                          "Pertinent Abnormalities: *DESCRIPTION*" + Environment.NewLine +
                          "Luminal Contents: *DESCRIPTION*" + Environment.NewLine +
                          "Fecalith: *PRESENT/ABSENT/DESCRIPTION*" + Environment.NewLine +
                          "Submitted: *CASSETTE/COLOR*";

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Opened or Unopened", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosal Surface", ""));            
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Attached Mesoappendix: Measurements and Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Pertinent Abnormalities", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Luminal Contents", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Fecalith Present or Absent: Measurements and Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision appendixExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.AppendixExcision();
            this.m_SpecimenCollection.Add(appendixExcision);            
        }
    }
   
    public class SkinShavePunchMiscTemplate : DictationTemplate
    {
        public SkinShavePunchMiscTemplate()
        {
            this.m_TemplateName = "Skin Shave Punch and Misc Biopsy";
            this.m_Text = "Specimen: *SPECIMENNUMBER* Received: *INFORMALIN/FRESH* Labeled: \"*PATIENTNAME*\"" + Environment.NewLine +
                          "Specimen Description: *SHAVE/PUNCH/MISC*" + Environment.NewLine +
                          "Measurements: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *ENTIRELY/BIS/TRI/SERIALLY* Cassette: *COLOR/NUMBER*";
                

            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number and Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Sectioning", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavePunchMiscBiopsy SkinShavePunchMiscBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinShavePunchMiscBiopsy();
            this.m_SpecimenCollection.Add(SkinShavePunchMiscBiopsy); 

        }
    }
    
    public class SkinExcisionTemplate : DictationTemplate
    {
        public SkinExcisionTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy";
            this.m_Text = "Specimen: *SPECIMENNUMBER* Received: *INFORMALIN/FRESH* Labeled: \"*PATIENTNAME*\"" + Environment.NewLine +
                "Excision Description: *OREINTED/UNORIENTED/INKED*" + Environment.NewLine +
                          "Measurements: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *ENTIRELY/BIS/TRI/SERIALLY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Number and Description with inking", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Sectioning", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionBiopsy skinexcisionBiopsy = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionBiopsy();
            this.m_SpecimenCollection.Add(skinexcisionBiopsy);

        }
    }

    public class GallbladderExcisionTemplate : DictationTemplate
    {
        public GallbladderExcisionTemplate()
        {
            this.m_TemplateName = "Gallbladder Excision";
            this.m_Text = "Specimen: *SPECIMENNUMBER* Received: *INFORMALIN/FRESH* Labeled: \"*PATIENTNAME*\"" + Environment.NewLine +
                          "Loose Stones: *YES/NO/QUANTITY/DESCRIPTION*" + Environment.NewLine +
                          "Received: *OPENED/UNOPENED*" + Environment.NewLine +
                          "Measurements: *MEASUREMENTS*" + Environment.NewLine +
                          "Serosal Surface: *SEROSALDESCRIPTION*" + Environment.NewLine +
                          "Adventitial Surface: *ADVENTITIALDESCRIPTION*" + Environment.NewLine +
                          "Mural Defects: *HOLES/PERFORATIONS/DIMENSIONS*" + Environment.NewLine +
                          "Luminal Contents: *BILE/BLOOD/STONES/ETC*" + Environment.NewLine +
                          "Mucosal Surface: *DESCRIPTION*, Wall Thickness: *MEASUREMENTS*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Loose Stones", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Opened or Unopened", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Gallbladder Measurments", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Serosal Surface", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adventitial Surface", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Mural Defects", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Luminal Contents", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Mucosal Surface", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Wall Thickness", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GallbladderExcision gallbladderExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.GallbladderExcision();
            this.m_SpecimenCollection.Add(gallbladderExcision);

        }
    }

        public class TonsilExcisionTemplate : DictationTemplate
    {
        public TonsilExcisionTemplate()
        {
            this.m_TemplateName = "Tonsil Excision";
            this.m_Text = "Specimen: *SPECIMENNUMBER* Received: *INFORMALIN/FRESH* Labeled: \"*PATIENTNAME*\"" + Environment.NewLine +
                          "Received: *TONSILDESCRIPTION*" + Environment.NewLine +
                          "Adenoids: *PRESENT/ABSENT*" + Environment.NewLine +
                          "Weight: *AGGREGATEWEIGHT*" + Environment.NewLine +
                          "Measurement Tonsil 1: *MEASUREMENTS*" + Environment.NewLine +
                          "Measurement Tonsil 2: *MEASUREMENTS*" + Environment.NewLine +
                          "[Measurement of Adenoids: *MEASUREMENTS*]" + Environment.NewLine +
                          "Cut Surfaces: *SMOOTH/TAN/PINK/GLISTENING*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Tonsil Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adenoids Present or Absent", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Aggregate Weight", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("First Tonsil Measurement", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Second Tonsil Measurement", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adenoid Measurement", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cut Surfaces", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilExcision TonsilExcision = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.TonsilExcision();
            this.m_SpecimenCollection.Add(TonsilExcision);

        }
    }

                    public class POCTemplate : DictationTemplate
    {
        public POCTemplate()
        {
            this.m_TemplateName = "POC";
            this.m_Text = "Specimen: *SPECIMENNUMBER* Received: *INFORMALIN/FRESH* Labeled: \"*PATIENTNAME*\"" + Environment.NewLine +
                          "Received: *TISSUE/MUCUS/CLOTTEDBLOOD*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Villi: *PRESENT/ABSENT/INDETERMINATE*" + Environment.NewLine +
                          "Fetal Parts: *PRESENT/ABSENT/INDETERMINATE*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Tissue Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Villi", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Fetal Parts", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.POC POC = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.POC();
            this.m_SpecimenCollection.Add(POC);

        }
    }

                    public class BreastReductionTemplate : DictationTemplate
    {
        public BreastReductionTemplate()
        {
            this.m_TemplateName = "BreastReduction";
            this.m_Text = "Specimen: *SPECIMENNUMBER* Received: *INFORMALIN/FRESH* Labeled: \"*PATIENTNAME*\"" + Environment.NewLine +
                          "Received: *NUMBER/SKIN/ADIPOSE*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Skin Surface: *DESCRIPTION*" + Environment.NewLine +
                          "Adipose: *DESCRIPTION*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Tissue Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Skin Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adipose Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.BreastReduction BreastReduction = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.BreastReduction();
            this.m_SpecimenCollection.Add(BreastReduction);

        }
    }

                    public class ECCTemplate : DictationTemplate
    {
        public ECCTemplate()
        {
            this.m_TemplateName = "ECC";
            this.m_Text = "Specimen: *SPECIMENNUMBER* Received: *INFORMALIN/FRESH* Labeled: \"*PATIENTNAME*\"" + Environment.NewLine +
                          "Received: *NUMBER/SKIN/ADIPOSE*" + Environment.NewLine +
                          "Measurement: *MEASUREMENTS*" + Environment.NewLine +
                          "Skin Surface: *DESCRIPTION*" + Environment.NewLine +
                          "Adipose: *DESCRIPTION*" + Environment.NewLine +
                          "Submitted: *SUBMISSIONKEY* Cassette: *COLOR/NUMBER*";


            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Specimen Number", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Recieved In", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Container Labeling", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Tissue Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Measurements", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Skin Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Adipose Description", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Submission Key", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Color", ""));
            this.m_WordList.Add(new YellowstonePathology.UI.Gross.TemplateWord("Cassette Number", ""));

            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ECC ECC = new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ECC();
            this.m_SpecimenCollection.Add(ECC);
}