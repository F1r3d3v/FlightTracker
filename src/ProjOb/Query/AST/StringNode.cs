using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class StringNode : ASTNode
    {
        public String Value { get; set; }

        public StringNode(String value)
        {
            Value = value;
        }

        public override ASTNode? Visit(IVisitorAST visitor) => visitor.Accept(this);
    }
}
