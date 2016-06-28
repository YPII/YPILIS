using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class ResultStatusEnum
    {        
        private static ResultStatus m_NoResult = new ResultStatus("X", "No result can be obtained for this request/specimen");
        private static ResultStatus m_Final = new ResultStatus("F", "Final");
        private static ResultStatus m_Correction = new ResultStatus("C", "Correction");
        private static ResultStatus m_InProcess = new ResultStatus("I", "In Process");

        public static ResultStatus NoResult
        {
            get { return m_NoResult; }
        }

        public static ResultStatus Final
        {
            get { return m_Final; }
        }

        public static ResultStatus Correction
        {
            get { return m_Correction; }
        }

        public static ResultStatus InProcess
        {
            get { return m_InProcess; }
        }
    }    
}
