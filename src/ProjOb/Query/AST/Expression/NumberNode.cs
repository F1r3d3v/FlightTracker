using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class NumberNode : ASTExpressionNode
    {
        public double Value { get; set; }

        public NumberNode(double value)
        {
            Value = value;
        }

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
