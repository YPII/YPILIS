using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test
{
    public class AccessionOrderTracker
    {
        public AccessionOrderTracker(YellowstonePathology.Business.Test.AccessionOrder accessionOrder)
        {

        }

        private void Setupthetracking()
        {

        }

        public void Save(bool releaseLock)
        {
            //code to submit changes to the gateway goes hear.
            //When saving a dna should call toxml to put xml back in the extension document. this will allow us to make the
            //dna pso and po just like the others.
        }
    }
}
