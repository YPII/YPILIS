using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public interface IPropertyReader
    {
        void ReadString(string propertyName, string value);
        void ReadInt(string propertyName, int value);
		void ReadNullableInt(string propertyName, Nullable<int> value);
		void ReadBoolean(string propertyName, bool value);
		void ReadNullableBoolean(string propertyName, bool? value);
		void ReadDateTime(string propertyName, DateTime value);
        void ReadNullableDateTime(string propertyName, DateTime? value);
        void ReadXElement(string propertyName, XElement value);
		void ReadDouble(string propertyName, double value);
	}
}
