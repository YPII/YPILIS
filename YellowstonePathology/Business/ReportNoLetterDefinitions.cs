using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business
{
    public class ReportNoLetterS : ReportNoLetter
    {
        public ReportNoLetterS()
        {
            this.m_Letter = "S";
            this.m_AllowMultipleInSameAccession = false;
        }
    }

    public class ReportNoLetterP : ReportNoLetter
    {
        public ReportNoLetterP()
        {
            this.m_Letter = "P";
            this.m_AllowMultipleInSameAccession = false;
        }
    }

    public class ReportNoLetterT : ReportNoLetter
    {
        public ReportNoLetterT()
        {
            this.m_Letter = "T";
            this.m_AllowMultipleInSameAccession = true;
        }
    }

    public class ReportNoLetterY : ReportNoLetter
    {
        public ReportNoLetterY()
        {
            this.m_Letter = "Y";
            this.m_AllowMultipleInSameAccession = true;
        }
    }

    public class ReportNoLetterM : ReportNoLetter
    {
        public ReportNoLetterM()
        {
            this.m_Letter = "M";
            this.m_AllowMultipleInSameAccession = true;
        }
    }

    public class ReportNoLetterQ : ReportNoLetter
    {
        public ReportNoLetterQ()
        {
            this.m_Letter = "Q";
            this.m_AllowMultipleInSameAccession = true;
        }
    }

    public class ReportNoLetterR : ReportNoLetter
    {
        public ReportNoLetterR()
        {
            this.m_Letter = "R";
            this.m_AllowMultipleInSameAccession = true;
        }
    }

    public class ReportNoLetterA : ReportNoLetter
    {
        public ReportNoLetterA()
        {
            this.m_Letter = "A";
            this.m_AllowMultipleInSameAccession = false;
        }
    }

    public class ReportNoLetterF : ReportNoLetter
    {
        public ReportNoLetterF()
        {
            this.m_Letter = "F";
            this.m_AllowMultipleInSameAccession = true;
        }
    }

    public class ReportNoLetterI : ReportNoLetter
    {
        public ReportNoLetterI()
        {
            this.m_Letter = "I";
            this.m_AllowMultipleInSameAccession = true;
        }
    }
}
