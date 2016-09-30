using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Model
{
    [PersistentClass("tblStainTest", "YPIDATA")]
	public class StainTest : INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;

		private string m_ObjectId;
		private int m_StainTestId;
        private int m_TestId;
        private string m_CptCode;
        private int m_CptCodeQuantity;
        private int m_ImmunoCommentId;
        private string m_ControlComment;
        private string m_StainType;

		public StainTest()
		{

		}

		[PersistentDocumentIdProperty()]
		[PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ObjectId
		{
			get { return this.m_ObjectId; }
			set
			{
				if (this.m_ObjectId != value)
				{
					this.m_ObjectId = value;
					this.NotifyPropertyChanged("ObjectId");
				}
			}
		}

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "11", "null", "int")]
		public int StainTestId
		{
			get { return this.m_StainTestId; }
			set
			{
				if (this.m_StainTestId != value)
				{
					this.m_StainTestId = value;
					this.NotifyPropertyChanged("StainTestId");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(false, "11", "null", "int")]
		public int TestId
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
        [PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public string CptCode
		{
			get { return this.m_CptCode; }
			set
			{
				if (this.m_CptCode != value)
				{
					this.m_CptCode = value;
					this.NotifyPropertyChanged("CptCode");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "20", "null", "varchar")]
		public int CptCodeQuantity
		{
			get { return this.m_CptCodeQuantity; }
			set
			{
				if (this.m_CptCodeQuantity != value)
				{
					this.m_CptCodeQuantity = value;
					this.NotifyPropertyChanged("CptCodeQuantity");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
		public int ImmunoCommentId
		{
			get { return this.m_ImmunoCommentId; }
			set
			{
				if (this.m_ImmunoCommentId != value)
				{
					this.m_ImmunoCommentId = value;
					this.NotifyPropertyChanged("ImmunoCommentId");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string ControlComment
		{
			get { return this.m_ControlComment; }
			set
			{
				if (this.m_ControlComment != value)
				{
					this.m_ControlComment = value;
					this.NotifyPropertyChanged("ControlComment");
				}
			}
		}

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
		public string StainType
		{
			get { return this.m_StainType; }
			set
			{
				if (this.m_StainType != value)
				{
					this.m_StainType = value;
					this.NotifyPropertyChanged("StainType");
				}
			}
		}

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }			
	}
}
