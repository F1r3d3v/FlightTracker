using ProjOb.Query.Invoker;

namespace ProjOb.Query
{
    public class QueryInvoker
    {
        private IQueryCommand? _command;

        public void SetCommand(IQueryCommand command) => _command = command;

        public void InvokeCommand() => _command?.Execute();
    }
}
