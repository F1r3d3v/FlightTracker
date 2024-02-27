namespace ProjOb.IO
{
    public interface IReader
    {
        String[]? Read();
        void Close();
    }
}