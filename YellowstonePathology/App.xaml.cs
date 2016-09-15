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
//using LibGit2Sharp;

namespace YellowstonePathology.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>

    public partial class App : Application
    {
		System.Timers.Timer m_Timer;

		public App()
		{            
			if (CheckDuplicateProcess())
			{

				MessageBox.Show("A LIS application is already running on this computer.\n\nThis application will now exit.",
					"Too many instances", MessageBoxButton.OK, MessageBoxImage.Exclamation);
				App.Current.Shutdown(-1);
				return;
			}

            this.Exit += App_Exit;
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(YellowstonePathology.Business.Logging.EmailExceptionHandler.HandleException);
		}

        private void App_Exit(object sender, ExitEventArgs e)
        {
            Business.Test.AccessionLockCollection accessionLockCollection = new Business.Test.AccessionLockCollection();
            accessionLockCollection.ClearLocks();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).SelectAll();
        }

        protected override void OnStartup(StartupEventArgs e)
        {            
            Business.Test.AccessionLockCollection accessionLockCollection = new Business.Test.AccessionLockCollection();
            accessionLockCollection.ClearLocks();

            string startUpWindow = string.Empty;

			if (System.Environment.MachineName.ToUpper() == "CUTTINGA" || System.Environment.MachineName.ToUpper() == "CUTTINGB" ) //|| System.Environment.MachineName.ToUpper() == "COMPILE")
            {                
                YellowstonePathology.UI.Cutting.CuttingStationPath cuttingStationPath = new Cutting.CuttingStationPath();
                cuttingStationPath.Start();
            }                        
            else if (System.Environment.MachineName.ToUpper() == "CYTOLOG2")// || System.Environment.MachineName.ToUpper() == "COMPILE")
            {
				YellowstonePathology.UI.Cytology.ThinPrepPapSlidePrintingPath thinPrepPapSlidePrintingPath = new Cytology.ThinPrepPapSlidePrintingPath();
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
            //HandleLocalRepository();          
		}

        public static void HandleLocalRepository()
        {
            /*
            string localRepoPath = @"C:\ProgramData\ypi\lisdata";

            if(System.IO.Directory.Exists(localRepoPath) == false)
                    System.IO.Directory.CreateDirectory(localRepoPath);

            Repository repo = null;
            if(Repository.IsValid(localRepoPath) == false)
            {
                string remoteRepoPath = "https://github.com/YPII/lisdata.git";
                Repository.Clone(remoteRepoPath, localRepoPath);
            }
            else
            {
                repo = new Repository(localRepoPath);
                RepositoryStatus repositoryStatus = repo.RetrieveStatus();
                if(repositoryStatus.IsDirty == false)
                {
                    repo.Fetch("origin");
                    Signature signature = new Signature("SidHarder", "softwarenavigator@gmail.com", new DateTimeOffset(DateTime.Now));
                    Branch master = repo.Branches["master"];
                    repo.MergeFetchedRefs(signature, new MergeOptions());
                }
                else
                {
                    MessageBox.Show("The local repository has uncommitted changes.");
                }
            }   
            */         
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
            if (Directory.Exists(YellowstonePathology.Properties.Settings.Default.LocalApplicationFolder) == false)
            {
                Directory.CreateDirectory(YellowstonePathology.Properties.Settings.Default.LocalApplicationFolder);
            }
            if (Directory.Exists(YellowstonePathology.Properties.Settings.Default.LocalDictationFolder) == false)
            {
                Directory.CreateDirectory(YellowstonePathology.Properties.Settings.Default.LocalDictationFolder);
            }
            if (Directory.Exists(YellowstonePathology.Properties.Settings.Default.LocalDoneDictationFolder) == false)
            {
                Directory.CreateDirectory(YellowstonePathology.Properties.Settings.Default.LocalDoneDictationFolder);
            }
            if (Directory.Exists(YellowstonePathology.Properties.Settings.Default.LocalLoginDataFolder) == false)
            {
                Directory.CreateDirectory(YellowstonePathology.Properties.Settings.Default.LocalLoginDataFolder);
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
    }
}
