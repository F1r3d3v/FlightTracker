using ProjOb.Exceptions;
using ProjOb.Query.AST;

namespace ProjOb.Query
{
    public class Parser
    {
        private readonly Lexer _lex;

        public Parser(Lexer lex)
        {
            _lex = lex;
        }

        public QueryNode Parse()
        {
            QueryNode result = ParseQuery();
            if (_lex.NextToken.Type != TokenType.EOS)
            {
                throw new ParseTreeException("Can't parse the Query!");
            }

            return result;
        }

        private QueryNode ParseQuery()
        {
            if (_lex.MatchTokenAndAdvance(TokenType.Display))
                return ParseDisplay();
            else if (_lex.MatchTokenAndAdvance(TokenType.Update))
                return ParseUpdate();
            else if (_lex.MatchTokenAndAdvance(TokenType.Delete))
                return ParseDelete();
            else if (_lex.MatchTokenAndAdvance(TokenType.Add))
                return ParseAdd();
            else
                throw new ParseTreeException("Can't parse the Query!");
        }

        private QueryNode ParseAdd()
        {
            IdentifierNode obj = new IdentifierNode(_lex.NextToken.Value);
            _lex.MatchTokenAndAdvance(TokenType.Identifier);

            if (!_lex.MatchTokenAndAdvance(TokenType.New))
                throw new ParseTreeException("Can't parse the Query!");

            List<KeyValuePair<String, String>> setlist = ParseSetlist();

            return new AddNode(obj, setlist);
        }

        private QueryNode ParseDelete()
        {
            IdentifierNode obj = new IdentifierNode(_lex.NextToken.Value);
            _lex.MatchTokenAndAdvance(TokenType.Identifier);

            ExpressionNode? node = null;
            if (_lex.MatchTokenAndAdvance(TokenType.Where))
                node = ParseLogicExpression();

            return new DeleteNode(obj, node);
        }

        private QueryNode ParseUpdate()
        {
            IdentifierNode obj = new IdentifierNode(_lex.NextToken.Value);
            _lex.MatchTokenAndAdvance(TokenType.Identifier);

            if (!_lex.MatchTokenAndAdvance(TokenType.Set))
                throw new ParseTreeException("Can't parse the Query!");

            List<KeyValuePair<String, String>> setlist = ParseSetlist();

            ExpressionNode? node = null;
            if (_lex.MatchTokenAndAdvance(TokenType.Where))
                node = ParseLogicExpression();

            return new UpdateNode(obj, setlist, node);
        }

