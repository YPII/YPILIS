using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test
{
    public class CanSetDistributionResult
    {
        private PanelSet.Model.PanelSet m_PanelSet;
        private bool m_CanSetProvidedDistributionType;
        private string m_DistributionTypeToCheck;
        private string m_DistributionTypeToSet;

        public CanSetDistributionResult(PanelSet.Model.PanelSet panelSet, string distributionTypeToCheck)
        {
            this.m_PanelSet = panelSet;
            this.m_DistributionTypeToCheck = distributionTypeToCheck;
        }

        public PanelSet.Model.PanelSet PanelSet
        {
            get { return this.m_PanelSet; }
        }
        public bool CanSetProvidedDistributionType
        {
            get { return this.m_CanSetProvidedDistributionType; }
            set { this.m_CanSetProvidedDistributionType = value; }
        }

        public string DistributionTypeToCheck
        {
            get { return this.m_DistributionTypeToCheck; }
        }

        public string DistributionTypeToSet
        {
            get { return this.m_DistributionTypeToSet; }
            set { this.m_DistributionTypeToSet = value; }
        }
    }
}
