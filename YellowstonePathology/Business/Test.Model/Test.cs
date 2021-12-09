using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.ComponentModel;
using Newtonsoft.Json;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Model
{
	public class Test : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

        protected YellowstonePathology.Test.Model.ResultItemCollection m_ResultItemCollection;

        protected string m_OrderComment;
        protected bool m_IsBillable;
        protected bool m_HasGCode;
        protected bool m_HasCptCodeLevels;
        protected bool m_IsDualOrder;        

        protected string m_TestId;
        protected string m_TestName;
        protected string m_TestAbbreviation;
        protected bool m_Active;
        protected int m_StainResultGroupId;
        protected string m_AliquotType;
        protected bool m_NeedsAcknowledgement;
        protected string m_DefaultResult;
        protected bool m_RequestForAdditionalReport;
        protected bool m_UseWetProtocol;
        protected bool m_PerformedByHand;        

        public Test()
		{
			this.m_ResultItemCollection = new YellowstonePathology.Test.Model.ResultItemCollection();
            this.m_UseWetProtocol = false;
            this.m_PerformedByHand = false;      
		}

        public Test(string testId, string testName)
        {
            this.m_ResultItemCollection = new YellowstonePathology.Test.Model.ResultItemCollection();
            this.m_TestId = testId;
            this.m_TestName = testName;
        }

        public Test(Stain.Model.Stain stain)
        {            
            this.m_ResultItemCollection = new YellowstonePathology.Test.Model.ResultItemCollection();
            this.m_OrderComment = stain.OrderComment;
            this.m_IsBillable = stain.IsBillable;
            this.m_HasGCode = stain.HasGCode;
            this.m_HasCptCodeLevels = stain.HasCptCodeLevels;
            this.m_IsDualOrder = stain.IsDualOrder;
            this.m_TestId = stain.TestId;
            this.m_TestName = stain.StainName;
            this.m_TestAbbreviation = stain.StainAbbreviation;
            this.m_Active = stain.Active;
            this.m_StainResultGroupId = stain.StainResultGroupId;
            this.m_AliquotType = stain.AliquotType;
            this.m_NeedsAcknowledgement = stain.NeedsAcknowledgement;
            this.m_DefaultResult = stain.DefaultResult;
            this.m_RequestForAdditionalReport = stain.RequestForAdditionalReport;
            this.m_UseWetProtocol = stain.UseWetProtocol;
            this.m_PerformedByHand = stain.PerformedByHand;            
        }


        [PersistentCollection()]
        public YellowstonePathology.Test.Model.ResultItemCollection ResultItemCollection
		{
			get { return m_ResultItemCollection; }
            set
            {
                this.m_ResultItemCollection = value;
                NotifyPropertyChanged("ResultItemCollection");
            }
		}

        public virtual YellowstonePathology.Business.Billing.Model.CptCode GetGradedCptCode(bool isTechnicalOnly)
        {
            if (isTechnicalOnly == false)
            {
                return Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", null);
            }
            else
            {
                return Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88360", "TC");
            }
        }

        public virtual YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(bool isTechnicalOnly)
        {
            throw new Exception("Not Implemented Here");
        }

        public virtual YellowstonePathology.Business.Billing.Model.CptCode GetGCode(bool isTechnicalOnly)
        {
            throw new Exception("Not Implemented Here");
        }

        public virtual YellowstonePathology.Business.Billing.Model.CptCode GetCptCode(CptCodeLevelEnum cptCodeLevel, bool isTechnicalOnly)
        {
            throw new Exception("Not Implemented Here");
        }

        public virtual YellowstonePathology.Business.Billing.Model.CptCode GetGCode(CptCodeLevelEnum cptCodeLevel, bool isTechnicalOnly)
        {
            throw new Exception("Not Implemented Here");
        }

        [PersistentProperty()]
        public string OrderComment
		{
			get { return this.m_OrderComment; }
			set { this.m_OrderComment = value; }
		}

        [PersistentProperty()]
        public bool IsBillable
        {
            get { return this.m_IsBillable; }
            set { this.m_IsBillable = value; }
        }

        public bool HasGCode
        {
            get { return this.m_HasGCode; }
            set { this.m_HasGCode = value; }
        }

        [PersistentProperty()]
        public bool IsDualOrder
        {
            get { return this.m_IsDualOrder; }
            set { this.m_IsDualOrder = value; }
        }

        [PersistentProperty()]
        public bool HasCptCodeLevels
        {
            get { return this.m_HasCptCodeLevels; }
            set { this.m_HasCptCodeLevels = value; }
        }

        [PersistentPrimaryKeyProperty(false)]
        public string TestId
		{
			get { return this.m_TestId; }
			set
			{
				if (this.m_TestId != value)
				{
					this.m_TestId = value;
					this.NotifyPropertyChanged("TestId");
				}
			}
		}

        [PersistentProperty()]
        public string TestName
		{
			get { return this.m_TestName; }
			set
			{
				if (this.m_TestName != value)
				{
					this.m_TestName = value;
					this.NotifyPropertyChanged("TestName");
				}
			}
		}

        [PersistentProperty()]
        public string TestAbbreviation
        {
            get { return this.m_TestAbbreviation; }
            set
            {
                if (this.m_TestAbbreviation != value)
                {
                    this.m_TestAbbreviation  = value;
                    this.NotifyPropertyChanged("TestAbbreviation");
                }
            }
        }

        [PersistentProperty()]
        public bool Active
		{
			get { return this.m_Active; }
			set
			{
				if (this.m_Active != value)
				{
					this.m_Active = value;
					this.NotifyPropertyChanged("Active");
				}
			}
		}

        [PersistentProperty()]
        public int StainResultGroupId
		{
			get { return this.m_StainResultGroupId; }
			set
			{
				if (this.m_StainResultGroupId != value)
				{
					this.m_StainResultGroupId = value;
					this.NotifyPropertyChanged("StainResultGroupId");
				}
			}
		}

        [PersistentProperty()]
        public string AliquotType
		{
			get { return this.m_AliquotType; }
			set
			{
				if (this.m_AliquotType != value)
				{
					this.m_AliquotType = value;
					this.NotifyPropertyChanged("AliquotType");
				}
			}
		}

        [PersistentProperty()]
        public bool NeedsAcknowledgement
		{
			get { return this.m_NeedsAcknowledgement; }
			set
			{
				if (this.m_NeedsAcknowledgement != value)
				{
					this.m_NeedsAcknowledgement = value;
					this.NotifyPropertyChanged("NeedsAcknowledgement");
				}
			}
		}

        [PersistentProperty()]
        public bool UseWetProtocol
        {
            get { return this.m_UseWetProtocol; }
            set
            {
                if (this.m_UseWetProtocol != value)
                {
                    this.m_UseWetProtocol = value;
                    this.NotifyPropertyChanged("UseWetProtocol");
                }
            }
        }

        [PersistentProperty()]
        public bool PerformedByHand
        {
            get { return this.m_PerformedByHand; }
            set
            {
                if (this.m_PerformedByHand != value)
                {
                    this.m_PerformedByHand = value;
                    this.NotifyPropertyChanged("PerformedByHand");
                }
            }
        }

        [PersistentProperty()]
        public string DefaultResult
		{
			get { return this.m_DefaultResult; }
			set
			{
				if (this.m_DefaultResult != value)
				{
					this.m_DefaultResult = value;
					this.NotifyPropertyChanged("DefaultResult");
				}
			}
		}

        [PersistentProperty()]
        public bool RequestForAdditionalReport
		{
			get { return this.m_RequestForAdditionalReport; }
			set
			{
				if (this.m_RequestForAdditionalReport != value)
				{
					this.m_RequestForAdditionalReport = value;
					this.NotifyPropertyChanged("RequestForAdditionalReport");
				}
			}
		}

        public virtual string GetCodeableType(bool orderedAsDual)
        {
            string result = null;
            return result;
        }

        public string HistologyDisplayString        
        {
            get
            {
                string result = this.m_TestAbbreviation;
                if (this.m_UseWetProtocol == true) result = result + "(W)";
                return result;
            }            
        }

        public void NotifyPropertyChanged(String info)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		public static int CompareByTestName(Test x, Test y)
		{
			if (x == null)
			{
				if (y == null)
				{
					return 0;
				}
				else
				{
					return -1;
				}
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				else
				{
					int retval = x.m_TestName.CompareTo(y.m_TestName);
					if (retval != 0)
					{
						return retval;
					}
					else
					{
						return x.m_TestName.CompareTo(y.m_TestName);
					}
				}
			}
		}

        public string ToJSON()
        {
            string result = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.None
            });

            return result;
        }
    }
}
