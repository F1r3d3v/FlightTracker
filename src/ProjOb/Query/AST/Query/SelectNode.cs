using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class SelectNode : ASTQueryNode
    {
        private List<IdentifierNode> _varlist;
        private IdentifierNode _obj;

        private ASTExpressionNode? _expression;

        public SelectNode(IdentifierNode Object, List<IdentifierNode> varlist, ASTExpressionNode? expression)
        {
            _obj = Object;
            _varlist = varlist;
            _expression = expression;
        }

        public override void Visit(IQueryVisitorAST visitor) => visitor.Accept(this);
    }
}
