using ProjOb.Query.AST;
using ProjOb.Query.Wrappers;

namespace ProjOb.Query
{
    public class ASTExpressionVisitor : IExpressionVisitorAST
    {
        private readonly Object _object;
        private readonly AccessorVisitor accessorComponent = new AccessorVisitor();
        public double Result { get; private set; }

        public ASTExpressionVisitor(Object obj)
        {
            _object = obj;
        }

        public void Accept(IdentifierNode node)
        {
            IQueryAccessor accessor = _object.Apply(accessorComponent);
            String? value = accessor.GetValue(node.Value);
        }

        public void Accept(NumberNode node)
        {
            throw new NotImplementedException();
        }

        public void Accept(StringNode node)
        {
            throw new NotImplementedException();
        }

        public void Accept(BinOpNode node)
        {
            throw new NotImplementedException();
        }

        public void Accept(UnOpNode node)
        {
            throw new NotImplementedException();
        }
    }
}
