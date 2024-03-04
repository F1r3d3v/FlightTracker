namespace ProjOb.IO
{
    public interface IWriter
    {
        void Write(object[] objArr);
        void Close();
    }
}