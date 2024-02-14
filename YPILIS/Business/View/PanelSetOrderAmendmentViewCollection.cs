using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.View
{
    public class PanelSetOrderAmendmentViewCollection : ObservableCollection<PanelSetOrderAmendmentView>
    {
        public PanelSetOrderAmendmentViewCollection()
        { }

        public PanelSetOrderAmendmentViewCollection(Test.AccessionOrder accessionOrder)
        {
            this.Refresh(accessionOrder);
        }

        public void Refresh(Test.AccessionOrder accessionOrder)
        {
            this.Clear();
            foreach (Test.PanelSetOrder panelSetOrder in accessionOrder.PanelSetOrderCollection)
            {
                this.Add(new View.PanelSetOrderAmendmentView(accessionOrder, panelSetOrder));
            }
        }
    }
}
