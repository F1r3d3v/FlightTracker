using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class WhereNode : ASTNode
    {
        public override ASTNode? Visit(IVisitorAST visitor) => visitor.Accept(this);
    }
}
