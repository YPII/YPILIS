using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Data;
using System.Data.SqlClient;

namespace YellowstonePathology.Business.Flow
{
    public class FlowTestTypeCollection : ObservableCollection<FlowTestTypeItem>
    {
        public FlowTestTypeCollection()
        {
        }
    }
}
