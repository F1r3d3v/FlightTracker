using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class NumberNode : ASTNode
    {
        public double Value { get; set; }

        public NumberNode(double value)
        {
            Value = value;
        }

        public override ASTNode? Visit(IVisitorAST visitor) => visitor.Accept(this);
    }
}
