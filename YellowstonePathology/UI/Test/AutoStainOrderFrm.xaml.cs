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
using YellowstonePathology.Business.Test;

namespace YellowstonePathology.UI.Test
{
	public partial class AutoStainOrderFrm : Window
	{
		private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;

		public AutoStainOrderFrm(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			m_AccessionOrder = accessionOrder;
			InitializeComponent();
			this.DataContext = this.m_AccessionOrder;
		}

		private void ButtonOrder_Click(object sender, RoutedEventArgs e)
		{
			/*List<int> aliquotOrderIdList =
				(from so in this.m_AccessionUI.SpecimenOrderCollection
				 from ao in so.AliquotOrderCollection
				 where ao.Order == true
				 select ao.AliquotOrderId).ToList<int>();

			List<YellowstonePathology.Business.Test.SpecimenOrder> specimenOrderItemList =
				(from so in this.m_AccessionUI.SpecimenOrderCollection
				 from ao in so.AliquotOrderCollection
				 from aio in aliquotOrderIdList
				 where ao.AliquotOrderId == aio
				 select so).ToList<YellowstonePathology.Business.Test.SpecimenOrder>();

			YellowstonePathology.Business.Test.PanelSetOrder panelSetOrder =
				(from pso in this.m_AccessionUI.PanelSetOrderCollection
				 where pso.PanelSetEnum == PanelSetEnum.SurgicalPathology
				 select pso).Single<YellowstonePathology.Business.Test.PanelSetOrder>();

			if (aliquotOrderIdList.Count > 0)
			{
				YellowstonePathology.Business.Rules.ExecutionStatus executionStatus = new YellowstonePathology.Business.Rules.ExecutionStatus();
				YellowstonePathology.Business.Test.PanelItem panelItem = new PanelItem();
				panelItem.SetFillCommandByPanelId(19);
				panelItem.Fill();
				YellowstonePathology.Business.Rules.Order.Order order = new YellowstonePathology.Business.Rules.Order.Order(specimenOrderItemList[0], panelSetOrder, panelItem, string.Empty);
				order.PlaceOrder(executionStatus);

				if (!executionStatus.Halted)
				{
					this.m_AccessionUI.Fill();

					List<YellowstonePathology.Business.Test.PanelOrder> panelOrderItemList =
						(from pso in this.m_AccessionUI.PanelSetOrderCollection
						 where pso.PanelSetEnum == PanelSetEnum.SurgicalPathology
						 from po in pso.PanelOrderCollection
						 where po.PanelId == 19 && po.Acknowledged == false
						 select po).ToList<YellowstonePathology.Business.Test.PanelOrder>();

					List<YellowstonePathology.Business.Test.AliquotOrder> aliquotOrderItemList =
						(from so in this.m_AccessionUI.SpecimenOrderCollection
						 from ao in so.AliquotOrderCollection
						 from aid in aliquotOrderIdList
						 where ao.AliquotOrderId == aid
						 select ao).ToList<YellowstonePathology.Business.Test.AliquotOrder>();

					YellowstonePathology.Business.Test.TestItem testItem = new TestItem();
					testItem.SetFillCommandByTestId(this.m_AccessionUI.AutoStainOrderId);
					testItem.Fill();

					foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderItemList)
					{
						order = new YellowstonePathology.Business.Rules.Order.Order(aliquotOrder, panelOrderItemList[0], testItem);
						order.PlaceOrder(executionStatus);
						if (executionStatus.Halted)
						{
							break;
						}
					}
				}

				if (executionStatus.Halted)
				{
					string msg = string.Empty;
					foreach (YellowstonePathology.Business.Rules.ExecutionMessage executionMessage in executionStatus.ExecutionMessages)
					{
						msg += executionMessage.Message + '\n';
					}
					MessageBox.Show(msg);
				}
			}

			this.m_AccessionUI.AutoStainOrderId = 0;
			this.m_AccessionUI.AutoStainOrderMessage = string.Empty;*/
			Close();
		}

		private void ButtonCancel_Click(object sender, RoutedEventArgs e)
		{
			List<YellowstonePathology.Business.Test.AliquotOrder> aliquotOrderItemList =
				(from so in this.m_AccessionOrder.SpecimenOrderCollection
				 from ao in so.AliquotOrderCollection
				 where ao.Order == true
				 select ao).ToList<YellowstonePathology.Business.Test.AliquotOrder>();

			foreach (YellowstonePathology.Business.Test.AliquotOrder aliquotOrder in aliquotOrderItemList)
			{
				aliquotOrder.Order = false;
			}

			Close();
		}
	}
}
