using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Domain
{
	public class DataLoadResult
	{
		YellowstonePathology.Business.Domain.DataLoadStatusEnum m_DataLoadStatusEnum;

		public DataLoadResult()
		{
			this.DataLoadStatusEnum = YellowstonePathology.Business.Domain.DataLoadStatusEnum.NotFound;
		}

		public YellowstonePathology.Business.Domain.DataLoadStatusEnum DataLoadStatusEnum
		{
			get { return this.m_DataLoadStatusEnum; }
			set { this.m_DataLoadStatusEnum = value; }
		}

		public bool Successful
		{
			get { return this.m_DataLoadStatusEnum == YellowstonePathology.Business.Domain.DataLoadStatusEnum.Successful; }
		}
	}
}
