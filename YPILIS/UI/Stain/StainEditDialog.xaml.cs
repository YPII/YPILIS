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
using System.Text.RegularExpressions;
using System.IO;

namespace YellowstonePathology.UI.Stain
{
    /// <summary>
    /// Interaction logic for StainEditDialog.xaml
    /// </summary>
    public partial class StainEditDialog : Window
    {
        private Business.Stain.Model.Stain m_Stain;
        private List<string> m_StainerTypes;
        private List<string> m_StainTypes;

        public StainEditDialog(Business.Stain.Model.Stain stain)
        {
            this.m_Stain = stain;

            this.m_StainerTypes = new List<string>();
            this.m_StainerTypes.Add(null);
            this.m_StainerTypes.Add("BenchMark ULTRA");
            this.m_StainerTypes.Add("BenchMark Special Stains");

            this.m_StainTypes = new List<string>();
            this.m_StainTypes.Add("IHC");
            this.m_StainTypes.Add("Crystallography");
            this.m_StainTypes.Add("CytochemicalStain");
            this.m_StainTypes.Add("CytochemicalForMicroorganisms");
            this.m_StainTypes.Add("GradedStain");
            this.m_StainTypes.Add("DualStain");

            InitializeComponent();
            DataContext = this;
        }

        public StainEditDialog()
        {
            this.m_Stain = new Business.Stain.Model.Stain();

            this.m_StainerTypes = new List<string>();
            this.m_StainerTypes.Add(null);
            this.m_StainerTypes.Add("BenchMark ULTRA");
            this.m_StainerTypes.Add("BenchMark Special Stains");

            this.m_StainTypes = new List<string>();
            this.m_StainTypes.Add("IHC");
            this.m_StainTypes.Add("CytochemicalStain");
            this.m_StainTypes.Add("CytochemicalForMicroorganisms");
            this.m_StainTypes.Add("GradedStain");
            this.m_StainTypes.Add("DualStain");

            InitializeComponent();
            DataContext = this;
        }

        public Business.Stain.Model.Stain Stain
        {
            get { return this.m_Stain; }
            set { this.m_Stain = value; }
        }

        public List<string> StainerTypes
        {
            get { return this.m_StainerTypes; }
        }

        public List<string> StainTypes
        {
            get { return this.m_StainTypes; }
        }

        public void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = this.CanSave();
            if (methodResult.Success == true)
            {
                methodResult = Business.Helper.JSONHelper.IsValidJSONString(this.m_Stain.ToJSON());
            }

            if (methodResult.Success == true)
            {
                this.m_Stain.Save();
                Close();
            }
            else
            {
                MessageBox.Show(methodResult.Message);
            }
        }

        private YellowstonePathology.Business.Rules.MethodResult CanSave()
        {
            YellowstonePathology.Business.Rules.MethodResult result = new Business.Rules.MethodResult();
            string id = this.DetermineStainId();
            if (this.m_Stain.PerformedByHand == false)
            {
                if (String.IsNullOrEmpty(this.m_Stain.VentanaBenchMarkId))
                {
                    result.Success = false;
                    result.Message = "The Ventana BenchMark Id cannot be null.";
                }
            }

            if (string.IsNullOrEmpty(this.m_Stain.StainId) == true)
            {
                if (YellowstonePathology.Business.Stain.Model.StainCollection.Instance.Exists(id) == true)
                {
                    result.Success = false;
                    result.Message = "The Stain Id is not unique. Add a number or a consonent to the Name.";
                }
                else if(result.Success == true)
                {
                    this.m_Stain.StainId = id;
                }
            }
            return result;
        }

        private string DetermineStainId()
        {
            string lower = this.m_Stain.StainName.ToLower();
            char first = lower[0];
            char last = lower[lower.Length - 1];
            string pattern = @"[aeiou]";
            Regex regex = new Regex(pattern);
            string result = regex.Replace(lower, "");
            if (first != result[0]) result = first + result;
            if (last != result[result.Length - 1]) result = result + last;
            pattern = @"[)(/\\,\.\s -]";
            regex = new Regex(pattern);
            result = regex.Replace(result, "");
            return result;
        }
    }
}
