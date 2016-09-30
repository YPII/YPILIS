using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Login
{
	public class SlideLabelBindingSource : System.Windows.Forms.BindingSource
	{
        ObservableCollection<YellowstonePathology.Business.Label.Model.AccessionLabel> m_AccessionLabelCollection;
        object m_Writer;

        public SlideLabelBindingSource(object writer)
        {
            this.m_Writer = writer;
            this.m_AccessionLabelCollection = new ObservableCollection<Business.Label.Model.AccessionLabel>();
            this.DataSource = this.m_AccessionLabelCollection;
        }

        public ObservableCollection<YellowstonePathology.Business.Label.Model.AccessionLabel> AccessionLabelCollection
        {
            get { return this.m_AccessionLabelCollection; }            
        }

        public void SetLabelData()
        {
            string reportNumbers = string.Empty;
            foreach (YellowstonePathology.Business.Label.Model.AccessionLabel accessionNameLabel in this.m_AccessionLabelCollection)
            {
				YellowstonePathology.Business.Test.AccessionOrder accessionOrder = YellowstonePathology.Business.Persistence.DocumentGateway.Instance.PullAccessionOrder(accessionNameLabel.MasterAccessionNo, this.m_Writer);
                accessionNameLabel.PatientLastName = accessionOrder.PLastName;
                accessionNameLabel.PatientFirstName = accessionOrder.PFirstName;
            }            
        }        

        public override void Clear()
        {
            for (int idx = this.m_AccessionLabelCollection.Count - 1; idx > -1; idx--)
			{
                this.m_AccessionLabelCollection.RemoveAt(idx);
			}
            base.Clear();
        }        

        public void Save(bool releaseLock)
        {
			
        }               
	}
}
