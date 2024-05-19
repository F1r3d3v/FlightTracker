namespace ProjOb.Query.AST.Expression
{
    public enum UnOpType
    {
        Negation,
        CondNot
    }

    public class UnOpNode : ExpressionNode
    {
        public ExpressionNode Arg { get; private set; }
        public UnOpType Type { get; private set; }

        public UnOpNode(ExpressionNode arg, UnOpType type)
        {
            Arg = arg;
            Type = type;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
