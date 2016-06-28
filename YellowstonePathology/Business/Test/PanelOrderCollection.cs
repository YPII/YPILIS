using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Data;
using System.Windows.Data;
using System.Data.SqlClient;
using System.Xml.Serialization;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Test
{
	public class PanelOrderCollection : ObservableCollection<PanelOrder>
	{
		public const string PREFIXID = "PO";
		public PanelOrderCollection()
		{

		}

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string panelOrderId = element.Element("PanelOrderId").Value;
                    if (this[i].PanelOrderId == panelOrderId)
                    {
                        found = true;
                        break;
                    }
                }
                if (found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }

        public bool HasPriorUnacceptedPanelOrder(PanelOrder currentPanelOrder, int panelId)
        {
            bool result = false;
            foreach (PanelOrder panelOrder in this)
            {
                if (panelOrder.PanelId == panelId)
                {
                    if (panelOrder.Accepted == false)
                    {
                        if (panelOrder.PanelOrderId != currentPanelOrder.PanelOrderId)
                        {
                            if (panelOrder.OrderTime < currentPanelOrder.OrderTime)
                            {
                                result = true;
                                break;
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool HasUnacknowledgededPanelOrder()
        {
            bool result = false;
            foreach (PanelOrder panelOrder in this)
            {
                if (panelOrder.Acknowledged == false)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public YellowstonePathology.Business.Test.PanelOrder GetUnacknowledgedPanelOrder()
		{
			YellowstonePathology.Business.Test.PanelOrder result = null;
			foreach (PanelOrder panelOrder in this)
			{
				if (panelOrder.Acknowledged == false)
				{
					result = panelOrder;
					break;
				}
			}
			return result;
		}

        /*
		public YellowstonePathology.Business.Test.PanelOrder Add(string reportNo, int panelId, string panelName, int orderedById)
		{
			YellowstonePathology.Business.Test.PanelOrder panelOrder = this.GetNew(reportNo, panelId, panelName, orderedById);
			this.Add(panelOrder);
			return panelOrder;
		}

		public YellowstonePathology.Business.Test.PanelOrder GetNew(string reportNo, YellowstonePathology.Business.Panel.Model.Panel panel, int orderedById)
		{
			string panelOrderId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			YellowstonePathology.Business.Test.PanelOrder panelOrder = YellowstonePathology.Business.Test.PanelOrderFactory.CreatePanelOrder(reportNo, panelOrderId, panelOrderId, panel, orderedById);
			return panelOrder;
		}
        */

		public PanelOrder GetByPanelOrderId(string panelOrderId)
		{
			foreach (PanelOrder item in this)
			{
				if (item.PanelOrderId == panelOrderId)
				{
					return item;
				}
			}
			return null;
		}

        public PanelOrder GetPanelByPanelId(int panelId)
        {
            YellowstonePathology.Business.Test.PanelOrder panelOrder = null;
            foreach (YellowstonePathology.Business.Test.PanelOrder poi in this)
            {
                if (poi.PanelId == panelId)
                {
                    panelOrder = poi;
                }
            }
            return panelOrder;
        }

        public bool HasInitialPanel()
        {
            bool result = false;
            foreach (PanelOrder panelOrder in this)
            {
                if(panelOrder.PanelId == 54)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }        

        public PanelOrder GetInitialPanel()
		{
			YellowstonePathology.Business.Test.PanelOrder result = null;
			foreach (PanelOrder panelOrder in this)
			{
				if (panelOrder.PanelId == 54)
				{
					result = panelOrder;
					break;
				}
			}
            return result;			
		}

		public void Remove(object obj)
		{
			PanelOrder item = obj as PanelOrder;
			if (item != null)
			{
				base.Remove(item);
			}
		}

		public bool PanelIdExists(int panelId)
		{
			bool result = false;
			foreach (PanelOrder item in this)
			{
				if (item.PanelId == panelId)
				{
					result = true;
					break;
				}
			}
			return result;
		}


        public bool Exists(string panelOrderId)
        {
            bool result = false;
            foreach (PanelOrder panelOrder in this)
            {
                if (panelOrder.PanelOrderId == panelOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public void AssignBatchId(int panelOrderBatchId)
		{
			foreach (PanelOrder panelOrder in this)
			{
				if (panelOrder.PanelOrderBatchId == 0)
				{
					panelOrder.PanelOrderBatchId = panelOrderBatchId;
				}
			}
		}

		public void RemovePanelFromBatch(int panelOrderBatchId)
		{
			foreach (PanelOrder panelOrder in this)
			{
				if (panelOrder.PanelOrderBatchId == panelOrderBatchId)
				{
					panelOrder.PanelOrderBatchId = 0;
				}
			}
		}

        public YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology GetPrimaryScreening()
        {
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology primaryScreening = null;
            foreach (PanelOrder panelOrder in this)
            {
                if (panelOrder is YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology poc = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (poc.ScreeningType.ToUpper() == "PRIMARY SCREENING")
                    {
                        primaryScreening = poc;
                        break;
                    }
                }
            }
            return primaryScreening;
        }

        public YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology GetPathologistReview()
        {
            YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology primaryScreening = null;
            foreach (PanelOrder panelOrder in this)
            {
                if (panelOrder is YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology poc = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (poc.ScreeningType.ToUpper() == "PATHOLOGIST REVIEW")
                    {
                        primaryScreening = poc;
                        break;
                    }
                }
            }
            return primaryScreening;
        }

        public bool HasPathologistReview()
        {
            bool result = false;
            foreach (PanelOrder panelOrder in this)
            {
                if (panelOrder is YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)
                {
                    YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology poc = (YellowstonePathology.Business.Test.ThinPrepPap.PanelOrderCytology)panelOrder;
                    if (poc.ScreeningType.ToUpper() == "PATHOLOGIST REVIEW")
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelOrder GetUnacceptedPanelOrder()
        {
            YellowstonePathology.Business.Test.PanelOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this)
            {
                if (panelOrder.Accepted == false)
                {
                    result = panelOrder;
                    break;
                }
            }
            return result;
        }

        public int GetAcceptedPanelCount()
        {
            int result = 0;
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this)
            {
                if (panelOrder.Accepted == true)
                {
                    result += 1;
                    break;
                }
            }
            return result;
        }

        public int GetUnacceptedPanelCount()
        {
            int result = 0;
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this)
            {
                if (panelOrder.Accepted == false)
                {
                    result += 1;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.PanelOrder GetLastAcceptedPanelOrder()
        {
            DateTime acceptedDate = DateTime.Parse("1/1/1900 12:00");
            YellowstonePathology.Business.Test.PanelOrder result = null;
            foreach (YellowstonePathology.Business.Test.PanelOrder panelOrder in this)
            {
                if (panelOrder.Accepted == true)
                {
                    if (panelOrder.AcceptedTime >= acceptedDate)
                    {
                        result = panelOrder;
                    }
                }
            }
            return result;
        }        

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }
	}
}
