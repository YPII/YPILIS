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

namespace YellowstonePathology.UI.Surgical
{
    /// <summary>
    /// Interaction logic for PeerReview.xaml
    /// </summary>
    public partial class PeerReview : Window
    {
        YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        List<YellowstonePathology.Business.Test.PanelOrder> m_PanelOrderItems;

        public PeerReview(YellowstonePathology.Business.Test.AccessionOrder accessonOrderItem)
        {
            this.m_PanelOrderItems = new List<YellowstonePathology.Business.Test.PanelOrder>();
            this.m_AccessionOrder = accessonOrderItem;
            InitializeComponent();
            this.DataContext = this;            
        }

        public YellowstonePathology.Business.Test.AccessionOrder AccessionOrder
        {
            get { return this.m_AccessionOrder; }
            set { this.m_AccessionOrder = value; }
        }

        //public List<YellowstonePathology.Business.Test.PanelOrder> PanelOrderItems
		public List<YellowstonePathology.Business.Interface.IPanelOrder> PanelOrderItems
        {
            get 
            {
				//List<YellowstonePathology.Business.Test.PanelOrder> panelOrderItems = null;
				List<YellowstonePathology.Business.Interface.IPanelOrder> panelOrderItems = null;
                var query = from pso in this.m_AccessionOrder.PanelSetOrderCollection
                            from po in pso.PanelOrderCollection
                            where po.PanelId == 33
                            select po;
                //if (query.Count<YellowstonePathology.Business.Test.PanelOrder>() != 0)
				if (query.Count<YellowstonePathology.Business.Interface.IPanelOrder>() != 0)
				{
					panelOrderItems = query.ToList<YellowstonePathology.Business.Interface.IPanelOrder>();
					//panelOrderItems = query.ToList<YellowstonePathology.Business.Test.PanelOrder>();
                }
                return panelOrderItems;
            }            
        }
    }
}
