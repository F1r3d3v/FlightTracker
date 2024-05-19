namespace ProjOb.Query.AST
{
    public abstract class QueryNode : IVisitableQueryAST
    {
        public abstract void Visit(IQueryVisitorAST visitor);
    }
}
