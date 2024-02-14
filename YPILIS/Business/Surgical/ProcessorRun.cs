using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class ProcessorRun
    {
        protected string m_Name;
        protected Nullable<DateTime> m_StartTime;
        protected TimeSpan m_FixationDuration;

        public ProcessorRun()
        {

        }

        public ProcessorRun(string name, Nullable<DateTime> startTime, TimeSpan fixationDuration)
        {
            this.m_Name = name;
            this.m_StartTime = startTime;
            this.m_FixationDuration = fixationDuration;
        }

        public Business.Rules.MethodResult FixationDurationIsOk()
        {
            Business.Rules.MethodResult result = new Rules.MethodResult();
            result.Success = true;
            if (this.m_FixationDuration.TotalHours < 6)
            {
                result.Message = "Warning! Fixation duration will be under 6 hours unless this specimen is held.";
                result.Success = false;
            }
            else if (this.m_FixationDuration.TotalHours > 72)
            {
                result.Message = "Warning! Fixation duration will be over 72 hours if processed normally.";
                result.Success = false;
            }
            return result;
        }

        public Nullable<DateTime> GetProcessorStartTime(Nullable<DateTime> receivedTime)
        {
            Nullable<DateTime> result = null;

            if (receivedTime.HasValue == true)
            {
                throw new Exception("needs work");
                /*
                DateTime processorStartDate = DateTime.Parse(receivedTime.Value.ToShortDateString());
                switch (this.m_ProcessorRunDay)
                {
                    case ProcessorRunDayEnum.Today:
                        result = processorStartDate + this.m_StartTime;
                        break;
                    case ProcessorRunDayEnum.Tomorrow:
                        result = processorStartDate.AddDays(1) + this.m_StartTime;
                        break;
                    case ProcessorRunDayEnum.Sunday:
                        int dayOfWeek = (int)receivedTime.Value.DayOfWeek;
                        result = processorStartDate.AddDays(7 - dayOfWeek) + this.m_StartTime;
                        break;
                }
                */
            }

            return result;
        }

        public DateTime GetFixationEndTime(DateTime fixationStartTime)
        {
            return this.m_StartTime.Value.Add(this.m_FixationDuration);
        }

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value; }
        }

        public Nullable<DateTime> StartTime
        {
            get { return this.m_StartTime; }
            set { this.m_StartTime = value; }
        }

        public TimeSpan FixationDuration
        {
            get { return this.m_FixationDuration; }
            set { this.m_FixationDuration = value; }
        }
    }
}