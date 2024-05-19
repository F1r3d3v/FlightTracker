using ProjOb.Query.AST.Expression;

namespace ProjOb.Query.AST
{
    public class DeleteNode : QueryNode
    {
        public IdentifierNode ObjectClass { get; private set; }
        public ExpressionNode? WhereExpression { get; private set; }

        public DeleteNode(IdentifierNode objectClass, ExpressionNode? expression)
        {
            ObjectClass = objectClass;
            WhereExpression = expression;
        }

        public override void Visit(IQueryVisitorAST visitor) => visitor.Accept(this);
    }
}
