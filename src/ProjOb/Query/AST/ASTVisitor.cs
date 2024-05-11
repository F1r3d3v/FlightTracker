using ProjOb.Query.AST;

namespace ProjOb.Query
{
    public class ASTVisitor : IVisitorAST
    {
        public QueryResult? Result { get; private set; }

        public ASTVisitor() { }
    }
}
