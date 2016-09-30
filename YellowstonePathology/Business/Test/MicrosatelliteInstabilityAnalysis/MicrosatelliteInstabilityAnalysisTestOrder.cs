using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.MicrosatelliteInstabilityAnalysis
{
	[PersistentClass("tblMicrosatelliteInstabilityAnalysisTestOrder", "tblPanelSetOrder", "YPIDATA")]
	public class MicrosatelliteInstabilityAnalysisTestOrder : YellowstonePathology.Business.Test.PanelSetOrder
	{
		private string m_Result;
		private string m_InstabilityLevel;
		private string m_BAT25Instability;
		private string m_BAT26Instability;
		private string m_D5S346Instability;
		private string m_D17S250Instability;
		private string m_D2S123Instability;
		private string m_Interpretation;
		private string m_Method;
		private string m_TestDevelopment;
		
		public MicrosatelliteInstabilityAnalysisTestOrder()
        {
        }

		public MicrosatelliteInstabilityAnalysisTestOrder(string masterAccessionNo, string reportNo, string objectId,
			YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
			YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
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
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string InstabilityLevel
		{
			get { return this.m_InstabilityLevel; }
			set
			{
				if (this.m_InstabilityLevel != value)
				{
					this.m_InstabilityLevel = value;
					this.NotifyPropertyChanged("InstabilityLevel");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string BAT25Instability
		{
			get { return this.m_BAT25Instability; }
			set
			{
				if (this.m_BAT25Instability != value)
				{
					this.m_BAT25Instability = value;
					this.NotifyPropertyChanged("BAT25Instability");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string BAT26Instability
		{
			get { return this.m_BAT26Instability; }
			set
			{
				if (this.m_BAT26Instability != value)
				{
					this.m_BAT26Instability = value;
					this.NotifyPropertyChanged("BAT26Instability");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string D5S346Instability
		{
			get { return this.m_D5S346Instability; }
			set
			{
				if (this.m_D5S346Instability != value)
				{
					this.m_D5S346Instability = value;
					this.NotifyPropertyChanged("D5S346Instability");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string D17S250Instability
		{
			get { return this.m_D17S250Instability; }
			set
			{
				if (this.m_D17S250Instability != value)
				{
					this.m_D17S250Instability = value;
					this.NotifyPropertyChanged("D17S250Instability");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string D2S123Instability
		{
			get { return this.m_D2S123Instability; }
			set
			{
				if (this.m_D2S123Instability != value)
				{
					this.m_D2S123Instability = value;
					this.NotifyPropertyChanged("D2S123Instability");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string Interpretation
		{
			get { return this.m_Interpretation; }
			set
			{
				if (this.m_Interpretation != value)
				{
					this.m_Interpretation = value;
					this.NotifyPropertyChanged("Interpretation");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "1000", "null", "varchar")]
		public string Method
		{
			get { return this.m_Method; }
			set
			{
				if (this.m_Method != value)
				{
					this.m_Method = value;
					this.NotifyPropertyChanged("Method");
				}
			}
		}

		[PersistentProperty()]
		[PersistentDataColumnProperty(true, "500", "null", "varchar")]
		public string TestDevelopment
		{
			get { return this.m_TestDevelopment; }
			set
			{
				if (this.m_TestDevelopment != value)
				{
					this.m_TestDevelopment = value;
					this.NotifyPropertyChanged("TestDevelopment");
				}
			}
		}

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			StringBuilder result = new StringBuilder();

			result.AppendLine("Result: " + this.m_Result);
			result.AppendLine();

			result.AppendLine("Instability Level: " + this.m_InstabilityLevel);
			result.AppendLine("BAT25 Instability: " + this.m_BAT25Instability);
			result.AppendLine("BAT26 Instability: " + this.m_BAT26Instability);
			result.AppendLine("D5S346 Instability: " + this.m_D5S346Instability);
			result.AppendLine("D17S250 Instability: " + this.m_D17S250Instability);
			result.AppendLine("D2S123 Instability: " + this.m_D2S123Instability);
			result.AppendLine();

			result.AppendLine("Interpretation: " + this.m_Interpretation);
			result.AppendLine();

			return result.ToString();
		}
	}
}
