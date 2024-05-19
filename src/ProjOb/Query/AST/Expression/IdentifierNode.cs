namespace ProjOb.Query.AST
{
    public class IdentifierNode : ExpressionNode
    {
        public String Value { get; set; }

        public IdentifierNode(String value)
        {
            Value = value;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
