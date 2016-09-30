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
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("85060")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("85097")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88300")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88302")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88304")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88305")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88307")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88309")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88104")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88112")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88160")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88161")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88172")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88173")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88177")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88311")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88321")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88323")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88325")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88329")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88331")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88332")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88333")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88334")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88342")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("88363")));
            this.Add(new TypingCptCodeListItem(CptCodeCollection.Instance.GetCptCode("99000")));
        }
    }
}
