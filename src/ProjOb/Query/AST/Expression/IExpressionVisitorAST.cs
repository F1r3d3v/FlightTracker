namespace ProjOb.Query.AST.Expression
{
    public interface IExpressionVisitorAST
    {
        void Accept(IdentifierNode node);
        void Accept(NumberNode node);
        void Accept(StringNode node);
        void Accept(BinOpNode node);
        void Accept(UnOpNode node);
    }
}
