using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.LynchSyndrome
{
    public class LynchSyndromeIHCPanel : YellowstonePathology.Business.Panel.Model.Panel
    {
        public LynchSyndromeIHCPanel()
        {
            this.m_PanelId = 66;
            this.m_PanelName = "Immunohistochemistry";
            this.m_AcknowledgeOnOrder = false;

            YellowstonePathology.Business.Test.Model.Test mlh1 = new YellowstonePathology.Business.Test.Model.Test();
            mlh1.TestId = 121;
            mlh1.TestName = "MLH1";
            mlh1.TestAbbreviation = "MLH1";
            this.m_TestCollection.Add(mlh1);

            string objectIdMsh2 = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Test.Model.Test msh2 = new YellowstonePathology.Business.Test.Model.Test();
            msh2.TestId = 122;
            msh2.TestName = "MSH2";
            msh2.TestAbbreviation = "MSH2";
            this.m_TestCollection.Add(msh2);

            string objectIdMsh6 = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Test.Model.Test msh6 = new YellowstonePathology.Business.Test.Model.Test();
            msh6.TestId = 218;
            msh6.TestName = "MSH6";
            msh6.TestAbbreviation = "MSH6";
            this.m_TestCollection.Add(msh6);

            string objectIdPms2 = MongoDB.Bson.ObjectId.GenerateNewId().ToString();
            YellowstonePathology.Business.Test.Model.Test pms2 = new YellowstonePathology.Business.Test.Model.Test();
            pms2.TestId = 217;
            pms2.TestName = "PMS2";
            pms2.TestAbbreviation = "PMS2";
            this.m_TestCollection.Add(pms2);
        }
    }
}
