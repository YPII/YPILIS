using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class OrderStatusEnum
    {
        private static OrderStatus m_Complete = m_Complete = new OrderStatus("CM", "Order is complete.");
        private static OrderStatus m_InProcess = m_InProcess = new OrderStatus("IP", "In Process, unspecified.");
        private static OrderStatus m_Cancelled = m_Cancelled = new OrderStatus("CA", "Order was cancelled.");        

        public static OrderStatus Complete
        {
            get { return m_Complete; }
        }

        public static OrderStatus InProcess
        {
            get { return m_InProcess; }
        }

        public static OrderStatus Cancelled
        {
            get { return m_Cancelled; }
        }        
    }
}
