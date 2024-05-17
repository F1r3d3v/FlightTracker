namespace ProjOb.Query.AST
{
    public enum UnOpType
    {
        Negation,
        CondNot
    }

    public class UnOpNode : ASTExpressionNode
    {
        public ASTExpressionNode Arg { get; private set; }
        public UnOpType Type { get; private set; }

        public UnOpNode(ASTExpressionNode arg, UnOpType type)
        {
            Arg = arg;
            Type = type;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
