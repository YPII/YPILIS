using System;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Billing.Model
{
    public class FISHCPTCodeList : ObservableCollection<TypingCptCodeListItem>
    {
        public FISHCPTCodeList(int probeSetCount)
        {
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88374", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88377", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88367", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88373", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88368", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88369", null)));

            foreach (TypingCptCodeListItem item in this) item.Quantity = probeSetCount;
        }
    }
}
