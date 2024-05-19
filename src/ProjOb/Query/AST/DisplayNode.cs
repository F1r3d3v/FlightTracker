using ProjOb.Query.AST.Expression;

namespace ProjOb.Query.AST
{
    public class DisplayNode : QueryNode
    {
        public IdentifierNode ObjectClass { get; private set; }
        public List<IdentifierNode>? Varlist { get; private set; }
        public ExpressionNode? WhereExpression { get; private set; }

        public DisplayNode(IdentifierNode Object, List<IdentifierNode>? varlist, ExpressionNode? expression)
        {
            ObjectClass = Object;
            Varlist = varlist;
            WhereExpression = expression;
        }

        public override void Visit(IQueryVisitorAST visitor) => visitor.Accept(this);
    }
}
