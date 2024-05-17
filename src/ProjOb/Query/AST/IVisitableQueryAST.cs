namespace ProjOb.Query.AST
{
    public interface IVisitableQueryAST
    {
        void Visit(IQueryVisitorAST visitor);
    }
}
