using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class ASTQueryVisitor : IQueryVisitorAST
    {
        public QueryResult? Result { get; private set; }

        public void Accept(SelectNode node)
        {
            throw new NotImplementedException();
        }
    }
}
