using System;
using System.Collections.Generic;
using System.Text;

namespace YellowstonePathology.Business.Helper
{
    public class FlowCommentHelper
    {                
		private string m_SpecimenDescription;
		private YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma m_PanelSetOrderLeukemiaLymphoma;        

		public FlowCommentHelper(string specimenDescription, YellowstonePathology.Business.Test.LLP.PanelSetOrderLeukemiaLymphoma panelSetOrderLeukemiaLymphoma)
        {
            this.m_SpecimenDescription = specimenDescription;
            this.m_PanelSetOrderLeukemiaLymphoma = panelSetOrderLeukemiaLymphoma;
        }

        public void SetInterpretiveComment()
        {
            StringBuilder result = new StringBuilder();

            foreach(Business.Flow.CellPopulationOfInterest cellPopulationOfInterest in this.m_PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.CellPopulationsOfInterest)
            {
                if(this.m_PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.CellPopulationsOfInterest.Count >= 2)
                {
                    result.AppendLine("Cell population of interest: " + cellPopulationOfInterest.Description);
                }
                
                Business.Flow.FlowMarkerCollection flowMarkerCollection = this.m_PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.GetMarkerPanel(cellPopulationOfInterest.Id);
                if (this.m_PanelSetOrderLeukemiaLymphoma.TestResult == "Normal")
                {
                    result.Append(this.GetNormalComment());
                }
                else
                {
                    if (this.m_PanelSetOrderLeukemiaLymphoma.CellDescription == "Myeloid Cells")
                    {
                        result.Append(this.GetNormalComment());
                    }
                    else
                    {
                        result.Append(this.GetAbnormalComment(flowMarkerCollection));
                    }
                }                
            }			

            if(this.m_PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.MarkerExists("CD19") == true)
            {
                Business.Flow.FlowMarkerItem flowMarker = this.m_PanelSetOrderLeukemiaLymphoma.FlowMarkerCollection.GetByMarkerName("CD19");
                if(flowMarker.Interpretation == "Negative")
                {
                    result = result.Replace(" and the B-cells appear to be polytypic", "");
                }
            }

            this.m_PanelSetOrderLeukemiaLymphoma.InterpretiveComment = result.ToString();
        }

        public void AddComment(string category, string impression, string comment)
        {
            if (category == "Myeloid Cells")
            {
				comment = comment.Replace("XCD34X", this.m_PanelSetOrderLeukemiaLymphoma.EGateCD34Percent.ToString() + "%");
				comment = comment.Replace("XCD117X", this.m_PanelSetOrderLeukemiaLymphoma.EGateCD117Percent.ToString() + "%");                
            }
			if (this.m_PanelSetOrderLeukemiaLymphoma.InterpretiveComment == string.Empty)
            {
				this.m_PanelSetOrderLeukemiaLymphoma.InterpretiveComment += comment;
            }
            else
            {
				this.m_PanelSetOrderLeukemiaLymphoma.InterpretiveComment += " " + comment;
            }
			if (this.m_PanelSetOrderLeukemiaLymphoma.Impression == string.Empty)
            {
				this.m_PanelSetOrderLeukemiaLymphoma.Impression += impression;
            }
            else
            {
				this.m_PanelSetOrderLeukemiaLymphoma.Impression += " " + impression;
            }         
        }

        private string GetNormalComment()
        {
            //string text = "Immunophenotypic analysis of the received specimen (" + this.m_SpecimenDescription + ") indicates that " +
            //	 this.m_PanelSetOrderLeukemiaLymphoma.LymphocyteCountPercent.ToString("p") + " of the " + this.m_PanelSetOrderLeukemiaLymphoma.GatingPopulationV2 + " are lymphocytes, " +
            //	 this.m_PanelSetOrderLeukemiaLymphoma.MonocyteCountPercent.ToString("p") + " are monocytes, " +
            //	 this.m_PanelSetOrderLeukemiaLymphoma.MyeloidCountPercent.ToString("p") + " are mature myeloid cells, " +
            //	 this.m_PanelSetOrderLeukemiaLymphoma.DimCD45ModSSCountPercent.ToString("p") + " have dim CD45 expression and moderate side scatter. ";

            List<string> stringList = new List<string>();
            string text = "Immunophenotypic analysis of the received specimen (" + this.LoserCaseFirstLetter(this.m_SpecimenDescription) + ") indicates that ";
            if (this.m_PanelSetOrderLeukemiaLymphoma.LymphocyteCountPercent != 0)
            {
                stringList.Add(this.m_PanelSetOrderLeukemiaLymphoma.LymphocyteCountPercent.ToString("p") + " of the " + this.m_PanelSetOrderLeukemiaLymphoma.GatingPopulationV2 + " are lymphocytes");                
            }
            
            if(this.m_PanelSetOrderLeukemiaLymphoma.MonocyteCountPercent != 0)
            {
                stringList.Add(this.m_PanelSetOrderLeukemiaLymphoma.MonocyteCountPercent.ToString("p") + " are monocytes");                
            }

            if(this.m_PanelSetOrderLeukemiaLymphoma.MyeloidCountPercent != 0)
            {
                stringList.Add(this.m_PanelSetOrderLeukemiaLymphoma.MyeloidCountPercent.ToString("p") + " are mature myeloid cells");                
            }

            if(this.m_PanelSetOrderLeukemiaLymphoma.DimCD45ModSSCountPercent != 0)
            {
                stringList.Add(this.m_PanelSetOrderLeukemiaLymphoma.DimCD45ModSSCountPercent.ToString("p") + " have dim CD45 expression and moderate side scatter");                
            }

            text += this.ConcatStringList(stringList);
            return text;			
        }

