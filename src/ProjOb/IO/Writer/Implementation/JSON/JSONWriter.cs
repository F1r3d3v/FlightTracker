using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ProjOb.IO
{
    internal class JSONWriter : IWriter
    {
        private StreamWriter _stream;

        public JsonSerializerOptions Options { get; set; } = new()
        {
            WriteIndented = true
        };

        internal JSONWriter(String path)
        {
            _stream = new StreamWriter(path);
        }

        public void Write(Database database)
        {
            String result = JsonSerializer.Serialize(database);
            JsonNode dbNode = JsonNode.Parse(result)!;
            JsonArray flights = dbNode["Flights"]!.AsArray();
            foreach(var flight in flights)
            {
                flight!["Origin"] = flight!["Origin"]!["ID"]!.GetValue<UInt64>();
                flight!["Target"] = flight!["Target"]!["ID"]!.GetValue<UInt64>();
                flight!["Plane"] = flight!["Plane"]!["ID"]!.GetValue<UInt64>();

                JsonArray crews = flight!["Crews"]!.AsArray();
                for (int j = 0; j < crews.Count; j++)
                    crews[j] = crews[j]!["ID"]!.GetValue<UInt64>();

                JsonArray loads = flight!["Loads"]!.AsArray();
                for (int j = 0; j < loads.Count; j++)
                    loads[j] = loads[j]!["ID"]!.GetValue<UInt64>();
            }
            _stream.Write(dbNode.ToJsonString(Options));
        }

        public void Close()
        {
            _stream.Close();
        }
    }
}
