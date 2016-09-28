using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Audit.Model;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.LLP
{
	[PersistentClass("tblFlowLeukemia", "tblPanelSetOrder", "YPIDATA")]
	public class PanelSetOrderLeukemiaLymphoma : PanelSetOrder, YellowstonePathology.Business.Interface.ILeukemiaLymphomaResult
	{		
        private int m_SpecimenNumber;
		private int m_GatingPopulation;
		private string m_GatingPopulationV2;
		private int m_LymphocyteCount;
		private double m_GPLymphocytePrc;
		private int m_MonocyteCount;
		private int m_MyeloidCount;
		private int m_DimCD45ModSSCount;
		private int m_OtherCount;
		private string m_OtherName;
		private int? m_LightScatter;
		//private string m_LightScatterV2;
		private double m_BCellPercent;
		private double m_TCellPercent;
		private double m_NKCellPercent;
		private double m_MyeloidCellPercent;
		private string m_InterpretiveComment;
		private string m_Impression;
		private int m_SpecimenViability;
		private double m_SpecimenViabilityPercent;
		private string m_CellPopulationOfInterest;
		private string m_TestResult;
		private string m_CellDescription;
		private string m_BTCellSelection;
		private string m_KappaLambda;
		private string m_EGateCD34Percent;
		private string m_EGateCD117Percent;
		private bool m_TechFinal;
		private Nullable<DateTime> m_TechFinalDate;
		private Nullable<DateTime> m_TechFinalTime;
		private int m_TechFinaledById;		
		private string m_ReportComment;
		private string m_TestCancelledComment;
		private bool m_TestCancelled;		

		private Flow.FlowMarkerCollection m_FlowMarkerCollection;

		public PanelSetOrderLeukemiaLymphoma()
		{
			this.m_FlowMarkerCollection = new Flow.FlowMarkerCollection();
		}

		public PanelSetOrderLeukemiaLymphoma(string masterAccessionNo, string reportNo, string objectId, 
            YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet,
            YellowstonePathology.Business.Interface.IOrderTarget orderTarget,
			bool distribute)
			: base(masterAccessionNo, reportNo, objectId, panelSet, orderTarget, distribute)
		{
            YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry panelSetFlowCytometry = (YellowstonePathology.Business.PanelSet.Model.FlowCytometry.PanelSetFlowCytometry)panelSet;
			this.m_FlowMarkerCollection = new Flow.FlowMarkerCollection();            
		}

        [PersistentCollection()]
		public Flow.FlowMarkerCollection FlowMarkerCollection
		{
			get { return this.m_FlowMarkerCollection; }
			set
			{
				this.m_FlowMarkerCollection = value;
				NotifyPropertyChanged("FlowMarkerCollection");
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public int SpecimenNumber
        {
            get { return this.m_SpecimenNumber; }
            set
            {
                if (this.m_SpecimenNumber != value)
                {
                    this.m_SpecimenNumber = value;
                    this.NotifyPropertyChanged("SpecimenNumber");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int GatingPopulation
		{
			get { return this.m_GatingPopulation; }
			set
			{
				if (this.m_GatingPopulation != value)
				{
					this.m_GatingPopulation = value;
					this.NotifyPropertyChanged("GatingPopulation");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string GatingPopulationV2
		{
			get { return this.m_GatingPopulationV2; }
			set
			{
				if (this.m_GatingPopulationV2 != value)
				{
					this.m_GatingPopulationV2 = value;
					this.NotifyPropertyChanged("GatingPopulationV2");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int LymphocyteCount
		{
			get { return this.m_LymphocyteCount; }
			set
			{
				if (this.m_LymphocyteCount != value)
				{
					this.m_LymphocyteCount = value;
					this.NotifyPropertyChanged("LymphocyteCount");
					this.RefreshGatingPercent();
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "0", "float")]
        public double GPLymphocytePrc
		{
			get { return this.m_GPLymphocytePrc; }
			set
			{
				if (this.m_GPLymphocytePrc != value)
				{
					this.m_GPLymphocytePrc = value;
					this.NotifyPropertyChanged("GPLymphocytePrc");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int MonocyteCount
		{
			get { return this.m_MonocyteCount; }
			set
			{
				if (this.m_MonocyteCount != value)
				{
					this.m_MonocyteCount = value;
					this.NotifyPropertyChanged("MonocyteCount");
					this.RefreshGatingPercent();
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int MyeloidCount
		{
			get { return this.m_MyeloidCount; }
			set
			{
				if (this.m_MyeloidCount != value)
				{
					this.m_MyeloidCount = value;
					this.NotifyPropertyChanged("MyeloidCount");
					this.RefreshGatingPercent();
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int DimCD45ModSSCount
		{
			get { return this.m_DimCD45ModSSCount; }
			set
			{
				if (this.m_DimCD45ModSSCount != value)
				{
					this.m_DimCD45ModSSCount = value;
					this.NotifyPropertyChanged("DimCD45ModSSCount");
					this.RefreshGatingPercent();
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int OtherCount
		{
			get { return this.m_OtherCount; }
			set
			{
				if (this.m_OtherCount != value)
				{
					this.m_OtherCount = value;
					this.NotifyPropertyChanged("OtherCount");
					this.RefreshGatingPercent();
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "200", "null", "varchar")]
        public string OtherName
		{
			get { return this.m_OtherName; }
			set
			{
				if (this.m_OtherName != value)
				{
					this.m_OtherName = value;
					this.NotifyPropertyChanged("OtherName");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int? LightScatter
		{
			get { return this.m_LightScatter; }
			set
			{
				if (this.m_LightScatter != value)
				{
					this.m_LightScatter = value;
					this.NotifyPropertyChanged("LightScatter");
				}
			}
		}

        /*[PersistentProperty()]
		public string LightScatterV2
		{
			get { return this.m_LightScatterV2; }
			set
			{
				if (this.m_LightScatterV2 != value)
				{
					this.m_LightScatterV2 = value;
					this.NotifyPropertyChanged("LightScatterV2");
				}
			}
		}*/

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "0", "float")]
        public double BCellPercent
		{
			get { return this.m_BCellPercent; }
			set
			{
				if (this.m_BCellPercent != value)
				{
					this.m_BCellPercent = value;
					this.NotifyPropertyChanged("BCellPercent");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "0", "float")]
        public double TCellPercent
		{
			get { return this.m_TCellPercent; }
			set
			{
				if (this.m_TCellPercent != value)
				{
					this.m_TCellPercent = value;
					this.NotifyPropertyChanged("TCellPercent");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "0", "float")]
        public double NKCellPercent
		{
			get { return this.m_NKCellPercent; }
			set
			{
				if (this.m_NKCellPercent != value)
				{
					this.m_NKCellPercent = value;
					this.NotifyPropertyChanged("NKCellPercent");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "0", "float")]
        public double MyeloidCellPercent
		{
			get { return this.m_MyeloidCellPercent; }
			set
			{
				if (this.m_MyeloidCellPercent != value)
				{
					this.m_MyeloidCellPercent = value;
					this.NotifyPropertyChanged("MyeloidCellPercent");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "5000", "null", "varchar")]
        public string InterpretiveComment
		{
			get { return this.m_InterpretiveComment; }
			set
			{
				if (this.m_InterpretiveComment != value)
				{
					this.m_InterpretiveComment = value;
					this.NotifyPropertyChanged("InterpretiveComment");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string Impression
		{
			get { return this.m_Impression; }
			set
			{
				if (this.m_Impression != value)
				{
					this.m_Impression = value;
					this.NotifyPropertyChanged("Impression");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "0", "int")]
        public int SpecimenViability
		{
			get { return this.m_SpecimenViability; }
			set
			{
				if (this.m_SpecimenViability != value)
				{
					this.m_SpecimenViability = value;
					this.NotifyPropertyChanged("SpecimenViability");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "", "0", "float")]
        public double SpecimenViabilityPercent
		{
			get { return this.m_SpecimenViabilityPercent; }
			set
			{
				if (this.m_SpecimenViabilityPercent != value)
				{
					this.m_SpecimenViabilityPercent = value;
					this.NotifyPropertyChanged("SpecimenViabilityPercent");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string CellPopulationOfInterest
		{
			get { return this.m_CellPopulationOfInterest; }
			set
			{
				if (this.m_CellPopulationOfInterest != value)
				{
					this.m_CellPopulationOfInterest = value;
					this.NotifyPropertyChanged("CellPopulationOfInterest");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string TestResult
		{
			get { return this.m_TestResult; }
			set
			{
				if (this.m_TestResult != value)
				{
					this.m_TestResult = value;
					this.NotifyPropertyChanged("TestResult");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string CellDescription
		{
			get { return this.m_CellDescription; }
			set
			{
				if (this.m_CellDescription != value)
				{
					this.m_CellDescription = value;
					this.NotifyPropertyChanged("CellDescription");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string BTCellSelection
		{
			get { return this.m_BTCellSelection; }
			set
			{
				if (this.m_BTCellSelection != value)
				{
					this.m_BTCellSelection = value;
					this.NotifyPropertyChanged("BTCellSelection");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string KappaLambda
		{
			get { return this.m_KappaLambda; }
			set
			{
				if (this.m_KappaLambda != value)
				{
					this.m_KappaLambda = value;
					this.NotifyPropertyChanged("KappaLambda");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string EGateCD34Percent
		{
			get { return this.m_EGateCD34Percent; }
			set
			{
				if (this.m_EGateCD34Percent != value)
				{
					this.m_EGateCD34Percent = value;
					this.NotifyPropertyChanged("EGateCD34Percent");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string EGateCD117Percent
		{
			get { return this.m_EGateCD117Percent; }
			set
			{
				if (this.m_EGateCD117Percent != value)
				{
					this.m_EGateCD117Percent = value;
					this.NotifyPropertyChanged("EGateCD117Percent");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool TechFinal
		{
			get { return this.m_TechFinal; }
			set
			{
				if (this.m_TechFinal != value)
				{
					this.m_TechFinal = value;
					this.NotifyPropertyChanged("TechFinal");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TechFinalDate
		{
			get { return this.m_TechFinalDate; }
			set
			{
				if (this.m_TechFinalDate != value)
				{
					this.m_TechFinalDate = value;
					this.NotifyPropertyChanged("TechFinalDate");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "3", "null", "datetime")]
        public Nullable<DateTime> TechFinalTime
		{
			get { return this.m_TechFinalTime; }
			set
			{
				if (this.m_TechFinalTime != value)
				{
					this.m_TechFinalTime = value;
					this.NotifyPropertyChanged("TechFinalTime");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "0", "int")]
        public int TechFinaledById
		{
			get { return this.m_TechFinaledById; }
			set
			{
				if (this.m_TechFinaledById != value)
				{
					this.m_TechFinaledById = value;
					this.NotifyPropertyChanged("TechFinaledById");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string ReportComment
		{
			get { return this.m_ReportComment; }
			set
			{
				if (this.m_ReportComment != value)
				{
					this.m_ReportComment = value;
					this.NotifyPropertyChanged("ReportComment");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
        public string TestCancelledComment
		{
			get { return this.m_TestCancelledComment; }
			set
			{
				if (this.m_TestCancelledComment != value)
				{
					this.m_TestCancelledComment = value;
					this.NotifyPropertyChanged("TestCancelledComment");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool TestCancelled
		{
			get { return this.m_TestCancelled; }
			set
			{
				if (this.m_TestCancelled != value)
				{
					this.m_TestCancelled = value;
					this.NotifyPropertyChanged("TestCancelled");
				}
			}
		}        

		public void RefreshGatingPercent()
		{
			this.NotifyPropertyChanged("LymphocyteCountPercent");
			this.NotifyPropertyChanged("MonocyteCountPercent");
			this.NotifyPropertyChanged("MyeloidCountPercent");
			this.NotifyPropertyChanged("DimCD45ModSSCountPercent");
			this.NotifyPropertyChanged("OtherCountPercent");
		}

		public double GatingCountTotal
		{
			get
			{
				return this.m_LymphocyteCount + this.m_MonocyteCount + this.m_MyeloidCount + this.m_DimCD45ModSSCount + this.m_OtherCount;
			}
		}

		public double LymphocyteCountPercent
		{
			get
			{
				double percent = this.m_LymphocyteCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}

		public double MonocyteCountPercent
		{
			get
			{
				double percent = this.m_MonocyteCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}

		public double MyeloidCountPercent
		{
			get
			{
				double percent = this.m_MyeloidCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}

		public double DimCD45ModSSCountPercent
		{
			get
			{
				double percent = this.m_DimCD45ModSSCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}


		public double OtherCountPercent
		{
			get
			{
				double percent = this.m_OtherCount / GatingCountTotal;
				if (percent.ToString() == "NaN")
				{
					return 0;
				}
				else
				{
					return percent;
				}
			}
		}

        public override AuditResult IsOkToFinalize(AccessionOrder accessionOrder)
        {
            AuditResult result = base.IsOkToFinalize(accessionOrder);
            if(result.Status == AuditStatusEnum.OK)
            {
                if (this.m_TechFinal == false)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = ("This case cannot be finaled because it has not been Tech finaled.");
                }
                else if (this.FlowMarkerCollection.HasQuestionMarks() == true)
                {
                    result.Status = AuditStatusEnum.Failure;
                    result.Message = ("Question marks ??? were found in the markers.");
                }
            }
            return result;
        }

		public override string ToResultString(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            StringBuilder result = new StringBuilder();
            result.AppendLine("Test: " + this.m_PanelSetName);
            result.AppendLine();

            result.AppendLine("Impression:");
            result.AppendLine(this.m_Impression);
            result.AppendLine();

            result.AppendLine("Interpetive Comment:");
            result.AppendLine(this.m_InterpretiveComment);

            return result.ToString();
        }
	}
}
