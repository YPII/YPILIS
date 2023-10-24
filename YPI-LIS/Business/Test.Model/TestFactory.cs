using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YellowstonePathology.Business.Test.Model
{
    public class TestFactory
    {

        public static Test TestFromStain(Stain.Model.Stain stain)
        {
            Test result = null;
            switch (stain.StainType)
            {
                case "IHC":
                    {
                        result = new ImmunoHistochemistryTest(stain);
                        break;
                    }
                case "CytochemicalStain":
                    {
                        result = new CytochemicalTest(stain);
                        break;
                    }
                case "CytochemicalForMicroorganisms":
                    {
                        result = new CytochemicalForMicroorganisms(stain);
                        break;
                    }
                case "GradedStain":
                    {
                        result = new GradedTest(stain);
                        break;
                    }
                case "DualStain":
                    {
                        result = new DualStain(stain);
                        break;
                    }
                case "Crystallography":
                    {
                        result = new CrystalographyTest(stain);
                        break;
                    }
            }
            return result;

        }
    }
}
