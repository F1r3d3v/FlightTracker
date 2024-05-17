namespace ProjOb.Query.AST
{
    public interface IVisitableExpressionAST
    {
        void Visit(IExpressionVisitorAST visitor);
    }
}
