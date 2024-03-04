namespace ProjOb.IO
{
    public interface IReader
    {
        String[]? Read();
        void Reset();
        void Close();
    }
}