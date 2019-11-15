using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Monitor.Model
{
    public class BlockCountCollection : ObservableCollection<BlockCount>
    {
        public BlockCountCollection()
        {

        } 
        
        public BlockCount GetByDate(DateTime date)
        {
            BlockCount result = null;
            foreach(BlockCount bc in this)
            {
                if (bc.BlockCountDate == date)
                {
                    result = bc;
                    break;
                }
            }
            return result;                
        }

        public bool ExistsByDate(DateTime date)
        {
            bool result = false;
            foreach (BlockCount bc in this)
            {
                if (bc.BlockCountDate == date)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public int SetBlocksToTransfer()
        {
            int result = 0;
            foreach(BlockCount blockCount in this)
            {
                Business.Calendar.PathologistsByLocation pathologistsByLocation = Business.Calendar.PathologistCalendarDayCollection.PathologistsCountByLocationOnDate(blockCount.BlockCountDate);
                int totalPaths = pathologistsByLocation.TotalCount;
                int totalBlocks = blockCount.GetTotalBlockCount();

                if(totalPaths != 0 && totalBlocks != 0)
                {
                    blockCount.YPIPaths = pathologistsByLocation.BillingsCount;
                    blockCount.BozemanPaths = pathologistsByLocation.BozemanCount;
                    blockCount.BlocksPerPath = totalBlocks / totalPaths;                    

                    if(blockCount.BozemanBlocks != 0 && blockCount.BozemanPaths != 0)
                    {
                        blockCount.BlocksPerPathBozeman = blockCount.BozemanBlocks / blockCount.BozemanPaths;
                        blockCount.BlocksToSend = blockCount.BlocksPerPath - blockCount.BlocksPerPathBozeman;
                        if (blockCount.BlocksToSend < 10)
                        {
                            blockCount.BlocksToSend = 0;
                        }                        
                    }
                    else
                    {
                        if(blockCount.BozemanBlocks == 0)
                        {                                                        
                            blockCount.BlocksToSend = blockCount.BlocksPerPath * blockCount.BozemanPaths;
                            if (blockCount.BlocksToSend < 10) blockCount.BlocksToSend = 0;
                        }
                        if(blockCount.BozemanPaths == 0)
                        {
                            blockCount.BlocksToSend = 0;
                        }
                    }                    
                }
                else
                {
                    blockCount.BlocksToSend = 0;
                }                                
            }                
            
            return result;
        }
    }
}
