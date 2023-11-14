using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.ClientOrder.Model
{
    public class ExternalOrderIdsCollection : ObservableCollection<ExternalOrderIds>
    {
        public ExternalOrderIdsCollection() { }

        public ExternalOrderIdsCollection(ClientOrderCollection clientOrders)
        {
            foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder in clientOrders)
            {
                if (string.IsNullOrEmpty(clientOrder.ExternalOrderId) == false && clientOrder.PanelSetId.HasValue)
                {
                    ExternalOrderIds externalOrderIds = new Model.ExternalOrderIds(clientOrder);
                    this.Add(externalOrderIds);
                }
            }
        }

        public static ExternalOrderIdsCollection FromFormattedValue(string formattedValue)
        {
            ExternalOrderIdsCollection result = new Model.ExternalOrderIdsCollection();
            if (string.IsNullOrEmpty(formattedValue) == false)
            {
                string[] values = formattedValue.Split(new char[] { '|' });
                foreach(string value in values)
                {
                    if (value.IndexOf(',') > 0)
                    {
                        ExternalOrderIds externalOrderIds = new ExternalOrderIds(value);
                        result.Add(externalOrderIds);
                    }
                }
            }

            return result;
        }

        public string ToFormattedValue()
        {
            StringBuilder result = new StringBuilder();
            foreach (ExternalOrderIds externalOrderIds in this)
            {
                result.Append(externalOrderIds.FormattedValue);
                result.Append("|");
            }

            result.Remove(result.Length - 1, 1);
            return result.ToString();
        }


        public bool Exists(int panelSetId)
        {
            bool result = false;
            foreach (ExternalOrderIds externalOrderIds in this)
            {
                if (externalOrderIds.PanelSetId == panelSetId)
                {
                    result = true;
                    break;
                }
            }

            return result;
        }

        public string GetExternalOrderId(int panelSetId)
        {
            string result = null;
            foreach (ExternalOrderIds externalOrderIds in this)
            {
                if (externalOrderIds.PanelSetId == panelSetId)
                {
                    result = externalOrderIds.ExternalOrderId;
                    break;
                }
            }
            return result;
        }

        public string GetUniversalServiceId(int panelSetId)
        {
            ExternalOrderIds externalOrderIds = this.FirstOrDefault(o => o.PanelSetId == panelSetId);
            return externalOrderIds == null ? null : externalOrderIds.UniversalServiceId;
        }
    }
}
