using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
    public class FlowCommentHelper
    {                
		string m_SpecimenDescription;
		YellowstonePathology.Business.Interface.ILeukemiaLymphomaResult m_LeukemiaLymphomaResult;
		List<YellowstonePathology.Business.Interface.IFlowMarker> m_FlowMarkers;

		public FlowCommentHelper(string specimenDescription, YellowstonePathology.Business.Interface.ILeukemiaLymphomaResult leukemiaLymphomaResult, List<YellowstonePathology.Business.Interface.IFlowMarker> flowMarkers)
        {
            this.m_SpecimenDescription = specimenDescription;
			this.m_LeukemiaLymphomaResult = leukemiaLymphomaResult;
			this.m_FlowMarkers = flowMarkers;
        }

        public void SetInterpretiveComment()
        {
			this.m_LeukemiaLymphomaResult.Impression = string.Empty;

			if (this.m_LeukemiaLymphomaResult.TestResult == "Normal")
            {
                this.SetNormalComment();
            }
            else
            {
				if (this.m_LeukemiaLymphomaResult.CellDescription == "Myeloid Cells")
                {
                    this.SetNormalComment();
                }
                else
                {
                    this.SetAbnormalComment();
                }
            }
        }

        public void AddComment(string category, string impression, string comment)
        {
            if (category == "Myeloid Cells")
            {
				comment = comment.Replace("XCD34X", this.m_LeukemiaLymphomaResult.EGateCD34Percent.ToString() + "%");
				comment = comment.Replace("XCD117X", this.m_LeukemiaLymphomaResult.EGateCD117Percent.ToString() + "%");                
            }
			if (this.m_LeukemiaLymphomaResult.InterpretiveComment == string.Empty)
            {
				this.m_LeukemiaLymphomaResult.InterpretiveComment += comment;
            }
            else
            {
				this.m_LeukemiaLymphomaResult.InterpretiveComment += " " + comment;
            }
			if (this.m_LeukemiaLymphomaResult.Impression == string.Empty)
            {
				this.m_LeukemiaLymphomaResult.Impression += impression;
            }
            else
            {
				this.m_LeukemiaLymphomaResult.Impression += " " + impression;
            }         
        }

        private void SetNormalComment()
        {
            string text = "Immunophenotypic analysis of the received specimen (" + this.m_SpecimenDescription + ") indicates that " +
				 this.m_LeukemiaLymphomaResult.LymphocyteCountPercent.ToString("p") + " of the " + this.m_LeukemiaLymphomaResult.GatingPopulationV2 + " are lymphocytes, " +
				 this.m_LeukemiaLymphomaResult.MonocyteCountPercent.ToString("p") + " are monocytes, " +
				 this.m_LeukemiaLymphomaResult.MyeloidCountPercent.ToString("p") + " are mature myeloid cells, " +
				 this.m_LeukemiaLymphomaResult.DimCD45ModSSCountPercent.ToString("p") + " have dim CD45 expression and moderate side scatter. ";

			this.m_LeukemiaLymphomaResult.InterpretiveComment = text;            
        }

        private void SetAbnormalComment()
        {            
            List<string> sentences = new List<string>();            
            this.SetSentence1(sentences);
            this.SetMarkerExpressionFragment(sentences);
            this.SetSurfaceImmunoglobulinSentence(sentences);
            this.SetEquivocalSentence(sentences);            

            string comment = string.Empty;
            foreach (string s in sentences)
            {
                comment += s.Trim() + ".  ";
            }
			this.m_LeukemiaLymphomaResult.InterpretiveComment = comment;
        }        

        private void SetSentence1(List<string> sentences)
        {            
            string sentence = "Immunophenotypic analysis of the received specimen (SPECIMENDESCRIPTION) identified an abnormal population of XXX-CELL";
			if (this.m_LeukemiaLymphomaResult.TestResult == "Abnormal")
            {
                sentence = sentence.Replace("SPECIMENDESCRIPTION", this.m_SpecimenDescription);
				if (this.m_LeukemiaLymphomaResult.CellDescription == "Lymphocytes")
                {
					sentence = sentence.Replace("XXX-CELL", this.m_LeukemiaLymphomaResult.BTCellSelection);
                }
                else
                {
					sentence = sentence.Replace("XXX-CELL", this.m_LeukemiaLymphomaResult.CellDescription.ToLower());
                }
            }
            sentences.Add(sentence);
        }

        private void SetMarkerExpressionFragment(List<string> sentences)
        {
            string expressesFragment = " that express ";
			foreach (YellowstonePathology.Business.Interface.IFlowMarker marker in this.m_FlowMarkers)
            {
                if (marker.Name != "Kappa" & marker.Name != "Lambda")
                {
                    if (marker.Expresses == 1)
                    {
                        expressesFragment += marker.Name + ", ";
                    }
                }                
            }
            expressesFragment = expressesFragment.Remove(expressesFragment.Length - 2, 2);
            string[] expressionSplit = expressesFragment.Split(',');
            if (expressionSplit.Length > 1)
            {
                string replaceText = ", " + expressionSplit[expressionSplit.Length - 1].Trim();
                expressesFragment = expressesFragment.Replace(replaceText, " and " + expressionSplit[expressionSplit.Length - 1].Trim());
            }

			foreach (YellowstonePathology.Business.Interface.IFlowMarker marker in this.m_FlowMarkers)
            {
                if (marker.Name == "Kappa")
                {
                    if (marker.Expresses == 1)
                    {
                        expressesFragment += " and Ig Kappa light chain restriction";                        
                    }
                }
                if (marker.Name == "Lambda")
                {
                    if (marker.Expresses == 1)
                    {
                        expressesFragment += " and Ig Lambda light chain restriction";                        
                    }
                }                
            }            
                        
            List<string> butNotMarkers = new List<string>();
			foreach (YellowstonePathology.Business.Interface.IFlowMarker marker in this.m_FlowMarkers)
            {
                if (marker.Name != "Kappa" & marker.Name != "Lambda")
                {
                    if (marker.Expresses == 2)
                    {
                        butNotMarkers.Add(marker.Name);
                    }
                }
            }

            if (butNotMarkers.Count > 0)
            {
                expressesFragment += " but not ";
                foreach (string str in butNotMarkers)
                {
                    if (str == butNotMarkers[butNotMarkers.Count - 1])
                    {
                        expressesFragment = expressesFragment.Substring(0, expressesFragment.Length - 2);
                        expressesFragment += " or " + str;
                    }
                    else
                    {
                        expressesFragment += str + ", ";
                    }
                }
            }
            sentences[0] = sentences[0] + expressesFragment;
        }        

        private void SetSurfaceImmunoglobulinSentence(List<string> sentences)
        {
            string sentence = string.Empty;
			foreach (YellowstonePathology.Business.Interface.IFlowMarker marker in this.m_FlowMarkers)
            {
                if (marker.Name == "Lambda" | marker.Name == "Kappa")
                {
                    if (marker.Expresses == 3)
                    {
                        sentences.Add("Surface immunoglobulin is too dim to assess clonality");
                        return;
                    }
                }
            }            
        }        

        private void SetEquivocalSentence(List<string> sentences)
        {            
            List<string> markers = new List<string>();
			foreach (YellowstonePathology.Business.Interface.IFlowMarker marker in this.m_FlowMarkers)
            {
                if (marker.Expresses == 3)
                {
                    if (marker.Name != "Kappa" & marker.Name != "Lambda")
                    {
                        markers.Add(marker.Name);                        
                    }
                }
            }

            string equivocalSentence = string.Empty;
            foreach (string marker in markers)
            {
                equivocalSentence += marker + ", ";
            }

            if (markers.Count > 0)
            {
                equivocalSentence = equivocalSentence.Remove(equivocalSentence.Length - 2, 1);            
            }

            switch (markers.Count)
            {            
                case 1:
                    equivocalSentence += "is equivocal";
                    break;
                default:
                    equivocalSentence += "are equivocal";
                    break;
            }
            if (markers.Count > 0)
            {
                sentences.Add(equivocalSentence);
            }
        }
    }
}
