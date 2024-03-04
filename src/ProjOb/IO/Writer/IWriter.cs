namespace ProjOb.IO
{
    public interface IWriter
    {
        void Write(Database database);
        void Close();
    }
}