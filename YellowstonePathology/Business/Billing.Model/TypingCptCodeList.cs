using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace YellowstonePathology.Business.Billing.Model
{
	public class TypingCptCodeList : ObservableCollection<TypingCptCodeListItem>
	{
		public TypingCptCodeList()
		{
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("85060", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("85097", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88300", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88302", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88304", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88304", "26")));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", null)));   
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88305", "26")));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88307", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88309", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88104", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88108", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88112", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88160", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88161", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88172", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88173", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88177", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88311", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88321", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88323", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88325", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88329", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88331", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88332", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88333", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88334", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88342", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("88363", null)));
            this.Add(new TypingCptCodeListItem(Store.AppDataStore.Instance.CPTCodeCollection.GetClone("99000", null)));
        }
    }
}
