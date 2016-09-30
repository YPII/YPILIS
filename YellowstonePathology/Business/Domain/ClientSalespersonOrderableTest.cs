using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace YellowstonePathology.Business.Domain
{
	public class ClientSalespersonOrderableTest : DomainBase
	{
        int m_ClientSalespersonOrderableTestId;
        int m_ClientSalespersonId;
        Nullable<DateTime> m_StartDate;
        Nullable<DateTime> m_EndDate;
        int m_OrderableTestId;

        public ClientSalespersonOrderableTest()
        {

        }

        [Column(Name = "ClientSalespersonOrderableTestId", Storage = "m_ClientSalespersonOrderableTestId", IsPrimaryKey = true, IsDbGenerated = true)]
        public int ClientSalespersonOrderableTestId
        {
            get { return this.m_ClientSalespersonOrderableTestId; }
            set
            {
                if (this.m_ClientSalespersonOrderableTestId != value)
                {
                    this.m_ClientSalespersonOrderableTestId = value;
                    this.NotifyPropertyChanged("ClientSalespersonOrderableTestId");
                }
            }
        }

        [Column(Name = "ClientSalespersonId", Storage = "m_ClientSalespersonId")]
        public int ClientSalespersonId
        {
            get { return this.m_ClientSalespersonId; }
            set
            {
                if (this.m_ClientSalespersonId != value)
                {
                    this.m_ClientSalespersonId = value;
                    this.NotifyPropertyChanged("ClientSalespersonId");
                }
            }
        }

        [Column(Name = "StartDate", Storage = "m_StartDate")]
        public Nullable<DateTime> StartDate
        {
            get { return this.m_StartDate; }
            set
            {
                if (this.m_StartDate != value)
                {
                    this.m_StartDate = value;
                    this.NotifyPropertyChanged("StartDate");
                }
            }
        }

        [Column(Name = "EndDate", Storage = "m_EndDate")]
        public Nullable<DateTime> EndDate
        {
            get { return this.m_EndDate; }
            set
            {
                if (this.m_EndDate != value)
                {
                    this.m_EndDate = value;
                    this.NotifyPropertyChanged("EndDate");
                }
            }
        }

        [Column(Name = "OrderableTestId", Storage = "m_OrderableTestId")]
        public int OrderableTestId
        {
            get { return this.m_OrderableTestId; }
            set
            {
                if (this.m_OrderableTestId != value)
                {
                    this.m_OrderableTestId = value;
                    this.NotifyPropertyChanged("OrderableTestId");
                }
            }
        }
	}
}
