using System.Data;
using System.Collections;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Document.Old.DataClasses
{   
    public class BaseData
    {

        public DataSet GetDataSetFromSqlStatementsWithHistory(ArrayList sqlStatements, ArrayList tableNames, string reportNo)
        {
            DataSet dataSet = new DataSet();
			MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString);
            cn.Open();

            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = cn;
            cmd.CommandType = CommandType.Text;

            for (int i = 0; i < sqlStatements.Count; i++)
            {
                MySqlDataAdapter dataAdapter = new MySqlDataAdapter();
                cmd.CommandText = (string)sqlStatements[i];
                dataAdapter.SelectCommand = cmd;                
                dataAdapter.Fill(dataSet, (string)tableNames[i]);
            }

            MySqlDataAdapter dataAdapterHist = new MySqlDataAdapter();
            cmd.CommandText = "prcGetCaseHistory";
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("ReportNo", reportNo);
            dataAdapterHist.SelectCommand = cmd;
            dataAdapterHist.Fill(dataSet, "tblCaseHistory");

            cn.Close();
            return dataSet;
        }
    }
}
