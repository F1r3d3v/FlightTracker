using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ProjOb.IO
{
    public class JSONTimeSpanConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options) =>
                TimeSpan.ParseExact(reader.GetString()!,
                    @"hh\:mm", CultureInfo.InvariantCulture);

        public override void Write(
            Utf8JsonWriter writer,
            TimeSpan timeSpanValue,
            JsonSerializerOptions options) =>
                writer.WriteStringValue(timeSpanValue.ToString(
                    @"hh\:mm", CultureInfo.InvariantCulture));
    }
}
