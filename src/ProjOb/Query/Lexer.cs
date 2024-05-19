using ProjOb.Exceptions;
using System.Text;
using System.Text.RegularExpressions;

namespace ProjOb.Query
{
    public class Lexer
    {
        private StringBuilder _input;
        public Token NextToken { get; private set; }

        private static readonly Dictionary<TokenType, Regex> _Tokens = new()
        {
            [TokenType.Whitespace] = new Regex("^\\s+", RegexOptions.Compiled),
            [TokenType.Display] = GetRegexFromWord("DISPLAY"),
            [TokenType.From] = GetRegexFromWord("FROM"),
            [TokenType.Update] = GetRegexFromWord("UPDATE"),
            [TokenType.Set] = GetRegexFromWord("SET"),
            [TokenType.Delete] = GetRegexFromWord("DELETE"),
            [TokenType.Add] = GetRegexFromWord("ADD"),
            [TokenType.New] = GetRegexFromWord("NEW"),
            [TokenType.Where] = GetRegexFromWord("WHERE"),
            [TokenType.Plus] = GetRegexFromWord("+"),
            [TokenType.Minus] = GetRegexFromWord("-"),
            [TokenType.Asterisk] = GetRegexFromWord("*"),
            [TokenType.Slash] = GetRegexFromWord("/"),
            [TokenType.LessEqual] = GetRegexFromWord("<="),
            [TokenType.GreaterEqual] = GetRegexFromWord(">="),
            [TokenType.Less] = GetRegexFromWord("<"),
            [TokenType.Greater] = GetRegexFromWord(">"),
            [TokenType.Equal] = GetRegexFromWord("="),
            [TokenType.NotEqual] = GetRegexFromWord("!="),
            [TokenType.ConditionalNegation] = GetRegexFromWord("NOT"),
            [TokenType.ConditionalAnd] = GetRegexFromWord("AND"),
            [TokenType.ConditionalOr] = GetRegexFromWord("OR"),
            [TokenType.Separator] = GetRegexFromWord(","),
            [TokenType.LeftParenthesis] = GetRegexFromWord("("),
            [TokenType.RightParenthesis] = GetRegexFromWord(")"),
            [TokenType.Identifier] = new Regex("^[a-zA-Z_][a-zA-Z0-9_]*(\\.[a-zA-Z_][a-zA-Z0-9_]*)*", RegexOptions.Compiled | RegexOptions.ExplicitCapture),
            [TokenType.Number] = new Regex("^-?[0-9]+(\\.[0-9]+)?", RegexOptions.Compiled | RegexOptions.ExplicitCapture),
            [TokenType.String] = new Regex("^\"([^\\\\\"]*(?:\\\\.[^\\\\\"]*)*)\"", RegexOptions.Compiled),
        };

        public Lexer(String input)
        {
            _input = new StringBuilder(input);
            NextToken = RetrieveToken();
        }

        public bool MatchTokenAndAdvance(TokenType type)
        {
            if (NextToken.Type == type)
            {
                GetNextToken();
                return true;
            }
            return false;
        }

        public void GetNextToken() => NextToken = RetrieveToken();

        private Token RetrieveToken()
        {
            foreach (var pair in _Tokens)
            {
                Token? token = GetTokenFromRegex(pair.Key, pair.Value);
                if (token != null && token.Type != TokenType.Whitespace)
                    return token;
            }

            if (_input.Length == 0)
            {
                return new Token(TokenType.EOS, "");
            }
            else
            {
                throw new InvalidTokenException("Unknown Token");
            }
        }

        private static Regex GetRegexFromWord(string word)
        {
            word = Regex.Escape(word);
            return (new Regex("^\\w+$", RegexOptions.Compiled).IsMatch(word))
                ? new Regex($"^{word}\\b", RegexOptions.Compiled | RegexOptions.IgnoreCase)
                : new Regex($"^{word}", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        }

        private Token? GetTokenFromRegex(TokenType type, Regex reg)
        {
            Match match;
            if ((match = reg.Match(_input.ToString())).Success)
            {
                _input.Remove(0, match.Length);
                return new Token(type, (type == TokenType.String) ? match.Groups[1].Value : match.Value);
            }
            return null;
        }
    }
}
