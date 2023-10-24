using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Audit.Model
{
    public class CanSaveFacilityAudit : Audit
    {
        private Business.Facility.Model.Facility m_Facility;

        public CanSaveFacilityAudit(Business.Facility.Model.Facility facility)
        {
            this.m_Facility = facility;
        }

        public override void Run()
        {
            this.m_Status = AuditStatusEnum.OK;
            this.m_Message.Clear();
            StringBuilder msg = new StringBuilder();
            msg.Append("The facility requires");

            if (string.IsNullOrEmpty(this.m_Facility.FacilityId) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                msg.Append(" an id (from the name),");
            }
            if (string.IsNullOrEmpty(this.m_Facility.FacilityName)== true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                msg.Append(" a name,");
            }
            if (string.IsNullOrEmpty(this.m_Facility.Address1) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                msg.Append(" an address,");
            }
            if (string.IsNullOrEmpty(this.m_Facility.City) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                msg.Append(" a city,");
            }
            if (string.IsNullOrEmpty(this.m_Facility.State) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                msg.Append(" a state,");
            }
            if (string.IsNullOrEmpty(this.m_Facility.ZipCode) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                msg.Append(" a zipcode,");
            }
            if (string.IsNullOrEmpty(this.m_Facility.PhoneNumber) == true)
            {
                this.m_Status = AuditStatusEnum.Failure;
                msg.Append(" a phone number,");
            }

            if(this.m_Status == AuditStatusEnum.Failure)
            {
                msg.Remove(msg.Length - 1, 1);
                msg.Append(".");
                this.m_Message = msg;
            }
        }
    }
}
