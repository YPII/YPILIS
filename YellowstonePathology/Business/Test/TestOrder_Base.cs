using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Xml.Linq;
using System.Xml.Serialization;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Test.Model
{
    [PersistentClass("tblTestOrder", "YPIDATA")]
    public class TestOrder_Base : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_ObjectId;
        protected string m_TestOrderId;
        protected string m_PanelOrderId;
        protected string m_AliquotOrderId;
        protected int m_TestId;
        protected string m_TestName;
        protected string m_TestAbbreviation;
        protected string m_Result;
        protected string m_Comment;
        protected bool m_OrderedAsDual;
        protected bool m_NoCharge;

        public TestOrder_Base()
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
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
        public string TestOrderId
        {
            get { return this.m_TestOrderId; }
            set
            {
                if (this.m_TestOrderId != value)
                {
                    this.m_TestOrderId = value;
                    this.NotifyPropertyChanged("TestOrderId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string PanelOrderId
        {
            get { return this.m_PanelOrderId; }
            set
            {
                if (this.m_PanelOrderId != value)
                {
                    this.m_PanelOrderId = value;
                    this.NotifyPropertyChanged("PanelOrderId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string AliquotOrderId
        {
            get { return this.m_AliquotOrderId; }
            set
            {
                if (this.m_AliquotOrderId != value)
                {
                    this.m_AliquotOrderId = value;
                    this.NotifyPropertyChanged("AliquotOrderId");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "11", "null", "int")]
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
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "100", "null", "varchar")]
        public string TestAbbreviation
        {
            get { return this.m_TestAbbreviation; }
            set
            {
                if (this.m_TestAbbreviation != value)
                {
                    this.m_TestAbbreviation = value;
                    this.NotifyPropertyChanged("TestAbbreviation");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "1000", "null", "varchar")]
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
        [PersistentDataColumnProperty(false, "1", "0", "bit")]
        public bool OrderedAsDual
        {
            get { return this.m_OrderedAsDual; }
            set
            {
                if (this.m_OrderedAsDual != value)
                {
                    this.m_OrderedAsDual = value;
                    this.NotifyPropertyChanged("OrderedAsDual");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "1", "0", "bit")]
        public bool NoCharge
        {
            get { return this.m_NoCharge; }
            set
            {
                if (this.m_NoCharge != value)
                {
                    this.m_NoCharge = value;
                    this.NotifyPropertyChanged("NoCharge");
                }
            }
        }

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
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
