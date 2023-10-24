using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Business.BarcodeScanning
{
    public class EmbeddingScanCollection : ObservableCollection<EmbeddingScan>
    {
        public EmbeddingScanCollection()
        {            
        }        

        public void UpdateStatus(EmbeddingScan scan)
        {           
            YellowstonePathology.Business.Gateway.AccessionOrderGateway.UpdateEmbeddingScan(scan, DateTime.Parse(scan.DateScanned.ToShortDateString()));
        }

        public EmbeddingScan HandleScan(string aliquotOrderId, DateTime processorStartTime, TimeSpan processorFixationDuration)
        {            
            EmbeddingScan scan = new EmbeddingScan(aliquotOrderId, processorStartTime, processorFixationDuration);
            EmbeddingScan existingScan = null;
            foreach (EmbeddingScan item in this)
            {
                if (item.AliquotOrderId == aliquotOrderId && item.DateScanned.ToShortDateString() == scan.DateScanned.ToShortDateString())
                {
                    existingScan = item;
                    this.Remove(item);
                    break;
                }
            }

            if (existingScan == null)
            {
                YellowstonePathology.Business.Gateway.AccessionOrderGateway.SetEmbeddingScan(scan, DateTime.Today);
            }
            else
            {
                scan.UpdateFromExistingScan(existingScan);
                YellowstonePathology.Business.Gateway.AccessionOrderGateway.UpdateEmbeddingScan(scan, DateTime.Today);
            }
            this.InsertItem(0, scan);

            return scan;
        }

        public static EmbeddingScanCollection GetByScanDate(DateTime scanDate)
        {
            EmbeddingScanCollection result = new EmbeddingScanCollection();
            EmbeddingScanCollection temp = Business.Gateway.AccessionOrderGateway.GetEmbeddingScanCollectionByScanDate(scanDate);

            List<EmbeddingScan> list = temp.ToList<EmbeddingScan>();

            list.Sort(delegate (EmbeddingScan x, EmbeddingScan y)
            {
                if (x.DateScanned == y.DateScanned) return 0;
                if (x.DateScanned > y.DateScanned)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });


            foreach (EmbeddingScan item in list)
            {
                result.InsertItem(0, item);
            }

            return result;
        }

        public static EmbeddingScanCollection GetAll()
        {            
            EmbeddingScanCollection result = Business.Gateway.AccessionOrderGateway.GetAllEmbeddingScans();
            return result;
        }
    }
}