using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace YellowstonePathology.Business.Domain.Persistence
{
    public interface IPropertyWriter
    {
        string WriteString(string propertyName);
        int WriteInt(string propertyName);
		Nullable<int> WriteNullableInt(string propertyName);
		bool WriteBoolean(string propertyName);
        DateTime WriteDateTime(string propertyName);
        Nullable<DateTime> WriteNullableDateTime(string propertyName);
        XElement WriteXElement(string propertyName);
		double WriteFloat(string propertyName);
		Nullable<double> WriteNullableFloat(string propertyName);
	}
}
