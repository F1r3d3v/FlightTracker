using ProjOb.Exceptions;
using ProjOb.Query.AST;
using System.Collections.Generic;

namespace ProjOb.Query
{
    public class Parser
    {
        private readonly Lexer _lex;

        public Parser(Lexer lex)
        {
            _lex = lex;
        }

        public ASTNode Parse()
        {
            ASTNode result = ParseQuery();
            if (_lex.NextToken.Type != TokenType.EOS)
            {
                throw new ParseTreeException("Can't parse the Query!");
            }

            return result;
        }

        private ASTNode ParseQuery()
        {
            if (_lex.MatchTokenAndAdvance(TokenType.Select))
                return ParseSelect();
            else
                throw new ParseTreeException();
        }

        private ASTNode ParseSelect()
        {
            List<IdentifierNode> list = ParseVarlist();
            if (!_lex.MatchTokenAndAdvance(TokenType.From))
                throw new ParseTreeException();

            IdentifierNode obj = new IdentifierNode(_lex.NextToken.Value);
            _lex.MatchTokenAndAdvance(TokenType.Identifier);

            ASTNode? node = null;
            if (_lex.MatchTokenAndAdvance(TokenType.Where))
                node = ParseLogicExpression();

            return new SelectNode(obj, list, node);
        }

        private List<IdentifierNode> ParseVarlist()
        {
            List<IdentifierNode> l = [];
            if (_lex.NextToken.Type != TokenType.Identifier)
                throw new ParseTreeException();

            l.Add(new IdentifierNode(_lex.NextToken.Value));
            _lex.MatchTokenAndAdvance(TokenType.Identifier);

            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Separator))
                {
                    if (_lex.NextToken.Type != TokenType.Identifier)
                        throw new ParseTreeException();

                    l.Add(new IdentifierNode(_lex.NextToken.Value));
                    _lex.MatchTokenAndAdvance(TokenType.Identifier);
                }
                else
                {
                    return l;
                }
            }
        }

        private ASTNode ParseLogicExpression()
        {
            ASTNode a = ParseLogicTerm();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.ConditionalOr))
                {
                    ASTNode b = ParseLogicTerm();
                    a = new BinOpNode(a, b, BinOpType.CondOr);
                }
                else
                    return a;
            }
        }

        private ASTNode ParseLogicTerm()
        {
            ASTNode a = ParseLogicFactor();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.ConditionalAnd))
                {
                    ASTNode b = ParseLogicFactor();
                    a = new BinOpNode(a, b, BinOpType.CondAnd);
                }
                else
                    return a;
            }
        }

        private ASTNode ParseLogicFactor()
        {
            if (_lex.MatchTokenAndAdvance(TokenType.ConditionalNegation))
            {
                return new UnOpNode(ParseRelationExpression(), UnOpType.CondNot);
            }
            else
            {
                return ParseRelationExpression();
            }
        }

        private ASTNode ParseRelationExpression()
        {
            ASTNode a = ParseArithmeticExpression();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Less))
                {
                    ASTNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Less);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.LessEqual))
                {
                    ASTNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.LessEqual);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Greater))
                {
                    ASTNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Greater);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.GreaterEqual))
                {
                    ASTNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.GreaterEqual);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Equal))
                {
                    ASTNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Equal);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.NotEqual))
                {
                    ASTNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.NotEqual);
                }
                else
                    return a;
            }
        }

        private ASTNode ParseArithmeticExpression()
        {
            ASTNode a = ParseArithmeticTerm();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Plus))
                {
                    ASTNode b = ParseArithmeticTerm();
                    a = new BinOpNode(a, b, BinOpType.Add);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Minus))
                {
                    ASTNode b = ParseArithmeticTerm();
                    a = new BinOpNode(a, b, BinOpType.Subtract);
                }
                else
                    return a;
            }
        }

        private ASTNode ParseArithmeticTerm()
        {
            ASTNode a = ParseArithmeticFactor();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Asterisk))
                {
                    ASTNode b = ParseArithmeticFactor();
                    a = new BinOpNode(a, b, BinOpType.Multiply);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Slash))
                {
                    ASTNode b = ParseArithmeticFactor();
                    a = new BinOpNode(a, b, BinOpType.Divide);
                }
                else
                    return a;
            }
        }

        private ASTNode ParseArithmeticFactor()
        {
            if (_lex.MatchTokenAndAdvance(TokenType.Minus))
            {
                return new UnOpNode(ParseAtom(), UnOpType.Negation);
            }
            else
            {
                return ParseAtom();
            }
        }

        private ASTNode ParseAtom()
        {
            String val = _lex.NextToken.Value;
            if (_lex.MatchTokenAndAdvance(TokenType.Identifier))
            {
                return new IdentifierNode(val);
            }
            else if (_lex.MatchTokenAndAdvance(TokenType.Number))
            {
                return new NumberNode(double.Parse(val));
            }
            else if (_lex.MatchTokenAndAdvance(TokenType.String))
            {
                return new StringNode(val);
            }
            else if (_lex.MatchTokenAndAdvance(TokenType.LeftParenthesis))
            {
                ASTNode node = ParseLogicExpression();
                if (_lex.MatchTokenAndAdvance(TokenType.RightParenthesis))
                    return node;
                else
                    throw new ParseTreeException();
            }
            else
            {
                throw new ParseTreeException();
            }
        }

    }
}
