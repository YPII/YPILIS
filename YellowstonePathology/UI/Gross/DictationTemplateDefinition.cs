using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Specimen.Model;
using YellowstonePathology.Business.Test;

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
            this.m_TemplateName = "Prostate Needle Core";
            this.m_Text = "[identifier] and consists of [number] tan-pink cylindrical tissue fragments measuring [measurement] in aggregate.  " +
                "The specimen is [cassettesummary].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen nb = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNPRSTTNDLBPSY"); // Definition.ProstateNeedleBiopsy();
            this.m_SpecimenCollection.Add(nb);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class ProstateTURTemplate : DictationTemplate
    {
        public ProstateTURTemplate()
        {
            this.m_TemplateName = "Prostate TUR";
            this.m_Text = "[identifiernodescription] and consists of [number] irregular ragged tan-pink tissue fragment[?s?] " +
                "weighing [weight] grams and measuring [measurement] [?in aggregate?].  [representativesections].  ";                

            YellowstonePathology.Business.Specimen.Model.Specimen tur = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNPRSTTTUR"); // Definition.ProstateTUR();
            this.m_SpecimenCollection.Add(tur);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            
            string result = base.ReplaceIdentifierNoDescription(this.m_Text, specimenOrder, accessionOrder);
            result = base.AppendInitials(result, specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }*/
    }

    public class BladderTURTemplate : DictationTemplate
    {
        public BladderTURTemplate()
        {
            this.m_TemplateName = "Bladder TUR";
            this.m_Text = "[identifiernodescription] and consists of [number] irregular ragged tan-pink tissue fragment[?s?] " +
                "measuring [measurement] [?in aggregate?]. The specimen is filtered through a fine mesh bag and [cassettesummary].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen turb = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNBLDDRTTUR"); // Definition.BladderTUR();
            this.m_SpecimenCollection.Add(turb);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            
            string result = base.ReplaceIdentifierNoDescription(this.m_Text, specimenOrder, accessionOrder);
            result = base.AppendInitials(result, specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class GITemplate : DictationTemplate
    {
        public GITemplate()
        {
            this.m_TemplateName = "GI Specimen";
            this.m_Text = "[identifier] and consists of [number] tan-pink tissue fragment[?s?] measuring [measurement] [?in aggregate?].  " +
                "The specimen is filtered through a fine mesh bag and [cassettesummary].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen gi = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("GISPCMN"); // Definition.GIBiopsy();
            this.m_SpecimenCollection.Add(gi);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class ConsultTemplate : DictationTemplate
    {
        public ConsultTemplate()
        {
            this.m_TemplateName = "Consult";
            this.m_Text = "Received in consultation from [physicianname] ([clientname] - [clientcitystate]) are [clientaccessionedslidecount] and [blockcount] labeled [clientaccession] for patient \"[patientname]\".  ";

            YellowstonePathology.Business.Specimen.Model.Specimen consult = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("CNSLT"); // Definition.Consult();
            this.m_SpecimenCollection.Add(consult);
        }

        /* WHC NEEDS WORK public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            
            string result = this.m_Text.Replace("[physicianname]", accessionOrder.PhysicianName);
            result = result.Replace("[clientaccession]", accessionOrder.ClientAccessionNo);
            result = result.Replace("[patientname]", accessionOrder.PatientDisplayName);
            result = result.Replace("[blockcount]", specimenOrder.AliquotOrderCollection.GetBlockCountString().ToString());
            result = result.Replace("[clientaccessionedslidecount]", specimenOrder.AliquotOrderCollection.GetClientAccessionedSlideOrderCountString());
            result = base.ReplaceIdentifier(result, specimenOrder, accessionOrder);

            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);
            result = result.Replace("[clientname]", client.ClientName);
            result = result.Replace("[clientcitystate]", client.City + ", " + client.State);
            return result;
        }*/
    }

    public class InitialReadingTemplate : DictationTemplate
    {
        public InitialReadingTemplate()
        {
            this.m_TemplateName = "Consult Initial Reading";
            this.m_Text = "Received from [physicianname] ([clientname] - [clientcitystate]) are [clientaccessionedslidecount] and [blockcount] labeled [clientaccession] for patient \"[patientname]\" for pathologist interpretation. ";

            YellowstonePathology.Business.Specimen.Model.Specimen initialReading = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("NTLRDNG"); // Definition.InitialReading();
            this.m_SpecimenCollection.Add(initialReading);
        }

        /* WHC NEEDS WORK public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = this.m_Text.Replace("[physicianname]", accessionOrder.PhysicianName);
            result = result.Replace("[clientaccession]", accessionOrder.ClientAccessionNo);
            result = result.Replace("[patientname]", accessionOrder.PatientDisplayName);
            result = result.Replace("[blockcount]", specimenOrder.AliquotOrderCollection.GetBlockCountString().ToString());
            result = result.Replace("[clientaccessionedslidecount]", specimenOrder.AliquotOrderCollection.GetClientAccessionedSlideOrderCountString().ToString());
            result = base.ReplaceIdentifier(result, specimenOrder, accessionOrder);

            YellowstonePathology.Business.Client.Model.Client client = YellowstonePathology.Business.Gateway.PhysicianClientGateway.GetClientByClientId(accessionOrder.ClientId);
            result = result.Replace("[clientname]", client.ClientName);
            result = result.Replace("[clientcitystate]", client.City + ", " + client.State);
            return result;
        }*/
    }

    public class PeripheralBloodTemplate : DictationTemplate
    {
        public PeripheralBloodTemplate()
        {
            this.m_TemplateName = "Peripheral Blood Smear";
            this.m_Text = "Received are [slidecount] [unstained/stained] peripheral blood smear[?s?] labeled \"[patientname]\" for pathologist review.  ";

            YellowstonePathology.Business.Specimen.Model.Specimen peripheral = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCPBS"); // Definition.Peripheral();
            this.m_SpecimenCollection.Add(peripheral);
        }

        /*WHC NEEDS WORK public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = this.m_Text.Replace("[patientname]", accessionOrder.PatientDisplayName);
            result = result.Replace("[slidecount]", specimenOrder.AliquotOrderCollection.GetSlideCount().ToString());
            return result;
        }*/
    }

    public class BXTemplate : DictationTemplate
    {
        public BXTemplate()
        {
            this.m_TemplateName = "Biopsy Specimen";
            this.m_Text = "[identifier] and consists of [number] tan-pink tissue fragment[?s?] measuring [measurement] [?in aggregate?].  " +
                "The specimen is filtered through a fine mesh bag and [cassettesummary].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen bx = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("BXSPCMN"); // Definition.Biopsy();
            this.m_SpecimenCollection.Add(bx);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class FluidTemplate : DictationTemplate
    {
        public FluidTemplate()
        {
            this.m_TemplateName = "Fluid Specimen";
            this.m_Text = "[Specimen] is received in CytoLyt in a container labeled \"[patientname]\" and consists of [Quantity] ml of [Color] fluid [?with particulate?].  The specimen is submitted for selective cellular enhancement processing.[cellblock]";
            
            YellowstonePathology.Business.Specimen.Model.Specimen fluid = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMFLUID"); // Definition.Fluid();
            YellowstonePathology.Business.Specimen.Model.Specimen urine = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMNURINE"); // Definition.Urine();
            YellowstonePathology.Business.Specimen.Model.Specimen fna = Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SPCMFNA"); // Definition.FNA();

            this.m_SpecimenCollection.Add(fluid);
            this.m_SpecimenCollection.Add(urine);
            this.m_SpecimenCollection.Add(fna);

        }

        /* WHC NEEDS WORK public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string identifier = accessionOrder.PatientDisplayName;            
            if(specimenOrder.AliquotOrderCollection.HasCellBlock() == true)
            {
                this.m_Text += " A cell block was made.";
            }

            this.m_Text = this.m_Text.Replace("[Specimen]", "Specimen " + specimenOrder.SpecimenNumber);
            return this.m_Text.Replace("[fluididentifier]", identifier);
        }*/
    }

    public class FallopianTubeTemplate : DictationTemplate
    {
        public FallopianTubeTemplate()
        {
            this.m_TemplateName = "Fallopian Tube Specimen";
            this.m_Text = "[identifier]." + Environment.NewLine +            	
            	"Fallopian Tube 1:" + Environment.NewLine +
                "  Dimensions:  [measurements] ([w/wo] fimbriated end)" + Environment.NewLine +
                "  Surface:  [description] [paratubal cysts absent/present]" + Environment.NewLine +
                "  Inked:  [color]" + Environment.NewLine +
                "Fallopian Tube 2:" + Environment.NewLine +
                "  Dimensions:  [measurements] ([w/wo] fimbriated end)" + Environment.NewLine +
                "  Surface:  [description] [paratubal cysts absent/present]" + Environment.NewLine +
                "  Inked:  [color]" + Environment.NewLine +
                "Submitted:  [representativesections].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen fallopianTube = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("FTSPCMN"); // Definition.FallopianTube();
            this.m_SpecimenCollection.Add(fallopianTube);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }*/
    }

    public class FallopianTubeAndOvariesTemplate : DictationTemplate
    {
        public FallopianTubeAndOvariesTemplate()
        {
            this.m_TemplateName = "Fallopian Tube and Ovaries Specimen";
            this.m_Text = "[identifier]." + Environment.NewLine +
                Environment.NewLine +
                "Right Fallopian Tube: " + Environment.NewLine +
                "      Dimensions:  [measurements]([w/wo] fimbriated end)" + Environment.NewLine +
                "      Surface:  [description/paratubal cysts absent/present]" + Environment.NewLine +
                "      Inked:  [color]" + Environment.NewLine +
                "Right Ovary:" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Surface:  [description]" + Environment.NewLine +
                "      Cut Surface:  [description]" + Environment.NewLine +
                "Left Fallopian Tube: " + Environment.NewLine +
                "      Dimensions:  [measurements]([w/wo] fimbriated end)" + Environment.NewLine +
                "      Surface:  [description/paratubal cysts absent/present]" + Environment.NewLine +
                "      Inked:  [color]" + Environment.NewLine +
                "Left Ovary:" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Surface:  [description]" + Environment.NewLine +
                "      Cut Surface:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "[summarysubmission].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen fallopianTubeAndOvaries = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("FTOSPCMN"); // Definition.FallopianTubeAndOvaries();
            this.m_SpecimenCollection.Add(fallopianTubeAndOvaries);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSummarySubmission(result, specimenOrder);
            return result;
        }*/
    }


    public class SinusContentTemplate : DictationTemplate
    {
        public SinusContentTemplate()
        {
            this.m_TemplateName = "Sinus Content Specimen";
            this.m_Text = "[identifier] and consists of multiple fragments of tan-pink tissue and bone aggregating to [measurement].  " +
                "They are filtered through a fine mesh bag and [cassettesummary] [?for decalcification prior to processing?].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen sinusContent = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SCSPCMN"); // Definition.SinusContent();
            this.m_SpecimenCollection.Add(sinusContent);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {            
            string result = base.ReplaceIdentifier(this.m_Text, specimenOrder, accessionOrder);
            result = base.AppendInitials(result, specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class AppendixExcisionTemplate : DictationTemplate
    {
        public AppendixExcisionTemplate()
        {
            this.m_TemplateName = "Appendix Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Specimen Integrity:  [description]" + Environment.NewLine +
                "Measurements:  [measurement]" + Environment.NewLine +
                "Serosal Surface:  [description]" + Environment.NewLine +
                "Mesoappendix:  [measurement], [description]" + Environment.NewLine +
                "Pertinent Abnormalities:  [description]" + Environment.NewLine +
                "Luminal Contents:  [description]" + Environment.NewLine +
                "Fecaliths:  [description] [measurement], [description]" + Environment.NewLine +
                "Submitted:  [representativesections].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen appendixExcision = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("APPNDXXCSN"); // Definition.AppendixExcision();
            this.m_SpecimenCollection.Add(appendixExcision);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }*/
    }

    public class SkinShavePunchMiscTemplate : DictationTemplate
    {
        public SkinShavePunchMiscTemplate()
        {
            this.m_TemplateName = "Skin Shave Punch and Misc Biopsy";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Measurements:  [measurements]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen skinShavePunchMiscBiopsy = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SKSHPHMSSPCMN"); // Definition.SkinShavePunchMiscBiopsy();
            this.m_SpecimenCollection.Add(skinShavePunchMiscBiopsy);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }*/
    }

    public class SkinShavewithCurettingsTemplate : DictationTemplate
    {
        public SkinShavewithCurettingsTemplate()
        {
            this.m_TemplateName = "Skin Shave with Curettings Biopsy";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Measurements:  [measurements]; Curettings: [measurement]" + Environment.NewLine +
                "Submitted:  [curettingssubmitted].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen skinShavewithCurettingsBiopsy = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SKSHCSPCMN"); // Definition.SkinShavewithCurettingsBiopsy();
            this.m_SpecimenCollection.Add(skinShavewithCurettingsBiopsy);

            this.m_UseAppendInitials = true;
        }

        /* WHC NEEDS WORK public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            //string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);

            string result = this.ReplaceIdentifier(this.m_Text, specimenOrder, accessionOrder);
            if (specimenOrder.AliquotOrderCollection.Count == 1)
            {
                result = result.Replace("[curettingssubmitted]", "Entirely submitted in cassette \"" + specimenOrder.AliquotOrderCollection[0].Label + "\"");
            }
            else if(specimenOrder.AliquotOrderCollection.Count == 2)
            {
                result = result.Replace("[curettingssubmitted]", "Shave [procedure] and submitted in cassette \"" + specimenOrder.SpecimenNumber + "A\".  " + "The curettings are filtered through a fine mesh bag and entirely submitted in cassette \"" + specimenOrder.AliquotOrderCollection.GetLastBlock().Label + "\"");                              
            }
            else
            {
                result = "This template only works with 2 blocks.";
            }
            
            result = this.AppendInitials(result, specimenOrder, accessionOrder, systemIdentity);
            this.m_Text = result;
            return result;
        }*/
    }

    public class SkinExcisionUnorientedTemplate : DictationTemplate
    {
        public SkinExcisionUnorientedTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy, Unoriented";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                "Gross Description:  Unoriented excision." + Environment.NewLine +
                "Measurements:  [measurements]" + Environment.NewLine +
                "Inking:  [color]" + Environment.NewLine +
                "Sectioning:  [description]" + Environment.NewLine +
                "Submitted:  [tipssubmitted].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen skinExcisionUnorientedBiopsy = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SKEXUOSPCMN"); // Definition.SkinExcisionUnorientedBiopsy();
            this.m_SpecimenCollection.Add(skinExcisionUnorientedBiopsy);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceTipsSubmitted(result, specimenOrder);
            return result;
        }*/
    }

    public class SkinExcisionOrientedTemplate : DictationTemplate
    {
        public SkinExcisionOrientedTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy, Oriented";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                "Gross Description:  Oriented excision." + Environment.NewLine +
                "Measurements:  [measurements]" + Environment.NewLine +
                "Orientation:  [designation]" + Environment.NewLine +
                "Inking:  12 to 3 o'clock = blue; 3 to 6 o'clock = red; 6 to 9 o'clock = green; 9 to 12 o'clock = orange; deep = black.  " + Environment.NewLine +
                "Sectioning:  [description]" + Environment.NewLine +
                "Submitted:  [tipssubmitted].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen skinExcisionOrientedBiopsy = SpecimenCollection.Instance.GetSpecimen("SKEXOSPCMN"); //new YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.SkinExcisionOrientedBiopsy();
            this.m_SpecimenCollection.Add(skinExcisionOrientedBiopsy);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceTipsSubmitted(result, specimenOrder);
            return result;
        }*/
    }
    
        public class SkinExcisionUnorientedwithCurettingsTemplate : DictationTemplate
    {
        public SkinExcisionUnorientedwithCurettingsTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy with Curettings, Unoriented";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                "Gross Description:  Unoriented excision with curettings." + Environment.NewLine +
                "Measurements:  [measurements]; Curettings: [measurements]" + Environment.NewLine +
                "Inking:  [color]" + Environment.NewLine +
                "Sectioning:  [description]" + Environment.NewLine +
                "Submitted:  [tipssubmittedwithcurettings].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen skinExcisionUnorientedwithCurettingsBiopsy = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SKEXUOCSPCMN"); // Definition.SkinExcisionUnorientedwithCurettingsBiopsy();
            this.m_SpecimenCollection.Add(skinExcisionUnorientedwithCurettingsBiopsy);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceTipsSubmittedWithCurettings(result, specimenOrder);
            return result;
        }*/
    }

    public class SkinExcisionOrientedwithCurettingsTemplate : DictationTemplate
    {
        public SkinExcisionOrientedwithCurettingsTemplate()
        {
            this.m_TemplateName = "Skin Excision Biopsy with Curettings, Oriented";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                "Gross Description:  Oriented excision with curettings." + Environment.NewLine +
                "Measurements:  [measurements]; Curettings: [measurements]" + Environment.NewLine +
                "Orientation:  [designation]" + Environment.NewLine +
                "Inking:  12 to 3 o'clock = blue; 3 to 6 o'clock = red; 6 to 9 o'clock = green; 9 to 12 o'clock = orange; deep = black.  " + Environment.NewLine +
                "Sectioning:  [description]" + Environment.NewLine +
                "Submitted:  [tipssubmittedwithcurettings].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen skinExcisionOrientedwithCurettingsBiopsy = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SKEXOCSPCMN"); // Definition.SkinExcisionOrientedwithCurettingsBiopsy();
            this.m_SpecimenCollection.Add(skinExcisionOrientedwithCurettingsBiopsy);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceTipsSubmittedWithCurettings(result, specimenOrder);
            return result;
        }*/
    }

    public class GallbladderExcisionTemplate : DictationTemplate
    {
        public GallbladderExcisionTemplate()
        {
            this.m_TemplateName = "Gallbladder Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Specimen Integrity:  [description]" + Environment.NewLine +
                "Measurements:  [measurement]" + Environment.NewLine +
                "Serosal Surface:  [description]" + Environment.NewLine +
                "Adventitial Surface:  [description]" + Environment.NewLine +
                "Mural Defects:  [description]" + Environment.NewLine +
                "Loose Stones:  [description]" + Environment.NewLine +
                "Luminal Contents:  [description]" + Environment.NewLine +
                "Mucosal Surface:  [description]" + Environment.NewLine +
                "Wall Thickness:  [thickness]" + Environment.NewLine +
                "Submitted:  [representativesections].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen gallbladderExcision = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("GBSPCMN"); // Definition.GallbladderExcision();
            this.m_SpecimenCollection.Add(gallbladderExcision);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }*/
    }

    public class TonsilAdenoidExcisionTemplate : DictationTemplate
    {
        public TonsilAdenoidExcisionTemplate()
        {
            this.m_TemplateName = "Tonsil Adenoid Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Tonsils:  Two tan-pink, lobular, palatine tonsils." + Environment.NewLine +
                "Adenoids:  [description]" + Environment.NewLine +
                "Weight:  [weight] grams." + Environment.NewLine +
                "Measurement Tonsil 1:  [measurement]" + Environment.NewLine +
                "Measurement Tonsil 2:  [measurement]" + Environment.NewLine +
                "Measurement Adenoids:  [measurement]" + Environment.NewLine +
                "Cut Surface:  [description]" + Environment.NewLine +
                "Submitted:  [representativesectionsagerestricted].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen tonsilAdenoidExcision = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("TNSLADSPCM"); // Definition.TonsilAdenoidExcision();
            this.m_SpecimenCollection.Add(tonsilAdenoidExcision);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);            
            result = this.ReplaceRepresentativeSectionsAgeRestricted(result, specimenOrder, accessionOrder);
            return result;
        }*/
    }

    public class TonsilExcisionTemplate : DictationTemplate
    {
        public TonsilExcisionTemplate()
        {
            this.m_TemplateName = "Tonsil Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [quantity] tan-pink, lobular, palatine [tonsils]." + Environment.NewLine +
                "Weight:  [weight] grams." + Environment.NewLine +
                "[measurementstring]" + Environment.NewLine +                
                "Cut Surface:  [description]" + Environment.NewLine +
                "Submitted:  [representativesectionsagerestricted].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen tonsilExcision = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("TNSLSPCM"); // Definition.TonsilExcision();
            this.m_SpecimenCollection.Add(tonsilExcision);

            this.m_UseAppendInitials = true;
        }

        /* WHC NEEDS WORK public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
                        
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
            
            result = this.ReplaceRepresentativeSectionsAgeRestricted(result, specimenOrder, accessionOrder);
            return result;
        }*/
    }    

    public class AdenoidExcisionTemplate : DictationTemplate
    {
        public AdenoidExcisionTemplate()
        {
            this.m_TemplateName = "Adenoid Excision";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Weight:  [weight] grams." + Environment.NewLine +
                "Measurement Adenoids:  [measurement]" + Environment.NewLine +
                "Cut Surface:  [description]" + Environment.NewLine +
                "Submitted:  [representativesectionsagerestricted].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen adenoidExcision = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("ADSPCM");
            this.m_SpecimenCollection.Add(adenoidExcision);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSectionsAgeRestricted(result, specimenOrder, accessionOrder);
            return result;
        }*/
    }

    public class POCTemplate : DictationTemplate
    {
        public POCTemplate()
        {
            this.m_TemplateName = "POC";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Villi:  [present/not present]" + Environment.NewLine +
                "Fetal Parts:  [description]" + Environment.NewLine +
                "Submitted:  [representativesections].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen poc = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("POCSPCMN"); // Definition.POC();
            this.m_SpecimenCollection.Add(poc);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }*/
    }

    public class BreastReductionTemplate : DictationTemplate
    {
        public BreastReductionTemplate()
        {
            this.m_TemplateName = "BreastReduction";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [description]" + Environment.NewLine +
                "Weight:  [weight] grams." + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Cut Surface:  [description]" + Environment.NewLine +
                "Submitted:  [representativesections].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen breastReduction = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("BRSTRDSPCMN"); // Definition.BreastReduction();
            this.m_SpecimenCollection.Add(breastReduction);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }*/
    }

    public class ECCTemplate : DictationTemplate
    {
        public ECCTemplate()
        {
            this.m_TemplateName = "ECC";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:   Multiple tan-pink fragments of tissue and mucus." + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Submitted:  Filtered through a fine mesh bag and [cassettesummary].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen ecc = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("ECCSPCMN"); // Definition.ECC();
            this.m_SpecimenCollection.Add(ecc);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class EMBTemplate : DictationTemplate
    {
        public EMBTemplate()
        {
            this.m_TemplateName = "EMB";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  Multiple tan-pink fragments of tissue and mucus." + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Submitted:  Filtered through a fine mesh bag and [cassettesummary].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen emb = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("EMBSPCMN"); // Definition.EMB();
            this.m_SpecimenCollection.Add(emb);

            YellowstonePathology.Business.Specimen.Model.Specimen emc = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("EMCSPCMN"); // Definition.EMC();
            this.m_SpecimenCollection.Add(emc);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class CervicalBiopsyTemplate : DictationTemplate
    {
        public CervicalBiopsyTemplate()
        {
            this.m_TemplateName = "CervicalBiopsy";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [number], tan-pink, rubbery fragment[s]." + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Submitted:  Filtered through a fine mesh bag and [cassettesummary].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen cervicalBiopsy = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("CVCLBSPYSPCMN"); // Definition.CervicalBiopsy();
            this.m_SpecimenCollection.Add(cervicalBiopsy);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class LEEPTemplate : DictationTemplate
    {
        public LEEPTemplate()
        {
            this.m_TemplateName = "LEEP";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [color], [characteristics]" + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Os:  [present/not present], [description/measurement]" + Environment.NewLine +
                "Inking:  [description]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen leep = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("LPSPCMN"); // Definition.LEEP();
            this.m_SpecimenCollection.Add(leep);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }*/
    }
    
    public class CervicalConeTemplate : DictationTemplate
     {
        public CervicalConeTemplate()
        {
            this.m_TemplateName = "CervicalCone";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [color], [characteristics]" + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +
                "Os:  [description], [measurement]" + Environment.NewLine +
                "Inking:  [description]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen cervicalCone = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("CXCNSPCMN"); // Definition.CervicalCone();
            this.m_SpecimenCollection.Add(cervicalCone);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }*/
    }

    public class LEEPPiecesTemplate : DictationTemplate
    {
        public LEEPPiecesTemplate()
        {
            this.m_TemplateName = "LEEPPieces";
            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description:  [color], [characteristics]" + Environment.NewLine +
                "Measurement:  [measurement]" + Environment.NewLine +                
                "Inking:  [description]" + Environment.NewLine +
                "Submitted:  [submitted].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen leepPieces = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("LPPCSPCMN"); // Definition.LEEPPieces();
            this.m_SpecimenCollection.Add(leepPieces);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSubmitted(result, specimenOrder);
            return result;
        }*/
    }

    public class SinglePlacentaTemplate : DictationTemplate
    {
        public SinglePlacentaTemplate()
        {
            this.m_TemplateName = "SinglePlacenta";
            this.m_Text = "[identifier] and consists of a singleton placenta with umbilical cord and attached membranes." + Environment.NewLine +
                Environment.NewLine +
                "Umbilical Cord:" + Environment.NewLine +
                "   Length:  [length]" + Environment.NewLine +
                "   Insertion:  [insertion]" + Environment.NewLine +
                "   Vessels:  [number]" + Environment.NewLine +
                "   Knots:  [description]" + Environment.NewLine +
                "   Coiling:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "Extraplacental Membranes:" + Environment.NewLine +
                "   Insertion:  [insertion]" + Environment.NewLine +
                "   Color:  [color]" + Environment.NewLine +
                "   Amnion Nodosum:  [description]" + Environment.NewLine +
                "   Cysts:  [description]" + Environment.NewLine +
                "   Point of Rupture:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "Placental Disc:" + Environment.NewLine +
                "   Weight:  [weight] grams." + Environment.NewLine +
                "   Shape:  [shape]" + Environment.NewLine +
                "   Measurements:  [measurement]" + Environment.NewLine +
                "   Fetal Surface:" + Environment.NewLine +
                "      Fetal Surface:  [description]" + Environment.NewLine +
                "      Amnion Nodosum:  [description]" + Environment.NewLine +
                "      Cysts:  [description]" + Environment.NewLine +
                "   Maternal Surface:" + Environment.NewLine +
                "      Hemorrhage:  " + Environment.NewLine +
                "         Adherent:  [measurement], [description]" + Environment.NewLine +
                "         Non-Adherent:  [measurement]" + Environment.NewLine +
                "      Maternal Surface:  [description]" + Environment.NewLine +
                "      Surface Infarcts:  [description]" + Environment.NewLine +
                "   Sectioned at 1 cm intervals:" + Environment.NewLine +
                "       Infarcts:  [description]" + Environment.NewLine +
                "       Cysts:  [description]" + Environment.NewLine + 
            	Environment.NewLine +
                "Cassette Summary: " + Environment.NewLine +
                "\"1A\" - Cord from both ends, " + Environment.NewLine +
                "\"1B\" - Membranes, " + Environment.NewLine +
                "\"1C\" - \"1D\" - Central placenta/infarcts.  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen singlePlacenta = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("SNGLPLCNTSPCMN"); // Definition.SinglePlacenta();
            this.m_SpecimenCollection.Add(singlePlacenta);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            return base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
        }*/
    }
    
        public class TwinPlacentaTemplate : DictationTemplate
    {
        public TwinPlacentaTemplate()
        {
            this.m_TemplateName = "TwinPlacenta";
            this.m_Text = "[identifier] and consists of a twin placenta with two umbilical cord and attached membranes." + Environment.NewLine +
                Environment.NewLine +
                "Extraplacental Membranes:" + Environment.NewLine +
            	"   Dividing Membrane:  [present/absent]"+ Environment.NewLine +
                "      Description:  [description]" + Environment.NewLine +
            	"      Intervening chorion:  [present/absent/indeterminate]" + Environment.NewLine +
                "   Color:  [color]" + Environment.NewLine +
                "   Amnion Nodosum:  [description]" + Environment.NewLine +
                "   Cysts:  [description]" + Environment.NewLine +
                "   Point of Rupture:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "Umbilical Cord:" + Environment.NewLine +
            	"   Designations:" + Environment.NewLine +
            	"      Twin A:  [designation]" + Environment.NewLine +
            	"      Twin B:  [designation]" + Environment.NewLine +
                "   Length:" + Environment.NewLine +
            	"      Twin A:  [length]" + Environment.NewLine +
            	"      Twin B:  [length]" + Environment.NewLine +
                "   Insertion:" + Environment.NewLine +
            	"      Twin A:  [insertion]" + Environment.NewLine +
            	"      Twin B:  [insertion]" + Environment.NewLine +
                "   Vessels:" + Environment.NewLine +
            	"      Twin A:  [number]" + Environment.NewLine +
            	"      Twin B:  [number]" + Environment.NewLine +
                "   Knots:" + Environment.NewLine +
            	"      Twin A:  [description]" + Environment.NewLine +
            	"      Twin B:  [description]" + Environment.NewLine +
                "   Coiling:" + Environment.NewLine +
            	"      Twin A:  [description]" + Environment.NewLine +
            	"      Twin B:  [description]" + Environment.NewLine +
                Environment.NewLine +
            	"Placental Disc:" + Environment.NewLine +
                "   Weight:  [weight] grams." + Environment.NewLine +
                "   Shape:  [shape]" + Environment.NewLine +
                "   Measurements:  [measurement]" + Environment.NewLine +
                "   Fetal Surface:" + Environment.NewLine +
                "      Fetal Surface:  [description]" + Environment.NewLine +
                "      Amnion Nodosum:  [description]" + Environment.NewLine +
                "      Cysts:  [description]" + Environment.NewLine +
                "   Maternal Surface:" + Environment.NewLine +
                "      Hemorrhage:  [description]" + Environment.NewLine +
                "         Adherent:  [measurement], [description]" + Environment.NewLine +
                "         Non-Adherent:  [measurement]" + Environment.NewLine +
                "      Maternal Surface:  [description]" + Environment.NewLine +
                "      Surface Infarcts:  [description]" + Environment.NewLine +
                "   Sectioned at 1 cm intervals:" + Environment.NewLine +
                "       Infarcts:  [description]" + Environment.NewLine +
                "       Cysts:  [description]" + Environment.NewLine + 
            	Environment.NewLine +
                "Cassette Summary: " + Environment.NewLine +
                "\"1A\" - Twin A umbilical cord and membranes, " + Environment.NewLine +
                "\"1B\" - \"1D\" - Twin A central placenta, " + Environment.NewLine +            	
            	"\"1E\" -Twin B umbilical cord and membranes" + Environment.NewLine +
                "\"1F\" - \"1I\" - Twin B central placenta" + Environment.NewLine +
                "[?\"1G\" - Dividing membranes ?]";

            YellowstonePathology.Business.Specimen.Model.Specimen twinPlacenta = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("TWNPLCNTWMSPCMN"); // Definition.TwinPlacenta();
            this.m_SpecimenCollection.Add(twinPlacenta);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            return base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
        }*/
    }

    public class UterusTemplate : DictationTemplate
    {
        public UterusTemplate()
        {
            this.m_TemplateName = "Uterus";
            this.m_Text = "[identifier]." + Environment.NewLine +                
                Environment.NewLine +
                "Uterine Corpus:" + Environment.NewLine +
                "   Weight:  [weight] grams." + Environment.NewLine +
                "   Dimensions:  [measurements]" + Environment.NewLine +
                "Serosa: [description]" + Environment.NewLine +
                "Cervix: [description]" + Environment.NewLine +
                "   Measurement:  [measurement] " + Environment.NewLine +
                "   Os:  [description], [measurement]" + Environment.NewLine +                
                "Sectioning of Uterus and Cervix:" + Environment.NewLine +
                "   Endocervical Canal:  [description]" + Environment.NewLine +
                "   Endometrial Cavity: " + Environment.NewLine +
                "      Description:  [description]" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Endometrial Thickness:  [thickness]" + Environment.NewLine +                
                "   Myometrium:" + Environment.NewLine +
                "      Myometrial Thickness:  [thickness]" + Environment.NewLine +                
                "      Lesions in Myometrium:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "[summarysubmission].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen uterus = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("UTRSSPCMN"); // Definition.Uterus();
            this.m_SpecimenCollection.Add(uterus);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSummarySubmission(result, specimenOrder);
            return result;
        }*/
    }

    public class UterusAdnexaTemplate : DictationTemplate
    {
        public UterusAdnexaTemplate()
        {
            this.m_TemplateName = "Uterus with Adnexa";
            this.m_FontSize = 16;
            this.m_Text = "[identifier] and consists of [description]." + Environment.NewLine +
                Environment.NewLine +
                "Right Adnexal Organs:" + Environment.NewLine +
                "   Fallopian Tube: " + Environment.NewLine +
                "      Dimensions:  [measurements]([w/wo] fimbriated end)" + Environment.NewLine +
                "      Surface:  [description/paratubal cysts absent/present]" + Environment.NewLine +
                "      Inked:  [color]" + Environment.NewLine +
                "   Ovary:" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Surface:  [description]" + Environment.NewLine +
                "      Cut Surface:  [description]" + Environment.NewLine +
                "Left Adnexal Organs:" + Environment.NewLine +
                "   Fallopian Tube: " + Environment.NewLine +
                "      Dimensions:  [measurements]([w/wo] fimbriated end)" + Environment.NewLine +
                "      Surface:  [description/paratubal cysts absent/present]" + Environment.NewLine +
                "      Inked:  [color]" + Environment.NewLine +
                "   Ovary:" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Surface:  [description]" + Environment.NewLine +
                "      Cut Surface:  [description]" + Environment.NewLine +
                "Uterine Corpus:" + Environment.NewLine +
                "   Weight:  [weight] grams." + Environment.NewLine +
                "   Dimensions: [measurements]" + Environment.NewLine +
                "   Serosa: [description]" + Environment.NewLine +
                "Cervix: [description]" + Environment.NewLine +
                "   Measurement:  [measurement] " + Environment.NewLine +
                "   Os:  [description], [measurement]" + Environment.NewLine +                
                "Sectioning of Uterus and Cervix:" + Environment.NewLine +
                "   Endocervical Canal:  [description]" + Environment.NewLine +
                "   Endocervical Mucosa:  [description]" + Environment.NewLine +
                "   Endometrial Cavity:" + Environment.NewLine +
                "      Description:  [description]" + Environment.NewLine +
                "      Dimensions:  [measurement]" + Environment.NewLine +
                "      Endometrial Thickness:  [thickness]" + Environment.NewLine +                
                "   Myometrium:" + Environment.NewLine +
                "      Myometrial Thickness:  [thickness]" + Environment.NewLine +                
                "      Lesions in Myometrium:  [description]" + Environment.NewLine +
                Environment.NewLine +
                "[summarysubmission].  ";
            

            YellowstonePathology.Business.Specimen.Model.Specimen uterusAdnexa = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("UTRADNSPCMN"); // Definition.UterusAdnexa();
            this.m_SpecimenCollection.Add(uterusAdnexa);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceSummarySubmission(result, specimenOrder);
            return result;
        }*/
    }

    public class NeedleCoreBiopsyTemplate : DictationTemplate
    {
        public NeedleCoreBiopsyTemplate()
        {
            this.m_TemplateName = "Needle Core Biopsy";
            this.m_Text = "[identifier] and consists of [number] [color] threadlike tissue fragments measuring [measurement] in aggregate.  " +
                "They are filtered through a fine mesh bag and [cassettesummary].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen needleCoreBiopsy = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("NCBSPCMN"); // Definition.NeedleCoreBiopsy();
            this.m_SpecimenCollection.Add(needleCoreBiopsy);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceCassetteSummary(result, specimenOrder);
            return result;
        }*/
    }

    public class AorticValveTemplate : DictationTemplate
    {
        public AorticValveTemplate()
        {
            this.m_TemplateName = "Aortic Valve";            

            this.m_Text = "[identifier]." + Environment.NewLine +
                "Gross Description: [description]" + Environment.NewLine +
                "Measurements: [measurements]" + Environment.NewLine +
                "Calcification: [present/not present]" + Environment.NewLine +
                "Vegetation: [present/not present]" + Environment.NewLine +
                "Submitted: [representativesections] [?for decalcification prior to processing?].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen aorticValve = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("AVSPCMN"); //SpecimenDefinition.AorticValve();
            this.m_SpecimenCollection.Add(aorticValve);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }*/
    }

    public class MitralValveTemplate : DictationTemplate
    {
        public MitralValveTemplate()
        {
            this.m_TemplateName = "Mitral Valve";            

            this.m_Text = "[identifier]." + Environment.NewLine +
               "Leaflets Description:  [description]" + Environment.NewLine +
               "Measurements:  [measurements]" + Environment.NewLine +
               "Chordae:  [description]" + Environment.NewLine +               
               "Submitted:  [representativesections] [?for decalcification prior to processing?].  ";

            YellowstonePathology.Business.Specimen.Model.Specimen mitralValve = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("MVSPCMN"); // Definition.MitralValve();
            this.m_SpecimenCollection.Add(mitralValve);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(result, specimenOrder);
            return result;
        }*/
    }

    public class KneeTissueTemplate : DictationTemplate
    {
        public KneeTissueTemplate()
        {
            this.m_TemplateName = "Knee Tissue";
            this.m_Text = "[identifier] and consists of [number] fragments of soft tissue and bone aggregating to [measurement].  " +
                "Recognizable tibial plateau and femoral condyles [are/not] identified. The articular surface shows [description] eburnation" +
                "and [description] osteophyte formation. Sectioning reveals tan-yellow trabecular bone.  " + 
                "Sections are not submitted for microscopic evaluation.  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen kneeTissue = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("KTSPCMN"); // Definition.KneeTissue();
            this.m_SpecimenCollection.Add(kneeTissue);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            return base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
        }*/
    }

    public class FemoralHeadTemplate : DictationTemplate
    {
        public FemoralHeadTemplate()
        {
            this.m_TemplateName = "Femoral Head";
            this.m_Text = "[identifier] and consists of a [description] femoral head with attached femoral neck measuring [measurement].  " +
                "The femoral neck margin is [description]. The articular surface shows [percent] eburnation and [percent] osteophyte formation.  " +
                "Sectioning reveals tan-yellow trabecular bone. [representativesections] [?following overnight decalcification?].  ";            

            YellowstonePathology.Business.Specimen.Model.Specimen femoralHead = YellowstonePathology.Business.Specimen.Model.SpecimenCollection.Instance.GetSpecimen("FHSPCMN"); // Definition.FemoralHead();
            this.m_SpecimenCollection.Add(femoralHead);

            this.m_UseAppendInitials = true;
        }

        /*public override string BuildResultText(SpecimenOrder specimenOrder, AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            string result = base.BuildResultText(specimenOrder, accessionOrder, systemIdentity);
            result = this.ReplaceRepresentativeSections(this.m_Text, specimenOrder);
            return result;
        }*/
    }
}