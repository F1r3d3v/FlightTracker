using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public interface IVisitableAST
    {
        ASTNode? Visit(IVisitorAST visitor);
    }
}