        private QueryNode ParseDisplay()
        {
            List<IdentifierNode>? list = null;
            if (!_lex.MatchTokenAndAdvance(TokenType.Asterisk))
                list = ParseVarlist();

            if (!_lex.MatchTokenAndAdvance(TokenType.From))
                throw new ParseTreeException("Can't parse the Query!");

            IdentifierNode obj = new IdentifierNode(_lex.NextToken.Value);
            _lex.MatchTokenAndAdvance(TokenType.Identifier);

            ExpressionNode? node = null;
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

        private List<KeyValuePair<String, String>> ParseSetlist()
        {
            if (_lex.MatchTokenAndAdvance(TokenType.LeftParenthesis))
            {
                List<KeyValuePair<String, String>> l = [];

                String key = _lex.NextToken.Value;
                if (!_lex.MatchTokenAndAdvance(TokenType.Identifier) || !_lex.MatchTokenAndAdvance(TokenType.Equal))
                    throw new ParseTreeException("Can't parse the Query!");

                bool negative = false;
                String value = _lex.NextToken.Value;
                if (_lex.MatchTokenAndAdvance(TokenType.Minus))
                {
                    negative = true;
                    value = _lex.NextToken.Value;

                    if (_lex.NextToken.Type != TokenType.Number)
                        throw new ParseTreeException("Can't parse the Query!");
                }
                else if (_lex.NextToken.Type != TokenType.Number && _lex.NextToken.Type != TokenType.String)
                    throw new ParseTreeException("Can't parse the Query!");

                value = $"{(negative ? "-" : "")}{value}";
                _lex.GetNextToken();

                l.Add(new KeyValuePair<String, String>(key, value));

                while (true)
                {
                    if (_lex.MatchTokenAndAdvance(TokenType.Separator))
                    {
                        key = _lex.NextToken.Value;
                        if (!_lex.MatchTokenAndAdvance(TokenType.Identifier) || !_lex.MatchTokenAndAdvance(TokenType.Equal))
                            throw new ParseTreeException("Can't parse the Query!");

                        negative = false;
                        value = _lex.NextToken.Value;
                        if (_lex.MatchTokenAndAdvance(TokenType.Minus))
                        {
                            negative = true;
                            value = _lex.NextToken.Value;

                            if (_lex.NextToken.Type != TokenType.Number)
                                throw new ParseTreeException("Can't parse the Query!");
                        }
                        else if (_lex.NextToken.Type != TokenType.Number && _lex.NextToken.Type != TokenType.String)
                            throw new ParseTreeException("Can't parse the Query!");

                        value = $"{(negative ? "-" : "")}{value}";
                        _lex.GetNextToken();

                        l.Add(new KeyValuePair<String, String>(key, value));
                    }
                    else if (_lex.MatchTokenAndAdvance(TokenType.RightParenthesis))
                        return l;
                    else
                        throw new ParseTreeException("Can't parse the Query!");
                }
            }
            else
            {
                throw new ParseTreeException("Can't parse the Query!");
            }
        }

        private ExpressionNode ParseLogicExpression()
        {
            if (_lex.MatchTokenAndAdvance(TokenType.ConditionalNegation))
            {
                return new UnOpNode(ParseLogicTerm(), UnOpType.CondNot);
            }
            else
            {
                return ParseLogicTerm();
            }
        }

        private ExpressionNode ParseLogicTerm()
        {
            ExpressionNode a = ParseLogicFactor();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.ConditionalAnd))
                {
                    ExpressionNode b = ParseLogicExpression();
                    a = new BinOpNode(a, b, BinOpType.CondAnd);
                }
                else
                    return a;
            }
        }

        private ExpressionNode ParseLogicFactor()
        {
            ExpressionNode a = ParseRelationExpression();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.ConditionalOr))
                {
                    ExpressionNode b = ParseLogicExpression();
                    a = new BinOpNode(a, b, BinOpType.CondOr);
                }
                else
                    return a;
            }
        }

        private ExpressionNode ParseRelationExpression()
        {
            ExpressionNode a = ParseArithmeticExpression();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Less))
                {
                    ExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Less);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.LessEqual))
                {
                    ExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.LessEqual);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Greater))
                {
                    ExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Greater);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.GreaterEqual))
                {
                    ExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.GreaterEqual);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Equal))
                {
                    ExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.Equal);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.NotEqual))
                {
                    ExpressionNode b = ParseArithmeticExpression();
                    a = new BinOpNode(a, b, BinOpType.NotEqual);
                }
                else
                    return a;
            }
        }

        private ExpressionNode ParseArithmeticExpression()
        {
            ExpressionNode a = ParseArithmeticTerm();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Plus))
                {
                    ExpressionNode b = ParseArithmeticTerm();
                    a = new BinOpNode(a, b, BinOpType.Add);
                }
                else if (_lex.MatchTokenAndAdvance(TokenType.Minus))
                {
                    ExpressionNode b = ParseArithmeticTerm();
                    a = new BinOpNode(a, b, BinOpType.Subtract);
                }
                else
                    return a;
            }
        }

        private ExpressionNode ParseArithmeticTerm()
        {
            ExpressionNode a = ParseArithmeticFactor();
            while (true)
            {
                if (_lex.MatchTokenAndAdvance(TokenType.Slash))
                {
                    ExpressionNode b = ParseArithmeticFactor();
                    a = new BinOpNode(a, b, BinOpType.Divide);
                }
                if (_lex.MatchTokenAndAdvance(TokenType.Asterisk))
                {
                    ExpressionNode b = ParseArithmeticFactor();
                    a = new BinOpNode(a, b, BinOpType.Multiply);
                }
                else
                    return a;
            }
        }

        private ExpressionNode ParseArithmeticFactor()
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

        private ExpressionNode ParseAtom()
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
                ExpressionNode node = ParseLogicExpression();
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
