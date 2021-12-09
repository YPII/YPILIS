using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace YellowstonePathology.Business.Test
{
    public class AccessionLockCollection : ObservableCollection<AccessionLock>
    {
        public AccessionLockCollection()
        {
            this.Build();
        }

        public void ClearLocks()
        {            
            foreach (Business.Test.AccessionLock accessionLock in this)
            {                
                if (accessionLock.IsLockAquiredByMe == true)
                {
                    accessionLock.ReleaseLock();
                }
            }
        }

        public void DelayDistributionLocksHeldBy(string lockHeldBy)
        {
            foreach (Business.Test.AccessionLock accessionLock in this)
            {
                if (accessionLock.Address == lockHeldBy)
                {
                    //accessionLock.ReleaseLock();
                }
            }
        }

        public void Refresh()
        {
            this.ClearItems();
            this.Build();
        }

        private void Build()
        {
            List<AccessionLock> list = new List<AccessionLock>();            

            Store.RedisDB locksDb = Store.AppDataStore.Instance.RedisStore.GetDB(Store.AppDBNameEnum.Lock);
            RedisResult[] redisResult = (RedisResult[])locksDb.GetAllHashes();

            foreach (RedisValue[] r in redisResult)
            {
                HashEntry he1 = new HashEntry(r[0], r[1]);
                HashEntry he2 = new HashEntry(r[2], r[3]);
                HashEntry he3 = new HashEntry(r[4], r[5]);
                AccessionLock item = new AccessionLock(new HashEntry[] { he1, he2, he3 });
                list.Add(item);
            }

            list.Sort(delegate(AccessionLock x, AccessionLock y) 
            {
                if(x.TimeAquired > y.TimeAquired)
                {
                    return 1;
                }
                else
                {
                    return -1;
                }
            });

            foreach(AccessionLock item in list)
            {
                this.Add(item);
            }
        }
    }
}
