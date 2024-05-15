namespace ProjOb.Query.AST
{
    public class DisplayNode : ASTQueryNode
    {
        public IdentifierNode ObjectClass { get; private set; }
        public List<IdentifierNode>? Varlist { get; private set; }
        public ASTExpressionNode? WhereExpression { get; private set; }

        public DisplayNode(IdentifierNode Object, List<IdentifierNode>? varlist, ASTExpressionNode? expression)
        {
            ObjectClass = Object;
            Varlist = varlist;
            WhereExpression = expression;
        }

        public override void Visit(IQueryVisitorAST visitor) => visitor.Accept(this);
    }
}
