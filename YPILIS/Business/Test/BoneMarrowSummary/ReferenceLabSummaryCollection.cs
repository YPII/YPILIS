using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.Test.BoneMarrowSummary
{
    public class ReferenceLabSummaryCollection: ObservableCollection<ReferenceLabSummary>
    {
        public ReferenceLabSummaryCollection()
        {
            ReferenceLabSummary rls1 = new ReferenceLabSummary();
            rls1.ReferenceReportNo = "22-1234.R1";
            rls1.SummaryReportNo = "22-1234.Y1";
            rls1.ResultSummary = "We hold these truths to be self-evident, that all men are created equal, that they are endowed by their Creator with certain unalienable Rights, that among these are Life, Liberty and the pursuit of Happiness.--That to secure these rights, Governments are instituted among Men, deriving their just powers from the consent of the governed, --That whenever any Form of Government becomes destructive of these ends, it is the Right of the People to alter or to abolish it, and to institute new Government, laying its foundation on such principles and organizing its powers in such form, as to them shall seem most likely to effect their Safety and Happiness. Prudence, indeed, will";
            rls1.PanelSetName = "Testing 123 by FISH";
            this.Add(rls1);

            ReferenceLabSummary rls2 = new ReferenceLabSummary();
            rls2.ReferenceReportNo = "22-1234.R2";
            rls2.SummaryReportNo = "22-1234.Y1";
            rls2.ResultSummary = "We hold these truths to be self-evident, that all men are created equal, that they are endowed by their Creator with certain unalienable Rights, that among these are Life, Liberty and the pursuit of Happiness.--That to secure these rights, Governments are instituted among Men, deriving their just powers from the consent of the governed, --That whenever any Form of Government becomes destructive of these ends, it is the Right of the People to alter or to abolish it, and to institute new Government, laying its foundation on such principles and organizing its powers in such form, as to them shall seem most likely to effect their Safety and Happiness. Prudence, indeed, will";
            rls2.PanelSetName = "Testing 123 by Molecular";
            this.Add(rls2);
        }
    }
}
