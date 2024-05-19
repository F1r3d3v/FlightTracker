namespace ProjOb.Query.AST
{
    public interface IQueryVisitorAST
    {
        void Accept(DisplayNode node);
        void Accept(UpdateNode node);
        void Accept(DeleteNode node);
        void Accept(AddNode node);
    }
}
