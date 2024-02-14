using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.ZAP70LymphoidPanel
{
	[PersistentClass("tblZAP70LymphoidPanelTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class ZAP70LymphoidPanelTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_Comment;
		private string m_Lymphocytes;
		private string m_PopulationAnalysis;
		private string m_MarkersPerformed;

		public ZAP70LymphoidPanelTestOrder()
		{
		}

		public ZAP70LymphoidPanelTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string Result
		{
			get { return this.m_Result; }
			set
			{
				if (this.m_Result != value)
				{
					this.m_Result = value;
					this.NotifyPropertyChanged("Result");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "5000", "null", "varchar")]
		public string Comment
		{
			get { return this.m_Comment; }
			set
			{
				if (this.m_Comment != value)
				{
					this.m_Comment = value;
					this.NotifyPropertyChanged("Comment");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string Lymphocytes
		{
			get { return this.m_Lymphocytes; }
			set
			{
				if (this.m_Lymphocytes != value)
				{
					this.m_Lymphocytes = value;
					this.NotifyPropertyChanged("Lymphocytes");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string PopulationAnalysis
		{
			get { return this.m_PopulationAnalysis; }
			set
			{
				if (this.m_PopulationAnalysis != value)
				{
					this.m_PopulationAnalysis = value;
					this.NotifyPropertyChanged("PopulationAnalysis");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string MarkersPerformed
		{
			get { return this.m_MarkersPerformed; }
			set
			{
				if (this.m_MarkersPerformed != value)
				{
					this.m_MarkersPerformed = value;
					this.NotifyPropertyChanged("MarkersPerformed");
				}
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Comment: " + this.m_Comment);
			result.AppendLine();

			result.AppendLine("Lymphocytes: " + this.m_Lymphocytes);
			result.AppendLine();

			result.AppendLine("Population Analysis: " + this.m_PopulationAnalysis);
			result.AppendLine();

			result.AppendLine("Markers Performed: " + this.m_MarkersPerformed);
			result.AppendLine();

			return result.ToString();
		}
	}
}
