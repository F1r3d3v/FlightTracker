﻿using System.Collections;
using System.Text.Json.Serialization.Metadata;

namespace ProjOb.IO
{
    internal static class JSONModifiers
    {
        public static void OnlyIDModifier(JsonTypeInfo typeInfo)
        {
            for (int i = 0; i < typeInfo.Properties.Count; i++)
            {
                JsonPropertyInfo propertyInfo = typeInfo.Properties[i];
                object[] onlyIDAttributes = propertyInfo.AttributeProvider?.GetCustomAttributes(typeof(JsonOnlyIDAttribute), true) ?? Array.Empty<object>();
                JsonOnlyIDAttribute? attribute = onlyIDAttributes.Length == 1 ? (JsonOnlyIDAttribute)onlyIDAttributes[0] : null;

                if (attribute != null)
                {
                    if (propertyInfo.PropertyType.GetInterface(nameof(IEnumerable<Object>)) != null)
                    {
                        JsonPropertyInfo newPropertyInfo = typeInfo.CreateJsonPropertyInfo(typeof(UInt64[]), propertyInfo.Name);
                        newPropertyInfo.Get = (x) =>
                        {
                            List<UInt64> res = [];
                            var arr = (IEnumerable<object>?)(propertyInfo.Get?.Invoke(x));
                            if (arr is null) return null;
                            foreach (var it in arr)
                                res.Add(((Object)it).ID);
                            return res.ToArray();
                        };
                        typeInfo.Properties.Remove(propertyInfo);
                        typeInfo.Properties.Insert(i, newPropertyInfo);
                    }
                    else if (propertyInfo.PropertyType.BaseType == typeof(Object))
                    {
                        JsonPropertyInfo newPropertyInfo = typeInfo.CreateJsonPropertyInfo(typeof(UInt64), propertyInfo.Name);
                        newPropertyInfo.Get = (x) => { return ((Object)propertyInfo.Get?.Invoke(x)!).ID; };
                        typeInfo.Properties.Remove(propertyInfo);
                        typeInfo.Properties.Insert(i, newPropertyInfo);
                    }
                }
            }
        }

        public static void OnlyDictValModifier(JsonTypeInfo typeInfo)
        {
            for (int i = 0; i < typeInfo.Properties.Count; i++)
            {
                JsonPropertyInfo propertyInfo = typeInfo.Properties[i];
                object[] onlyIDAttributes = propertyInfo.AttributeProvider?.GetCustomAttributes(typeof(JsonOnlyDictValAttribute), true) ?? Array.Empty<object>();
                JsonOnlyDictValAttribute? attribute = onlyIDAttributes.Length == 1 ? (JsonOnlyDictValAttribute)onlyIDAttributes[0] : null;

                if (attribute != null)
                {
                    if (propertyInfo.PropertyType.GetInterface(nameof(IDictionary)) != null)
                    {
                        var valueType = propertyInfo.PropertyType.GetGenericArguments()[1];

                        JsonPropertyInfo newPropertyInfo = typeInfo.CreateJsonPropertyInfo(valueType.MakeArrayType(), propertyInfo.Name);
                        newPropertyInfo.Get = (x) =>
                        {
                            var dict = (IDictionary?)(propertyInfo.Get?.Invoke(x));
                            if (dict is null) return null;

                            Array res = Array.CreateInstance(valueType, dict.Values.Count);
                            int j = 0;
                            foreach (var val in dict.Values)
                                res.SetValue(val, j++);

                            return res;
                        };
                        typeInfo.Properties.Remove(propertyInfo);
                        typeInfo.Properties.Insert(i, newPropertyInfo);
                    }
                }
            }
        }
    }
}
