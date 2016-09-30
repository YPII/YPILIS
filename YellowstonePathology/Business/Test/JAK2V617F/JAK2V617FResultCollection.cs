using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.JAK2V617F
{
	public class JAK2V617FResultCollection : ObservableCollection<JAK2V617FResult>
	{
		public JAK2V617FResultCollection()
		{
			this.Add(new JAK2V617FDetectedResult());
			this.Add(new JAK2V617FNotDetectedResult());
			this.Add(new JAK2V617FNoResult());
		}

		public JAK2V617FResult GetResult(JAK2V617FTestOrder panelSetOrder)
		{
            JAK2V617FResult result = null;
            foreach (JAK2V617FResult jak2V617Result in this)
            {
                if (jak2V617Result.ResultCode == panelSetOrder.ResultCode)
                {
                    result = jak2V617Result;
                    break;
                }
            }
			return result;
		}

		public static JAK2V617FResultCollection GetAllResults()
		{
			JAK2V617FResultCollection result = new JAK2V617FResultCollection();
			result.Add(new JAK2V617FDetectedResult());
			result.Add(new JAK2V617FNotDetectedResult());
			return result;
		}
	}
}
