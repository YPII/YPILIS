using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace YellowstonePathology.Business.Rules
{
	public class RuleBrowserUI
	{        
        RulesNameSpaceList m_RulesNameSpaceList;
        RulesClassList m_RulesClassList;
        
        public RuleBrowserUI()
        {
            this.m_RulesNameSpaceList = new RulesNameSpaceList();
            this.m_RulesClassList = new RulesClassList();
        }

        public RulesNameSpaceList RulesNameSpaceList
        {
            get { return this.m_RulesNameSpaceList; }
        }

        public RulesClassList RulesClassList
        {
            get { return this.m_RulesClassList; }
        }
    }

    public class RulesNameSpaceList : List<RulesNameSpaceItem>
    {
        public RulesNameSpaceList()
        {
            this.Add(new RulesNameSpaceItem("YellowstonePathology.Business.Rules.Billing"));
            this.Add(new RulesNameSpaceItem("YellowstonePathology.Business.Rules.Common"));
            this.Add(new RulesNameSpaceItem("YellowstonePathology.Business.Rules.Surgical"));
            this.Add(new RulesNameSpaceItem("YellowstonePathology.Business.Rules.PanelOrder"));
            this.Add(new RulesNameSpaceItem("YellowstonePathology.Business.Rules.PanelSetOrder"));
            this.Add(new RulesNameSpaceItem("YellowstonePathology.Business.Rules.ReportDistribution"));
			this.Add(new RulesNameSpaceItem("YellowstonePathology.Business.Rules.Typing"));
			this.Add(new RulesNameSpaceItem("YellowstonePathology.Business.Rules.Creation"));
		}
    }

    public class RulesNameSpaceItem
    {
        string m_NameSpace;        

        public RulesNameSpaceItem(string nameSpace)
        {
            this.m_NameSpace = nameSpace;
        }

        public string NameSpace
        {
            get { return this.m_NameSpace; }
            set { this.m_NameSpace = value; }
        }
    }

    public class RulesClassList : ObservableCollection<RulesClassItem>
    {
        public void Fill(string nameSpace)
        {
            this.Clear();
            Assembly asm = Assembly.GetExecutingAssembly();            
            foreach (Type type in asm.GetTypes())
            {
                if (type.Namespace == nameSpace)
                {
                    RulesClassItem ruleClassItem = new RulesClassItem();
                    ruleClassItem.ClassType = type;
                    this.Add(ruleClassItem);
                }                    
            }
        }
    }    
}
