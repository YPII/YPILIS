using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.HL7View
{
    public interface IResultView
    {
        void Send(YellowstonePathology.Business.Rules.MethodResult result);
        void CanSend(YellowstonePathology.Business.Rules.MethodResult result);
        XElement GetDocument();        
    }
}
