using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace YellowstonePathology.Business.Mongo
{
	public class ValueHelper
	{
		public static string GetStringValue(BsonValue bsonValue)
		{
			string result = null;
			if (bsonValue.IsBsonNull == false) result = bsonValue.AsString;
			return result;
		}
	}
}
