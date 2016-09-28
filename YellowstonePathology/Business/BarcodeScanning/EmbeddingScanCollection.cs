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
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            if (db.KeyExists(scan.HashKey) == true)
            {
                HashEntry[] hashEntries = scan.GetHasEntries();
                db.HashSet(scan.HashKey, "Updated", scan.Updated.ToString());
            }
        }

        public EmbeddingScan HandleScan(string aliquotOrderId, string processorRunId, string processorRun)
        {
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            EmbeddingScan scan = new EmbeddingScan(aliquotOrderId, processorRunId, processorRun);

            if (db.KeyExists("EmbeddingScan:" + aliquotOrderId) == true)
            {
                foreach (EmbeddingScan item in this)
                {
                    if (item.AliquotOrderId == aliquotOrderId)
                    {
                        this.Remove(item);
                        break;
                    }
                }
            }
            else
            {
                db.SetAdd("EmbeddingScans:" + DateTime.Today.ToShortDateString(), scan.HashKey);
            }

            HashEntry[] hashEntries = scan.GetHasEntries();
            db.HashSet(scan.HashKey, hashEntries);
            this.InsertItem(0, scan);

            return scan;
        }

        public static EmbeddingScanCollection GetByScanDate(DateTime scanDate)
        {
            EmbeddingScanCollection result = new EmbeddingScanCollection();

            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            RedisValue[] members = db.SetMembers("EmbeddingScans:" + scanDate.ToShortDateString());

            List<EmbeddingScan> list = new List<EmbeddingScan>();
            for (int i = 0; i < members.Length; i++)
            {
                HashEntry[] hashEntries = db.HashGetAll(members[i].ToString());
                EmbeddingScan item = new EmbeddingScan(hashEntries);
                list.Add(item);
            }


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
    }
}