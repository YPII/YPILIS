using System;
using System.Reflection;

namespace YellowstonePathology.Business.Persistence
{
    public class PropertyBridgeFactory
    {        
        public static PropertyBridge GetPropertyBridge(PropertyInfo property, object obj)
        {
            PropertyBridge propertyBridge = null;
            if (property.PropertyType.Name == typeof(string).Name)
            {
                propertyBridge = new StringPropertyBridge(property, obj);
            }
            else if (property.PropertyType.Name == typeof(bool).Name)
            {
                propertyBridge = new BooleanPropertyBridge(property, obj);
            }
            else if (property.PropertyType.Name == typeof(int).Name)
            {                
                propertyBridge = new IntPropertyBridge(property, obj);
            }
            else if (property.PropertyType.Name == typeof(double).Name)
            {
                propertyBridge = new DoublePropertyBridge(property, obj);
            }
            else if (property.PropertyType.Name == typeof(DateTime).Name)
            {
                propertyBridge = new DateTimePropertyBridge(property, obj);
            }
            else if (property.PropertyType.IsGenericType)
            {
                Type genericTypeDef = property.PropertyType.UnderlyingSystemType;
                if (genericTypeDef == typeof(Nullable<DateTime>))
                {
                    propertyBridge = new DateTimePropertyBridge(property, obj);
                }
                else if (genericTypeDef == typeof(Nullable<int>))
                {                    
                    propertyBridge = new IntPropertyBridge(property, obj);
                }
                else if (genericTypeDef == typeof(Nullable<double>))
                {
                    propertyBridge = new DoublePropertyBridge(property, obj);
                }
                else if (genericTypeDef == typeof(Nullable<bool>))
                {
                    propertyBridge = new IntPropertyBridge(property, obj);
                }
            }
            else
            {
                throw new Exception("This data type not implemented in the Property Bridge Factory");
            }
            return propertyBridge;
        }
    }
}
