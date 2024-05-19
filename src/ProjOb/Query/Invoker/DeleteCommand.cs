using ProjOb.Exceptions;
using ProjOb.Query.AST;
using ProjOb.Query.AST.Expression;

namespace ProjOb.Query.Invoker
{
    public class DeleteCommand : IQueryCommand
    {
        private QueryReceiver _receiver;
        private DeleteNode _node;

        public DeleteCommand(QueryReceiver receiver, DeleteNode node)
        {
            _receiver = receiver;
            _node = node;
        }

        public void Execute()
        {
            if (Enum.TryParse(_node.ObjectClass.Value, true, out ObjectClassEnum objClass))
            {
                _receiver.DeleteAction(objClass, (Object obj) =>
                {
                    if (_node.WhereExpression == null)
                        return true;
                    else
                        return ExpressionEvaluator.Evaluate(_node.WhereExpression, obj) != 0;
                });
            }
            else
            {
                throw new QueryExecutionException("Invalid object class");
            }
        }
    }
}
