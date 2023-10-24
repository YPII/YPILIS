using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Specimen.Model
{
    public class AliquotCollection : ObservableCollection<Aliquot>
    {
        public AliquotCollection()
        {

        }

        public bool ExistsByAliquotType(string aliquotType)
        {
            bool result = false;
            foreach (Aliquot aliquot in this)
            {
                if (aliquot.AliquotType == aliquotType)
                {
                    result = true;
                }
            }
            return result;
        }

        public Aliquot GetByAliquotType(string aliquotType)
        {
            Aliquot result = null;
            foreach (Aliquot aliquot in this)
            {
                if (aliquot.AliquotType == aliquotType)
                {
                    result = aliquot;
                }
            }
            return result;
        }

        public static AliquotCollection GetAll()
        {
            AliquotCollection result = new AliquotCollection();
            return result;
        }
    }
}
