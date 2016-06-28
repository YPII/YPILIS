using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace YellowstonePathology.Business.Domain
{
    /*[Table(Name = "tblClientSalesperson")]
    public class ClientSalesperson : DomainBase
	{        
        int m_ClientId;
        int m_SalespersonId;

        EntitySet<YellowstonePathology.Business.Domain.ClientSalespersonOrderableTest> m_ClientSalespersonOrderableTests;
        EntityRef<YellowstonePathology.Business.Domain.SystemUser> m_SystemUser;
        EntityRef<YellowstonePathology.Business.Domain.Client> m_Client;

        public ClientSalesperson()
        {

        }

        [Association(Storage = "m_ClientSalespersonOrderableTests", ThisKey = "ClientSalesPersonId")]
        public EntitySet<YellowstonePathology.Business.Domain.ClientSalespersonOrderableTest> ClientSalespersonOrderableTests
        {
            get { return this.m_ClientSalespersonOrderableTests; }
            set { this.m_ClientSalespersonOrderableTests = value; }
        }

        [Association(Storage = "m_SystemUser", ThisKey = "SalespersonId", OtherKey="SystemUserId")]
        public YellowstonePathology.Business.Domain.SystemUser SystemUser
        {
            get { return this.m_SystemUser.Entity; }
            set { this.m_SystemUser.Entity = value; }
        }

        [Association(Storage = "m_Client", ThisKey = "ClientId", OtherKey = "ClientId")]
        public YellowstonePathology.Business.Domain.Client Client
        {
            get { return this.m_Client.Entity; }
            set { this.m_Client.Entity = value; }
        }

        [Column(Name = "SalespersonId", Storage = "m_SalespersonId", IsPrimaryKey=true, IsDbGenerated=true)]
        public int SalespersonId
        {
            get { return this.m_SalespersonId; }
            set
            {
                if (this.m_SalespersonId != value)
                {
                    this.m_SalespersonId = value;
                    this.NotifyPropertyChanged("SalespersonId");
                }
            }
        }

        [Column(Name = "ClientId", Storage = "m_ClientId")]
        public int ClientId
        {
            get { return this.m_ClientId; }
            set
            {
                if (this.m_ClientId != value)
                {
                    this.m_ClientId = value;
                    this.NotifyPropertyChanged("ClientId");
                }
            }
        }

        
	}*/
}
