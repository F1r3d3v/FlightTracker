using ProjOb.Query.AST;

namespace ProjOb.Query
{
    public abstract class ExpressionNode : IVisitableExpressionAST
    {
        public abstract void Visit(IExpressionVisitorAST visitor);
    }
}
