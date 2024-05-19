using ProjOb.Query.Invoker;

namespace ProjOb.Query.AST
{
    public class QueryVisitor : IQueryVisitorAST
    {
        public QueryResult? Result { get; private set; }

        private readonly QueryReceiver _receiver;
        private readonly QueryInvoker _invoker;

        public QueryVisitor(Database db)
        {
            _receiver = new QueryReceiver(db);
            _invoker = new QueryInvoker();
        }

        public void Accept(DisplayNode node)
        {
            var command = new DisplayCommand(_receiver, node);
            _invoker.SetCommand(command);
            _invoker.InvokeCommand();
            Result = command.getResult();
        }

        public void Accept(UpdateNode node)
        {
            var command = new UpdateCommand(_receiver, node);
            _invoker.SetCommand(command);
            _invoker.InvokeCommand();
        }

        public void Accept(DeleteNode node)
        {
            var command = new DeleteCommand(_receiver, node);
            _invoker.SetCommand(command);
            _invoker.InvokeCommand();
        }

        public void Accept(AddNode node)
        {
            var command = new AddCommand(_receiver, node);
            _invoker.SetCommand(command);
            _invoker.InvokeCommand();
        }
    }
}
