using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Common
{
	public class PatientHistoryReview
	{
		private YellowstonePathology.Business.Rules.Surgical.WordSearchList m_WordSearchList;
		private const string CheckPatientHistoryMessage = "Check patient history.";
		private const string AlopeciaMessage = "Check for alopecia.";
        private const string CervixMessage = "Please check to see if PAP slides need to be pulled.";        

		public PatientHistoryReview()
		{
			this.m_WordSearchList = new YellowstonePathology.Business.Rules.Surgical.WordSearchList();

			YellowstonePathology.Business.Rules.Surgical.WordSearchListItem uterusDysplasiaItem = new YellowstonePathology.Business.Rules.Surgical.WordSearchListItem("UTERUS", true, CheckPatientHistoryMessage);
			this.m_WordSearchList.Add(uterusDysplasiaItem);

			YellowstonePathology.Business.Rules.Surgical.WordSearchListItem alopeciaItem = new YellowstonePathology.Business.Rules.Surgical.WordSearchListItem("SCALP", true, AlopeciaMessage);
			this.m_WordSearchList.Add(alopeciaItem);

            YellowstonePathology.Business.Rules.Surgical.WordSearchListItem cervixItem = new YellowstonePathology.Business.Rules.Surgical.WordSearchListItem("CERVIX", true, CervixMessage);
            this.m_WordSearchList.Add(cervixItem);
		}

		public YellowstonePathology.Business.Rules.ExecutionStatus ExecutionStatus
		{
			get { return this.m_WordSearchList.ExecutionStatus; }
		}

		public bool Run(YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection specimenOrderCollection)
		{
			bool result = specimenOrderCollection.FindWordsInDescription(this.m_WordSearchList);
			return result;
		}
	}
}
