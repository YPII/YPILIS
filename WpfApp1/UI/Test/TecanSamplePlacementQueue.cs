using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI.Test
{
    public class TecanSamplePlacementQueue
    {
        private Queue<TecanSample> m_Queue;

        public TecanSamplePlacementQueue()
        {
            this.m_Queue = new Queue<TecanSample>();

            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(8, 2), new ExcelWorksheetCell(9, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(9, 2), new ExcelWorksheetCell(10, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(10, 2), new ExcelWorksheetCell(11, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(11, 2), new ExcelWorksheetCell(12, 1)));                        

            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(4, 5), new ExcelWorksheetCell(13, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(5, 5), new ExcelWorksheetCell(14, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(6, 5), new ExcelWorksheetCell(15, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(7, 5), new ExcelWorksheetCell(16, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(8, 5), new ExcelWorksheetCell(17, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(9, 5), new ExcelWorksheetCell(18, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(10, 5), new ExcelWorksheetCell(19, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(11, 5), new ExcelWorksheetCell(20, 1)));            

            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(4, 8), new ExcelWorksheetCell(21, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(5, 8), new ExcelWorksheetCell(22, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(6, 8), new ExcelWorksheetCell(23, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(7, 8), new ExcelWorksheetCell(24, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(8, 8), new ExcelWorksheetCell(25, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(9, 8), new ExcelWorksheetCell(26, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(10, 8), new ExcelWorksheetCell(27, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(11, 8), new ExcelWorksheetCell(28, 1)));

            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(4, 11), new ExcelWorksheetCell(29, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(5, 11), new ExcelWorksheetCell(30, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(6, 11), new ExcelWorksheetCell(31, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(7, 11), new ExcelWorksheetCell(32, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(8, 11), new ExcelWorksheetCell(33, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(9, 11), new ExcelWorksheetCell(34, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(10, 11), new ExcelWorksheetCell(35, 1)));
            this.m_Queue.Enqueue(new TecanSample(new ExcelWorksheetCell(11, 11), new ExcelWorksheetCell(36, 1)));           
        }

        public Queue<TecanSample> Queue
        {
            get { return this.m_Queue; }
        }
    }
}
