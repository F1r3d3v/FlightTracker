namespace ProjOb.IO
{
    public interface IWriter
    {
        void Write(Object[] objArr);
        void Close();
    }
}