using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

///comment 11 eric
//This is sids comment
namespace YellowstonePathology.UI.Billing
{
    public class CodeSelection
    {
        PanelSetCptCodeCollection m_PanelSetCptCodeCollection;
        ObservableCollection<CptCodeItem> m_CptCodeView;

        public CodeSelection()
        {
            this.m_PanelSetCptCodeCollection = new PanelSetCptCodeCollection();
            this.m_CptCodeView = new ObservableCollection<CptCodeItem>();
        }

        public ObservableCollection<CptCodeItem> CptCodeView
        {
            get { return this.m_CptCodeView; }
        }

        public PanelSetCptCodeCollection PanelSetCptCodeCollection
        {
            get { return this.m_PanelSetCptCodeCollection; }
        }

        public void SetFilterByPanelSetId(int panelSetId)
        {
            this.m_CptCodeView.Clear();
            foreach (PanelSetCptCodeItem panelSetCptCodeItem in this.m_PanelSetCptCodeCollection)
            {
                if (panelSetCptCodeItem.PanelSetId == panelSetId)
                {
                    foreach (CptCodeItem cptCodeItem in panelSetCptCodeItem.CptCodeItemCollection)
                    {
                        this.m_CptCodeView.Add(cptCodeItem);
                    }
                }
            }
        }

        public void SetFilterByAll()
        {
            this.m_CptCodeView.Clear();
            foreach (PanelSetCptCodeItem panelSetCptCodeItem in this.m_PanelSetCptCodeCollection)
            {
                foreach (CptCodeItem cptCodeItem in panelSetCptCodeItem.CptCodeItemCollection)
                {
                    this.m_CptCodeView.Add(cptCodeItem);
                }
            }
        }
    }
}
