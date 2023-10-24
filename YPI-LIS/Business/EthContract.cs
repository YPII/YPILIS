using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business
{
    public class EthContract
    {
        private string m_ContractAddress;
        private int m_TransactionCount;
        private DateTime m_DateSubmitted;

        public EthContract(string contractAddress, int transactionCount, DateTime dateSubmitted)
        {
            this.m_ContractAddress = contractAddress;
            this.m_TransactionCount = transactionCount;
            this.m_DateSubmitted = dateSubmitted;
        }

        public string ContractAddress
        {
            get { return this.m_ContractAddress; }
            set { this.m_ContractAddress = value; }
        }

        public int TransactionCount
        {
            get { return this.m_TransactionCount; }
            set { this.m_TransactionCount = value; }
        }

        public DateTime DateSubmitted
        {
            get { return this.m_DateSubmitted; }
            set { this.m_DateSubmitted = value; }
        }

    }
}
