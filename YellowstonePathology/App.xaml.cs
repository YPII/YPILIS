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
using System.Threading;
using System.IO;
using System.ComponentModel;
using System.ServiceModel;
using System.ServiceModel.Description;
using Microsoft.Win32;
using System.Diagnostics;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
		System.Timers.Timer m_Timer;
        private List<string> m_ContainerList;
        private int m_CurrentContainerIndex = 0;

        public App()
		{
            this.m_ContainerList = new List<string>();
            /*
            this.m_ContainerList.Add("CTNR97AB77F6-3C9D-4986-A40C-28A25043441B");
            this.m_ContainerList.Add("CTNR90E68A30-C1C7-4602-A7B0-D55814F41564");
            this.m_ContainerList.Add("CTNR5BAC4806-DF10-41FB-8914-7AEDEB007B21");
            this.m_ContainerList.Add("CTNR3432C29F-8A8E-40E6-B924-7B8F902B295F");
            this.m_ContainerList.Add("CTNR918C85A7-A7CF-40B9-ABA7-EF7ED8E8BA17");
            this.m_ContainerList.Add("CTNRFEE96B84-73E0-468C-A26A-F75D3E6224F3");
            this.m_ContainerList.Add("CTNR24EA31DC-0802-4679-956A-C35091022211");
            this.m_ContainerList.Add("CTNR75B444B6-1286-483D-AECC-BB4B93FA0D46");
            this.m_ContainerList.Add("CTNR8FE60DBB-91F5-446E-8FBB-6BE644CD638F");
            this.m_ContainerList.Add("CTNR8594A5DE-B20A-4830-9F0F-B69947267B93");
            this.m_ContainerList.Add("CTNR72E93EC2-A997-4AA0-99A7-F4D304203171");
            this.m_ContainerList.Add("CTNR2FFBCC6B-BB68-44CA-A5F2-ED2393D68A50");
            this.m_ContainerList.Add("CTNRCCA3010B-5BEC-4696-8D14-BBA4A3F42A58");
            this.m_ContainerList.Add("CTNR1A101E60-6AC3-41A5-BB41-B8C232AF370D");
            this.m_ContainerList.Add("CTNR33F021DA-E80B-4326-B03C-D58079DD3EFD");
            this.m_ContainerList.Add("CTNR7CA6FBA7-8C9F-4E66-9A1D-6688350F4889");
            this.m_ContainerList.Add("CTNR6A48163D-DDCF-48C4-892C-016AFC20D7C4");
            this.m_ContainerList.Add("CTNRC96EC223-E4DE-4579-BC23-91476D2F0BBD");
            this.m_ContainerList.Add("CTNR2E303E50-E771-4A33-A17E-28B38918C86B");
            this.m_ContainerList.Add("CTNR5916F756-BF66-43D4-BE33-5F4A433E790F");
            this.m_ContainerList.Add("CTNR2FA23BD8-E429-4EE9-90D9-2BB2CD7D2F13");
            this.m_ContainerList.Add("CTNR6416D82F-8BA8-4FD4-A580-B632FDC919D7");
            this.m_ContainerList.Add("CTNRE2D8FF85-7791-4001-B4E6-74BABD1E7F16");
            this.m_ContainerList.Add("CTNR36827E38-3D7A-4B55-A75C-2DF7C5677C61");
            this.m_ContainerList.Add("CTNRA3F40F44-B43E-4C66-9255-9AD62E3C7AC0");
            this.m_ContainerList.Add("CTNR6B4EDD29-FF88-46CB-9719-75187E4FAD78");
            this.m_ContainerList.Add("CTNR8C4A4932-57FA-4D14-884B-B5C5288F71BE");
            this.m_ContainerList.Add("CTNRABE5A58A-FA20-4FA8-927A-085BA38367DE");
            this.m_ContainerList.Add("CTNRC751CC3B-1FB2-4B9E-808A-575BA1BCCE76");
            this.m_ContainerList.Add("CTNR1D89079E-C4FA-4E3A-9386-D322DF8800A3");
            this.m_ContainerList.Add("CTNRF14F4A0A-8A14-4DF7-BAE7-DA759E0AE1A8");
            this.m_ContainerList.Add("CTNRD105A91C-A745-4B7D-B66E-976D2746B3E5");
            this.m_ContainerList.Add("CTNRF5D1EE6D-DC5A-42BC-A045-70228B1C7EA7");
            this.m_ContainerList.Add("CTNR6EA95EFA-75C7-41E3-944F-83C7FD170B09");
            this.m_ContainerList.Add("CTNR91D1CD23-6805-4735-A5C2-502CFC942BA3");
            this.m_ContainerList.Add("CTNR4D5B1704-FEE5-4DBE-B162-1E4BE8604255");
            this.m_ContainerList.Add("CTNR0DF04CEF-C62C-4FB7-A956-8C00C6F6E7F6");
            this.m_ContainerList.Add("CTNRD01115A0-1C40-43BB-956E-B77854AE72F2");
            this.m_ContainerList.Add("CTNRCBAE7714-F55C-4B21-B1C9-89068D0B74A7");
            this.m_ContainerList.Add("CTNRF48706B0-8D67-497F-8717-78B7EE7B531F");
            this.m_ContainerList.Add("CTNRC1457ED8-51F7-43EB-B335-898D03559F47");
            this.m_ContainerList.Add("CTNR2DA2737E-83FC-4A73-AB5C-C5BD5F8B2D4D");
            this.m_ContainerList.Add("CTNR27D7C3BE-FA90-4065-A8EA-7495F26F144B");
            this.m_ContainerList.Add("CTNRD18D4D47-DBF9-40D7-BE1A-1DD1F3E2D541");
            this.m_ContainerList.Add("CTNR0FF46759-8CCD-4E54-B314-9238967A3D5B");
            this.m_ContainerList.Add("CTNR5FF32B15-BC18-43AF-80E6-36EBF29D73E8");
            this.m_ContainerList.Add("CTNR3AE1B8FF-C20D-4855-B4AB-2FCCC851538E");
            this.m_ContainerList.Add("CTNR46C02842-3BFD-4142-A878-08E00DC82E7E");
            this.m_ContainerList.Add("CTNRF18A5D56-005D-4895-97C0-77771CD4FCF7");
            this.m_ContainerList.Add("CTNR6974B942-9C19-4A76-B508-06BA2D52D906");
            this.m_ContainerList.Add("CTNRAA4AB959-91EC-453D-9715-8737E47C25D1");
            this.m_ContainerList.Add("CTNRB2AC42E7-BE3E-4B92-AC15-C4982003ACE2");
            this.m_ContainerList.Add("CTNR5B3A8752-4D8B-4F9A-855D-79D025F00FC0");
            this.m_ContainerList.Add("CTNRC7CC9ED8-8B67-4BC4-B51D-D884CFF366DA");
            this.m_ContainerList.Add("CTNRFFF52799-44D1-4978-8AA0-83872226D7AB");
            this.m_ContainerList.Add("CTNR19D1EEB1-7CC3-4621-A76E-83313EC49BCB");
            this.m_ContainerList.Add("CTNR3351EE6A-5F74-4CD2-8461-0DC9271AA333");
            this.m_ContainerList.Add("CTNR6A978BCA-69DC-4EA0-AC47-AC2FB0854321");
            this.m_ContainerList.Add("CTNR1541BB04-D04F-484B-B4B8-3A75AB86A34E");
            this.m_ContainerList.Add("CTNRA6A58B7F-1526-468B-BB0F-001C26433646");
            this.m_ContainerList.Add("CTNR6CF4C770-653B-4B76-A3CB-59FE96F2C1DF");
            this.m_ContainerList.Add("CTNRED874105-4043-44D2-B494-285352E320B8");
            this.m_ContainerList.Add("CTNR85CD92AD-4E7E-4940-956B-B98E5AD19E09");
            this.m_ContainerList.Add("CTNRBE8A2221-2225-43D8-8DB6-F64DE72A78A3");
            this.m_ContainerList.Add("CTNR82182CA8-39A5-457B-9C7F-CEAEFA84B64E");
            this.m_ContainerList.Add("CTNRFC276AD2-BE0E-4B19-BB51-D954273A5382");
            this.m_ContainerList.Add("CTNR9405CA71-71DB-4450-AECA-D3E95111787B");
            this.m_ContainerList.Add("CTNR345D1E59-23B4-487E-9A32-0D2016C9BC27");
            this.m_ContainerList.Add("CTNR4FB406F5-843E-4E5D-B29D-F00A576B772D");
            this.m_ContainerList.Add("CTNRDA6CC4CE-5B35-4B6D-8B9C-562E2D927F8D");
            this.m_ContainerList.Add("CTNR51C1A4A3-9B6F-4B5E-AB66-0320EF063561");
            this.m_ContainerList.Add("CTNR975E4DA2-68E5-421F-9FEE-2C364539A13A");
            this.m_ContainerList.Add("CTNR14C82BD8-D3F1-430B-83B5-3EC140CB1E5F");
            this.m_ContainerList.Add("CTNR7E13636A-313D-48C7-A9B0-4759436FFF31");
            this.m_ContainerList.Add("CTNRF16DFD50-C44C-4241-A335-4FC91BD16610");
            this.m_ContainerList.Add("CTNR0CD4DC8D-E485-4B55-BB3F-6F301CE8AF22");
            this.m_ContainerList.Add("CTNRDF236E28-0D46-4A7E-95C7-D9B77A2D2C04");
            this.m_ContainerList.Add("CTNRFA9A5CAE-7791-476F-83FE-100ED59B4892");
            this.m_ContainerList.Add("CTNR655AFC76-6E46-4AF8-AE8B-F8C86ED4FD3D");
            this.m_ContainerList.Add("CTNR4E03B484-0495-4C15-869F-123503C70647");
            this.m_ContainerList.Add("CTNR8E833B85-31B2-46D4-8AEA-63B2960C5813");
            this.m_ContainerList.Add("CTNRB020F716-5A28-41C9-8A64-A2897FB4D86B");
            this.m_ContainerList.Add("CTNRA5E10E93-305F-4EDA-BC48-ADEAD2F18887");
            this.m_ContainerList.Add("CTNR908EA400-1459-48A3-AC84-ADA3830B6376");
            this.m_ContainerList.Add("CTNR1B17926C-F6EA-4F41-8620-C8A0C81AB426");
            this.m_ContainerList.Add("CTNR108B84A5-C035-4670-8A5C-1D31C98086FE");
            this.m_ContainerList.Add("CTNR4A8D8A91-455A-4D0E-96D1-D7F68D7CE211");
            this.m_ContainerList.Add("CTNR0C049924-72AF-4413-B24A-2E873467D155");
            this.m_ContainerList.Add("CTNR811DFF1E-DD03-4E36-8BB4-9A59DC5AD37F");
            this.m_ContainerList.Add("CTNR99CE4ED8-561E-4331-B80C-5378AA6261B6");
            this.m_ContainerList.Add("CTNR34B779D4-51A5-486F-B3A6-619B7D71F403");
            this.m_ContainerList.Add("CTNR69FA0F0C-804B-487F-843B-EEA07D31963C");
            this.m_ContainerList.Add("CTNR80167664-6995-4E66-82D6-6F5E89B9F1F4");
            this.m_ContainerList.Add("CTNR117005E0-9A91-45A8-8C4E-0212C6110E5B");
            this.m_ContainerList.Add("CTNR97BA5B20-599D-446B-9092-F02FBD3E55C1");
            this.m_ContainerList.Add("CTNR4429FF7B-7C2B-4F2A-90F3-574AF8BA6E93");
            this.m_ContainerList.Add("CTNR6A7EED7E-ABB2-49A4-ADDE-36FB43411336");
            this.m_ContainerList.Add("CTNR11F9ABE6-48C4-416C-9D38-5B88939AD250");
            this.m_ContainerList.Add("CTNREC94C458-D2AB-44F0-B9F9-15621D2EFFA2");
            this.m_ContainerList.Add("CTNR1C049673-EBD0-4BE1-9E82-19D4E7D89189");
            this.m_ContainerList.Add("CTNR1FD940A3-3609-4BF3-B2B8-574FC82F517D");
            this.m_ContainerList.Add("CTNR024A55AA-729E-470C-AFD6-7D6E1338979E");
            this.m_ContainerList.Add("CTNRA580BEED-A564-4176-9BAC-86CB3C677B5B");
            this.m_ContainerList.Add("CTNR02904CC5-CD19-4F5F-A2D0-677A189C3348");
            this.m_ContainerList.Add("CTNR90334F7B-EF94-41FE-8710-F2D87B117F72");
            this.m_ContainerList.Add("CTNREF285AD1-198A-48CC-899D-3781C3F275AD");
            this.m_ContainerList.Add("CTNR050FAF43-1C0C-431D-9CB2-44703CD5E017");
            this.m_ContainerList.Add("CTNR652493F5-0DB3-4FD6-B4CE-470948E1B6EB");
            this.m_ContainerList.Add("CTNRAC54679C-46F6-4FCC-9899-1B6EDB05AE40");
            this.m_ContainerList.Add("CTNR407EB8C5-FEE5-4463-AB4D-B17B14938FEA");
            this.m_ContainerList.Add("CTNR7360C89B-CC5A-40D9-AF42-F22AB20E73D4");
            this.m_ContainerList.Add("CTNR54837EC5-8111-4350-A738-CF18F9FCB978");
            this.m_ContainerList.Add("CTNR85BDBEE0-D124-4A11-A5C8-0E9C09577301");
            this.m_ContainerList.Add("CTNRBA3DB7B6-4DAC-487F-9A1B-9E1908D30556");
            this.m_ContainerList.Add("CTNRC4B84E10-B181-4C21-9435-338F4F4D3EA8");
            this.m_ContainerList.Add("CTNRB4709CD6-47CC-408A-9A35-849C8AD4C1A4");
            this.m_ContainerList.Add("CTNR1A4B2AD1-4CDF-4DFA-A8A0-D6DCDB4B3C36");
            this.m_ContainerList.Add("CTNR2795DA92-0CEA-40CB-9A0B-8097F4379660");
            this.m_ContainerList.Add("CTNRBBD80860-95B8-496E-9876-3464E2A36CEF");
            this.m_ContainerList.Add("CTNR3874F873-46B5-4C2A-8512-12C8E8A67C3E");
            this.m_ContainerList.Add("CTNR89E25CD2-C91D-4BF7-ADE7-C1058C2AF056");
            this.m_ContainerList.Add("CTNRB32C766E-1BFB-48EF-9B18-D3A4FF7F02B7");
            this.m_ContainerList.Add("CTNR14DEF5AF-F7E2-4429-8848-D9231A25701E");
            */

            if (CheckDuplicateProcess())
			{
				MessageBox.Show("A LIS application is already running on this computer.\n\nThis application will now exit.",
					"Too many instances", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				App.Current.Shutdown(-1);
				return;
			}

            this.Exit += App_Exit;
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(Business.Logging.EmailExceptionHandler.HandleException);
		}

        private void App_Exit(object sender, ExitEventArgs e)
        {
            Business.Test.AccessionLockCollection accessionLockCollection = new Business.Test.AccessionLockCollection();
            accessionLockCollection.ClearLocks();
        }

        public List<string> ContainerList
        {
            get { return this.m_ContainerList; }
        }

        public int CurrentContainerIndex
        {
            get { return this.m_CurrentContainerIndex; }
            set { this.m_CurrentContainerIndex = value; }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            if(string.IsNullOrEmpty(YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.HostName) == true)
            {
                string path = System.Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\ypilis.json";
                if (File.Exists(path) == false)
                {
                    YellowstonePathology.Business.User.UserPreferenceInstance.SetDefaultUserPreference();
                }
                else
                {
                    YellowstonePathology.Business.User.UserPreferenceInstance.SetUserPreferenceHostNameByLocation();
                }
            }

            Store.AppDataStore.Instance.LoadData();

            Business.Test.AccessionLockCollection accessionLockCollection = new Business.Test.AccessionLockCollection();
            //accessionLockCollection.DelayDistributionLocksHeldBy("");
            accessionLockCollection.ClearLocks();

            string startUpWindow = string.Empty;

            if (System.Environment.MachineName.ToUpper() == "CUTTING-D" || System.Environment.MachineName.ToUpper() == "CUTTINGA" || System.Environment.MachineName.ToUpper() == "CUTTINGB" 
                || System.Environment.MachineName.ToUpper() == "NMHC" || System.Environment.MachineName.ToUpper() == "CUTTINGC") // || System.Environment.MachineName.ToUpper() == "COMPILE")
            {
                YellowstonePathology.UI.Cutting.CuttingStationPath cuttingStationPath = new Cutting.CuttingStationPath();
                cuttingStationPath.Start();
            }
            else if (System.Environment.MachineName.ToUpper() == "CYTOLOG2") // || System.Environment.MachineName.ToUpper() == "COMPILE")
            {
                UI.Cytology.ThinPrepPapSlidePrintingPath thinPrepPapSlidePrintingPath = new Cytology.ThinPrepPapSlidePrintingPath();
                thinPrepPapSlidePrintingPath.Start();
            }
            else
            {
                startUpWindow = @"UI\MainWindow.xaml";
                this.StartupUri = new System.Uri(startUpWindow, System.UriKind.Relative);
            }

            EventManager.RegisterClassHandler(typeof(TextBox), TextBox.GotFocusEvent, new RoutedEventHandler(TextBox_GotFocus));
            base.OnStartup(e);


            this.StartTimer();
            this.SetupApplicationFolders();
            this.EmptyDraftsFolder();
        }        

        public static bool HandledictionarySetup()
        {            
            try
            {
                System.IO.File.Copy(YellowstonePathology.Properties.Settings.Default.ServerDICFile, YellowstonePathology.Properties.Settings.Default.LocalDICFile, true);
                System.IO.File.Copy(YellowstonePathology.Properties.Settings.Default.ServerAFFFile, YellowstonePathology.Properties.Settings.Default.LocalAFFFile, true);
                return true;
            }
            catch(Exception e)
            {
                System.Windows.MessageBox.Show(e.Message);
                return false;
            }            
        }        

		protected override void OnExit(ExitEventArgs e)
		{
			this.m_Timer.Stop();
			this.m_Timer.Dispose();
            YellowstonePathology.Business.Persistence.DocumentGateway.Instance.Flush();
            base.OnExit(e);
        }

        private void SetupApplicationFolders()
        {            
            List<string> appDirectories = new List<string>();            
            appDirectories.Add(@"%USERPROFILE%\AppData\Local\ypi\");
            appDirectories.Add(YellowstonePathology.Properties.Settings.Default.MonitoredPropertyFolder);
            appDirectories.Add(@"C:\ProgramData\ypi\");
            appDirectories.Add(@"C:\ProgramData\ypi\drafts\");
            appDirectories.Add(@"C:\ProgramData\ypi\dictionary\");
            appDirectories.Add(YellowstonePathology.Properties.Settings.Default.LocalDictationFolder);
            appDirectories.Add($"{YellowstonePathology.Properties.Settings.Default.LocalDictationFolder}done");

            foreach (string appDir in appDirectories)
            {
                if (System.IO.Directory.Exists(Environment.ExpandEnvironmentVariables(appDir)) == false)
                {
                    try
                    {                        
                        System.IO.Directory.CreateDirectory(Environment.ExpandEnvironmentVariables(appDir));
                    }
                    catch (Exception e)
                    {
                        Business.Logging.EmailExceptionHandler.HandleException(e.Message);
                    }                    
                }
            }                                    
        }                      

		private bool CheckDuplicateProcess()
		{            
			System.Diagnostics.Process currentProcess = System.Diagnostics.Process.GetCurrentProcess();
			System.Diagnostics.Process[] openProcesses = System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName);
			foreach(System.Diagnostics.Process process in openProcesses)
			{
				if ((currentProcess.SessionId == 0 && currentProcess.Id != process.Id) ||
					currentProcess.SessionId != 0 && currentProcess.SessionId == process.SessionId && currentProcess.Id != process.Id)
				{
					if (currentProcess.StartInfo.UserName == process.StartInfo.UserName)
					{
						return true;
					}
				}
			}         
			return false;
		}                    

		private void StartTimer()
		{
			this.m_Timer = new System.Timers.Timer();
			this.m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);
						
            TimeSpan timeToNextEvent = new TimeSpan(0, 15, 0);
			this.m_Timer.Interval = timeToNextEvent.TotalMilliseconds;
			this.m_Timer.Enabled = true;
		}

		private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			
		}
        
        private void PullLisData()
        {
            string cmd = Environment.GetEnvironmentVariable("GitCmdPath");
            if (string.IsNullOrEmpty(cmd) == false)
            {
                Process p = new Process();
                ProcessStartInfo info = new ProcessStartInfo(cmd, "PullLisData.bat");
                info.WindowStyle = ProcessWindowStyle.Hidden;
                p.StartInfo = info;
                p.Start();
            }
        }

        private void EmptyDraftsFolder()
        {
            string[] drafts = Directory.GetFiles(@"C:\ProgramData\ypi\drafts\");
            foreach(string fileName in drafts)
            {
                try
                {
                    File.Delete(fileName);
                }
                catch (Exception)
                {
                    continue;
                }
            }
        }
    }
}
