namespace ProjOb.Query.AST
{
    public class NumberNode : ASTExpressionNode
    {
        public double Value { get; set; }

        public NumberNode(double value)
        {
            Value = value;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
