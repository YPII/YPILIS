using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Surgical
{
    public class AssignmentScanCollection : ObservableCollection<AssignmentScan>
    {
        public AssignmentScanCollection()
        { }

        public AssignmentScan GetAssignmentScan(string scanId)
        {
            AssignmentScan result = this.FirstOrDefault(t => t.ScanId == scanId);
            return result;
        }

        public bool Exists(string scanId)
        {
            AssignmentScan test = this.FirstOrDefault(t => t.ScanId == scanId);
            return test == null ? false : true;
        }
    }
}
