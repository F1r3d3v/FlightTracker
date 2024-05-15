using ProjOb.Query.AST;

namespace ProjOb.Query
{
    public abstract class ASTExpressionNode : IVisitableExpressionAST
    {
        public abstract void Visit(IExpressionVisitorAST visitor);
    }
}
