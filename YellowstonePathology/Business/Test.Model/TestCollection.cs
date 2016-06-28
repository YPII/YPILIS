using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Test.Model
{
	public class TestCollection : ObservableCollection<Test>
    {
        public TestCollection()
        {

        }

        public bool HasTestRequiringAcknowledgement()
        {
            bool result = false;
            foreach (YellowstonePathology.Business.Test.Model.Test test in this)
            {
                if (test.NeedsAcknowledgement == true)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public Test GetTest(int testId)
        {
			Test result = null;
			foreach (Test test in this)
            {
                if (test.TestId == testId)
                {
                    result = test;
                    break;
                }
            }
            return result;
        }

        public bool Exists(int testId)
        {
            bool result = false;
            foreach (Test test in this)
            {
                if (test.TestId == testId)
                {
                    result = true;
                    break;
                }
            }
            return result;
        }

		public TestCollection GetTestsStartingWith(string firstLetter)
		{
			TestCollection result = new TestCollection();
			List<Test> tests = new List<Test>();
			TestCollection allTests = TestCollection.GetAllTests();
			foreach (Test test in allTests)
			{
				if (test.TestName.Substring(0, 1) == firstLetter) tests.Add(test);
			}

			tests.Sort(Test.CompareByTestName);
			foreach (Test test in tests)
			{
				result.Add(test);
			}
			return result;
		}

        public ObservableCollection<object> GetTestsStartingWithToObjectCollection(string firstLetter)
        {
            ObservableCollection<object> result = new ObservableCollection<object>();
            List<Test> tests = new List<Test>();
            TestCollection allTests = TestCollection.GetAllTests();
            foreach (Test test in allTests)
            {
                if (test.TestName.Substring(0, 1) == firstLetter) tests.Add(test);
            }

            tests.Sort(Test.CompareByTestName);
            foreach (Test test in tests)
            {
                result.Add(test);
            }
            return result;
        }

        public static TestCollection GetAllTests()
        {
            TestCollection result = new TestCollection();
            
            //No CptCode Tests
            result.Add(new HandE());
            result.Add(new TouchPrep());
            result.Add(new Recut());
            result.Add(new NonGynCytologyStain());
            result.Add(new UnstainedRecut());
            result.Add(new BRAF());
            result.Add(new DiffQuik());
            result.Add(new CellBlock());
            result.Add(new GrossOnly());
            result.Add(new KRASBRAFReflex());
            result.Add(new Negative());
            result.Add(new HandEAfterSlide());
            result.Add(new WrightsStain());
            result.Add(new FineNeedleAspirate());
            result.Add(new IntraoperativeConsultationwithFrozen());
            result.Add(new NonGynCytology());
            result.Add(new TissueSmear());
            result.Add(new KRASMutationAnalysis());
            result.Add(new BCellClonality());
            result.Add(new IntraoperativeConsultation());
            result.Add(new HER2Amplification());

			//Cytochemical For Microorganisms
            result.Add(new AcidFast());
            result.Add(new Giemsa());
            result.Add(new GMS());
            result.Add(new Gout());
            result.Add(new HuckerTwordt());
            result.Add(new PASforFungus());
            result.Add(new SteinerandSteiner());

            //Cytochemical
            result.Add(new AlcianBlue());
            result.Add(new CongoRed());
            result.Add(new Elastic());
            result.Add(new Iron());
            result.Add(new Melanin());
            result.Add(new Mucin());
            result.Add(new OilRedO());
            result.Add(new PAS());
            result.Add(new PASAlcianBlue());
            result.Add(new PASWithDiastase());
            result.Add(new Reticulin());
            result.Add(new AlphaNaphthylAcetateEsterase());
            result.Add(new NaphtholASDChloroacetateEsterase());
            result.Add(new CopperRhodanine());
            result.Add(new Fites());
            result.Add(new Alk());
            result.Add(new Trichrome());

            //IHC
            result.Add(new ALK1());
            result.Add(new AmyloidAProtein());
            result.Add(new AmyloidPProtein());            
            result.Add(new Bcl2());
            result.Add(new Bcl6());
            result.Add(new CA125());
            result.Add(new CA199());
            result.Add(new Calretinin());
            result.Add(new CD10());
            result.Add(new CD117());
            result.Add(new CD138());
            result.Add(new CD138Quantitative());
            result.Add(new CD15());
			result.Add(new CD20());
            result.Add(new CD23());
            result.Add(new CD25());
			result.Add(new CD3());
            result.Add(new CD30());
            result.Add(new CD34());
            result.Add(new CD4());            
            result.Add(new CD45());
            result.Add(new CD5());
            result.Add(new CD52());
            result.Add(new CD56());
            result.Add(new CD68());
            result.Add(new CD79a());
            result.Add(new CD8());
			result.Add(new CDX2());
			result.Add(new CEA());
            result.Add(new Chromogranin());
            result.Add(new CyclinD1());
            result.Add(new Cytokeratin17());
            result.Add(new Cytokeratin20());
			result.Add(new Cytokeratin34());

			result.Add(new Cytokeratin56());
            result.Add(new Cytokeratin7());
            result.Add(new Cytokeratin818());
			result.Add(new CytomegaloVirus());
            result.Add(new Desmin());
            result.Add(new Ecadherin());
            result.Add(new EGFR());
            result.Add(new EMA());
            result.Add(new EstrogenReceptor());
            result.Add(new FactorVIII());
            result.Add(new FactorXIIIa());
            result.Add(new Fascin());
            result.Add(new GCDFP15());
            result.Add(new GFAP());
            result.Add(new HelicobacterPylori());
            result.Add(new HER2Neu());
            result.Add(new HMB45());

			result.Add(new IgKappa());
			result.Add(new IgLambda());
            result.Add(new Ki67());            

            result.Add(new Lysozyme());
            result.Add(new Mammaglobin());

            result.Add(new MelanA());

            result.Add(new MLH1());
            result.Add(new MSH2());
            result.Add(new MUM1());
            result.Add(new MuscleSpecificActin());
            result.Add(new Myeloperoxidase());
			result.Add(new NKX31());
            result.Add(new NSE());
            result.Add(new P16());

			result.Add(new P504sRacemase());

			result.Add(new P53());
            result.Add(new P63());
            result.Add(new Pancytokeratin());

			result.Add(new PAX5());

			result.Add(new PlacentalAlkalinePhosphatase());
            result.Add(new ProgesteroneReceptor());
            result.Add(new ProstateSpecificAntigen());
            result.Add(new ProstaticAcidPhosphatase());
            result.Add(new RCC());
            result.Add(new S100());
            result.Add(new SmoothMuscleActin());

			result.Add(new SmoothMuscleMyosin());

			result.Add(new Synaptophysin());
            result.Add(new TdT());
            result.Add(new Thyroglobulin());
            result.Add(new TRAP());

			result.Add(new TTF1());

			result.Add(new Tyrosinase());

			result.Add(new Villin());

			result.Add(new Vimentin());

			result.Add(new Zap70());

			result.Add(new BartonellaHenselae());
            result.Add(new Uroplakin());

			result.Add(new OSCAR());

			result.Add(new Surfactant());
            result.Add(new CD19());
            result.Add(new WT1());
            result.Add(new PAX2());
            result.Add(new CD235a());
            result.Add(new CD31());
            result.Add(new CD99());
            result.Add(new MITF());
            result.Add(new CD38());
            result.Add(new CD7());
            result.Add(new PanMelanomaCocktail());
            result.Add(new HepatocyteSpecificAntigen());
            result.Add(new MOC31());
            result.Add(new BerEP4());
            result.Add(new PMS2());
            result.Add(new MSH6());
            result.Add(new D240());            

            result.Add(new PAX8());
			result.Add(new NapsinA());
			result.Add(new Glypican3());
            result.Add(new DOG1());
            result.Add(new BetaCatenin());
            result.Add(new GATA3());
            result.Add(new NotListed1());
            result.Add(new NotListed2());
            result.Add(new NotListed3());

			result.Add(new Ki67SemiQuantitative());
            result.Add(new EstrogenReceptorSemiquant());
            result.Add(new ProgesteroneReceptorSemiquant());

            result.Add(new SOX10());
            result.Add(new SATB2());
            
            result.Add(new P40());

            //NG CT
            result.Add(new NeisseriaGonorrhoeae());
            result.Add(new ChlamydiaTrachomatis());
            result.Add(new ERV3());
            result.Add(new ERV3NG());
            result.Add(new ERV3CT());

            result.Add(new ParoxysmalNocturnalHemoglobinuria());
            result.Add(new Tryptase());
            
            return result;
        }

        public static TestCollection GetIHCTests()
        {
            TestCollection result = new TestCollection();
            TestCollection allTests = TestCollection.GetAllTests();
            foreach (Test test in allTests)
            {
                if (test is ImmunoHistochemistryTest == true)
                {
                    result.Add(test);
                }
            }
            return result;
        }

        public static TestCollection GetGradedTests()
        {
            TestCollection result = new TestCollection();
            TestCollection allTests = TestCollection.GetAllTests();
            foreach (Test test in allTests)
            {
                if (test is GradedTest == true)
                {
                    result.Add(test);
                }
            }
            return result;
        }

        public static TestCollection GetCytochemicalTests()
        {
            TestCollection result = new TestCollection();
            TestCollection allTests = TestCollection.GetAllTests();
            foreach (Test test in allTests)
            {
                if (test is CytochemicalTest == true)
                {
                    result.Add(test);
                }
            }
            return result;
        }

        public static TestCollection GetCytochemicalForMicroorganismsTests()
        {
            TestCollection result = new TestCollection();
            TestCollection allTests = TestCollection.GetAllTests();
            foreach (Test test in allTests)
            {
                if (test is CytochemicalForMicroorganisms)
                {
                    result.Add(test);
                }
            }
            return result;
        }
    }
}
