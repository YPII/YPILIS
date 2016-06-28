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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YellowstonePathology.UI.Gross
{
    public partial class DictationTemplatePage : UserControl
    {
        private string m_GrossDescription;
        
        private YellowstonePathology.Business.Specimen.Model.SpecimenOrder m_SpecimenOrder;
        private YellowstonePathology.Business.Test.AccessionOrder m_AccessionOrder;
        private YellowstonePathology.Business.User.SystemIdentity m_SystemIdentity;
        private DictationTemplate m_DictationTemplate;

        public DictationTemplatePage(YellowstonePathology.Business.Specimen.Model.SpecimenOrder specimenOrder, YellowstonePathology.Business.Test.AccessionOrder accessionOrder, YellowstonePathology.Business.User.SystemIdentity systemIdentity)
        {
            this.m_SpecimenOrder = specimenOrder;
            this.m_AccessionOrder = accessionOrder;
            this.m_SystemIdentity = systemIdentity;

            DictationTemplateCollection dictationTemplateCollection = DictationTemplateCollection.GetAll();
            this.m_DictationTemplate = dictationTemplateCollection.GetTemplate(m_SpecimenOrder.SpecimenId);
            this.SetGrossDescription();

            InitializeComponent();

            DataContext = this;
        }

        public void SetGrossDescription()
        {
            if(string.IsNullOrEmpty(this.m_DictationTemplate.Text) == false)
            {
                this.m_GrossDescription = this.m_DictationTemplate.BuildResultText(this.m_SpecimenOrder, this.m_AccessionOrder, this.m_SystemIdentity);
            }            
        }

        public string GrossDescription
        {
            get { return this.m_GrossDescription; }
            set { this.m_GrossDescription = value; }
        }

        public YellowstonePathology.Business.Specimen.Model.SpecimenOrder SpecimenOrder
        {
            get { return this.m_SpecimenOrder; }
        }

        public DictationTemplate DictationTemplate
        {
            get { return this.m_DictationTemplate; }
        }        

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ButtonCreateParagraph_Click(object sender, RoutedEventArgs e)
        {
            
        }
    }
}
