using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public interface IVisitorAST
    {
        ASTNode? Accept(IdentifierNode node) => default;
        ASTNode? Accept(NumberNode node) => default;
        ASTNode? Accept(StringNode node) => default;
        ASTNode? Accept(SelectNode node) => default;
        ASTNode? Accept(WhereNode node) => default;
        ASTNode? Accept(BinOpNode node) => default;
        ASTNode? Accept(UnOpNode node) => default;
    }
}
