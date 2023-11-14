using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace YellowstonePathology.UI
{  
    public partial class ChainExplorer : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        Business.GethAPI m_GethAPI;
        Business.EthContractCollection m_ContractCollection;
        Business.Specimen.Model.ContainerCollection m_ContainerCollection;

        public ChainExplorer()
        {            
            this.m_GethAPI = new Business.GethAPI();
            this.m_ContractCollection = new Business.EthContractCollection();
            this.m_ContainerCollection = new Business.Specimen.Model.ContainerCollection();
                 
            InitializeComponent();
            this.DataContext = this;
        }
        
        public Business.EthContractCollection ContractCollection
        {
            get { return this.m_ContractCollection; }
        }

        public Business.Specimen.Model.ContainerCollection ContainerCollection
        {
            get { return this.m_ContainerCollection; }
        }

        private void Button_Go(object sender, RoutedEventArgs e)
        {         
            this.m_ContractCollection.Clear();
            this.LoopTheChain();            
        }

        public static byte[] StringToByteArray(String hex)
        {
            int NumberChars = hex.Length / 2;
            byte[] bytes = new byte[NumberChars];
            using (var sr = new System.IO.StringReader(hex))
            {
                for (int i = 0; i < NumberChars; i++)
                    bytes[i] =
                      Convert.ToByte(new string(new char[2] { (char)sr.Read(), (char)sr.Read() }), 16);
            }
            return bytes;
        }

        private void LoopTheChain()
        {            
            Business.EthBlock latestBlock = this.m_GethAPI.GetLatestBlock();
            int startingBlockNumber = this.GetFirstBlockNumberForDate(DateTime.Today.AddDays(-2));
            for (int i = startingBlockNumber; i < latestBlock.Number; i++)
            {
                int transactionCount = this.m_GethAPI.GetBlockTransactionCountByNumber(i);
                if (transactionCount > 0)
                {
                    Business.EthBlock block = this.m_GethAPI.GetBlockByNumber(i);
                    foreach (string transactionHash in block.TransactionHashes)
                    {
                        string contractAddress = this.m_GethAPI.GetContractAddress(transactionHash);
                        if (string.IsNullOrEmpty(contractAddress) == false)
                        {
                            int containerCount = this.m_GethAPI.GetContainerCount(contractAddress);                            
                            Business.EthContract contract = new Business.EthContract(contractAddress, containerCount, block.TimeStamp);
                            this.m_ContractCollection.Add(contract);
                        }
                    }
                }
            }            
        }

        private int GetFirstBlockNumberForDate(DateTime workDate)
        {
            double averageBlockTime = this.GetAverageBlockTime();
            Business.EthBlock latestBlock = this.m_GethAPI.GetLatestBlock();
            double secondsInADay = 86400;
            double days = (DateTime.Today - workDate).TotalDays;
            double numberOfBlocksInADay = secondsInADay / averageBlockTime;
            return Convert.ToInt32(latestBlock.Number - (numberOfBlocksInADay * days));
        }

        private double GetAverageBlockTime()
        {            
            Business.EthBlock latestBlock = this.m_GethAPI.GetLatestBlock();
            List<double> blockTimes = new List<double>();

            Business.EthBlock lastBlock = this.m_GethAPI.GetBlockByNumber(latestBlock.Number - 100);
            for (int i=latestBlock.Number - 99; i<latestBlock.Number; i++)
            {
                Business.EthBlock thisBlock = this.m_GethAPI.GetBlockByNumber(i);
                blockTimes.Add(thisBlock.TimeStamp.Subtract(lastBlock.TimeStamp).TotalSeconds);
                lastBlock = thisBlock;
            }

            return blockTimes.Average();            
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ListViewContracts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(this.ListViewContracts.SelectedItem != null)
            {
                this.m_ContainerCollection.Clear();
                Business.EthContract ethContract = (Business.EthContract)this.ListViewContracts.SelectedItem;
                int containerCount = this.m_GethAPI.GetContainerCount(ethContract.ContractAddress);
                for(int i=0; i<containerCount; i++)
                {
                    string result = this.m_GethAPI.GetContainer(ethContract.ContractAddress, i);
                    Business.Specimen.Model.Container container = new Business.Specimen.Model.Container(result);
                    this.m_ContainerCollection.Add(container);
                }
            }
        }
    }
}
