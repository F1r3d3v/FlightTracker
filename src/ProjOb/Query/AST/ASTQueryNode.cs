namespace ProjOb.Query.AST
{
    public abstract class ASTQueryNode : IVisitableQueryAST
    {
        public abstract void Visit(IQueryVisitorAST visitor);
    }
}
