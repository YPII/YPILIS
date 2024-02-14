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
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace YellowstonePathology.UI.Redis
{
    /// <summary>
    /// Interaction logic for StoreSelection.xaml
    /// </summary>
    public partial class StoreSelection : Window
    {
        public StoreSelection()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            string basePath = this.TextBoxPath.Text;
            if (this.CanSave(basePath) == true)
            {
                if (this.CheckBoxCPTCode.IsChecked == true)
                {
                    string path = basePath + @"\cpt-codes";
                    if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                    this.HandleJSON(YellowstonePathology.Store.AppDBNameEnum.CPTCode, "code", path);
                }

                if (this.CheckBoxICDCode.IsChecked == true)
                {
                    string path = basePath + @"\icd-codes";
                    if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                    this.HandleJSON(YellowstonePathology.Store.AppDBNameEnum.ICDCode, "code", path);
                }

                if (this.CheckBoxDictationTemplate.IsChecked == true)
                {
                    string path = basePath + @"\dictation-template";
                    if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                    this.HandleJSON(YellowstonePathology.Store.AppDBNameEnum.DictationTemplate, "templateId", path);
                }

                if (this.CheckBoxSpecimen.IsChecked == true)
                {
                    string path = basePath + @"\specimen";
                    if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                    this.HandleJSON(YellowstonePathology.Store.AppDBNameEnum.Specimen, "specimenId", path);
                }

                if (this.CheckBoxStain.IsChecked == true)
                {
                    string path = basePath + @"\stains";
                    if (Directory.Exists(path) == false) Directory.CreateDirectory(path);
                    this.HandleJSON(YellowstonePathology.Store.AppDBNameEnum.Stain, "stainId", path);
                }

                MessageBox.Show("All selected data was written to file.");
            }
        }

        private void ButtonSelectPath_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dlg = new System.Windows.Forms.FolderBrowserDialog();
            dlg.SelectedPath = @"C:\";
            dlg.ShowDialog();
            this.TextBoxPath.Text = dlg.SelectedPath;
        }

        private void ButtonOK_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private bool CanSave(string path)
        {
            bool result = true;

            if (string.IsNullOrEmpty(path) == true)
            {
                MessageBox.Show("Select a directory to store the files.");
                result = false;
            }
            else if (Directory.Exists(path) == false)
            {
                MessageBox.Show("The directory " + path + " does not exist.");
                result = false;
            }

            return result;
        }

        private void HandleJSON(YellowstonePathology.Store.AppDBNameEnum db, string keyField, string path)
        {
            JsonSerializerSettings camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            Store.RedisDB redisDb = Store.AppDataStore.Instance.RedisStore.GetDB(db);
            foreach (string jString in (string[])redisDb.GetAllJSONKeys())
            {
                JObject jObject = JsonConvert.DeserializeObject<JObject>(jString);
                string name = jObject[keyField].ToString().ToLower();
                if (string.IsNullOrEmpty(name) == true) name = "nullId";
                string fileName = name + ".json";
                using (StreamWriter sw = new StreamWriter(path + @"\" + fileName, false))
                {
                    string formatted = JsonConvert.SerializeObject(jObject, Formatting.Indented, camelCaseFormatter);
                    sw.Write(formatted);
                }
            }
        }
    }
}
