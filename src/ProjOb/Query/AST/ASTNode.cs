using ProjOb.Query.AST;

namespace ProjOb.Query
{
    public abstract class ASTNode : IVisitableAST
    {
        public abstract ASTNode? Visit(IVisitorAST visitor);
    }
}
