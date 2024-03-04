using System.Text.Json;

namespace ProjOb.IO
{
    internal class JSONWriter : IWriter
    {
        private StreamWriter _stream;

        public JsonSerializerOptions Options { get; set; } = new() { WriteIndented = true };

        internal JSONWriter(String path)
        {
            _stream = new StreamWriter(path);
        }

        public void Write(object[] objArr)
        {
            String result = JsonSerializer.Serialize(objArr, Options);
            _stream.Write(result);
        }

        public void Close()
        {
            _stream.Close();
        }
    }
}
