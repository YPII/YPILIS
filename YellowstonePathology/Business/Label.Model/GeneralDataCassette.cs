﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Label.Model
{
    public class GeneralDataCassette : Cassette
    {
        private string m_Delimeter = "|";

        public override void Print()
        {
            //C:\Program Files\General Data Company\Cassette Printing\Normal.itl|102|15-28044|1A|JA|YPII|ALQ15-28044.1A|15|28044
            YellowstonePathology.Business.OrderIdParser orderIdParser = new YellowstonePathology.Business.OrderIdParser(this.m_MasterAccessionNo);
            StringBuilder line = new StringBuilder(TemplateFileName + this.m_Delimeter);

            line.Append(this.m_CassetteColumn.ToString() + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNo + this.m_Delimeter);
            line.Append(this.BlockTitle + this.m_Delimeter);
            line.Append(this.PatientInitials + this.m_Delimeter);

            if (this.m_ClientAccessioned == true)
            {
                line.Append(this.m_ClientAccessionNo + this.m_Delimeter);
            }
            else
            {
                if (string.IsNullOrEmpty(this.m_EmbeddingInstructions) == false)
                {
                    line.Append(this.m_CompanyId + ":" + this.m_EmbeddingInstructions + this.m_Delimeter);
                }
                else
                {
                    line.Append(this.m_CompanyId + this.m_Delimeter);
                }
            }

            line.Append(this.ScanningId + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNoYear.Value.ToString() + this.m_Delimeter);
            line.Append(orderIdParser.MasterAccessionNoNumber.Value.ToString());            

            string path = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LaserCassettePrinter + System.Guid.NewGuid().ToString() + ".gdc";            

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {                    
                    file.Write(line + "\r\n");
                    this.m_AliquotOrder.Printed = true;                    
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(path + ": " + e.Message, "Cassette Printer Location.", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
        }
    }
}
