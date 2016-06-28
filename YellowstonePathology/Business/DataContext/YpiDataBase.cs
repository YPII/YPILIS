using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Text;
using System.Reflection;
using System.Xml.Linq;

namespace YellowstonePathology.Business.DataContext
{
    public class YpiDataBase : System.Data.Linq.DataContext
	{        
        public YpiDataBase(string connectionString)
            : base(connectionString)
        {                        
            this.DeferredLoadingEnabled = false;            
			DataLoadOptions dataLoadOptions = new DataLoadOptions();
			
			this.LoadOptions = dataLoadOptions;            
		}

        public void Insert<T>(object o)
        {
            ITable table = this.GetTable(typeof(T));
            table.InsertOnSubmit(o);            
        }

        public void Delete<T>(object o)
        {
            ITable table = this.GetTable(typeof(T));
            table.DeleteOnSubmit(o);
        }        

        /*[Function(Name = "dbo.linqGetPhysiciansWithSalesperson")]
        public ISingleResult<YellowstonePathology.Business.Domain.Physician> GetPhysiciansWithSalesperson()
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())));
            return ((ISingleResult<YellowstonePathology.Business.Domain.Physician>)(result.ReturnValue));
        }        

        [Function(Name = "dbo.pDeletePhysicianSalesperson")]
        public int DeletePhysicianSalesperson([Parameter(DbType = "Int")] int physicianSalespersonId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), physicianSalespersonId);
            return 1;
        }

        [Function(Name = "dbo.prcInsertReportDistributionByMasterAccessionNoXML")]
		public int InsertReportDistributionByMasterAccessionNoXML([Parameter(DbType = "VarChar")] string masterAccessionNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), masterAccessionNo);
            return 1;
        }       

		[Function(Name = "prcInsertReportDistributionByMasterAccessionNoXML")]
		public int SetReportDistribution(string masterAccessionNo)
		{
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), masterAccessionNo);
            return 1;
		}

        [Function(Name = "prcInsertReportDistributionLogFromDistributionId")]
        public int SetReportDistributionLogByReportDistributionId(int reportDistributionId)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), reportDistributionId);
            return 1;
        }

        [Function(Name = "prcInsertReportDistributionLogFromMasterAccessionNo")]
		public int SetReportDistributionLogByMasterAccessionNo(string masterAccessionNo)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), masterAccessionNo);
            return 1;
        }

        [Function(Name = "prcSlideLabelsBuildTableSurgical")]
        public int SetSlideLabelSetSurgicalTable(DateTime accessionDate, string location)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), accessionDate, location);
            return 1;
        }        

        [Function(Name = "dbo.prcGetDistinctAccessionDates")]
        public ISingleResult<YellowstonePathology.Business.Domain.WorkingDate> GetDistinctAccessionDates([Parameter(DbType = "DateTime")] DateTime startAccessionDate, [Parameter(DbType = "DateTime")] DateTime endAccessionDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startAccessionDate, endAccessionDate);
            return ((ISingleResult<YellowstonePathology.Business.Domain.WorkingDate>)(result.ReturnValue));
        }

		[Function(Name = "dbo.pGetXmlOrdersToAcknowledge")]
		public ISingleResult<Domain.XElementFromSql> GetXmlOrdersToAcknowledge([Parameter(DbType = "varchar")] string panelOrderIdString)
		{
			IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), panelOrderIdString);
			return ((ISingleResult<Domain.XElementFromSql>)(result.ReturnValue));
		}*/

        [Function(Name = "dbo.prcPOCRetensionReport")]
        public ISingleResult<Domain.XElementFromSql> GetPOCRetensionReport([Parameter(DbType = "DateTime")] DateTime startDate, [Parameter(DbType = "DateTime")] DateTime endDate)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), startDate, endDate);
            return ((ISingleResult<Domain.XElementFromSql>)(result.ReturnValue));            
        }

        /*[Function(Name = "dbo.prcInsertChannelMessage")]
        public int InsertChannelMessage([Parameter(DbType = "int")] int channelId, [Parameter(DbType = "VarChar")] string messageControlId, [Parameter(DbType = "VarChar")] string message, [Parameter(DbType = "VarChar")] string messageType)
        {
            IExecuteResult result = this.ExecuteMethodCall(this, ((MethodInfo)(MethodInfo.GetCurrentMethod())), channelId, messageControlId, message, messageType);
            return 1;
        }*/
	}
}
