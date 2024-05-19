using ProjOb.Query.AST.Expression;

namespace ProjOb.Query.AST
{
    public class AddNode : QueryNode
    {
        public IdentifierNode ObjectClass { get; private set; }
        public List<KeyValuePair<String, String>> SetList { get; private set; }

        public AddNode(IdentifierNode objectClass, List<KeyValuePair<String, String>> setList)
        {
            ObjectClass = objectClass;
            SetList = setList;
        }

        public override void Visit(IQueryVisitorAST visitor) => visitor.Accept(this);
    }
}
