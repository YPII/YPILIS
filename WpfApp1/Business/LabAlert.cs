using System;

namespace YellowstonePathology.Business
{
    public class LabAlert
    {
        /*string SoundFileName = @"C:\Chimes.Wav";
        System.Timers.Timer m_Timer;

        [System.Runtime.InteropServices.DllImport("winmm.DLL", EntryPoint = "PlaySound", SetLastError = true)]
        private static extern bool PlaySound(string szSound, System.IntPtr hMod, PlaySoundFlags flags);

        [System.Flags]
        public enum PlaySoundFlags : int
        {
            SND_SYNC = 0x0000,
            SND_ASYNC = 0x0001,
            SND_NODEFAULT = 0x0002,
            SND_LOOP = 0x0008,
            SND_NOSTOP = 0x0010,
            SND_NOWAIT = 0x00002000,
            SND_FILENAME = 0x00020000,
            SND_RESOURCE = 0x00040004
        }

        public LabAlert()
        {
            this.m_Timer = new System.Timers.Timer();
            this.m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(m_Timer_Elapsed);

            this.m_Timer.Interval = 1000 * 5;
            this.m_Timer.Enabled = true;        

        }

        void m_Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            System.Windows.MessageBox.Show("asdf");            
            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = "prcGetLabOrdersNotAcknowledged";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                using (MySqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows == true)
                    {
                        PlaySound(this.SoundFileName, new System.IntPtr(), PlaySoundFlags.SND_SYNC);
                    }
                }
            }
        }*/
    }
}
