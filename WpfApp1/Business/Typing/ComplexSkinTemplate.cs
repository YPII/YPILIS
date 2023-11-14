using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YellowstonePathology.Business.Typing
{
    public class ComplexSkinTemplate : ParagraphTemplate
    {
        public ComplexSkinTemplate()
        {
            this.Description = "Complex Skin Template";
            this.Text = "The skin is *COLOR* in color and is *WIDTH* wide and *HEIGHT* in height. It is also *SHAPE* in shape.";
            //this.WordCollection.Add("COLOR");
            //this.WordCollection.Add("WIDTH");
            //this.WordCollection.Add("HEIGHT");
            //this.WordCollection.Add("SHAPE");
        }
    }
}
