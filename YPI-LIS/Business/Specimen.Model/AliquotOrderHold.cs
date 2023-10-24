using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class AliquotOrderHold
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected string m_AliquotOrderId;
        protected string m_AliquotType;
        protected string m_Description;
        protected string m_EmbeddingInstructions;
        protected string m_Status;

        public AliquotOrderHold()
        {

        }

        [PersistentPrimaryKeyProperty(false)]
        [PersistentDataColumnProperty(false, "100", "null", "varchar")]
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
        [PersistentDataColumnProperty(true, "50", "''", "varchar")]
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
        [PersistentDataColumnProperty(true, "500", "null", "varchar")]
        public string Description
        {
            get { return this.m_Description; }
            set
            {
                if (this.m_Description != value)
                {
                    this.m_Description = value;
                    this.NotifyPropertyChanged("Description");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string Status
        {
            get
            {
                return this.m_Status;
            }
            set
            {
                if (this.m_Status != value)
                {
                    this.m_Status = value;
                    this.NotifyPropertyChanged("Status");
                }
            }
        }

        [PersistentProperty()]
        [PersistentDataColumnProperty(true, "50", "null", "varchar")]
        public string EmbeddingInstructions
        {
            get
            {
                return this.m_EmbeddingInstructions;
            }
            set
            {
                if (this.m_EmbeddingInstructions != value)
                {
                    this.m_EmbeddingInstructions = value;
                    this.NotifyPropertyChanged("EmbeddingInstructions");
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
