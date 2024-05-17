namespace ProjOb.Query.AST
{
    public enum BinOpType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Less,
        LessEqual,
        Greater,
        GreaterEqual,
        Equal,
        NotEqual,
        CondAnd,
        CondOr
    }

    public class BinOpNode : ASTExpressionNode
    {
        public ASTExpressionNode Left { get; private set; }
        public ASTExpressionNode Right { get; private set; }
        public BinOpType Type { get; private set; }

        public BinOpNode(ASTExpressionNode left, ASTExpressionNode right, BinOpType type)
        {
            Left = left;
            Right = right;
            Type = type;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
