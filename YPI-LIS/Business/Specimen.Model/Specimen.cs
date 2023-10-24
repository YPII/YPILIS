using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Linq;
using YellowstonePathology.Business.Persistence;
using System.Data;
using MySql.Data.MySqlClient;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class Specimen : YellowstonePathology.Business.Interface.IOrderTargetType
    {
        protected string m_SpecimenId;
        protected string m_SpecimenName;
        protected string m_Description;

        protected YellowstonePathology.Business.Billing.Model.CptCode m_CPTCode;
        protected int m_CPTCodeQuantity;

        protected string m_ClientFixation;
        protected string m_LabFixation;        
        protected string m_ProcessorRunId;
        protected bool m_RequiresGrossExamination;
        protected bool m_Active;
        

        public Specimen()
        {
            this.m_RequiresGrossExamination = true;
            this.m_CPTCodeQuantity = 1;
        }

        public string TypeId
        {
            get { return this.m_SpecimenId; }
        }

        [PersistentPrimaryKeyProperty(false)]
        public string SpecimenId
        {
            get { return this.m_SpecimenId; }
            set { this.m_SpecimenId = value; }
        }

        [PersistentProperty()]
        public string SpecimenName
        {
            get { return this.m_SpecimenName; }
            set { this.m_SpecimenName = value; }
        }

        [PersistentProperty()]
        public string Description
        {
            get { return this.m_Description; }
            set { this.m_Description = value; }
        }

        [PersistentProperty()]
        public YellowstonePathology.Business.Billing.Model.CptCode CPTCode
        {
            get { return this.m_CPTCode; }
            set { this.m_CPTCode = value; }
        }

        [PersistentProperty()]
        public int CPTCodeQuantity
        {
            get { return this.m_CPTCodeQuantity; }
            set { this.m_CPTCodeQuantity = value; }
        }

        [PersistentProperty()]
        public string ClientFixation
        {
            get { return this.m_ClientFixation; }
            set { this.m_ClientFixation = value; }
        }

        [PersistentProperty()]
        public string LabFixation
        {
            get { return this.m_LabFixation; }
            set { this.m_LabFixation = value; }
        }

        [PersistentProperty()]
        public bool RequiresGrossExamination
        {
            get { return this.m_RequiresGrossExamination; }
            set { this.m_RequiresGrossExamination = value; }
        }

        [PersistentProperty()]
        public string ProcessorRunId
        {
            get { return this.m_ProcessorRunId; }
            set { this.m_ProcessorRunId = value; }
        }

        [PersistentProperty()]
        public bool Active
        {
            get { return this.m_Active; }
            set { this.m_Active = value; }
        }

        public string ToJSON()
        {
            var camelCaseFormatter = new JsonSerializerSettings();
            camelCaseFormatter.ContractResolver = new CamelCasePropertyNamesContractResolver();
            string jString = JsonConvert.SerializeObject(this, Formatting.Indented, camelCaseFormatter);

            JObject jObject = JObject.Parse(jString);
            jObject.Remove("typeId");

            if (this.m_CPTCode != null)
            {
                jObject.Property("cptCode").AddAfterSelf(new JProperty("cptCodeId", this.m_CPTCode.Code));
            }
            else
            {
                jObject.Property("cptCode").AddAfterSelf(new JProperty("cptCodeId", null));
            }
            jObject.Remove("cptCode");

            string result = JsonConvert.SerializeObject(jObject, Formatting.Indented, camelCaseFormatter);
            return result;
        }

        public void Save()
        {
            string jString = this.ToJSON();
            MySqlCommand cmd = new MySqlCommand("Insert tblSpecimen (SpecimenId, JSONValue) values (@SpecimenId, @JSONValue) ON DUPLICATE KEY UPDATE SpecimenId = @SpecimenId, JSONValue = @JSONValue;");
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@SpecimenId", this.m_SpecimenId);
            cmd.Parameters.AddWithValue("@JSONValue", jString);

            using (MySqlConnection cn = new MySqlConnection(YellowstonePathology.Properties.Settings.Default.CurrentConnectionString))
            {
                cn.Open();
                cmd.Connection = cn;
                cmd.ExecuteNonQuery();
            }
        }
    }
}
