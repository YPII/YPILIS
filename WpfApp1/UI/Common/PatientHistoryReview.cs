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
        private const string OvaryMessage = "A specimen with an Ovary was found please check the patient history.";
        private const string FallopianTubeMessage = "A specimen with an Fallopian Tubes was found please check the patient history.";

        public PatientHistoryReview()
		{
			this.m_WordSearchList = new YellowstonePathology.Business.Rules.Surgical.WordSearchList();

			YellowstonePathology.Business.Rules.Surgical.WordSearchListItem uterusDysplasiaItem = new YellowstonePathology.Business.Rules.Surgical.WordSearchListItem("UTERUS", true, CheckPatientHistoryMessage);
			this.m_WordSearchList.Add(uterusDysplasiaItem);

			YellowstonePathology.Business.Rules.Surgical.WordSearchListItem alopeciaItem = new YellowstonePathology.Business.Rules.Surgical.WordSearchListItem("SCALP", true, AlopeciaMessage);
			this.m_WordSearchList.Add(alopeciaItem);

            YellowstonePathology.Business.Rules.Surgical.WordSearchListItem cervixItem = new YellowstonePathology.Business.Rules.Surgical.WordSearchListItem("CERVIX", true, CervixMessage);
            this.m_WordSearchList.Add(cervixItem);

            YellowstonePathology.Business.Rules.Surgical.WordSearchListItem ovaryItem = new YellowstonePathology.Business.Rules.Surgical.WordSearchListItem("OVARY", true, OvaryMessage);
            this.m_WordSearchList.Add(ovaryItem);

            YellowstonePathology.Business.Rules.Surgical.WordSearchListItem falopianTubesItem = new YellowstonePathology.Business.Rules.Surgical.WordSearchListItem("FALLOPIAN", true, FallopianTubeMessage);
            this.m_WordSearchList.Add(falopianTubesItem);
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
