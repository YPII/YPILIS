using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Domain
{
	public class CaseNotesKeyCollection : ObservableCollection<CaseNotesKey>
	{
		public CaseNotesKeyCollection()
		{
		}

		public CaseNotesKeyCollection(YellowstonePathology.Business.ClientOrder.Model.ClientOrder clientOrder)
		{
			CaseNotesKey caseNotesKey = null;
			if (clientOrder.MasterAccessionNo != null)
			{
				caseNotesKey = new CaseNotesKey(CaseNotesKeyNameEnum.MasterAccessionNo, clientOrder.MasterAccessionNo.ToString());
			}
			else
			{
				caseNotesKey = new CaseNotesKey(CaseNotesKeyNameEnum.ClientOrderId, clientOrder.ClientOrderId);
				foreach (YellowstonePathology.Business.ClientOrder.Model.ClientOrderDetail clientOrderDetail in clientOrder.ClientOrderDetailCollection)
				{
					CaseNotesKey detailKey = new CaseNotesKey(CaseNotesKeyNameEnum.ContainerId, clientOrderDetail.ContainerId);
					this.Add(detailKey);
				}
			}
			this.Add(caseNotesKey);
		}

		public CaseNotesKeyCollection(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
		{
			CaseNotesKey caseNotesKey = new CaseNotesKey(CaseNotesKeyNameEnum.MasterAccessionNo, accessionOrder.MasterAccessionNo.ToString());
			this.Add(caseNotesKey);
		}

		public void WriteKeys(YellowstonePathology.Business.Domain.OrderCommentLog orderCommentLog)
		{
			if (this.HasId(CaseNotesKeyNameEnum.MasterAccessionNo))
			{
				orderCommentLog.MasterAccessionNo = this.GetId(CaseNotesKeyNameEnum.MasterAccessionNo);
			}

			if (this.HasId(CaseNotesKeyNameEnum.ClientOrderId))
			{
				orderCommentLog.ClientOrderId = this.GetId(CaseNotesKeyNameEnum.ClientOrderId);
			}

			if (this.HasId(CaseNotesKeyNameEnum.ContainerId))
			{
				orderCommentLog.ContainerId = this.GetId(CaseNotesKeyNameEnum.ContainerId);
			}
		}

		public bool HasId(CaseNotesKeyNameEnum caseNotesKeyNameEnum)
		{
			bool result = false;
			foreach (CaseNotesKey caseNotesKey in this)
			{
				if (caseNotesKey.CaseNotesKeyName == caseNotesKeyNameEnum)
				{
					result = true;
					break;
				}
			}
			return result;
		}

		public string GetId(CaseNotesKeyNameEnum caseNotesKeyNameEnum)
		{
			string result = string.Empty;
			foreach (CaseNotesKey caseNotesKey in this)
			{
				if (caseNotesKey.CaseNotesKeyName == caseNotesKeyNameEnum)
				{
					result = caseNotesKey.Key;
					break;
				}
			}
			return result;
		}
	}
}
