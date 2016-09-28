using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using YellowstonePathology.Business.Persistence;

namespace YellowstonePathology.UI.Billing
{    
    public partial class SVHCDMResultDialog : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        private DateTime m_WorkDate;
        private List<SVHCDMItem> m_SVHCDMItemList;

        public SVHCDMResultDialog()
        {
            this.m_WorkDate = DateTime.Today;
            this.m_SVHCDMItemList = this.GetList(this.m_WorkDate);
            InitializeComponent();
            this.DataContext = this;
        }   
        
        public List<SVHCDMItem> SVHCDMItemList
        {
            get { return this.m_SVHCDMItemList; }
        }


        public DateTime WorkDate
        {
            get { return this.m_WorkDate; }
            set { this.m_WorkDate = value; }
        }

        private void ButtonClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ButtonForward_Click(object sender, RoutedEventArgs e)
        {
            this.m_WorkDate = this.m_WorkDate.AddDays(1);
            this.m_SVHCDMItemList = this.GetList(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            this.m_WorkDate = this.m_WorkDate.AddDays(-1);
            this.m_SVHCDMItemList = this.GetList(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

        private void ScheduleDistribution(string testOrderReportDistributionId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update tbltestOrderReportDistribution set [distributed] = 0, ScheduledDistributiontime = getdate() " +
                "where testOrderReportDistributionId = '" + testOrderReportDistributionId + "'";

            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();                
            }
        }

        private void UpdateResultStatus(string testOrderReportDistributionId)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update tblTestOrderReportDistribution set ResultStatus = 'F' " +                
                "where tor.testOrderReportDistributionId = '" + testOrderReportDistributionId + "'";

            cmd.CommandType = CommandType.Text;
            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }

        private List<SVHCDMItem> GetList(DateTime workDate)
        {
            List<SVHCDMItem> result = new List<SVHCDMItem>();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select pso.ReportNo, ao.PLastName, ao.PFirstName, ao.ClientName, ao.PhysicianName, pso.PanelSetName, tor.ResultStatus, tor.TestOrderReportDistributionId, pso.Distribute, tor.ScheduledDistributionTime " +
                "from tblPanelSetOrder pso " +
                "join tblTestOrderReportDistribution tor on pso.ReportNo = tor.ReportNo " +
                "join tblAccessionOrder ao on pso.MasterAccessionno = ao.MasterAccessionNo " +
                "where pso.IsPosted = 1 and tor.DistributionType = 'EPIC' " +
                "and exists (select null from tblPanelSetOrderCPTCodeBill where reportNo = pso.ReportNo and PostDate = '" + workDate.ToString() + "') order by ao.AccessionTime";

            cmd.CommandType = CommandType.Text;

            using (SqlConnection cn = new SqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SVHCDMItem item = new SVHCDMItem();
                        YellowstonePathology.Business.Persistence.SqlDataReaderPropertyWriter sqlDataReaderPropertyWriter = new Business.Persistence.SqlDataReaderPropertyWriter(item, dr);
                        sqlDataReaderPropertyWriter.WriteProperties();
                        result.Add(item);
                    }
                }
            }
            return result;
        }

        private void ButtonProcess_Click(object sender, RoutedEventArgs e)
        {
            foreach(SVHCDMItem item in this.m_SVHCDMItemList)
            {                
                if(item.ResultStatus == "P")
                {
                    if(item.Distribute == true)
                    {
                        this.ScheduleDistribution(item.TestOrderReportDistributionId);
                    }
                    else
                    {
                        this.UpdateResultStatus(item.TestOrderReportDistributionId);
                    }
                }                
            }

            this.m_SVHCDMItemList = this.GetList(this.m_WorkDate);
            this.NotifyPropertyChanged(string.Empty);
        }
    }

    public class SVHCDMItem: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string m_ReportNo;             
        private string m_PLastName;
        private string m_PFirstName;
        private string m_ClientName;
        private string m_PhysicianName;                
        private string m_PanelSetName;                        
        private string m_ResultStatus;
        private bool m_Distribute;
        private string m_TestOrderReportDistributionId;
        private Nullable<DateTime> m_ScheduledDistributionTime;

        public SVHCDMItem()
        {

        }

        [PersistentProperty()]
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set
            {
                if (value != this.m_ReportNo)
                {
                    this.m_ReportNo = value;
                    this.NotifyPropertyChanged("ReportNo");
                }
            }
        }

        [PersistentProperty()]
        public string PLastName
        {
            get { return this.m_PLastName; }
            set
            {
                if (value != this.m_PLastName)
                {
                    this.m_PLastName = value;
                    this.NotifyPropertyChanged("PLastName");
                }
            }
        }

        [PersistentProperty()]
        public string PFirstName
        {
            get { return this.m_PFirstName; }
            set
            {
                if (value != this.m_PFirstName)
                {
                    this.m_PFirstName = value;
                    this.NotifyPropertyChanged("PFirstName");
                }
            }
        }

        [PersistentProperty()]
        public string ClientName
        {
            get { return this.m_ClientName; }
            set
            {
                if (value != this.m_ClientName)
                {
                    this.m_ClientName = value;
                    this.NotifyPropertyChanged("ClientName");
                }
            }
        }

        [PersistentProperty()]
        public string PhysicianName
        {
            get { return this.m_PhysicianName; }
            set
            {
                if (value != this.m_PhysicianName)
                {
                    this.m_PhysicianName = value;
                    this.NotifyPropertyChanged("PhysicianName");
                }
            }
        }

        [PersistentProperty()]
        public string PanelSetName
        {
            get { return this.m_PanelSetName; }
            set
            {
                if (value != this.m_PanelSetName)
                {
                    this.m_PanelSetName = value;
                    this.NotifyPropertyChanged("PanelSetName");
                }
            }
        }       

        [PersistentProperty()]
        public string ResultStatus
        {
            get { return this.m_ResultStatus; }
            set
            {
                if (value != this.m_ResultStatus)
                {
                    this.m_ResultStatus = value;
                    this.NotifyPropertyChanged("ResultStatus");
                }
            }
        }

        [PersistentProperty()]
        public string TestOrderReportDistributionId
        {
            get { return this.m_TestOrderReportDistributionId; }
            set
            {
                if (value != this.m_TestOrderReportDistributionId)
                {
                    this.m_TestOrderReportDistributionId = value;
                    this.NotifyPropertyChanged("TestOrderReportDistributionId");
                }
            }
        }

        [PersistentProperty()]
        public bool Distribute
        {
            get { return this.m_Distribute; }
            set
            {
                if (value != this.m_Distribute)
                {
                    this.m_Distribute = value;
                    this.NotifyPropertyChanged("Distribute");
                }
            }
        }

        [PersistentProperty()]
        public Nullable<DateTime> ScheduledDistributionTime
        {
            get { return this.m_ScheduledDistributionTime; }
            set
            {
                if (value != this.m_ScheduledDistributionTime)
                {
                    this.m_ScheduledDistributionTime = value;
                    this.NotifyPropertyChanged("ScheduledDistributionTime");
                }
            }
        }

        public void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
