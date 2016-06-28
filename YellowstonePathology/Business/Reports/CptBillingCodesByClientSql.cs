using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace YellowstonePathology.Business.Reports
{
    public class CptBillingCodesByClientSql
    {
        public CptBillingCodesByClientSql()
        {
            
        }

        /*public void CreateSql()
        {
            YellowstonePathology.Business.Billing.CptCodeList cptCodeList = new YellowstonePathology.Business.Billing.CptCodeList();
            cptCodeList.SetFillByAll();
            cptCodeList.Fill();

            System.Windows.MessageBox.Show(cptCodeList.Count.ToString());

            using (StreamWriter sw = new StreamWriter("c:\\CptByClient.sql"))
            {                
                sw.WriteLine("Declare @ClientPivot table");
                sw.WriteLine("(");
                sw.WriteLine("ClientName varchar(100),");                
                sw.WriteLine("CptCode varchar(50),");                
                sw.WriteLine("Total int");                
                sw.WriteLine(")");                
                sw.WriteLine("Insert @ClientPivot (ClientName, CptCode, Total)");                
                sw.WriteLine("Select convert(varchar(100), c.ClientName), bc.CptCode, count(*)");                
                sw.WriteLine("from tblCptBillingCode bc");                
                sw.WriteLine("join tblClient c on bc.ClientId = c.ClientId");                
                sw.WriteLine("where bc.BillingDate between '6/1/08' and '6/30/08'");                
                sw.WriteLine("group by c.ClientName, bc.CptCode");
                sw.WriteLine("order by c.ClientName");
                sw.WriteLine("SELECT ClientName, ");

                foreach (YellowstonePathology.Business.Billing.CptCodeListItem item in cptCodeList)
                {
                    StringBuilder line = new StringBuilder();
                    line.Append("SUM(CASE CptCode WHEN '");
                    line.Append(item.CptCode); 
                    line.Append("' THEN Total ELSE 0 END) AS [");
                    line.Append(item.CptCode);
                    line.Append("],");
                    sw.WriteLine(line);
                }
                
                sw.WriteLine("FROM @ClientPivot");
                sw.WriteLine("GROUP BY ClientName");
            }                         
        }*/
    }
}
