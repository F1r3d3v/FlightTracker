using ProjOb.Query.AST;

namespace ProjOb.Query
{
    public class Query
    {
        private readonly ASTQueryNode _query;

        public Query(ASTQueryNode query) 
        {
            _query = query;
        }

        public QueryResult Execute()
        {
            ASTQueryVisitor visitor = new ASTQueryVisitor();
            _query.Visit(visitor);

            return visitor.Result!;
        }
    }
}
