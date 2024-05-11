using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public enum BinOpType
    {
        Add,
        Subtract,
        Multiply,
        Divide,
        Less,
        LessEqual,
        Greater,
        GreaterEqual,
        Equal,
        NotEqual,
        CondAnd,
        CondOr
    }

    public class BinOpNode : ASTNode
    {
        public ASTNode Left { get; private set; }
        public ASTNode Right { get; private set; }
        public BinOpType Type { get; private set; }

        public BinOpNode(ASTNode left, ASTNode right, BinOpType type)
        {
            Left = left;
            Right = right;
            Type = type;
        }

        public override ASTNode? Visit(IVisitorAST visitor) => visitor.Accept(this);
    }
}
