namespace ProjOb.Query.AST.Expression
{
    public interface IVisitableExpressionAST
    {
        void Visit(IExpressionVisitorAST visitor);
    }
}
