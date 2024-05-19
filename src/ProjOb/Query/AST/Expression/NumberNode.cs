namespace ProjOb.Query.AST.Expression
{
    public class NumberNode : ExpressionNode
    {
        public double Value { get; set; }

        public NumberNode(double value)
        {
            Value = value;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
