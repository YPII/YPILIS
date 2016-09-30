using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Slide.Model
{
    public class SlideOrderCollection_Base : ObservableCollection<YellowstonePathology.Business.Slide.Model.SlideOrder_Base>
    {
        public SlideOrderCollection_Base()
        {
            
        }

        public void RemoveDeleted(DataTable dataTable)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                for (int idx = 0; idx < dataTable.Rows.Count; idx++)
                {
                    string slideOrderId = dataTable.Rows[idx]["SlideOrderId"].ToString();
                    if (this[i].SlideOrderId == slideOrderId)
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

        public void Sync(DataTable dataTable, string aliquotOrderId)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string slideOrderId = dataTableReader["SlideOrderId"].ToString();
                string slideAliquotOrderId = dataTableReader["AliquotOrderId"].ToString();

                SlideOrder slideOrder = null;

                if (this.Exists(slideOrderId) == true)
                {
                    slideOrder = this.Get(slideOrderId);
                }
                else if (slideAliquotOrderId == aliquotOrderId)
                {
                    slideOrder = new SlideOrder();
                    this.Add(slideOrder);
                }

                if (slideOrder != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(slideOrder, dataTableReader);
                    sqlDataTableReaderPropertyWriter.WriteProperties();
                }
            }
        }

        public void SyncForTestOrder(DataTable dataTable, string testOrderId)
        {
            this.RemoveDeleted(dataTable);
            DataTableReader dataTableReader = new DataTableReader(dataTable);
            while (dataTableReader.Read())
            {
                string slideOrderId = dataTableReader["SlideOrderId"].ToString();
                string slideTestOrderId = dataTableReader["TestOrderId"].ToString();

                SlideOrder slideOrder = null;

                if (this.Exists(slideOrderId) == true)
                {
                    slideOrder = this.Get(slideOrderId);
                }
                else if (slideTestOrderId == testOrderId)
                {
                    slideOrder = new SlideOrder();
                    this.Add(slideOrder);
                }

                if (slideOrder != null)
                {
                    YellowstonePathology.Business.Persistence.SqlDataTableReaderPropertyWriter sqlDataTableReaderPropertyWriter = new Persistence.SqlDataTableReaderPropertyWriter(slideOrder, dataTableReader);
                    sqlDataTableReaderPropertyWriter.WriteProperties();
                }
            }
        }

        public void RemoveDeleted(IEnumerable<XElement> elements)
        {
            for (int i = this.Count - 1; i > -1; i--)
            {
                bool found = false;
                foreach (XElement element in elements)
                {
                    string slideOrderId = element.Element("SlideOrderId").Value;
                    if (this[i].SlideOrderId == slideOrderId)
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

        public void SetAsPrinted(YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            foreach (SlideOrder_Base slideOrder in this)
            {
                slideOrder.SetAsPrinted(systemIdentity);
            }
        }

        public void Remove(string slideOrderid)
        {
            for (int i = 0; i < this.Count; i++ )
            {
                if (this[i].SlideOrderId == slideOrderid)
                {                    
                    this.Remove(this[i]);
                    break;
                }
            }
        }

		public bool Exists(string slideOrderId)
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in this)
            {
                if (slideOrder.SlideOrderId == slideOrderId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasPrintedSlide()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in this)
            {
                if (slideOrder.Printed ==  true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public bool HasUnprintedSlide()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in this)
            {
                if (slideOrder.Printed == false)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public SlideOrder Get(string slideOrderId)
        {
            SlideOrder result = null;
            foreach (YellowstonePathology.Business.Slide.Model.SlideOrder slideOrder in this)
            {
                if (slideOrder.SlideOrderId == slideOrderId)
                {
                    result = slideOrder;
                    break;
                }
            }
            return result;
        }   

        /*
        public int GetNextSlideNumber(string blockLabel)
        {
            int result = 1;
            if (this.Count > 0)
            {
                string label = this[this.Count - 1].Label;
                if (label.Length == 1)
                {
                    result = Int32.Parse(label) + 1;
                }
                else
                {
                    string lastChar = label.Substring(label.Length - 1);
                    string secondToLastChar = label.Substring(label.Length - 2, 1);

                    int secondToLastInt = 0;
                    if (Int32.TryParse(secondToLastChar, out secondToLastInt) == true)
                    {
                        result = Int32.Parse(secondToLastChar + lastChar) + 1;
                    }
                    else
                    {
                        result = Int32.Parse(lastChar) + 1;
                    }
                }
            }
            return result;
        }
        */
    }
}
