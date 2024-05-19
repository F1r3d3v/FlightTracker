using ProjOb.Exceptions;
using ProjOb.Query.AST;
using ProjOb.Query.AST.Expression;

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
                    if (_node.WhereExpression == null)
                        return true;
                    else
                        return ExpressionEvaluator.Evaluate(_node.WhereExpression, obj) != 0;
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
