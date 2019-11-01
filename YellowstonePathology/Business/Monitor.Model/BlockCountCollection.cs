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
                if (blockCount.YPIBlocks > 0 && blockCount.BozemanBlocks > 0 && pathologistsByLocation.BillingsCount > 0 && pathologistsByLocation.BozemanCount > 0)
                {
                    int totalCountPer = (blockCount.YPIBlocks + blockCount.BozemanBlocks) / pathologistsByLocation.TotalCount;
                    int billingsCountPer = blockCount.YPIBlocks / pathologistsByLocation.BillingsCount;
                    int bozemanCountPer = blockCount.BozemanBlocks / pathologistsByLocation.BozemanCount;
                    if (billingsCountPer > totalCountPer)
                    {
                        int excessBlocks = (billingsCountPer - bozemanCountPer) * pathologistsByLocation.BillingsCount;
                        int excessBlocksPer = excessBlocks / pathologistsByLocation.TotalCount;
                        int blocksToSend = excessBlocksPer * pathologistsByLocation.BozemanCount;
                        blockCount.BlocksToSend = blocksToSend > 10 ? blocksToSend : 0;
                        if(blockCount.BlockCountDate == DateTime.Today) result = blocksToSend > 10 ? blocksToSend : 0;
                    }
                }
            }                
            
            return result;
        }
    }
}
