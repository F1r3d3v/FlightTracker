using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class SelectNode : ASTNode
    {
        private List<String> _varlist;
        private String _obj;

        public WhereNode? WhereNode { get; set; }

        public SelectNode(String Object, List<String> varlist)
        {
            _obj = Object;
            _varlist = varlist;
        }

        public override ASTNode? Visit(IVisitorAST visitor) => visitor.Accept(this);
    }
}
