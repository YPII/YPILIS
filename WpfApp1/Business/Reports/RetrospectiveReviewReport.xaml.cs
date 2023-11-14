using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace YellowstonePathology.Business.Reports
{    
    public partial class RetrospectiveReviewReport : FixedDocument
    {
        private List<string> m_ReportNoCollection;
        private List<string> m_WordList;

        public RetrospectiveReviewReport(DateTime actionDate)
        {
            this.m_ReportNoCollection = new List<string>();
            this.m_WordList = this.GetWordList();

            DateTime finalDate = actionDate.AddDays(-1);
            if (actionDate.DayOfWeek == DayOfWeek.Monday)
            {
                finalDate = finalDate.AddDays(-3);
            }

            Business.ReportNoCollection finalCases = Business.Gateway.AccessionOrderGateway.GetSurgicalFinal(finalDate);
            int count = finalCases.Count;
            double tenPercentOfCount = Math.Round((count * .1), 0);

            Random rnd = new Random();
            int i = 0;
            while(true)
            {                
                int nextRnd = rnd.Next(0, count - 1);
                string nextMasterAccessionNo = finalCases[nextRnd].MasterAccessionNo;

                Business.Persistence.AODocumentBuilder documentBuilder = new Persistence.AODocumentBuilder(nextMasterAccessionNo, false);
                Business.Test.AccessionOrder ao = (Business.Test.AccessionOrder)documentBuilder.BuildNew();
                if(DoWordsExist(ao) == true)
                {
                    this.m_ReportNoCollection.Add(finalCases[nextRnd].Value);
                }                                    

                i += 1;
                if (i == 10) break;
            }            

            InitializeComponent();
            this.DataContext = this;
        }

        private bool DoWordsExist(Business.Test.AccessionOrder ao)
        {
            bool result = false;
            foreach(Business.Specimen.Model.SpecimenOrder specimenOrder in ao.SpecimenOrderCollection)
            {                
                foreach(string word in this.m_WordList)
                {
                    if(string.IsNullOrEmpty(specimenOrder.Description) == false)
                    {
                        if(specimenOrder.Description.ToLower().Contains(word.ToLower()))
                        {
                            return true;
                        }
                    }
                }
            }
            return result;
        }

        private List<string> GetWordList()
        {            
            List<string> result = new List<string>();
            result.Add("esophagus");
            result.Add("ge junction");
            result.Add("gastroesophageal junction");
            result.Add("stomach");
            result.Add("small bowel");
            result.Add("duodenum");
            result.Add("jejunum");
            result.Add("ampulla");
            result.Add("common bile duct");
            result.Add("ileum");
            result.Add("terminal ileum");
            result.Add("ileocecal valve");
            result.Add("cecum");
            result.Add("colon");
            result.Add("rectum");
            result.Add("anus");
            result.Add("cervix");
            result.Add("endocervix");
            result.Add("endometrium");
            result.Add("vagina");
            result.Add("vulva");
            result.Add("perineum");
            result.Add("labia majora");
            result.Add("labia minora");
            result.Add("ovary");
            result.Add("fallopian tube");
            result.Add("skin");
            result.Add("lung");
            result.Add("bronchus");
            result.Add("larynx");
            result.Add("vocal cord");
            result.Add("oral mucosa");
            result.Add("oral cavity");
            result.Add("tongue");
            result.Add("gingiva");
            result.Add("pharynx");
            result.Add("epiglottis");
            result.Add("kidney");
            result.Add("nasopharynx");
            result.Add("oropharynx");
            result.Add("peritoneum");
            result.Add("pleura");
            result.Add("tonsil");
            result.Add("trachea");
            result.Add("ureter");
            result.Add("urethra");
            result.Add("bladder");
            return result;
        }

        public List<string> ReportNoCollection
        {
            get { return this.m_ReportNoCollection; }
        }
    }
}
