using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace YellowstonePathology.MySQLMigration
{
    public class NonpersistentTableDefCollection : ObservableCollection<NonpersistentTableDef>
    {
        public NonpersistentTableDefCollection()
        { }

        public static NonpersistentTableDefCollection GetAll()
        {
            NonpersistentTableDefCollection result = new NonpersistentTableDefCollection();
            result.Add(new NonpersistentTableDefADT());
            result.Add(new NonpersistentTableDefCytologyDiagnosis());
            result.Add(new NonpersistentTableDefCytologyOtherConditions());
            result.Add(new NonpersistentTableDefCytologyReportComment());
            result.Add(new NonpersistentTableDefCytologySAComments());
            result.Add(new NonpersistentTableDefCytologyScreeningImpression());
            result.Add(new NonpersistentTableDefFlowCommentV2());
            result.Add(new NonpersistentTableDefFlowMarkerPanel());
            result.Add(new NonpersistentTableDefFlowPanel());
            result.Add(new NonpersistentTableDefICD9Code());
            result.Add(new NonpersistentTableDefImmunoComment());
            result.Add(new NonpersistentTableDefMarkers());
            result.Add(new NonpersistentTableDefMasterAccessionNo());
            result.Add(new NonpersistentTableDefMaterialLocation());
            result.Add(new NonpersistentTableDefOrderComment());
            result.Add(new NonpersistentTableDefPanel());
            result.Add(new NonpersistentTableDefPanelSet());
            result.Add(new NonpersistentTableDefPatient());
            result.Add(new NonpersistentTableDefPsaImport());
            result.Add(new NonpersistentTableDefSpecimenAdequacy());
            result.Add(new NonpersistentTableDefStainResultOption());
            result.Add(new NonpersistentTableDefStainResultOptionGroup());
            result.Add(new NonpersistentTableDefStVincent());
            result.Add(new NonpersistentTableDefTest());
            result.Add(new NonpersistentTableDefWebServiceAccount());
            result.Add(new NonpersistentTableDefWebServiceAccountClient());

            return result;
        }
    }
}
