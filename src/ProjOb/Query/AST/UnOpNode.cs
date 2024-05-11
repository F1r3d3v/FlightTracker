using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public enum UnOpType
    {
        Negation,
        CondNot
    }

    public class UnOpNode : ASTNode
    {
        public ASTNode Arg { get; private set; }
        public UnOpType Type { get; private set; }

        public UnOpNode(ASTNode arg, UnOpType type)
        {
            Arg = arg;
            Type = type;
        }   

        public override ASTNode? Visit(IVisitorAST visitor) => visitor.Accept(this);
    }
}
