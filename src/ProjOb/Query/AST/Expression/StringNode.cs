namespace ProjOb.Query.AST
{
    public class StringNode : ExpressionNode
    {
        public String Value { get; set; }

        public StringNode(String value)
        {
            Value = value;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
