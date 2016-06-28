using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Surgical
{
    public class ProcessorRun
    {
        private string m_ProcessorRunId;
        private string m_Name;
        private TimeSpan m_StartTime;
        private TimeSpan m_FixationTime;
        private string m_FixationTimeString;
        private ProcessorRunDayEnum m_ProcessorRunDay;

        public ProcessorRun(string processorRunId, string name, TimeSpan startTime, TimeSpan fixationTime, string fixationTimeString, ProcessorRunDayEnum processorRunDay)
        {
            this.m_ProcessorRunId = processorRunId;
            this.m_Name = name;
            this.m_StartTime = startTime;
            this.m_FixationTime = fixationTime;
            this.m_FixationTimeString = fixationTimeString;
            this.m_ProcessorRunDay = processorRunDay;
        }             

        public Nullable<DateTime> GetProcessorStartTime(Nullable<DateTime> receivedTime)
        {
            Nullable<DateTime> result = null;

            if (receivedTime.HasValue == true)
            {
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
            }

            return result;
        }

        public Nullable<DateTime> GetFixationEndTime(Nullable<DateTime> fixationStartTime)
        {
            Nullable<DateTime> result = null;

            if (fixationStartTime.HasValue == true)
            {
                Nullable<DateTime> processorStartTime = this.GetProcessorStartTime(fixationStartTime);
                if (processorStartTime.HasValue == true)
                {
                    result = processorStartTime + this.m_FixationTime;
                }
            }

            return result;
        }        

        public string ProcessorRunId
        {
            get { return this.m_ProcessorRunId; }
            set { this.m_ProcessorRunId = value; }
        }

        public string Name
        {
            get { return this.m_Name; }
            set { this.m_Name = value;}
        }

        public TimeSpan StartTime
        {
            get { return this.m_StartTime; }
            set { this.m_StartTime = value; }
        }

        public TimeSpan FixationTime
        {
            get { return this.m_FixationTime; }
            set { this.m_FixationTime = value; }
        }

        public ProcessorRunDayEnum ProcessorRunDay
        {
            get { return this.m_ProcessorRunDay; }
            set { this.m_ProcessorRunDay = value; }
        }
    }
}
