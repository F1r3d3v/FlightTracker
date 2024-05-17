namespace ProjOb.Query.AST
{
    public interface IQueryVisitorAST
    {
        void Accept(DisplayNode node);
    }
}
