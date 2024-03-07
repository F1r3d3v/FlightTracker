using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace ProjOb.IO
{
    [AttributeUsage(AttributeTargets.Property)]
    public class JsonOnlyIDAttribute : Attribute { }

    internal class JSONWriter : IWriter
    {
        static void OnlyIDModifier(JsonTypeInfo typeInfo)
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
                        newPropertyInfo.Get = (x) => { return (propertyInfo.Get?.Invoke(x) as Object)!.ID; };
                        typeInfo.Properties.Remove(propertyInfo);
                        typeInfo.Properties.Insert(i, newPropertyInfo);
                    }
                }
            }
        }

        private StreamWriter _stream;

        public JsonSerializerOptions Options { get; set; } = new()
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers = { OnlyIDModifier }
            },
            WriteIndented = true
        };

        internal JSONWriter(String path)
        {
            _stream = new StreamWriter(path);
        }

        //TODO: Make a JSONComponent using UTF8Writer
        public void Write(Database database)
        {
            String result = JsonSerializer.Serialize(database, Options);
            //JsonNode dbNode = JsonNode.Parse(result)!;
            //JsonArray flights = dbNode["Flights"]!.AsArray();
            //foreach (var flight in flights)
            //{
            //    flight!["Origin"] = flight!["Origin"]!["ID"]!.GetValue<UInt64>();
            //    flight!["Target"] = flight!["Target"]!["ID"]!.GetValue<UInt64>();
            //    flight!["Plane"] = flight!["Plane"]!["ID"]!.GetValue<UInt64>();

            //    JsonArray crews = flight!["Crews"]!.AsArray();
            //    for (int j = 0; j < crews.Count; j++)
            //        crews[j] = crews[j]!["ID"]!.GetValue<UInt64>();

            //    JsonArray loads = flight!["Loads"]!.AsArray();
            //    for (int j = 0; j < loads.Count; j++)
            //        loads[j] = loads[j]!["ID"]!.GetValue<UInt64>();
            //}
            //_stream.Write(dbNode.ToJsonString(Options));
            _stream.Write(result);
        }

        public void Close()
        {
            _stream.Close();
        }
    }
}
