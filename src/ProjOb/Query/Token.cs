namespace ProjOb.Query
{
    public class Token
    {
        public TokenType Type { get; set; }
        public String Value { get; set; }

        public Token(TokenType type, String value)
        {
            Type = type;
            Value = value;
        }
    }
}
