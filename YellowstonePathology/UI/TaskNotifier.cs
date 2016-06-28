using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.UI
{
    public class TaskNotifier
    {
        public delegate void AlertEventHandler(object sender, CustomEventArgs.TaskOrderCollectionReturnEventArgs e);
        public event AlertEventHandler Alert;

        private static TaskNotifier TaskNotifierInstance;
        private System.Timers.Timer m_Timer;
        private System.Media.SoundPlayer m_WavPlayer;

        private const int ShortIntervalTicks = 1000 * 60;
        private const int LongIntervalTicks = 1000 * 60 * 5;

        private TaskNotifier()
        {
            
        }

        public static TaskNotifier Instance
        {
            get
            {
                if (TaskNotifierInstance == null)
                {
                    TaskNotifierInstance = new TaskNotifier();
                }
                return TaskNotifierInstance;
            }
        }

        public void Start()
        {
            this.m_WavPlayer = new System.Media.SoundPlayer();
            this.m_WavPlayer.SoundLocation = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.AlertWaveFileName;
            this.m_WavPlayer.LoadAsync();
            this.StartTimer();
        }

        public TaskNotifier Notifier
        {
            get { return TaskNotifierInstance; }
        }

        private void StartTimer()
        {
            this.m_Timer = new System.Timers.Timer();
            this.m_Timer.Elapsed += new System.Timers.ElapsedEventHandler(Timer_Elapsed);

            this.m_Timer.Interval = LongIntervalTicks;
            this.m_Timer.Enabled = true;
        }

        private void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            string assignedTo = YellowstonePathology.Business.User.UserPreferenceInstance.Instance.UserPreference.AcknowledgeTasksFor;
			YellowstonePathology.Business.Task.Model.TaskOrderCollection taskOrderCollection = YellowstonePathology.Business.Gateway.AccessionOrderGateway.GetTasksNotAcknowledged(assignedTo, YellowstonePathology.Business.Task.Model.TaskAcknowledgementType.Immediate);
            if (taskOrderCollection.Count != 0)
            {
                this.m_WavPlayer.Play();
                this.m_Timer.Interval = ShortIntervalTicks;
                if(this.Alert != null) this.Alert(this, new CustomEventArgs.TaskOrderCollectionReturnEventArgs(taskOrderCollection));
            }
            else
            {
                this.m_Timer.Interval = LongIntervalTicks;
            }
        }
    }
}
