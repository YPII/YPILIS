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

        public int BlocksToTransfer(DateTime date)
        {
            int result = 0;
            if(this.ExistsByDate(date) == true)
            {
                BlockCount blockCount = this.GetByDate(date);
                Business.Calendar.PathologistsByLocation pathologistsByLocation = Business.Calendar.PathologistCalendarDayCollection.PathologistsCountByLocationOnDate(date);
                int totalCountPer = (blockCount.YPIBlocks + blockCount.BozemanBlocks) / pathologistsByLocation.TotalCount;
                int billingsCountPer = blockCount.YPIBlocks / pathologistsByLocation.BillingsCount;
                int bozemanCountPer = blockCount.BozemanBlocks / pathologistsByLocation.BozemanCount;
                if(billingsCountPer > totalCountPer)
                {
                    int excessBlocks = (billingsCountPer - bozemanCountPer) * pathologistsByLocation.BillingsCount;
                    int excessBlocksPer = excessBlocks / pathologistsByLocation.TotalCount;
                    int blocksToSend = excessBlocksPer * pathologistsByLocation.BozemanCount;
                    result = blocksToSend > 10 ? blocksToSend : 0;
                }
            }
            return result;
        }
    }
}
