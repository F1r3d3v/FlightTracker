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

        public ASTQueryNode Parse()
        {
            ASTQueryNode result = ParseQuery();
            if (_lex.NextToken.Type != TokenType.EOS)
            {
                throw new ParseTreeException("Can't parse the Query!");
            }

            return result;
        }

        private ASTQueryNode ParseQuery()
        {
            if (_lex.MatchTokenAndAdvance(TokenType.Select))
                return ParseSelect();
            else
                throw new ParseTreeException("Can't parse the Query!");
        }

        private ASTQueryNode ParseSelect()
        {
            List<IdentifierNode>? list = null;
            if (!_lex.MatchTokenAndAdvance(TokenType.Asterisk))
                list = ParseVarlist();

            if (!_lex.MatchTokenAndAdvance(TokenType.From))
                throw new ParseTreeException("Can't parse the Query!");

            IdentifierNode obj = new IdentifierNode(_lex.NextToken.Value);
            _lex.MatchTokenAndAdvance(TokenType.Identifier);

            ASTExpressionNode? node = null;
            if (_lex.MatchTokenAndAdvance(TokenType.Where))
                node = ParseLogicExpression();

            return new DisplayNode(obj, list, node);
        }

        private List<IdentifierNode> ParseVarlist()
        {
            List<IdentifierNode> l = [];
            if (_lex.NextToken.Type != TokenType.Identifier)
                throw new ParseTreeException("Can't parse the Query!");

            l.Add(new IdentifierNode(_lex.NextToken.Value));
            _lex.MatchTokenAndAdvance(TokenType.Identifier);

            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Separator))
                {
                    if (_lex.NextToken.Type != TokenType.Identifier)
                        throw new ParseTreeException("Can't parse the Query!");

                    l.Add(new IdentifierNode(_lex.NextToken.Value));
                    _lex.MatchTokenAndAdvance(TokenType.Identifier);
                }
                else
                {
                    return l;
                }
            }
        }

        private ASTExpressionNode ParseLogicExpression()
        {
            ASTExpressionNode a = ParseLogicTerm();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.ConditionalOr))
                {
                    ASTExpressionNode b = ParseLogicTerm();
                    a = new BinOpNode(a, b, BinOpType.CondOr);
                }
                else
                    return a;
            }
        }

        private ASTExpressionNode ParseLogicTerm()
        {
            ASTExpressionNode a = ParseLogicFactor();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.ConditionalAnd))
                {
                    ASTExpressionNode b = ParseLogicFactor();
                    a = new BinOpNode(a, b, BinOpType.CondAnd);
                }
                else
                    return a;
            }
        }

        private ASTExpressionNode ParseLogicFactor()
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

        private ASTExpressionNode ParseRelationExpression()
        {
            ASTExpressionNode a = ParseArithmeticExpression();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Less))
                {
                    ASTExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Less);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.LessEqual))
                {
                    ASTExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.LessEqual);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Greater))
                {
                    ASTExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Greater);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.GreaterEqual))
                {
                    ASTExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.GreaterEqual);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Equal))
                {
                    ASTExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Equal);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.NotEqual))
                {
                    ASTExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.NotEqual);
                }
                else
                    return a;
            }
        }

        private ASTExpressionNode ParseArithmeticExpression()
        {
            ASTExpressionNode a = ParseArithmeticTerm();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Plus))
                {
                    ASTExpressionNode b = ParseArithmeticTerm();
                    a = new BinOpNode(a, b, BinOpType.Add);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Minus))
                {
                    ASTExpressionNode b = ParseArithmeticTerm();
                    a = new BinOpNode(a, b, BinOpType.Subtract);
                }
                else
                    return a;
            }
        }

        private ASTExpressionNode ParseArithmeticTerm()
        {
            ASTExpressionNode a = ParseArithmeticFactor();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Asterisk))
                {
                    ASTExpressionNode b = ParseArithmeticFactor();
                    a = new BinOpNode(a, b, BinOpType.Multiply);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Slash))
                {
                    ASTExpressionNode b = ParseArithmeticFactor();
                    a = new BinOpNode(a, b, BinOpType.Divide);
                }
                else
                    return a;
            }
        }

        private ASTExpressionNode ParseArithmeticFactor()
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

        private ASTExpressionNode ParseAtom()
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
                ASTExpressionNode node = ParseLogicExpression();
                if (_lex.MatchTokenAndAdvance(TokenType.RightParenthesis))
                    return node;
                else
                    throw new ParseTreeException("Can't parse the Query!");
            }
            else
            {
                throw new ParseTreeException("Can't parse the Query!");
            }
        }

    }
}
