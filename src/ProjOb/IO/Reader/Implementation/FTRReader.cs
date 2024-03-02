namespace ProjOb.IO
{
    internal class FTRReader : IReader
    {
        private StreamReader _stream;

        internal FTRReader(String path)
        {
            _stream = new StreamReader(path);
        }

        public String[]? Read()
        {
            if (!_stream.EndOfStream)
                return _stream.ReadLine()!.Split(',', StringSplitOptions.TrimEntries);
            else 
                return null;
        }

        public void Close()
        {
            _stream.Close();
        }
    }
}