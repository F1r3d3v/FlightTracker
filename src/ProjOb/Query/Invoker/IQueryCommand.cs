namespace ProjOb.Query.Invoker
{
    public interface IQueryCommand
    {
        void Execute();
    }

    public interface IQueryCommand<T> : IQueryCommand
    {
        T getResult();
    }
}
