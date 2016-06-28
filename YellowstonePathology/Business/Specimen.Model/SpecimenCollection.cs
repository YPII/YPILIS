using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using StackExchange.Redis;

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

        private static SpecimenCollection Sort(SpecimenCollection specimenCollection)
        {
            SpecimenCollection result = new SpecimenCollection();
            IOrderedEnumerable<Specimen> orderedResult = specimenCollection.OrderBy(i => i.SpecimenName);
            foreach (Specimen specimen in orderedResult)
            {
                result.Add(specimen);
            }
            return result;
        }

        public static SpecimenCollection GetAll()
        {
            SpecimenCollection result = new SpecimenCollection();
            result.Add(new SpecimenDefinition.NullSpecimen());
            result.Add(new SpecimenDefinition.AdenoidExcision());
            result.Add(new SpecimenDefinition.AorticValve());
            result.Add(new SpecimenDefinition.AppendixExcision());
            result.Add(new SpecimenDefinition.BreastReduction());
            result.Add(new SpecimenDefinition.CervicalBiopsy());
            result.Add(new SpecimenDefinition.ECC());
            result.Add(new SpecimenDefinition.EMB());
            result.Add(new SpecimenDefinition.FemoralHead());
            result.Add(new SpecimenDefinition.FallopianTube());
            result.Add(new SpecimenDefinition.GallbladderExcision());
            result.Add(new SpecimenDefinition.Biopsy());
            result.Add(new SpecimenDefinition.GIBiopsy());
            result.Add(new SpecimenDefinition.KneeTissue());
            result.Add(new SpecimenDefinition.LEEP());
            result.Add(new SpecimenDefinition.CervicalCone());            
            result.Add(new SpecimenDefinition.LEEPPieces());
            result.Add(new SpecimenDefinition.MitralValve());
            result.Add(new SpecimenDefinition.NeedleCoreBiopsy());
            result.Add(new SpecimenDefinition.POC());
            result.Add(new SpecimenDefinition.ProstateExceptRadicalResection());            
            result.Add(new SpecimenDefinition.ProstateNeedleBiopsy());
            result.Add(new SpecimenDefinition.ProstateRadicalResection());
            result.Add(new SpecimenDefinition.ProstateTUR());
            result.Add(new SpecimenDefinition.SinusContent());
            result.Add(new SpecimenDefinition.SinglePlacenta());
            result.Add(new SpecimenDefinition.Twin1Placenta());
            result.Add(new SpecimenDefinition.Twin2Placenta());
            result.Add(new SpecimenDefinition.SkinExcisionOrientedBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionUnorientedBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionOrientedwithCurettingsBiopsy());
            result.Add(new SpecimenDefinition.SkinExcisionUnorientedwithCurettingsBiopsy());            
            result.Add(new SpecimenDefinition.SkinShavePunchMiscBiopsy());
            result.Add(new SpecimenDefinition.SkinShavewithCurettingsBiopsy());
            result.Add(new SpecimenDefinition.ThinPrepFluid());
            result.Add(new SpecimenDefinition.TonsilAdenoidExcision());
            result.Add(new SpecimenDefinition.TonsilExcision());
            result.Add(new SpecimenDefinition.Uterus());            
            result.Add(new SpecimenDefinition.UterusAdnexa());
            result.Add(new SpecimenDefinition.GenericSpecimenGrossOnly());
            result.Add(new SpecimenDefinition.GenericSpecimenGrossRequiredWithBlocks());
            result.Add(new SpecimenDefinition.AutopsySpecimen());
            result.Add(new SpecimenDefinition.FluidWithCellBlock());
            result.Add(new SpecimenDefinition.Urine());
            return Sort(result);
        }

        public string ToJSON()
        {
            string result = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

            return result;
        }

        public void WriteToRedis()
        {
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            db.KeyDelete("specimens");

            foreach (Specimen specimen in this)
            {
                db.KeyDelete("specimen:" + specimen.SpecimenId);

                string result = JsonConvert.SerializeObject(specimen, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                });

                db.ListRightPush("specimens", "specimen:" + specimen.SpecimenId);
                db.StringSet("specimen:" + specimen.SpecimenId, result);
            }
        }

        public static SpecimenCollection BuildFromRedis()
        {
            SpecimenCollection result = new SpecimenCollection();
            IDatabase db = Business.RedisConnection.Instance.GetDatabase();
            RedisValue[] items = db.ListRange("specimens", 0, -1);

            for (int i = 0; i < items.Length; i++)
            {
                RedisValue json = db.StringGet(items[i].ToString());
                Business.Specimen.Model.Specimen specimen = JsonConvert.DeserializeObject<Business.Specimen.Model.Specimen>(json, new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All,
                    ObjectCreationHandling = ObjectCreationHandling.Replace
                });

                result.Add(specimen);
            }

            return result;
        }
    }
}
