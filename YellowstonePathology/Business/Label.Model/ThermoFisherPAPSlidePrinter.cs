using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Label.Model
{
    public class ThermoFisherPAPSlidePrinter
    {
        private List<ThinPrepSlideDirectPrintLabel> m_Queue;

        public ThermoFisherPAPSlidePrinter()
        {
            this.m_Queue = new List<ThinPrepSlideDirectPrintLabel>();
        }

        public List<ThinPrepSlideDirectPrintLabel> Queue
        {
            get { return this.m_Queue; }
        }

        public void Print()
        {
            if (string.IsNullOrEmpty(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.SlideMatePrinterPath) == false)
            {
                StringBuilder lines = new StringBuilder();
                foreach (ThinPrepSlideDirectPrintLabel thinPrepSlideDirectPrintLabel in this.m_Queue)
                {
                    lines.Append(thinPrepSlideDirectPrintLabel.GetLine());
                }
                
                string fileName = System.IO.Path.Combine(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.SlideMatePrinterPath, Guid.NewGuid() + ".txt");
                using (System.IO.StreamWriter streamWriter = new System.IO.StreamWriter(fileName))
                {
                    streamWriter.Write(lines.ToString());
                }
            }
        }
    }
}
