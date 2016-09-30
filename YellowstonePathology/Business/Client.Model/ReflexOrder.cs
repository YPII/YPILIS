using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Client.Model
{
    public class ReflexOrder
    {
        protected int m_RuleNumber;
        protected string m_ReflexOrderCode;
        protected string m_Description;
        protected YellowstonePathology.Business.PanelSet.Model.PanelSet m_PanelSet;
        
        public ReflexOrder()
        {

        }

        public int RuleNumber
        {
            get { return this.m_RuleNumber; }
            set { this.m_RuleNumber = value; }
        }

        public string ReflexOrderCode
        {
            get { return this.m_ReflexOrderCode; }
            set { this.m_ReflexOrderCode = value; }
        }

        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        public YellowstonePathology.Business.PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
            set { this.m_PanelSet = value; }
        }

        public virtual bool IsRequired(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            throw new Exception("Not implemented here.");
        }
    }
}
