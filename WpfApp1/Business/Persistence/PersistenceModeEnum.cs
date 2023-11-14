using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Persistence
{
    public enum PersistenceModeEnum
    {
        DoNotPersist,
        UpdateChangedProperties,
        DeleteObject,
        DeleteSubClassObject,
        AddNewObject,
        UpdateBaseAndAddNewSubclass
    }
}
