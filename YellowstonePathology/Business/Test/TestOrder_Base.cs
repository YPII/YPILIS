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
        protected string m_ClientAccessionNo;

        public TestOrder_Base()
        {

        }

        [PersistentDocumentIdProperty()]
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
                    this.m_TestAbbreviation = value;
                    this.NotifyPropertyChanged("TestAbbreviation");
                }
            }
        }

        [PersistentProperty()]
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

        [PersistentProperty()]
        public string ClientAccessionNo
        {
            get { return this.m_ClientAccessionNo; }
            set
            {
                if (this.m_ClientAccessionNo != value)
                {
                    this.m_ClientAccessionNo = value;
                    this.NotifyPropertyChanged("ClientAccessionNo");
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
