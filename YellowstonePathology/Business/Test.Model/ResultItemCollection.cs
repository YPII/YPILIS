using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Test.Model
{
	public class ResultItemCollection : ObservableCollection<ResultItem>
	{
		public ResultItemCollection()
		{
		}
	}
}
