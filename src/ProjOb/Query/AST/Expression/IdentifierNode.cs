using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class IdentifierNode : ASTExpressionNode
    {
        public String Value { get; set; }

        public IdentifierNode(String value)
        {
            Value = value;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
