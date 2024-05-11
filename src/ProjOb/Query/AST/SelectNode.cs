using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class SelectNode : ASTNode
    {
        private List<IdentifierNode> _varlist;
        private IdentifierNode _obj;

        private ASTNode? _expression;

        public SelectNode(IdentifierNode Object, List<IdentifierNode> varlist, ASTNode? expression)
        {
            _obj = Object;
            _varlist = varlist;
            _expression = expression;
        }

        public override ASTNode? Visit(IVisitorAST visitor) => visitor.Accept(this);
    }
}
