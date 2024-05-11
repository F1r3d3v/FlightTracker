using ProjOb.Query.AST;

namespace ProjOb.Query
{
    public class Query
    {
        private readonly ASTNode _query;

        public Query(ASTNode query) 
        {
            _query = query;
        }

        public QueryResult Execute()
        {
            ASTVisitor visitor = new ASTVisitor();
            _query.Visit(visitor);

            return visitor.Result!;
        }
    }
}
