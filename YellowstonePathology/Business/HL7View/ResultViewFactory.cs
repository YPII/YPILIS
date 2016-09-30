using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class ResultViewFactory
    {
        public static IResultView GetResultView(string reportNo, Business.Test.AccessionOrder accessionOrder, int clientId, string resultStatus, bool testing)
        {            
            IResultView resultView = null;
            switch (clientId)
            {                
                case 558:
                case 820:
                case 723:
                case 33:
                case 1417:
                case 650:
                case 1421:
                case 649:
                case 230:
                case 123:
                case 126:
                case 242:
                case 253:
                case 1313:
                case 1096:
                case 287:
                case 968:
                case 250:
                case 57:
                case 313:
                case 1025:
                case 1321:    
                case 25:
                case 90:
                case 505:
                case 154:
                case 184:
                case 969:
                case 1422:
                case 1456:
                case 1279:
                case 67:
                case 673:
                case 149:
                case 1119:
                    resultView = new Business.HL7View.EPIC.EPICResultView(reportNo, accessionOrder, resultStatus, testing);                    
                    break;
                case 203: //Richard Taylor
                case 1177: //Spring Creek
                case 196: //Central Montana
                case 209: //Laura Bennett                
                case 954: // Barb Miller
                case 1471: //Marchion
                case 861:
                case 219:
                    resultView = new HL7View.CMMC.CMMCResultView(reportNo, accessionOrder);
                    break;
                case 1337:
                    resultView = new HL7View.CDC.MTDohResultView(reportNo, accessionOrder);
                    break;
                case 1335:
                    resultView = new HL7View.WYDOH.WYDOHResultView(reportNo, accessionOrder);
                    break;
                case 1203:
                    resultView = new HL7View.ECW.ECWResultView(reportNo, accessionOrder, testing);
                    break;
                case 553:
                    resultView = new HL7View.WPH.WPHResultView(reportNo, accessionOrder, testing);
                    break;
            }       
            return resultView;
        }
    }
}
