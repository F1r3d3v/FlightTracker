﻿using System.Text.Json;
using System.Text.Json.Serialization.Metadata;

namespace ProjOb.IO
{
    internal class JSONWriter : IWriter
    {
        private StreamWriter _stream;

        public JsonSerializerOptions Options { get; set; } = new()
        {
            TypeInfoResolver = new DefaultJsonTypeInfoResolver
            {
                Modifiers =
                {
                    JSONModifiers.OnlyIDModifier,
                    JSONModifiers.OnlyDictValModifier
                }
            },
            Converters =
            {
                new JSONTimeSpanConverter()
            },
            WriteIndented = true
        };

        internal JSONWriter(String path)
        {
            _stream = new StreamWriter(path);
        }

        public void Write(Database database)
        {
            String result = JsonSerializer.Serialize(database, Options);
            _stream.Write(result);
        }

        public void Close()
        {
            _stream.Close();
        }
    }
}
