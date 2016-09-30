using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace YellowstonePathology.UI.Test
{
    public class ResultControl : UserControl
    {
        private YellowstonePathology.Business.Test.PanelSetOrder m_TestOrder;
        private bool m_DisableRequired;
        protected List<FrameworkElement> m_ControlsNotDisabledOnFinal;

        public ResultControl(YellowstonePathology.Business.Test.PanelSetOrder testOrder,
            YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {
            this.m_TestOrder = testOrder;
            this.m_ControlsNotDisabledOnFinal = new List<FrameworkElement>();

            this.m_DisableRequired = false;
            if (accessionOrder.AccessionLock.IsLockAquiredByMe == false)
            {
                this.m_DisableRequired = true;
            }
            else if(this.m_TestOrder.Final == true)
            {
                if(this.m_TestOrder.Distribute == false)
                {
                    this.m_DisableRequired = true;
                }
                else if(this.m_TestOrder.TestOrderReportDistributionCollection.HasDistributedItems())
                {
                    this.m_DisableRequired = true;
                }
            }            

            this.Loaded += ResultControl_Loaded;
        }

        public ResultControl()
        {
            this.m_ControlsNotDisabledOnFinal = new List<FrameworkElement>();
        }

        private void ResultControl_Loaded(object sender, RoutedEventArgs e)
        {
            if (this.m_DisableRequired) DisableContents(this.Content);
        }

        private void DisableContents(object o)
        {
        	if(this.m_ControlsNotDisabledOnFinal.Contains(o))
        	{
        		((FrameworkElement)o).IsEnabled = true;
        	}
            else if (o is Panel)
            {
                Panel panel = (Panel)o;
                foreach (UIElement element in panel.Children)
                {
                    DisableContents(element);
                }
            }
            else if(o is CheckBox)
            {
                ((UIElement)o).IsEnabled = false;
            }
            else if (o is ContentControl)
            {
                ContentControl contentControl = (ContentControl)o;
                if (contentControl.Content != null)
                {
                    DisableContents(contentControl.Content);
                }
                else
                {
                    contentControl.IsEnabled = false;
                }
            }
            else
            {
                ((UIElement)o).IsEnabled = false;
            }
        }
    }
}
