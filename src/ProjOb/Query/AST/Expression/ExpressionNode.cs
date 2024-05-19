namespace ProjOb.Query.AST.Expression
{
    public abstract class ExpressionNode : IVisitableExpressionAST
    {
        public abstract void Visit(IExpressionVisitorAST visitor);
    }
}
