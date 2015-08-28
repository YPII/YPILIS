using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class SpecimenCollection : ObservableCollection<Specimen>
    {
        public SpecimenCollection()
        {

        }

        public bool Exists(string specimenId)
        {
            bool result = false;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

        public Specimen GetSpecimen(string specimenId)
        {
            Specimen result = null;
            foreach (Specimen specimen in this)
            {
                if (specimen.SpecimenId == specimenId)
                {
                    result = specimen;
                    break;
                }
            }
            return result;
        }

        public static SpecimenCollection GetAll()
        {
            SpecimenCollection result = new SpecimenCollection();
            result.Add(new SpecimenDefinition.NullSpecimen());
            result.Add(new SpecimenDefinition.GIBiopsy());
            result.Add(new SpecimenDefinition.ThinPrepFluid());
            result.Add(new SpecimenDefinition.ProstateRadicalResection());
            result.Add(new SpecimenDefinition.ProstateExceptRadicalResection());
            result.Add(new SpecimenDefinition.ProstateNeedleBiopsy());
            result.Add(new SpecimenDefinition.ProstateTUR());
            result.Add(new SpecimenDefinition.SkinExcisionBiopsy());
            result.Add(new SpecimenDefinition.SkinShavePunchMiscBiopsy());
            result.Add(new SpecimenDefinition.AppendixExcision());
            result.Add(new SpecimenDefinition.GallbladderExcision());
            result.Add(new SpecimenDefinition.TonsilExcision());
            result.Add(new SpecimenDefinition.POC());
            result.Add(new SpecimenDefinition.BreastReduction());
            result.Add(new SpecimenDefinition.ECC());
            result.Add(new SpecimenDefinition.EMB());
            result.Add(new SpecimenDefinition.CervicalBiopsy());
            result.Add(new SpecimenDefinition.LEEPExcision());
            result.Add(new SpecimenDefinition.Placenta());
            result.Add(new SpecimenDefinition.Uterus());
            result.Add(new SpecimenDefinition.NeedleCoreBiopsy());
            return result;
        }
    }
}
