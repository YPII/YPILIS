using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace YellowstonePathology.Business
{
    public class SearchList : ObservableCollection<SearchListItem>
    {
        SqlCommand m_Cmd;
        string m_SearchType;
        string m_SearchString;        
       
        public SearchList()
        {
            this.m_Cmd = new SqlCommand();
        }

        public string SearchString
        {
            get { return this.m_SearchString; }
            set { this.m_SearchString = value; }
        }

        public string SearchType
        {
            get { return this.m_SearchType; }
            set { this.m_SearchType = value; }
        }

        public string PFirstName
        {
            get 
            {
                string[] commaSplit = this.m_SearchString.Split(',');                
                switch (commaSplit.Length)
                {
                    case 1:
                        return string.Empty;
                    case 2:
                        return commaSplit[1].Trim();
                    default:
                        return string.Empty;
                }   
            }            
        }

        public string PLastName
        {
            get
            {
                string[] commaSplit = this.m_SearchString.Split(',');
                return commaSplit[0].Trim();                
            }            
        }

        public string AccessionNo
        {
            get { return this.m_SearchString; }
        }

        public string Birthdate
        {
            get { return DateTime.Parse(this.m_SearchString).ToShortDateString(); }
        }

        public string SSN
        {
            get { return this.m_SearchString; }
        }

        public string PhysicianClientId
        {
			get { return this.m_SearchString; }
            //{
                //long result = 0;
                //long.TryParse(this.m_SearchString, out result);
                //return result;
            //}
        }

		public string MasterAccessionNo
		{
			get  {return this.m_SearchString; }
		}

        public YellowstonePathology.Business.Rules.MethodResult SetFill()
        {
            YellowstonePathology.Business.Rules.MethodResult methodResult = new Rules.MethodResult();
            switch (this.m_SearchType.ToUpper())
            {
                case "PN":
                    this.SetFillByPatientName();
                    this.Fill();
                    break;
                case "RN":
                    this.SetFillByAccessionNo();
                    this.Fill();
                    break;                
                case "BD":
                    this.DateIsValid(methodResult);
                    if (methodResult.Success == true)
                    {
                        this.SetFillByBirthdate();
                        this.Fill();
                    }
                    break;
                case "SN":
                    this.SetFillBySSN();
                    this.Fill();
                    break;
                case "PH":
                    this.SetFillByPhysicianClientId();
                    this.Fill();
                    break;
				case "MA":
					this.SetFillByMasterAccessionNo();
                    this.Fill();
					break;
			}
            return methodResult;
        }

        public void SetFillByAccessionNo()
        {
            this.m_Cmd.Parameters.Clear();
			this.m_Cmd.CommandText = "prcGetSearchListByReportNo"; //"prcGetSearchListByAccessionNo";
            this.m_Cmd.CommandType = CommandType.StoredProcedure;
			this.m_Cmd.Parameters.Add("@ReportNo", SqlDbType.VarChar).Value = this.AccessionNo;
        }

        public void SetFillByPatientName()
        {            
            this.m_Cmd.Parameters.Clear();
            this.m_Cmd.CommandText = "prcGetSearchListByPatientName";
            this.m_Cmd.CommandType = CommandType.StoredProcedure;
            this.m_Cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 100).Value = this.PLastName;
            this.m_Cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 100).Value = this.PFirstName;
        }

        public void SetFillByBirthdate()
        {
            this.m_Cmd.Parameters.Clear();
            this.m_Cmd.CommandText = "prcGetSearchListByBirthdate";
            this.m_Cmd.CommandType = CommandType.StoredProcedure;
            this.m_Cmd.Parameters.Add("@Birthdate", SqlDbType.VarChar, 20).Value = this.Birthdate;            
        }

        public void SetFillBySSN()
        {
            this.m_Cmd.Parameters.Clear();
            this.m_Cmd.CommandText = "prcGetSearchListBySSN";
            this.m_Cmd.CommandType = CommandType.StoredProcedure;
            this.m_Cmd.Parameters.Add("@SSN", SqlDbType.VarChar, 20).Value = this.SSN;
        }

        public void SetFillByPhysicianClientId()
        {
            this.m_Cmd.Parameters.Clear();
            this.m_Cmd.CommandText = "prcGetSearchListByPhysicianClientId_1";
            this.m_Cmd.CommandType = CommandType.StoredProcedure;
            this.m_Cmd.Parameters.Add("@PhysicianClientId", SqlDbType.VarChar).Value = this.PhysicianClientId;
        }

		public void SetFillByMasterAccessionNo()
		{
			this.m_Cmd.Parameters.Clear();
			this.m_Cmd.CommandText = "prcGetSearchListByMasterAccessionNo";
			this.m_Cmd.CommandType = CommandType.StoredProcedure;
			this.m_Cmd.Parameters.Add("@MasterAccessionNo", SqlDbType.VarChar).Value = this.MasterAccessionNo;
		}

        public void Fill()
        {
            this.ClearItems();
            using (SqlConnection cn = new SqlConnection(BaseData.SqlConnectionString))
            {
                cn.Open();                               
                this.m_Cmd.Connection = cn;               
                using (SqlDataReader dr = this.m_Cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        SearchListItem item = new SearchListItem();
                        BaseData.Fill(item, dr);
                        this.Add(item);
                    }
                }                
            }
        }
        
        private void DateIsValid(YellowstonePathology.Business.Rules.MethodResult methodResult)
        {
            if(YellowstonePathology.Business.Helper.DateTimeExtensions.IsStringAValidDate(this.m_SearchString) == false)
            {
                methodResult.Success = false;
                methodResult.Message = "The date is not valid";
            }
        }
    }    
}