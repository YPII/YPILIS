using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.MaterialTracking
{
    public class MaterialTrackingSummaryCollection : ObservableCollection<MaterialTrackingSummary>
    {
        public MaterialTrackingSummaryCollection()
        {

        }

        public void SetTotals()
        {            
            if (this.Count > 0)
            {                
                MaterialTrackingSummary materialTrackingSummaryTotals = new MaterialTrackingSummary(null, true);
                MaterialTrackingSummary materialTrackingSummaryFirst = this[0];

                foreach (MaterialTrackingSummaryColumn materialTrackingSummaryColumnFirst in materialTrackingSummaryFirst.ColumnList)
                {
                    materialTrackingSummaryTotals.ColumnList.Add(new MaterialTrackingSummaryColumn(materialTrackingSummaryColumnFirst.Name, 0));
                }

                foreach (MaterialTrackingSummary materialTrackingSummary in this)
                {
                    for (int x=0; x<materialTrackingSummaryFirst.ColumnList.Count; x++)
                    {
                        materialTrackingSummaryTotals.ColumnList[x].Value += materialTrackingSummary.ColumnList[x].Value;
                    }
                }

                this.Add(materialTrackingSummaryTotals);
            }            
        }
    }
}
