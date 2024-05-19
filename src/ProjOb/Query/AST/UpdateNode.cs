namespace ProjOb.Query.AST
{
    public class UpdateNode : QueryNode
    {
        public IdentifierNode ObjectClass { get; private set; }
        public List<KeyValuePair<String, String>> SetList { get; private set; }
        public ExpressionNode? WhereExpression { get; private set; }

        public UpdateNode(IdentifierNode objectClass, List<KeyValuePair<String, String>> setList, ExpressionNode? expression)
        {
            ObjectClass = objectClass;
            SetList = setList;
            WhereExpression = expression;
        }

        public override void Visit(IQueryVisitorAST visitor) => visitor.Accept(this);
    }
}
