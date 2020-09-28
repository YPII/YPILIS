using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace YellowstonePathology.Business.Test.Model
{
    public class TestCollectionInstance
    {
        private static TestCollection instance;
        private static object syncRoot = new Object();

        private TestCollectionInstance()
        {

        }

        private static TestCollection Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null) instance = Load();
                    }
                }
                return instance;
            }
        }

        public static Test GetClone(string testId)
        {
            Test result = null;
            Test test = TestCollectionInstance.Instance.FirstOrDefault(t => t.TestId == testId);
            if (test != null) result = TestCollectionInstance.GetTestClone(test);
            return result;
        }

        public static bool Exists(string testId)
        {
            Test test = TestCollectionInstance.Instance.FirstOrDefault(t => t.TestId == testId);
            return test == null ? false : true;
        }

        public static ObservableCollection<object> GetTestsStartingWithToObjectCollection(string firstLetter, bool includeWetProtocols)
        {
            ObservableCollection<object> result = new ObservableCollection<object>();
            List<Test> tests = new List<Test>();
            Test wetIron = TestCollectionInstance.GetWetIron();
            if (wetIron.TestName.ToUpper().Substring(0, 1) == firstLetter.ToUpper()) tests.Add(wetIron);
            foreach (Test test in TestCollectionInstance.Instance)
            {
                if (test.TestName.ToUpper().Substring(0, 1) == firstLetter.ToUpper())
                {
                    if(test.TestId != "177") // CD99
                    tests.Add(TestCollectionInstance.GetClone(test.TestId));
                }
            }

            tests.Sort(Test.CompareByTestName);
            foreach (Test test in tests)
            {
                if(test.Active == true)
                    result.Add(test);
            }
            return result;
        }

        private static TestCollection Load()
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
            result.Add(new Giemsa());
            result.Add(new AlphaNaphthylAcetateEsterase());
            result.Add(new NaphtholASDChloroacetateEsterase());
            result.Add(new Alk());

            //IHC
            result.Add(new AmyloidAProtein());
            result.Add(new AmyloidPProtein());
            result.Add(new CD138Quantitative());
            result.Add(new CD25());
            result.Add(new CD52());
            result.Add(new CD79a());
            result.Add(new Cytokeratin17());
            result.Add(new Cytokeratin818());
            result.Add(new EGFR());
            result.Add(new FactorVIII());
            result.Add(new GCDFP15());
            result.Add(new Lysozyme());
            result.Add(new MuscleSpecificActin());
            result.Add(new NSE());
            result.Add(new TRAP());
            result.Add(new Tyrosinase());
            result.Add(new Villin());
            result.Add(new BartonellaHenselae());
            result.Add(new Uroplakin());
            result.Add(new Surfactant());
            result.Add(new PAX2());
            result.Add(new CD235a());
            result.Add(new MITF());
            result.Add(new CD38());
            result.Add(new PanMelanomaCocktail());
            result.Add(new BerEP4());
            result.Add(new NotListed1());
            result.Add(new NotListed2());
            result.Add(new NotListed3());

            //NG CT
            result.Add(new NeisseriaGonorrhoeae());
            result.Add(new ChlamydiaTrachomatis());
            result.Add(new ERV3());
            result.Add(new ERV3NG());
            result.Add(new ERV3CT());

            result.Add(new ParoxysmalNocturnalHemoglobinuria());
            result.Add(new UnstainedSlide());


            foreach (Stain.Model.Stain stain in Stain.Model.StainCollection.Instance)
            {
                Test test = TestFactory.TestFromStain(stain);
                result.Add(test);

                if(test.HistologyDisplayString == "PSA")
                {
                    Console.WriteLine(test.HistologyDisplayString);
                }
            }

            return result;
        }

        public static TestCollection GetIHCTests()
        {
            TestCollection result = new TestCollection();
            foreach (Test test in TestCollectionInstance.Instance)
            {
                if (test is ImmunoHistochemistryTest == true)
                {
                    result.Add(TestCollectionInstance.GetTestClone(test));
                }
            }
            return result;
        }

        public static TestCollection GetGradedTests()
        {
            TestCollection result = new TestCollection();
            foreach (Test test in TestCollectionInstance.Instance)
            {
                if (test is GradedTest == true)
                {
                    result.Add(TestCollectionInstance.GetTestClone(test));
                }
            }
            return result;
        }

        public static TestCollection GetCytochemicalTests()
        {
            TestCollection result = new TestCollection();
            foreach (Test test in TestCollectionInstance.Instance)
            {
                if (test is CytochemicalTest == true)
                {
                    result.Add(TestCollectionInstance.GetTestClone(test));
                }
            }
            return result;
        }

        public static TestCollection GetCytochemicalForMicroorganismsTests()
        {
            TestCollection result = new TestCollection();
            foreach (Test test in TestCollectionInstance.Instance)
            {
                if (test is CytochemicalForMicroorganisms)
                {
                    result.Add(TestCollectionInstance.GetTestClone(test));
                }
            }
            return result;
        }

        public static Test GetWetIron()
        {
            Test wetIron = TestCollectionInstance.GetClone("115");
            wetIron.UseWetProtocol = true;
            return wetIron;
        }

        private static Test GetTestClone(Test test)
        {
            YellowstonePathology.Business.Persistence.ObjectCloner objectCloner = new Persistence.ObjectCloner();
            Test result = (Test)objectCloner.Clone(test);
            return result;
        }

        public static void Reload()
        {
            TestCollectionInstance.instance = TestCollectionInstance.Load();
        }
    }
}
