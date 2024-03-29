﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View
{
    public class ResultViewFactory
    {
        public static IResultView GetResultView(string reportNo, Business.Test.AccessionOrder accessionOrder, int clientId, bool sendUnsoliceted, bool testing)
        {            
            IResultView resultView = null;
            switch (clientId)
            {                
                case 558:
                case 820:
                case 845:
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
                case 1565:
                case 100:
                case 1460:
                case 1662:
                case 1663:
                case 1664:
                case 1665:
                case 1684:
                case 162:
                case 1707:
                case 1731:
                case 1732:
                case 1668:
                case 1759:
                case 1306:
                case 1308:
                case 1058:
                case 54:
                case 1833:
                case 660:
                case 1830:
                case 1838:
                case 831:
                case 1870:
                case 1871:
                case 1872:
                case 1873:
                    resultView = new Business.HL7View.EPIC.EPICBeakerResultView(reportNo, accessionOrder, sendUnsoliceted, testing);                    
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
                case 1341:
                case 1088:
                case 1063:
                case 1288:                
                case 1035:
                case 935:
                case 1439:
                case 1399:
                case 1508:
                case 1319:
                case 1338:
                    resultView = new Business.HL7View.EPIC.EPICBeakerResultView(reportNo, accessionOrder, sendUnsoliceted, testing);
                    break;
                case 587: //NMH                    
                case 588:
                case 1604:
                case 1607:
                case 1608:
                case 1699:
                    resultView = new HL7View.NMH.NMHResultView(reportNo, accessionOrder, testing);
                    break;
                case 1622:
                    resultView = new HL7View.CMMC.CMMCResultView(reportNo, accessionOrder);
                    break;
                case 136:
                    resultView = new HL7View.Riverstone.RiverstoneResultView(reportNo, accessionOrder, testing);
                    break;
                case 1822:
                    resultView = new HL7View.EMA.EMAResultView(reportNo, accessionOrder, testing);
                    break;
            }       
            return resultView;
        }
    }
}
