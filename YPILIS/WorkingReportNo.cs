using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology
{
    public class WorkingReportNo
    {
        private static WorkingReportNo instance;
        private string m_ReportNo;

        public static WorkingReportNo Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WorkingReportNo();
                }
                return instance;
            }
        } 
        
        public string ReportNo
        {
            get { return this.m_ReportNo; }
            set { this.m_ReportNo = value; }
        }
    }
}