        private string GetAbnormalComment(Business.Flow.FlowMarkerCollection flowMarkerCollection)
        {            
            List<string> sentences = new List<string>();            
            this.SetSentence1(sentences);
            this.SetMarkerExpressionFragment(sentences, flowMarkerCollection);
            this.SetSurfaceImmunoglobulinSentence(sentences, flowMarkerCollection);
            this.SetEquivocalSentence(sentences, flowMarkerCollection);            

            string comment = string.Empty;
            foreach (string s in sentences)
            {
                comment += s.Trim() + ".  ";
            }

			return comment;
        }        

        private void SetSentence1(List<string> sentences)
        {            
            string sentence = "Immunophenotypic analysis of the received specimen (SPECIMENDESCRIPTION) identified an abnormal population of XXX-CELL";
			if (this.m_PanelSetOrderLeukemiaLymphoma.TestResult == "Abnormal")
            {
                sentence = sentence.Replace("SPECIMENDESCRIPTION", this.m_SpecimenDescription);
				if (this.m_PanelSetOrderLeukemiaLymphoma.CellDescription == "Lymphocytes")
                {
					sentence = sentence.Replace("XXX-CELL", this.m_PanelSetOrderLeukemiaLymphoma.BTCellSelection);
                }
                else
                {
					sentence = sentence.Replace("XXX-CELL", this.m_PanelSetOrderLeukemiaLymphoma.CellDescription.ToLower());
                }
            }
            sentences.Add(sentence);
        }

        private void SetMarkerExpressionFragment(List<string> sentences, Business.Flow.FlowMarkerCollection flowMarkerCollection)
        {
            string expressesFragment = " that express ";
			foreach (YellowstonePathology.Business.Flow.FlowMarkerItem marker in flowMarkerCollection)
            {
                if (marker.Name != "Kappa" & marker.Name != "Lambda")
                {
                    if (marker.Expresses == 1)
                    {                        
                        if(marker.Predictive == true)
                        {
                            expressesFragment += marker.Intensity + " " + marker.Name + ", ";
                        }
                        else
                        {
                            expressesFragment += marker.Name + ", ";
                        }
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

            foreach (YellowstonePathology.Business.Flow.FlowMarkerItem marker in flowMarkerCollection)
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
            foreach (YellowstonePathology.Business.Flow.FlowMarkerItem marker in flowMarkerCollection)
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

        private void SetSurfaceImmunoglobulinSentence(List<string> sentences, Business.Flow.FlowMarkerCollection flowMarkerCollection)
        {
            string sentence = string.Empty;
            foreach (YellowstonePathology.Business.Flow.FlowMarkerItem marker in flowMarkerCollection)
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

        private void SetEquivocalSentence(List<string> sentences, Business.Flow.FlowMarkerCollection flowMarkerCollection)
        {            
            List<string> markers = new List<string>();
            foreach (YellowstonePathology.Business.Flow.FlowMarkerItem marker in flowMarkerCollection)
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

        private string LoserCaseFirstLetter(string text)
        {
            if (text != string.Empty && char.IsUpper(text[0]))
            {
                text = char.ToLower(text[0]) + text.Substring(1);
            }
            return text;
        }

        private string ConcatStringList(List<string> stringList)
        {            
            StringBuilder result = new StringBuilder();
            for(int i=0; i<stringList.Count; i++)
            {
                if (stringList.Count == 1)
                {
                    result.Append(stringList[i] + ". ");
                }                
                else if (stringList.Count >= 2 && i == stringList.Count - 1)
                {
                    result.Append(" and ");
                    result.Append(stringList[i] + ". ");
                }
                else
                {
                    result.Append(stringList[i]);
                    if (stringList.Count > 2 && i < stringList.Count - 2)
                    {
                        result.Append(", ");
                    }
                }                
            }            
            return result.ToString();
        }
    }
}
