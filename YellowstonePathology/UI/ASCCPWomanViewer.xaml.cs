﻿using System;
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

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for ASCCPWomanViewer.xaml
    /// </summary>
    public partial class ASCCPWomanViewer : Window
    {
        Business.ASCCPRule.Woman m_Woman;
        Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder m_WomensHealthProfileTestOrder;

        public ASCCPWomanViewer(Business.ASCCPRule.Woman woman, Business.Test.WomensHealthProfile.WomensHealthProfileTestOrder womensHealthProfileTestOrder)
        {
            this.m_Woman = woman;
            this.m_WomensHealthProfileTestOrder = womensHealthProfileTestOrder;
            InitializeComponent();
            this.DataContext = woman;
        }

        public Business.ASCCPRule.Woman Woman
        {
            get { return this.m_Woman; }
        }

        private void HyperLinkUpdateWHP_Click(object sender, RoutedEventArgs e)
        {
            this.m_WomensHealthProfileTestOrder.ManagementRecommendation = this.m_Woman.ManagementRecommendation;
        }
    }
}
