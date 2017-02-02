using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.Data;

namespace YellowstonePathology.Business.Specimen.Model
{
	public class SpecimenOrderCollection : ObservableCollection<SpecimenOrder>
	{
		public bool m_IsLoading;

		public SpecimenOrderCollection()
		{
			this.m_IsLoading = false;
		}        

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for(int i=this.Count - 1; i>-1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string specimenOrderId = element.Element("SpecimenOrderId").Value;
                    if(this[i].SpecimenOrderId == specimenOrderId)
                    {
                        found = true;
                        break;
                    }
                }
                if(found == false)
                {
                    this.RemoveItem(i);
                }
            }
        }

        public bool SpecimenIdExists(string specimenId)
        {
            bool result = false;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenId == specimenId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }
        
        public bool IsLastSpecimen(string specimenOrderId)
        {
            bool result = false;
            if(this.Count != 0)
            {
                if (this[this.Count - 1].SpecimenOrderId == specimenOrderId)
                {
                    result = true;
                }
            }            
            return result;
        }

        public bool IsLastNonPAPSpecimen(string specimenOrderId)
        {
            bool result = false;
            YellowstonePathology.Business.Specimen.Model.SpecimenOrderCollection specimenWithBlocks = this.GetNonPAPSpecimen();
            if(specimenWithBlocks.IsLastSpecimen(specimenOrderId) == true)
            {
                result = true;
            }
            return result;
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder GetThinPrep()
        {
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder result = null;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.Description.Contains("Thin Prep Fluid") == true)
                {
                    result = specimenOrder;
                    break;
                }
            }
            return result;
        }        

		public bool IsLoading
		{
			get { return this.m_IsLoading; }
			set { this.m_IsLoading = value; }
		}

        public bool IsFixationHandled()
        {
            bool result = true;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.FixationDuration == 0)
                {
                    result = false;
                    break;
                }                
            }
            return result;
        }

        public YellowstonePathology.Business.Interface.IOrderTarget GetOrderTarget(string orderTargetId)
        {
            YellowstonePathology.Business.Interface.IOrderTarget result = null;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenOrderId == orderTargetId)
                {
                    result = specimenOrder;
                    break;
                }
                else
                {
                    YellowstonePathology.Business.Interface.IOrderTarget aliquotOrderTarget = specimenOrder.AliquotOrderCollection.GetOrderTarget(orderTargetId);
                    if (aliquotOrderTarget != null)
                    {
                        result = aliquotOrderTarget;
                        break;
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Interface.IOrderTarget GetOrderTarget(YellowstonePathology.Business.Interface.IOrderTargetType orderTargetType)
        {
            YellowstonePathology.Business.Interface.IOrderTarget result = null;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                if (orderTargetType.TypeId == specimenOrder.SpecimenId)
                {
                    result = specimenOrder;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.OrderTargetCollection GetOrderTargetCollection(YellowstonePathology.Business.OrderTargetTypeCollection orderTargetTypeExclusionCollection)
        {
            YellowstonePathology.Business.OrderTargetCollection result = new YellowstonePathology.Business.OrderTargetCollection();
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                if (orderTargetTypeExclusionCollection.Exists(specimenOrder) == false)
                {
                    result.Add(specimenOrder);
                }
            }
            return result;
        }

        public SpecimenOrderCollection GetOrderTargetList(YellowstonePathology.Business.PanelSet.Model.PanelSet panelSet)
        {
            SpecimenOrderCollection result = new SpecimenOrderCollection();
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                if (panelSet.OrderTargetTypeCollectionExclusions.Exists(specimenOrder) == false)
                {
                    result.Add(specimenOrder);
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Interface.IOrderTarget GetFirstOrderTarget(YellowstonePathology.Business.OrderTargetTypeCollection orderTargetRestrictionCollection)
        {
            YellowstonePathology.Business.Interface.IOrderTarget result = null;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (orderTargetRestrictionCollection.Exists(specimenOrder) == true)
                {
                    result = specimenOrder;
                    break;
                }
            }
            return result;
        }

        public SpecimenOrder GetSpecimenOrderByOrderTarget(string orderTargetId)
        {
            SpecimenOrder result = null;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenOrderId == orderTargetId)
                {
                    result = specimenOrder;
                    break;
                }
                else
                {
                    YellowstonePathology.Business.Interface.IOrderTarget aliquotOrderTarget = specimenOrder.AliquotOrderCollection.GetOrderTarget(orderTargetId);
                    if (aliquotOrderTarget != null)
                    {
                        result = specimenOrder;
                        break;
                    }
                }
            }
            return result;
        }

		public bool ContainsString(string text)
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
			{
				if (string.IsNullOrEmpty(specimenOrder.Description) == false)
				{
					if (specimenOrder.Description.ToUpper().Contains(text.ToUpper()) == true)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public void AccessionSpecimen(string masterAccessionNo, YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetailCollection clientOrderDetailCollection)
		{
			foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrderDetailCollection)
			{
				if (clientOrderDetail.Received == true && clientOrderDetail.Accessioned == false)
				{
					this.Add(masterAccessionNo, clientOrderDetail);
					clientOrderDetail.Accessioned = true;
				}
			}
		}

		public SpecimenOrder GetNextItem(string masterAccessionNo)
		{
			string specimenOrderId = this.GetNewId(masterAccessionNo);
			string objectId = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
			SpecimenOrder specimenOrder = new SpecimenOrder(specimenOrderId, objectId, masterAccessionNo);

			return specimenOrder;
		}

		private string GetNewId(string masterAccessionNo)
		{			
			string result = OrderIdParser.GetNextSpecimenOrderId(this, masterAccessionNo);
			return result;
		}

		public SpecimenOrder Add(string masterAccessionNo)
		{
			SpecimenOrder specimenOrder = this.GetNextItem(masterAccessionNo);
			this.Add(specimenOrder);
			this.Renumber();            
			return specimenOrder;
		}

        public int GetFNAPassCount()
        {
            int result = 0;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenType == SpecimenType.FNAPASS)
                {
                    result += 1;
                }
            }
            return result;                
        }

		public SpecimenOrder Add(string masterAccessionNo, YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail)
		{
			SpecimenOrder specimenOrder = this.GetNextItem(masterAccessionNo);
			specimenOrder.FromClientOrderDetail(clientOrderDetail);
            specimenOrder.SpecimenId = clientOrderDetail.SpecimenId;            
			specimenOrder.SpecimenNumber = this.Count + 1;
			this.Add(specimenOrder);
			return specimenOrder;
		}

		public void SetAliquotRequestCount()
		{
			foreach (SpecimenOrder specimenOrder in this)
			{
				specimenOrder.SetAliquotRequestCount();
			}
		}

		public bool FindWordsInDescription(YellowstonePathology.Business.Rules.Surgical.WordSearchList wordSearchList)
		{
			bool result = false;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
			{
				if (string.IsNullOrEmpty(specimenOrder.Description) == false)
				{
					if (wordSearchList.Search(specimenOrder.Description) == true)
					{
						result = true;
						break;
					}
				}
			}
			return result;
		}

		public string GetContainerIdString()
		{
			System.Text.StringBuilder result = new System.Text.StringBuilder();
			foreach (SpecimenOrder specimenOrder in this)
			{
				result.Append(specimenOrder.ContainerId + ",");
			}
			if (result.Length > 1)
			{
				result.Remove(result.Length - 1, 1);
			}
			return result.ToString();
		}

		public void SetNewSpecimenDetails(string specimenOrderId)
		{
			SpecimenOrder specimenOrder = this.GetSpecimenOrderById(specimenOrderId);
			specimenOrder.SpecimenOrderId = specimenOrderId;
			specimenOrder.SpecimenNumber = this.Count;
			specimenOrder.AliquotRequestCount = 1;
			specimenOrder.Accepted = false;
		}

        public int GetBlockCount()
        {
            int result = 0;
            foreach(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {                
                foreach(YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if(aliquotOrder.IsBlock() == true)
                    {
                        result += 1;
                    }
                }
            }
            return result;
        }

		public SpecimenOrder GetSpecimenOrderByBlock(string blockNumber)
		{
			if (string.IsNullOrEmpty(blockNumber) == false)
			{
				int specimenNumber = Convert.ToInt32(blockNumber.Substring(0, 1));
				foreach (SpecimenOrder specimenOrder in this)
				{
					if (specimenOrder.SpecimenNumber == specimenNumber)
					{
						return specimenOrder;
					}
				}
			}
			return null;
		}

        public SpecimenOrder GetSpecimenOrderByTestId(int testId)
        {
            SpecimenOrder result = null;
            foreach (SpecimenOrder specimenOrder in this)
            {
				foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (aliquotOrder.TestOrderCollection.Exists(testId) == true)
                    {
                        result = specimenOrder;
                        break;
                    }
                }
            }
            return result;
        }

        public SpecimenOrder GetSpecimenOrderByAliquotOrderId(string aliquotOrderId)
        {
            SpecimenOrder result = null;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.AliquotOrderCollection.Exists(aliquotOrderId) == true)
                {
                    result = specimenOrder;
                    break;
                }
            }
            return result;
        }

		public SpecimenOrder GetSpecimenOrderById(string specimenOrderId)
		{
			foreach (SpecimenOrder specimenOrder in this)
			{
				if (specimenOrder.SpecimenOrderId == specimenOrderId)
				{
					return specimenOrder;
				}
			}
			return null;
		}

        public SpecimenOrder GetBySpecimenNumber(int specimenNumber)
        {
            SpecimenOrder result = null;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenNumber == specimenNumber)
                {
                    result = specimenOrder;
                }
            }
            return result;
        }

        public SpecimenOrderCollection GetSpecimenWithBlocks()
        {
            SpecimenOrderCollection result = new SpecimenOrderCollection();
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.AliquotOrderCollection.HasBlocks() == true)
                {
                    result.Add(specimenOrder);
                }
            }
            return result;
        }

        public SpecimenOrderCollection GetNonPAPSpecimen()
        {
            SpecimenOrderCollection result = new SpecimenOrderCollection();
            YellowstonePathology.Business.Specimen.Model.SpecimenDefinition.ThinPrepFluid thinPrepFluid = new SpecimenDefinition.ThinPrepFluid();
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenId != thinPrepFluid.SpecimenId)
                {
                    result.Add(specimenOrder);
                }
            }
            return result;
        }

        public SpecimenOrder GetSpecimenOrderByContainerId(string containerId)
		{
			SpecimenOrder result = null;
			foreach (SpecimenOrder specimenOrder in this)
			{
				if (specimenOrder.ContainerId == containerId)
				{
					result = specimenOrder;
				}
			}
			return result;
		}

		public SpecimenOrder GetByAliquotOrderId(string aliquotOrderId)
		{
			SpecimenOrder result = null;
			bool found = false;
			foreach (SpecimenOrder specimenOrder in this)
			{
				foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
				{
					if (aliquotOrder.AliquotOrderId == aliquotOrderId)
					{
						result = specimenOrder;
						found = true;
						break;
					}
				}
				if (found)
				{
					break;
				}
			}
			return result;
		}

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder GetSpecimenOrderBySpecimenId(string specimenId)
        {
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder result = null;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenId == specimenId)
                {
                    result = specimenOrder;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder GetSpecimenOrder(string specimenOrderId)
        {
            YellowstonePathology.Business.Specimen.Model.SpecimenOrder result = null;
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenOrderId == specimenOrderId)
                {
                    result = specimenOrder;
                    break;
                }
            }
            return result;
        }

		public YellowstonePathology.Business.Specimen.Model.SpecimenOrder GetSpecimenOrder(string targetType, string targetId)
        {
			YellowstonePathology.Business.Specimen.Model.SpecimenOrder result = null;
            if (targetType == YellowstonePathology.Business.OrderedOn.Specimen || targetType == YellowstonePathology.Business.OrderedOn.ThinPrepFluid)
            {                
                result = this.GetSpecimenOrderById(targetId);                    
            }
            else if (targetType == YellowstonePathology.Business.OrderedOn.Aliquot)
            {
                result = this.GetByAliquotOrderId(targetId);
            }
            return result;
        }        

		public SpecimenOrder GetByTestOrderId(string testOrderId)
		{
			SpecimenOrder result = null;			
			foreach (SpecimenOrder specimenOrder in this)
			{
				foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
				{
                    if (aliquotOrder.TestOrderCollection.Exists(testOrderId) == true)
                    {
                        result = specimenOrder;
                        break;
                    }
				}				
			}
			return result;
		}

		public void Renumber()
		{
			int specimenNumber = 1;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
			{
				specimenOrder.SpecimenNumber = specimenNumber;
				specimenOrder.Accepted = true;
				specimenNumber++;
			}
		}

		protected override void InsertItem(int index, SpecimenOrder item)
		{
			base.InsertItem(index, item);
			if (this.IsLoading == false)
			{
				if (item.SpecimenNumber == 0)
				{
					int maxSpecimenNumber = this.Max(obj => obj.SpecimenNumber);
					item.SpecimenNumber = maxSpecimenNumber + 1;
				}
			}
		}

		public void Remove(object obj)
		{
			SpecimenOrder item = obj as SpecimenOrder;
			if (item != null)
			{
				base.Remove(item);
			}
		}

        public bool HasThinPrepFluidSpecimen()
        {
            bool result = false;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (string.IsNullOrEmpty(specimenOrder.Description) == false)
                {
                    if (specimenOrder.Description.Contains("Thin Prep Fluid") == true)
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public bool HasMultipleSpecimenWithThinPrepInFirstPosition()
        {
            bool result = false;
            if (this.Count > 1)
            {
                if (HasThinPrepFluidSpecimen() == true)
                {
                    SpecimenOrder thinPrep = this.GetThinPrep();
                    if (thinPrep.SpecimenOrderId == thinPrep.MasterAccessionNo + ".1")
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        public int SpecimenIdCount(string specimenId)
        {
            int result = 0;
            foreach(SpecimenOrder specimenOrder in this)
            {
                if(specimenOrder.SpecimenId == specimenId)
                {
                    result += 1;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.AliquotOrder GetAliquotOrder(string aliquotOrderId)
		{
			YellowstonePathology.Business.Test.AliquotOrder result = null;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
			{
				foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
				{
					if (aliquotOrder.AliquotOrderId == aliquotOrderId)
					{
						result = aliquotOrder;
						break;
					}
				}
			}
			return result;
		}

        public bool HasPantherAliquot()
        {
            bool result = false;
            YellowstonePathology.Business.Specimen.Model.PantherAliquot pantherAliquot = new PantherAliquot();
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (aliquotOrder.AliquotType == pantherAliquot.AliquotType)
                    {
                        result = true;
                        break;
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.AliquotOrder GetPantherAliquot()
        {
            YellowstonePathology.Business.Test.AliquotOrder result = null;
            YellowstonePathology.Business.Specimen.Model.PantherAliquot pantherAliquot = new PantherAliquot();
            foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (aliquotOrder.AliquotType == pantherAliquot.AliquotType)
                    {
                        result = aliquotOrder;
                        break;
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Slide.Model.SlideOrder GetSlideOrder(string slideOrderId)
        {
            YellowstonePathology.Business.Slide.Model.SlideOrder result = null;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in aliquotOrder.SlideOrderCollection)
                    {
                        if (slideOrder.SlideOrderId == slideOrderId)
                        {
                            result = slideOrder;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.AliquotOrder GetAliquotOrderByTestOrderId(string testOrderId)
        {
            YellowstonePathology.Business.Test.AliquotOrder result = null;
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {                    
                    if (aliquotOrder.TestOrderCollection.Exists(testOrderId) == true)
                    {
                        result = aliquotOrder;
                        break;
                    }                    
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Test.AliquotOrderCollection GetAliquotOrdersThatHaveTestOrders()
        {
			YellowstonePathology.Business.Test.AliquotOrderCollection result = new YellowstonePathology.Business.Test.AliquotOrderCollection();
			foreach (YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (aliquotOrder.TestOrderCollection.Count > 0)
                    {
                        result.Add(aliquotOrder);
                    }
                }
            }
            return result;
        }


        public bool Exists(YellowstonePathology.Business.OrderTargetTypeCollection orderTargetTypeCollection)
        {
            bool result = false;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (orderTargetTypeCollection.Exists(specimenOrder) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool SpecimenTypeExists(List<Business.Specimen.Model.Specimen> specimenList)
        {
            bool result = false;
            foreach (Business.Specimen.Model.Specimen specimen in specimenList)
            {
                if (SpecimenTypeExists(specimen) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool SpecimenTypeExists(Business.Specimen.Model.Specimen specimen)
        {
            bool result = false;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenId == specimen.SpecimenId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool Exists(string specimenOrderId)
        {
            bool result = false;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.SpecimenOrderId == specimenOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public YellowstonePathology.Business.Interface.IOrderTarget GetOrderTarget(YellowstonePathology.Business.OrderTargetTypeCollection orderTargetTypeCollection)
        {
            SpecimenOrder result = null;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (orderTargetTypeCollection.Exists(specimenOrder) == true)
                {
                    result = specimenOrder;
                    break;
                }
            }
            return result;
        } 

		public bool AliquotTypeExists(string aliquotType)
		{
			bool result = false;
			foreach (SpecimenOrder specimenOrder in this)
			{
				if (specimenOrder.AliquotOrderCollection.AliquotTypeExists(aliquotType) == true)
				{
					result = true;
					break;
				}
			}
			return result;
		}

        public bool AliquotOrderExists(string aliquotOrderId)
        {
            bool result = false;
            foreach (SpecimenOrder specimenOrder in this)
            {
                if (specimenOrder.AliquotOrderCollection.Exists(aliquotOrderId) == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool SlideOrderExists(string testOrderId)
        {
            bool result = false;
            foreach (SpecimenOrder specimenOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (aliquotOrder.SlideOrderCollection.TestOrderExists(testOrderId) == true)
                    {
                        result = true;
                        break;
                    }
                }                
            }
            return result;
        }

        public YellowstonePathology.Business.Slide.Model.SlideOrder GetSlideOrderByTestOrderId(string testOrderId)
        {
            YellowstonePathology.Business.Slide.Model.SlideOrder result = null;
            foreach (SpecimenOrder specimenOrder in this)
            {
                foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in specimenOrder.AliquotOrderCollection)
                {
                    if (aliquotOrder.SlideOrderCollection.TestOrderExists(testOrderId) == true)
                    {
                        result = aliquotOrder.SlideOrderCollection.GetSlideOrderByTestOrderId(testOrderId);
                        break;
                    }
                }
            }
            return result;
        }        

        public virtual void PullOver(YellowstonePathology.Business.Visitor.AccessionTreeVisitor accessionTreeVisitor)
        {
            accessionTreeVisitor.Visit(this);
        }

        public void Sync(DataTable dataTable)
        {
            this.IsLoading = true;
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);

            while(dataTableReader.Read())
            {
                string specimenOrderId = dataTableReader["SpecimenOrderId"].ToString();

                Business.Specimen.Model.SpecimenOrder specimenOrder = null;

                if (this.Exists(specimenOrderId) == true)
                {
                    specimenOrder = this.GetSpecimenOrder(specimenOrderId);
                }
                else
                {
                    specimenOrder = new Business.Specimen.Model.SpecimenOrder();
                    this.Add(specimenOrder);
                }

                YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(specimenOrder, dataTableReader);
                sqlDataTableReaderPropertyWriter.WriteProperties();

            }

            this.IsLoading = false;
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string specimenOrderId = dataTable.Rows[idx]["SpecimenOrderId"].ToString();
                    if (this[i].SpecimenOrderId == specimenOrderId)
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
    }
}
