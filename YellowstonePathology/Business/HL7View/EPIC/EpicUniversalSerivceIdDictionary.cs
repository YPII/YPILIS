using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.HL7View.EPIC
{
    public class EPICUniversalSerivceIdDictionary : Dictionary<string, string>
    {
        public EPICUniversalSerivceIdDictionary()
        {
            this.Add("SRGCL", "Surgical Pathology");
            this.Add("JAK2MUT", "JAK2 Mutation V617F");
            this.Add("CTGC", "Chlamydia trachomatis/ Neisseria gonorrhoeae Screen by PCR");
            this.Add("HERAMP", "HER2 Gene Amplification");
            this.Add("YPI", "Pathology Procedure/Specimen");
            this.Add("HRHPVTEST", "High Risk HPV Testing");            
            this.Add("BRAFMANAL", "BRAF V600E Mutation Analysis");
            this.Add("PNHHS", "PNH, Highly Sensitive");
            this.Add("HPV16", "HPV-16 Testing");
            this.Add("KRASBRAF", "KRAS with BRAF Reflex");
            this.Add("FVLMUT", "Factor V Leiden (R506Q) Mutation Analysis");
            this.Add("PROMUT", "Prothrombin 20210A Mutation Analysis (Factor II)");
            this.Add("MTHFRM", "MTHFR Mutation Analysis");
            this.Add("AUTOPSY", "Autopsy");
            this.Add("BCELLCLON", "B-Cell Clonality");
            this.Add("KRAS", "KRAS");
            this.Add("THINPREP", "ThinPrep Pap");
            this.Add("CYTO", "Cytology, Non-GYN");
            this.Add("FHGB", "Fetal Hmoglobin");
            this.Add("LLPHENO", "Leukemia/Lymphoma Phenotyping");
            this.Add("PLATEAB", "Platelet Antibodies");
            this.Add("RETICPLATE", "Reticulated Platelet");
            this.Add("THROMPRO", "Thrombocytopenia Profile (Platelet Antibody & Reticulated Platelet)");            
            this.Add("STEMCE", "Stem Cell Enumeration");            
            this.Add("EPPR", "Estrogen Receptor/Progesterone Receptor (IHC)");
            this.Add("SMP", "Peripheral Smear");
            this.Add("EGFR", "EGFR");
            this.Add("FNA", "Fine Needle Aspirate");
            this.Add("BM", "Bone Marrow");
            this.Add("CMS", "Chromosome Analysis");
            this.Add("ALK", "ALK");
            this.Add("TCELLCLO", "T-Cell Clonality");
            this.Add("BCRABL", "BCR-ABL");
            this.Add("AMLPP", "AML Prognostic Panel");
            this.Add("CLLPP", "CLL Prognositic Panel");
            this.Add("AMLFP", "AMLFP");
            this.Add("CLLFP", "CLL Fish Panel");
            this.Add("MMFP", "Multiple Myeloma Fish Panel");
            this.Add("MDSFP", "MDS Fish Panel");
            this.Add("ONCFISH", "Oncology Fish");
            this.Add("MOLEONC", "Molecular Oncology");
            this.Add("MOLEGEN", "Molecular Genetics");
            this.Add("PHARMCOG", "Pharmacogenetics");                                    
        }
    }
}
