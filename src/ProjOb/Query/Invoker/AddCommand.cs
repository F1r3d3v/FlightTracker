using ProjOb.Exceptions;
using ProjOb.Query.AST;

namespace ProjOb.Query.Invoker
{
    public class AddCommand : IQueryCommand
    {
        private QueryReceiver _receiver;
        private AddNode _node;

        public AddCommand(QueryReceiver receiver, AddNode node)
        {
            _receiver = receiver;
            _node = node;
        }

        public void Execute()
        {
            if (Enum.TryParse(_node.ObjectClass.Value, true, out ObjectClassEnum objClass))
            {
                _receiver.AddAction(objClass, _node.SetList);
            }
            else
            {
                throw new QueryExecutionException("Invalid object class");
            }
        }
    }
}
