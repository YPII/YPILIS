﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace YellowstonePathology.Properties {
    
    
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "17.3.0.0")]
    internal sealed partial class Settings : global::System.Configuration.ApplicationSettingsBase {
        
        private static Settings defaultInstance = ((Settings)(global::System.Configuration.ApplicationSettingsBase.Synchronized(new Settings())));
        
        public static Settings Default {
            get {
                return defaultInstance;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Yellowstone Pathology Institute\\")]
        public string LocalApplicationFolder {
            get {
                return ((string)(this["LocalApplicationFolder"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("c:\\program data\\ypi\\dictation\\")]
        public string LocalDictationFolder {
            get {
                return ((string)(this["LocalDictationFolder"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Yellowstone Pathology Institute\\Application\\")]
        public string LocalLoginDataFolder {
            get {
                return ((string)(this["LocalLoginDataFolder"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Yellowstone Pathology Institute\\HistologyAlert.wav")]
        public string LocalHistologyAlertFile {
            get {
                return ((string)(this["LocalHistologyAlertFile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Yellowstone Pathology Institute\\Dictation\\Done\\")]
        public string LocalDoneDictationFolder {
            get {
                return ((string)(this["LocalDoneDictationFolder"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\Program Files\\Yellowstone Pathology Institute\\ClientMissingInformationLetter.d" +
            "oc")]
        public string ClientMissingInformationLetterFileName {
            get {
                return ((string)(this["ClientMissingInformationLetterFileName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Cytology Slide Label Printer")]
        public string CytologySlideLabelPrinterName {
            get {
                return ((string)(this["CytologySlideLabelPrinterName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Histology Slide Label Printer")]
        public string HistologySlideLabelPrinterName {
            get {
                return ((string)(this["HistologySlideLabelPrinterName"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\ProgramData\\ypi\\dictionary\\ypi-custom.dic")]
        public string LocalDICFile {
            get {
                return ((string)(this["LocalDICFile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("C:\\ProgramData\\ypi\\dictionary\\ypi-custom.aff")]
        public string LocalAFFFile {
            get {
                return ((string)(this["LocalAFFFile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\ypiidc\\YPIILIS\\Dictionary\\ypi-custom.dic")]
        public string ServerDICFile {
            get {
                return ((string)(this["ServerDICFile"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\ypiidc\\YPIILIS\\Dictionary\\ypi-custom.aff")]
        public string ServerAFFFile {
            get {
                return ((string)(this["ServerAFFFile"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Server = 10.1.2.26; Uid = sqldude; Pwd = 123Whatsup; Database = lis; Pooling=True" +
            ";")]
        public string CurrentConnectionString {
            get {
                return ((string)(this["CurrentConnectionString"]));
            }
            set {
                this["CurrentConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("changedb")]
        public string ChangeDBPassword {
            get {
                return ((string)(this["ChangeDBPassword"]));
            }
            set {
                this["ChangeDBPassword"] = value;
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\\\\\fileserver\\\\documents\\\\SpecimenLog\\\\")]
        public string SpecimenLogScannedDocumentFilePath {
            get {
                return ((string)(this["SpecimenLogScannedDocumentFilePath"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\fileserver\\documents\\Dictation\\")]
        public string ServerDictationFolder {
            get {
                return ((string)(this["ServerDictationFolder"]));
            }
        }
        
        [global::System.Configuration.ApplicationScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Server = 10.1.2.26; Uid = sqldude; Pwd = 123Whatsup; Database = lis; Pooling=True" +
            ";")]
        public string MySqlConnectionString {
            get {
                return ((string)(this["MySqlConnectionString"]));
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("Data Source=TestSQL;Initial Catalog=YPIData;Integrated Security=True")]
        public string SqlServerConnectionString {
            get {
                return ((string)(this["SqlServerConnectionString"]));
            }
            set {
                this["SqlServerConnectionString"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("%USERPROFILE%\\AppData\\Local\\ypi\\MonitoredProperty")]
        public string MonitoredPropertyFolder {
            get {
                return ((string)(this["MonitoredPropertyFolder"]));
            }
            set {
                this["MonitoredPropertyFolder"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("true")]
        public string ShowSecondMonitorOnSame {
            get {
                return ((string)(this["ShowSecondMonitorOnSame"]));
            }
            set {
                this["ShowSecondMonitorOnSame"] = value;
            }
        }
        
        [global::System.Configuration.UserScopedSettingAttribute()]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [global::System.Configuration.DefaultSettingValueAttribute("\\\\FileServer\\AccessionDocuments")]
        public string AccessionDocuments {
            get {
                return ((string)(this["AccessionDocuments"]));
            }
            set {
                this["AccessionDocuments"] = value;
            }
        }
    }
}
