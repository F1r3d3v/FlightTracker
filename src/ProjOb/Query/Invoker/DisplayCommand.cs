using ProjOb.Exceptions;
using ProjOb.Objects;
using ProjOb.Query.AST;

namespace ProjOb.Query.Invoker
{
    public class DisplayCommand : IQueryCommand<QueryResult?>
    {
        private QueryReceiver _receiver;
        private DisplayNode _node;
        private QueryResult? _result;

        public DisplayCommand(QueryReceiver receiver, DisplayNode node)
        {
            _receiver = receiver;
            _node = node;
        }

        public void Execute()
        {
            if (Enum.TryParse(_node.ObjectClass.Value, true, out ObjectClassEnum objClass))
            {
                _result = _receiver.DisplayAction(objClass, _node.Varlist?.Select(x => x.Value).ToArray(), (Object obj) =>
                {
                    if (_node.WhereExpression == null) return true;

                    ASTExpressionVisitor visitor = new ASTExpressionVisitor(obj);
                    _node.WhereExpression?.Visit(visitor);
                    return visitor.Result != 0;
                });
            }
            else
            {
                throw new QueryExecutionException("Invalid Object Class");
            }
        }

        public QueryResult? getResult() => _result;
    }
}
