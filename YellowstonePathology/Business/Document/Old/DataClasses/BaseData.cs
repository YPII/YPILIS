using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace YellowstonePathology.Business.Document.Old.DataClasses
{   
    public class BaseData
    {

        public DataSet GetDataSetFromSqlStatementsWithHistory(ArrayList sqlStatements, ArrayList tableNames, string reportNo)
        {
            DataSet dataSet = new DataSet();
			SqlConnection cn = new SqlConnection(Properties.Settings.Default.CurrentConnectionString);
            cn.Open();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;

            for (int i = 0; i < sqlStatements.Count; i++)
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                cmd.CommandText = (string)sqlStatements[i];
                dataAdapter.SelectCommand = cmd;                
                dataAdapter.Fill(dataSet, (string)tableNames[i]);
            }

            SqlDataAdapter dataAdapterHist = new SqlDataAdapter();
            cmd.CommandText = "prcGetCaseHistory";
            cmd.CommandType = CommandType.StoredProcedure;
            SqlParameter param = new SqlParameter("@ReportNo", SqlDbType.VarChar);
            param.Value = reportNo;
            cmd.Parameters.Add(param);
            dataAdapterHist.SelectCommand = cmd;
            dataAdapterHist.Fill(dataSet, "tblCaseHistory");

            cn.Close();
            return dataSet;
        }
    }
}
