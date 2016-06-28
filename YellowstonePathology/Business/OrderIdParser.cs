using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
	public class OrderIdParser
	{
		#region Patterns
		private static readonly string LegacyReportNoPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d-\d+";
		private static readonly string LegacyReportNoNumberPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d-(?<ReportNoNumber>\d+)";
		private static readonly string LegacyReportNoLetterPattern = @"(?<ReportNoLetter>[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*)\d\d-\d+";
		private static readonly string LegacyReportNoYearPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*(?<ReportNoYear>\d\d)-\d+";
		private static readonly string LegacyPSAReportNoPattern = @"^[AaBbCcFfMmPpRrSsTtYy][MmBbEe]*\d\d\d+";

		private static readonly string LegacyMasterAccessionNoPattern = @"^\d+";
		private static readonly string MasterAccessionNoPattern = @"^\d\d-\d+";

		private static readonly string ReportNoPattern = @"^\d\d-\d+.[AaBbFfIiMmPpRrSsTtYy]{1}\d*";
		private static readonly string ReportNoLetterPattern = @"^\d\d-\d+.(?<ReportNoPrefix>[AaBbFfIiMmPpRrSsTtYyQq])\d*";
		private static readonly string ReportNoNumberPattern = @"^\d\d-\d+.[AaBbFfIiMmPpRrSsTtYyQq](?<ReportNoNumber>\d*)";
		private static readonly string ReportNoYearPattern = @"^(?<ReportNoYear>\d\d)-\d+.[AaBbFfIiMmPpRrSsTtYyQq]{1}\d*";

		private static readonly string PanelOrderIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+";
		private static readonly string PanelOrderNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO(?<PanelOrderNo>\d+)";

		private static readonly string TestOrderIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+.TO\d+";
		private static readonly string TestOrderNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PO\d+.TO(?<TestOrderNo>\d+)";

		private static readonly string SurgicalSpecimenIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+";
		private static readonly string SurgicalSpecimenNoPattern = @"^\d\d-\d+.[Ss]\.SSR+(?<SurgicalSpecimenNo>\d+)";

		private static readonly string StainResultIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.STR\d+";
		private static readonly string StainResultNoPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.STR(?<StainResultNo>\d+)";

		private static readonly string IntraoperativeConsultationResultIdPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.IC\d+";
		private static readonly string IntraoperativeConsultationResultNoPattern = @"^\d\d-\d+.[Ss]\.SSR\d+\.IC(?<IntraopResultNo>\d+)";

		private static readonly string SurgicalAuditIdPattern = @"^\d\d-\d+.[Ss]\.SRA\d+";
		private static readonly string SurgicalAuditNoPattern = @"^\d\d-\d+.[Ss]\.SRA+(?<SurgicalAuditNo>\d+)";

		private static readonly string SurgicalSpecimenAuditIdPattern = @"^\d\d-\d+.[Ss]\.SRA\d+\.SSRA\d+";
		private static readonly string SurgicalSpecimenAuditNoPattern = @"^\d\d-\d+.[Ss]\.SRA+\d+\.SSRA(?<SurgicalSpecimenAuditNo>\d+)";

		private static readonly string CptBillingCodeIdPattern = @"^\d\d-\d+.CPT\d+";
		private static readonly string CptBillingCodeNoPattern = @"^\d\d-\d+.CPT(?<CptBillingCodeNo>\d+)";

		private static readonly string Icd9BillingCodeIdPattern = @"^\d\d-\d+.ICD\d+";
		private static readonly string Icd9BillingCodeNoPattern = @"^\d\d-\d+.ICD(?<IcdBillingCodeNo>\d+)";

		private static readonly string TaskOrderIdPattern = @"^\d\d-\d+.TSKO\d+";
		private static readonly string TaskOrderNoPattern = @"^\d\d-\d+.TSKO(?<TaskOrderNo>\d+)";

		private static readonly string TaskOrderDetailIdPattern = @"^\d\d-\d+.TSKO\d+\.TSKOD\d+";
		private static readonly string TaskOrderDetailNoPattern = @"^\d\d-\d+.TSKO\d+\.TSKOD(?<TaskOrderDetailNo>\d+)";

		private static readonly string AmendmentIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.AM\d+";
		private static readonly string AmendmentNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.AM(?<AmendmentNo>\d+)";

		private static readonly string FlowMarkerIdPattern = @"^\d\d-\d+.[Ff]{1}\d*.FM\d+";
		private static readonly string FlowMarkerNoPattern = @"^\d\d-\d+.[Ff]{1}\d*.FM(?<FlowMarkerNo>\d+)";

		private static readonly string PanelSetOrderBillableIdPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PSOB\d+";
		private static readonly string PanelSetOrderBillableNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.PSOB(?<PanelSetOrderBillableNo>\d+)";

		private static readonly string SpecimenOrderIdPattern = @"^\d\d-\d+\.\d+";
		private static readonly string SpecimenOrderNoPattern = @"^\d\d-\d+\.(?<SpecimenOrderNo>\d+)";        

        private static readonly string PanelSetOrderCPTCodeNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.CPT(?<PanelSetOrderCPTCodeNo>\d+)";
        private static readonly string PanelSetOrderCPTCodeBillNoPattern = @"^\d\d-\d+.[AaBbFfMmPpRrSsTtYy]{1}\d*.BLL(?<PanelSetOrderCPTCodeBillNo>\d+)";

		private static readonly string AliquotOrderBlockIdPattern = @"^\d\d-\d+\.\d+[A-Z](?<AliquotLetter>[A-Z]|$)";
		private static readonly string AliquotOrderBlockLetterPattern = @"^\d\d-\d+\.\d+(?<AliquotLetter>[A-Z])";
		private static readonly string AliquotOrderBlockLetter2Pattern = @"^\d\d-\d+\.\d+[A-Z](?<AliquotLetter2>[A-Z])";
		private static readonly string AliquotOrderAlqIdPattern = @"^\d\d-\d+\.\d+\.\d+";
		private static readonly string AliquotOrderAlqNoPattern = @"^\d\d-\d+\.\d+\.(?<AliquotOrderNo>\d+)";

		private static readonly string SlideOrderIdBlockPattern = @"^\d\d-\d+\.\d+[A-Z]+\d+";
		private static readonly string SlideOrderNoBlockPattern = @"^\d\d-\d+\.\d+[A-Z]+(?<SlideOrderNo>\d+)";
		#endregion

		private string m_IdToParse;	

		public OrderIdParser(string idToParse)
		{
            this.m_IdToParse = idToParse;
		}

		private static int GetIdNumber(string idString, string prefix)
		{
			int result = 0;
			if(idString.Contains(prefix) == true)
			{
				int startIndex = idString.IndexOf(prefix) + prefix.Length;
				result = Convert.ToInt32(idString.Substring(startIndex));
			}
			return result;
		}

		#region MasterAccessionNo
		public string MasterAccessionNo
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(MasterAccessionNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? MasterAccessionNoNumber
		{
			get
			{
				int? result = null;
				string masterAccessionNo = this.MasterAccessionNo;
				if (string.IsNullOrEmpty(masterAccessionNo) == false) result = Convert.ToInt32(masterAccessionNo.Substring(3));
				return result;
			}
		}

		public int? MasterAccessionNoYear
		{
			get
			{
				int? result = null;
				string masterAccessionNo = this.MasterAccessionNo;
				if (string.IsNullOrEmpty(masterAccessionNo) == false) result = Convert.ToInt32(masterAccessionNo.Substring(0, 2));
				return result;
			}
		}

		public bool IsLegacyMasterAccessionNo
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyMasterAccessionNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

		public bool IsValidMasterAccessionNo
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(MasterAccessionNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

        public bool IsValidAliquotOrderId
        {
            get
            {
                bool result = false;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AliquotOrderAlqIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = true;
                return result;
            }
        }
		#endregion

		#region ReportNo
		public bool IsLegacyReportNo
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

		public bool IsLegacyPSAReportNo
		{
			get
			{
				bool result = false;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyPSAReportNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = true;
				return result;
			}
		}

		public string CreateCyotlogyReportNoFromMasterAccessionNo()
		{
			return this.MasterAccessionNo + ".P";
		}

		public string CreateSurgicalReportNoFromMasterAccessionNo()
		{
			return this.MasterAccessionNo + ".S";
		}

		public bool IsValidReportNo
		{
			get
			{
				bool result = false;
				if (string.IsNullOrEmpty(this.ReportNo) == false)
				{
					if (string.IsNullOrEmpty(this.ReportNoLetter) == false)
					{
						result = true;
					}
				}
				return result;
			}
		}

		public bool IsValidCytologyReportNo
		{
			get
			{
				bool result = false;
				if (this.ReportNo != null)
				{
					if (this.ReportNoLetter == "P") result = true;
				}
				return result;
			}
		}

		public bool IsValidSurgicalReportNo
		{
			get
			{
				bool result = false;
				if (this.ReportNo != null)
				{
					if (this.ReportNoLetter == "S") result = true;
				}
				return result;
			}
		}

		public string LegacyReportNo
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public string ReportNo
		{
			get
			{
				string result = null;
				if (this.IsLegacyReportNo == true) result = LegacyReportNo;
				else
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (match.Captures.Count != 0) result = match.Value;
				}
				return result;
			}
		}

		public string ReportNoLetter
		{
			get
			{
				string result = null;
				if (this.IsLegacyReportNo == false)
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoLetterPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (match.Captures.Count != 0) result = match.Groups["ReportNoPrefix"].Captures[0].Value;
				}
				else
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoLetterPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (match.Captures.Count != 0) result = match.Groups["ReportNoLetter"].Captures[0].Value;
				}
				return result;
			}
		}

		public int? ReportNoNumber
		{
			get
			{
				int? result = null;
				if (this.IsLegacyReportNo == true)
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoNumberPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (string.IsNullOrEmpty(match.Groups["ReportNoNumber"].Captures[0].Value) == false)
						result = Convert.ToInt32(match.Groups["ReportNoNumber"].Captures[0].Value);
				}
				else
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoNumberPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (string.IsNullOrEmpty(match.Groups["ReportNoNumber"].Captures[0].Value) == false)
						result = Convert.ToInt32(match.Groups["ReportNoNumber"].Captures[0].Value);
				}
				return result;
			}
		}

		public int? ReportNoYear
		{
			get
			{
				int? result = null;
				if (this.IsLegacyReportNo == true)
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(LegacyReportNoYearPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (string.IsNullOrEmpty(match.Groups["ReportNoYear"].Captures[0].Value) == false)
						result = Convert.ToInt32(match.Groups["ReportNoYear"].Captures[0].Value);
				}
				else
				{
					System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(ReportNoYearPattern);
					System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
					if (string.IsNullOrEmpty(match.Groups["ReportNoYear"].Captures[0].Value) == false)
						result = Convert.ToInt32(match.Groups["ReportNoYear"].Captures[0].Value);
				}
				return result;
			}
		}

		public string SurgicalReportNoFromMasterAccessionNo
		{
			get
			{
				string result = null;
				if (this.MasterAccessionNo != null)
				{
					result = this.MasterAccessionNo + ".S";
				}
				return result;
			}
		}

		public static string SurgicalReportNoFromNumber(string number)
		{
			string result = null;
			int num;
			if (Int32.TryParse(number, out num) == true)
			{
				if (DateTime.Today.Year > 2013) result = DateTime.Today.Year.ToString().Substring(2) + "-" + number + ".S";
				else result = "S" + DateTime.Today.Year.ToString().Substring(2) + "-" + number;
			}
			return result;
		}

		public static string IncrementReportNo(string reportNo, int increment)
		{
			string result = null;
			int number;
			OrderIdParser orderIdParser = new OrderIdParser(reportNo);
			if (orderIdParser.ReportNo != null)
			{
				if (orderIdParser.IsLegacyReportNo == true)
				{
					number = orderIdParser.ReportNoNumber.Value + increment;
					result = orderIdParser.ReportNoLetter + orderIdParser.ReportNoYear.Value.ToString() + "-" + number.ToString();
				}
				else
				{
					number = orderIdParser.MasterAccessionNoNumber.Value + increment;
					result = orderIdParser.ReportNoYear.Value.ToString() + "-" + number.ToString() + "." + orderIdParser.ReportNoLetter;
					int? reportNumber = orderIdParser.ReportNoNumber;
					if (reportNumber != null) result += reportNumber.Value.ToString();
				}
			}
			return result;
		}

		public string LegacyReportNoFromLegacyPSAReportNo
		{
			get
			{
				string result = null;
				if (this.IsLegacyPSAReportNo == true)
				{
					result = this.m_IdToParse.Substring(0, 3) + "-" + this.m_IdToParse.Substring(3);
				}
				return result;
			}
		}

		public static string GetNextReportNo(Test.PanelSetOrderCollection panelSetOrderCollection, YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet, string masterAccessionNo)
		{
			string result = null;
			if (panelSet.ReportNoLetter.AllowMultipleInSameAccession == false)
			{
				if (panelSetOrderCollection.Exists(panelSet.PanelSetId) == false) result = masterAccessionNo + "." + panelSet.ReportNoLetter.Letter;
			}
			else
			{
				int largestId = 0;
				foreach (Test.PanelSetOrder panelSetOrder in panelSetOrderCollection)
				{
					OrderIdParser orderIdParser = new OrderIdParser(panelSetOrder.ReportNo);
					int? reportNoNumber = orderIdParser.ReportNoNumber;
					if (reportNoNumber == null)
					{
						int currentId = GetIdNumber(panelSetOrder.ReportNo, panelSet.ReportNoLetter.Letter);
						if (currentId > largestId) largestId = currentId;
					}
					else
					{
						string reportNoLetter = orderIdParser.ReportNoLetter;
						if (reportNoLetter == panelSet.ReportNoLetter.Letter && reportNoNumber.Value > largestId) largestId = reportNoNumber.Value;
					}
				}
				result = masterAccessionNo + "." + panelSet.ReportNoLetter.Letter + (largestId + 1).ToString();
			}
			return result;
		}
		#endregion

		#region TaskOrder
		public string TaskOrderId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TaskOrderIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? TaskOrderNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TaskOrderNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["TaskOrderNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["TaskOrderNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextTaskOrderId(YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection, string masterAccessionNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (YellowstonePathology.Business.Task.Model.TaskOrder taskOrder in taskOrderCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(taskOrder.TaskOrderId);
				int? taskOrderNo = orderIdParser.TaskOrderNo;
				if (taskOrderNo == null)
				{
					int currentId = GetIdNumber(taskOrder.TaskOrderId, YellowstonePathology.Business.Task.Model.TaskOrderCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (taskOrderNo.Value > largestId) largestId = taskOrderNo.Value;
				}
			}
			return masterAccessionNo + "." + YellowstonePathology.Business.Task.Model.TaskOrderCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region TaskOrderDetail
		public string TaskOrderDetailId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TaskOrderDetailIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? TaskOrderDetailNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TaskOrderDetailNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["TaskOrderDetailNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["TaskOrderDetailNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextTaskOrderDetailId(YellowstonePathology.Business.Task.Model.TaskOrderDetailCollection taskOrderDetailCollection, string taskOrderId)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (YellowstonePathology.Business.Task.Model.TaskOrderDetail taskOrderDetail in taskOrderDetailCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(taskOrderDetail.TaskOrderDetailId);
				int? taskOrderDetailNo = orderIdParser.TaskOrderDetailNo;
				if (taskOrderDetailNo == null)
				{
					int currentId = GetIdNumber(taskOrderDetail.TaskOrderDetailId, YellowstonePathology.Business.Task.Model.TaskOrderDetailCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (taskOrderDetailNo.Value > largestId) largestId = taskOrderDetailNo.Value;
				}
			}
			return taskOrderId + "." + YellowstonePathology.Business.Task.Model.TaskOrderDetailCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region Amendment
		public string AmendmentId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AmendmentIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? AmendmentNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AmendmentNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["AmendmentNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["AmendmentNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextAmendmentId(YellowstonePathology.Business.Amendment.Model.AmendmentCollection amendmentCollection, string reportNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (YellowstonePathology.Business.Amendment.Model.Amendment amendment in amendmentCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(amendment.AmendmentId);
				int? amendmentNo = orderIdParser.AmendmentNo;
				if (amendmentNo == null)
				{
					int currentId = GetIdNumber(amendment.AmendmentId, YellowstonePathology.Business.Amendment.Model.AmendmentCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (amendmentNo.Value > largestId) largestId = amendmentNo.Value;
				}
			}
			return reportNo + "." + YellowstonePathology.Business.Amendment.Model.AmendmentCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region PanelSetOrderBillable
		public string PanelSetOrderBillableId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelSetOrderBillableIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? PanelSetOrderBillableNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelSetOrderBillableNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["PanelSetOrderBillableNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["PanelSetOrderBillableNo"].Captures[0].Value);
				return result;
			}
		}
		#endregion

		#region PanelSetOrderCPTCode
		public static string GetNextPanelSetOrderCPTCodeId(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection panelSetCPTCodeCollection, string reportNo)
        {
            string result = string.Empty;
            int largestId = 0;
            foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCode panelSetOrderCPTCode in panelSetCPTCodeCollection)
            {
                OrderIdParser orderIdParser = new OrderIdParser(panelSetOrderCPTCode.PanelSetOrderCPTCodeId);
                int? panelSetOrderCPTCodeNo = orderIdParser.PanelSetOrderCPTCodeNo;
                if (panelSetOrderCPTCodeNo == null)
                {
                    int currentId = GetIdNumber(panelSetOrderCPTCode.PanelSetOrderCPTCodeId, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection.PREFIXID);
                    if (currentId > largestId) largestId = currentId;
                }
                else
                {
                    if (panelSetOrderCPTCodeNo.Value > largestId) largestId = panelSetOrderCPTCodeNo.Value;
                }
            }
            return reportNo + "." + YellowstonePathology.Business.Test.PanelSetOrderCPTCodeCollection.PREFIXID + (largestId + 1).ToString();
        }

        public int? PanelSetOrderCPTCodeNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelSetOrderCPTCodeNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["PanelSetOrderCPTCodeNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["PanelSetOrderCPTCodeNo"].Captures[0].Value);
                return result;
            }
        }
		#endregion

		#region PanelSetOrderCPTCodeBill
		public static string GetNextPanelSetOrderCPTCodeBillId(YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection panelSetCPTCodeBillCollection, string reportNo)
        {
            string result = string.Empty;
            int largestId = 0;
            foreach (YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBill panelSetOrderCPTCodeBill in panelSetCPTCodeBillCollection)
            {
                OrderIdParser orderIdParser = new OrderIdParser(panelSetOrderCPTCodeBill.PanelSetOrderCPTCodeBillId);
                int? panelSetOrderCPTCodeBillNo = orderIdParser.PanelSetOrderCPTCodeBillNo;
                if (panelSetOrderCPTCodeBillNo == null)
                {
                    int currentId = GetIdNumber(panelSetOrderCPTCodeBill.PanelSetOrderCPTCodeBillId, YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection.PREFIXID);
                    if (currentId > largestId) largestId = currentId;
                }
                else
                {
                    if (panelSetOrderCPTCodeBillNo.Value > largestId) largestId = panelSetOrderCPTCodeBillNo.Value;
                }
            }
            return reportNo + "." + YellowstonePathology.Business.Test.PanelSetOrderCPTCodeBillCollection.PREFIXID + (largestId + 1).ToString();
        }

        public int? PanelSetOrderCPTCodeBillNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelSetOrderCPTCodeBillNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["PanelSetOrderCPTCodeBillNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["PanelSetOrderCPTCodeBillNo"].Captures[0].Value);
                return result;
            }
        }
		#endregion

		#region SpecimenOrder
		public string SpecimenOrderId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SpecimenOrderIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? SpecimenOrderNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SpecimenOrderNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["SpecimenOrderNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["SpecimenOrderNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextSpecimenOrderId(Specimen.Model.SpecimenOrderCollection specimenOrderCollection, string masterAccessionNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (Specimen.Model.SpecimenOrder specimenOrder in specimenOrderCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(specimenOrder.SpecimenOrderId);
				int? specimenOrderNo = orderIdParser.SpecimenOrderNo;
				if (specimenOrderNo.Value > largestId) largestId = specimenOrderNo.Value;
			}
			return masterAccessionNo + "." + (largestId + 1).ToString();
		}
		#endregion

		#region AliquotOrder
		public string AliquotOrderId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AliquotOrderBlockIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				else
				{
					System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(AliquotOrderAlqIdPattern);
					System.Text.RegularExpressions.Match match2 = regex2.Match(this.m_IdToParse);
					if (match2.Captures.Count != 0) result = match2.Value;
				}
				return result;
			}
		}

		private string AliquotNextBlockLetterString
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AliquotOrderBlockLetterPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["AliquotLetter"].Captures.Count != 0)
					result = (match.Groups["AliquotLetter"].Captures[0].Value);
				System.Text.RegularExpressions.Regex regex2 = new System.Text.RegularExpressions.Regex(AliquotOrderBlockLetter2Pattern);
				System.Text.RegularExpressions.Match match2 = regex2.Match(this.m_IdToParse);
				if (match2.Groups["AliquotLetter2"].Captures.Count != 0)
					result += (match2.Groups["AliquotLetter2"].Captures[0].Value);

				YellowstonePathology.Business.Specimen.Model.AliquotLetterList aliquotLetterList = new YellowstonePathology.Business.Specimen.Model.AliquotLetterList();
				for (int idx = 0; idx < aliquotLetterList.Count; idx++)
				{
					if (result == aliquotLetterList[idx])
					{
						result = aliquotLetterList[idx + 1];
						break;
					}
				}

				return result;
			}
		}

		private int? AliquotOrderAlqNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(AliquotOrderAlqNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["AliquotOrderNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["AliquotOrderNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextAliquotOrderId(Test.AliquotOrderCollection aliquotOrderCollection, string specimenOrderId, string aliquotType)
		{
			string id = string.Empty;
			switch (aliquotType)
			{
				case YellowstonePathology.Business.Test.AliquotType.Block:
				case YellowstonePathology.Business.Test.AliquotType.CellBlock:				
				case YellowstonePathology.Business.Test.AliquotType.FrozenBlock:
					id = GetNextAliquotBlockLetter(aliquotOrderCollection);
					break;
				case YellowstonePathology.Business.Test.AliquotType.Slide:
                case YellowstonePathology.Business.Test.AliquotType.FNA:
				case YellowstonePathology.Business.Test.AliquotType.Specimen:
					id = "." + GetNextAliquotAlqNo(aliquotOrderCollection);
					break;
			}
			return specimenOrderId + id;
		}

		private static string GetNextAliquotAlqNo(Test.AliquotOrderCollection aliquotOrderCollection)
		{
			int largestId = 0;
			foreach (Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
			{
				if (aliquotOrder.IsBlock() == false)
				{
					OrderIdParser orderIdParser = new OrderIdParser(aliquotOrder.AliquotOrderId);
					int? aliquotOrderNo = orderIdParser.AliquotOrderAlqNo;
					if (aliquotOrderNo.Value > largestId) largestId = aliquotOrderNo.Value;
				}
			}
			largestId++;
			return largestId.ToString();
		}

		private static string GetNextAliquotBlockLetter(Test.AliquotOrderCollection aliquotOrderCollection)
		{
			string result = "A";
			foreach (Test.AliquotOrder aliquotOrder in aliquotOrderCollection)
			{
				if (aliquotOrder.IsBlock() == true)
				{
					OrderIdParser orderIdParser = new OrderIdParser(aliquotOrder.AliquotOrderId);
					result = orderIdParser.AliquotNextBlockLetterString;
				}
			}
			return result;
		}
		#endregion

		#region SlideOrder
		public string SlideOrderId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SlideOrderIdBlockPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? SlideOrderNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SlideOrderNoBlockPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["SlideOrderNo"].Captures.Count != 0) result = Convert.ToInt32(match.Groups["SlideOrderNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextSlideOrderId(YellowstonePathology.Business.Slide.Model.SlideOrderCollection slideOrderCollection, string aliquotOrderId)
		{
			string result = string.Empty;
			int largestId = 0;

            if (aliquotOrderId.Length < 10)
            {
                foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in slideOrderCollection)
                {
                    OrderIdParser orderIdParser = new OrderIdParser(slideOrder.SlideOrderId);
                    int? slideOrderNo = orderIdParser.SlideOrderNo;
                    if (slideOrderNo.HasValue && slideOrderNo.Value > largestId) largestId = slideOrderNo.Value;
                }

                result = aliquotOrderId + (largestId + 1).ToString();
            }
            else
            {
                result = aliquotOrderId + (slideOrderCollection.Count + 1).ToString();
            }

            return result;
		}
		#endregion

		#region PanelOrder
		public string PanelOrderId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelOrderIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? PanelOrderNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(PanelOrderNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["PanelOrderNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["PanelOrderNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextPanelOrderId(Test.PanelOrderCollection panelOrderCollection, string reportNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (Test.PanelOrder panelOrder in panelOrderCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(panelOrder.PanelOrderId);
				int? panelOrderNo = orderIdParser.PanelOrderNo;
				if (panelOrderNo == null)
				{
					int currentId = GetIdNumber(panelOrder.PanelOrderId, Test.PanelOrderCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (panelOrderNo.Value > largestId) largestId = panelOrderNo.Value;
				}
			}
			return reportNo + "." + Test.PanelOrderCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region CptBillingCode
		public string CptBillingCodeId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(CptBillingCodeIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? CptBillingCodeNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(CptBillingCodeNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["CptBillingCodeNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["CptBillingCodeNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextCptBillingCodeId(Billing.Model.CptBillingCodeItemCollection cptBillingCodeItemCollection, string masterAccessionNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (Billing.Model.CptBillingCodeItem cptBillingCodeItem in cptBillingCodeItemCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(cptBillingCodeItem.CptBillingId);
				int? cptBillingCodeNo = orderIdParser.CptBillingCodeNo;
				if (cptBillingCodeNo == null)
				{
					int currentId = GetIdNumber(cptBillingCodeItem.CptBillingId, Billing.Model.CptBillingCodeItemCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (cptBillingCodeNo.Value > largestId) largestId = cptBillingCodeNo.Value;
				}
			}
			return masterAccessionNo + "." + Billing.Model.CptBillingCodeItemCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region Icd9BillingCode
		public string Icd9BillingCodeId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(Icd9BillingCodeIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? Icd9BillingCodeNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(Icd9BillingCodeNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["IcdBillingCodeNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["IcdBillingCodeNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextICD9BillingCodeId(Billing.Model.ICD9BillingCodeCollection icd9BillingCodeCollection, string masterAccessionNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (Billing.Model.ICD9BillingCode icd9BillingCode in icd9BillingCodeCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(icd9BillingCode.Icd9BillingId);
				int? icd9BillingCodeNo = orderIdParser.Icd9BillingCodeNo;
				if (icd9BillingCodeNo == null)
				{
                    int currentId = GetIdNumber(icd9BillingCode.Icd9BillingId, Billing.Model.ICD9BillingCodeCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (icd9BillingCodeNo.Value > largestId) largestId = icd9BillingCodeNo.Value;
				}
			}
			return masterAccessionNo + "." + Billing.Model.ICD9BillingCodeCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region SurgicalSpecimen
		public string SurgicalSpecimenId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalSpecimenIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? SurgicalSpecimenResultNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalSpecimenNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["SurgicalSpecimenNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["SurgicalSpecimenNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextSurgicalSpecimenId(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection surgicalSpecimenCollection, string reportNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimen surgicalSpecimen in surgicalSpecimenCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(surgicalSpecimen.SurgicalSpecimenId);
				int? surgicalSpecimenResultNo = orderIdParser.SurgicalSpecimenResultNo;
				if (surgicalSpecimenResultNo == null)
				{
					int currentId = GetIdNumber(surgicalSpecimen.SurgicalSpecimenId, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (surgicalSpecimenResultNo.Value > largestId) largestId = surgicalSpecimenResultNo.Value;
				}
			}
			return reportNo + "." + YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region StainResult
		public string StainResultId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(StainResultIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? StainResultNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(StainResultNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["StainResultNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["StainResultNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextStainResultId(SpecialStain.StainResultItemCollection stainResultItemCollection, string surgicalSpecimenId)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (SpecialStain.StainResultItem stainResult in stainResultItemCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(stainResult.StainResultId);
				int? stainResultNo = orderIdParser.StainResultNo;
				if (stainResultNo == null)
				{
					int currentId = GetIdNumber(stainResult.StainResultId, SpecialStain.StainResultItemCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (stainResultNo.Value > largestId) largestId = stainResultNo.Value;
				}
			}
			return surgicalSpecimenId + "." + SpecialStain.StainResultItemCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region IntraoperativeConsultationResult
		public string IntraperativeConsultationResultId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(IntraoperativeConsultationResultIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? IntraperativeConsultationResultNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(IntraoperativeConsultationResultNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["IntraopResultNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["IntraopResultNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextIntraoperativeConsultationResultId(YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResultCollection intraoperativeConsultationResultCollection,
			string surgicalSpecimenId)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResult intraoperativeConsultationResult in intraoperativeConsultationResultCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(intraoperativeConsultationResult.IntraoperativeConsultationResultId);
				int? intraoperativeConsultationResultNo = orderIdParser.IntraperativeConsultationResultNo;
				if (intraoperativeConsultationResultNo == null)
				{
					int currentId = GetIdNumber(intraoperativeConsultationResult.IntraoperativeConsultationResultId, YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResultCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (intraoperativeConsultationResultNo.Value > largestId) largestId = intraoperativeConsultationResultNo.Value;
				}
			}
			return surgicalSpecimenId + "." + YellowstonePathology.Business.Test.Surgical.IntraoperativeConsultationResultCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region SurgicalAudit
		public string SurgicalAuditId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalAuditIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? SurgicalAuditNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalAuditNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["SurgicalAuditNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["SurgicalAuditNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextSurgicalAuditId(YellowstonePathology.Business.Test.Surgical.SurgicalAuditCollection surgicalAuditCollection, string reportNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalAudit surgicalAudit in surgicalAuditCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(surgicalAudit.SurgicalAuditId);
				int? surgicalAuditNo = orderIdParser.SurgicalAuditNo;
				if (surgicalAuditNo == null)
				{
					int currentId = GetIdNumber(surgicalAudit.SurgicalAuditId, YellowstonePathology.Business.Test.Surgical.SurgicalAuditCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (surgicalAuditNo.Value > largestId) largestId = surgicalAuditNo.Value;
				}
			}
			return reportNo + "." + YellowstonePathology.Business.Test.Surgical.SurgicalAuditCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region SurgicalSpecimenAudit
		public string SurgicalSpecimenAuditId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalSpecimenAuditIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? SurgicalSpecimenAuditNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(SurgicalSpecimenAuditNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["SurgicalSpecimenAuditNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["SurgicalSpecimenAuditNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextSurgicalSpecimenAuditId(YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAuditCollection surgicalSpecimenAuditCollection,
			string surgicalAuditId)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAudit surgicalSpecimenAudit in surgicalSpecimenAuditCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(surgicalSpecimenAudit.SurgicalSpecimenAuditId);
				int? surgicalSpecimenResultAuditNo = orderIdParser.SurgicalSpecimenAuditNo;
				if (surgicalSpecimenResultAuditNo == null)
				{
					int currentId = GetIdNumber(surgicalSpecimenAudit.SurgicalSpecimenAuditId, YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAuditCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (surgicalSpecimenResultAuditNo.Value > largestId) largestId = surgicalSpecimenResultAuditNo.Value;
				}
			}
			return surgicalAuditId + "." + YellowstonePathology.Business.Test.Surgical.SurgicalSpecimenAuditCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

		#region FlowMarker
		public string FlowMarkerId
		{
			get
			{
				string result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(FlowMarkerIdPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Captures.Count != 0) result = match.Value;
				return result;
			}
		}

		public int? FlowMarkerNo
		{
			get
			{
				int? result = null;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(FlowMarkerNoPattern);
				System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
				if (match.Groups["FlowMarkerNo"].Captures.Count != 0)
					result = Convert.ToInt32(match.Groups["FlowMarkerNo"].Captures[0].Value);
				return result;
			}
		}

		public static string GetNextFlowMarkerId(Flow.FlowMarkerCollection flowMarkerCollection, string reportNo)
		{
			string result = string.Empty;
			int largestId = 0;
			foreach (Flow.FlowMarkerItem flowMarker in flowMarkerCollection)
			{
				OrderIdParser orderIdParser = new OrderIdParser(flowMarker.FlowMarkerId);
				int? flowMarkerNo = orderIdParser.FlowMarkerNo;
				if (flowMarkerNo == null)
				{
					int currentId = GetIdNumber(flowMarker.FlowMarkerId, Flow.FlowMarkerCollection.PREFIXID);
					if (currentId > largestId) largestId = currentId;
				}
				else
				{
					if (flowMarkerNo.Value > largestId) largestId = flowMarkerNo.Value;
				}
			}
			return reportNo + "." + Flow.FlowMarkerCollection.PREFIXID + (largestId + 1).ToString();
		}
		#endregion

        #region TestOrder
        public string TestOrderId
        {
            get
            {
                string result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TestOrderIdPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Captures.Count != 0) result = match.Value;
                return result;
            }
        }

        public int? TestOrderNo
        {
            get
            {
                int? result = null;
                System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(TestOrderNoPattern);
                System.Text.RegularExpressions.Match match = regex.Match(this.m_IdToParse);
                if (match.Groups["TestOrderNo"].Captures.Count != 0)
                    result = Convert.ToInt32(match.Groups["TestOrderNo"].Captures[0].Value);
                return result;
            }
        }

        public static string GetNextTestOrderId(YellowstonePathology.Business.Test.Model.TestOrderCollection testOrderCollection, string panelOrderId)
        {
            string result = string.Empty;
            int largestId = 0;
            foreach (YellowstonePathology.Business.Test.Model.TestOrder testOrder in testOrderCollection)
            {
                OrderIdParser orderIdParser = new OrderIdParser(testOrder.TestOrderId);
                int? testOrderNo = orderIdParser.TestOrderNo;
                if (testOrderNo == null)
                {
                    int currentId = GetIdNumber(testOrder.TestOrderId, YellowstonePathology.Business.Test.Model.TestOrderCollection.PREFIXID);
                    if (currentId > largestId) largestId = currentId;
                }
                else
                {
                    if (testOrderNo.Value > largestId) largestId = testOrderNo.Value;
                }
            }
            return panelOrderId + "." + YellowstonePathology.Business.Test.Model.TestOrderCollection.PREFIXID + (largestId + 1).ToString();
        }

        #endregion       
	}
}
