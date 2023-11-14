using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Reflection;

namespace YellowstonePathology.UI
{    
    public partial class SpellCheck : System.Windows.Window
    {
        Microsoft.Office.Interop.Word.Application m_WordApp;                        

        object oMissing = System.Reflection.Missing.Value;
        Object oTrue = true;
        Object oFalse = false;

        Object MainDictionary = Type.Missing;
        Object CustomDictionary = Type.Missing;
        Object IgnoreUppercase = true;
        Object SuggestionMode = Type.Missing;

        Object CustomDictionary2 = Type.Missing;
        Object CustomDictionary3 = Type.Missing;
        Object CustomDictionary4 = Type.Missing;
        Object CustomDictionary5 = Type.Missing;
        Object CustomDictionary6 = Type.Missing;
        Object CustomDictionary7 = Type.Missing;
        Object CustomDictionary8 = Type.Missing;
        Object CustomDictionary9 = Type.Missing;
        Object CustomDictionary10 = Type.Missing;
        
        object m_DataObject;
        List<SpellCheckListItem> m_SpellCheckList;
        int m_CurrentPropertyIndex = -1;

        public SpellCheck()
        {           
            this.m_WordApp =  new Microsoft.Office.Interop.Word.Application();            
            this.m_WordApp.Visible = false;
            this.m_WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oTrue);

            InitializeComponent();            
        }

        public void StartSpellCheck(object dataObject)
        {
            this.m_CurrentPropertyIndex = 0;

            this.m_DataObject = dataObject;
            this.m_SpellCheckList = new List<SpellCheckListItem>();

            Type objectType = this.m_DataObject.GetType();
            this.SetupSpellCheckingList(objectType);
            
            this.FindNextSpellingError();
        }

        public void FindNextSpellingError()
        {
            this.m_CurrentPropertyIndex += 1;
            if (this.m_CurrentPropertyIndex == this.m_SpellCheckList.Count)
            {
                return;
            }
            else
            {
                object value = this.m_SpellCheckList[this.m_CurrentPropertyIndex].Property.GetValue(this.m_DataObject, null);

                Microsoft.Office.Interop.Word._Document document = this.m_WordApp.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oFalse);
                document.Words.First.InsertBefore(value.ToString());
                Microsoft.Office.Interop.Word.ProofreadingErrors spellingErrors = document.SpellingErrors;

                if (spellingErrors.Count > 0)
                {
                    document.CheckSpelling(ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing, ref oMissing,
                        ref oMissing, ref oMissing);

                    object oFirst = 0;
                    object oLast = document.Characters.Count - 1;
                    
                    string text = document.Range(ref oFirst, ref oLast).Text; 
                    this.m_SpellCheckList[this.m_CurrentPropertyIndex].Property.SetValue(this.m_DataObject, text, null);
                }
            }
            this.FindNextSpellingError();                  
        }                

        public void SetupSpellCheckingList(Type objectType)
        {            
            YellowstonePathology.Business.CustomAttributes.SqlTableAttribute tableAttribute =
                (YellowstonePathology.Business.CustomAttributes.SqlTableAttribute)Attribute.GetCustomAttribute(objectType, typeof(YellowstonePathology.Business.CustomAttributes.SqlTableAttribute));
            
            if (tableAttribute != null)
            {                
                if (tableAttribute.HasFieldsToSpellCheck == true)
                {
                    PropertyInfo[] propertyList = objectType.GetProperties();                    
                    foreach (PropertyInfo property in propertyList)
                    {
                        YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute fieldAttribute = (YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute)Attribute.GetCustomAttribute(property, typeof(YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute));
                        if (fieldAttribute != null)
                        {
                            if (fieldAttribute.NeedsSpellCheck == true)
                            {
                                SpellCheckListItem item = new SpellCheckListItem();
                                item.Property = property;
                                item.FieldAttribute = fieldAttribute;
                                this.m_SpellCheckList.Add(item);
                            }
                        }            
                    }
                }
            }
        }

        public void ListViewSuggestedWords_MouseLeftButtonUp(object sender, RoutedEventArgs args)
        {

        }

        public void ButtonIgnore_Click(object sender, RoutedEventArgs args)
        {

        }

        public void ButtonChange_Click(object sender, RoutedEventArgs args)
        {

        }

        public void ButtonCancel_Click(object sender, RoutedEventArgs args)
        {

        }

        public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs args)
        {
            this.m_WordApp.Quit(ref oFalse, ref oMissing, ref oMissing);
        }
        
    }

    public class SpellCheckListItem
    {
        PropertyInfo m_Property;
        YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute m_FieldAttribute;

        public SpellCheckListItem()
        {

        }

        public PropertyInfo Property
        {
            get { return this.m_Property; }
            set { this.m_Property = value; }
        }

        public YellowstonePathology.Business.CustomAttributes.SqlFieldAttribute FieldAttribute
        {
            get { return this.m_FieldAttribute; }
            set { this.m_FieldAttribute = value; }
        }
    }
}