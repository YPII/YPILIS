using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace YellowstonePathology.Business.ReportDistribution.Model
{
    public class IncompatibleDistributionTypeCollection : ObservableCollection<IncompatibleDistributionType>
    {
        public IncompatibleDistributionTypeCollection()
        {
            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPIC, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPIC, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPIC, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPICANDFAX, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPICANDFAX, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.EPICANDFAX, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.ATHENA, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.ATHENA, DistributionType.EPICANDFAX));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.ATHENA, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.ATHENA, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.ECW, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.ECW, DistributionType.EPICANDFAX));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.ECW, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.ECW, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.MEDITECH, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MEDITECH, DistributionType.EPICANDFAX));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MEDITECH, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MEDITECH, DistributionType.ATHENA));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.EPICANDFAX));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.FAX, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.EPICANDFAX));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.MAIL, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.EPICANDFAX));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.PRINT, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.EPICANDFAX));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICE, DistributionType.MEDITECH));

            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.EPIC));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.EPICANDFAX));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.ATHENA));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.ECW));
            this.Add(new Model.IncompatibleDistributionType(DistributionType.WEBSERVICEANDFAX, DistributionType.MEDITECH));
        }

        public bool TypesAreIncompatible(string primaryDistributionType, string checkingingDistributionType)
        {
            IncompatibleDistributionType incompatibleDistributionType = this.FirstOrDefault(i => i.PrimaryDistributionType == primaryDistributionType && i.SecondaryDistributionType == checkingingDistributionType);
            return incompatibleDistributionType != null ? true : false;
        }
    }
}
