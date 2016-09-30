using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class CassettePrinter
    {
        private YellowstonePathology.Business.Test.AliquotOrderCollection m_AliquotOrderCollection;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private List<Cassette> m_Cassettes;

        public CassettePrinter(YellowstonePathology.Business.Test.AliquotOrderCollection aliquotOrderCollection, YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_AliquotOrderCollection = aliquotOrderCollection;
            this.m_AccessionOrder = accessionOrder;
            this.Initialize();
        }

        private void Initialize()
        {
            this.m_Cassettes = new List<Cassette>();
            foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in this.m_AliquotOrderCollection)
            {
                if (aliquotOrder.IsBlock() == true)
                {
                    if (aliquotOrder.LabelType == YellowstonePathology.Business.Specimen.Model.AliquotLabelType.DirectPrint == true)
                    {
                        Cassette cassette = new Cassette();
                        cassette.FromAliquotOrder(aliquotOrder, this.m_AccessionOrder);
                        this.m_Cassettes.Add(cassette);
                    }
                }
            }
        }

        public bool HasItemsToPrint()
        {
            bool result = false;
            if (this.m_Cassettes.Count > 0) result = true;
            return result;
        }

        public void Print()
        {
            string path = null;
            if(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.UseLaserCassettePrinter == true)
            {
                path = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.LaserCassettePrinter + System.Guid.NewGuid().ToString() + ".gdc";
            }
            else
            {
                path = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.CassettePrinter + System.Guid.NewGuid().ToString() + ".txt";
            }

            try
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(path))
                {
                    foreach (Cassette cassette in this.m_Cassettes)
                    {
                        string line = null;
                        if (YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.UseLaserCassettePrinter == true)
                        {
                            line = cassette.ToLaserString();
                        }
                        else
                        {
                            line = cassette.ToString();
                        }
                         
                        file.Write(line + "\r\n");
                        cassette.AliquotOrder.Printed = true;
                    }
                }
            }
            catch (Exception e)
            {
                System.Windows.MessageBox.Show(path + ": " + e.Message, "Cassette Printer Location.", System.Windows.MessageBoxButton.OK, System.Windows.MessageBoxImage.Exclamation);
            }
        }
    }
}
