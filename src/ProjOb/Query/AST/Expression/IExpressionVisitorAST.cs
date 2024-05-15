using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public interface IExpressionVisitorAST
    {
        void Accept(IdentifierNode node);
        void Accept(NumberNode node);
        void Accept(StringNode node);
        void Accept(BinOpNode node);
        void Accept(UnOpNode node);
    }
}
