using System.Numerics;

namespace ProjOb.Query.AST.Expression
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

    public class BinOpNode : ExpressionNode
    {
        public ExpressionNode Left { get; private set; }
        public ExpressionNode Right { get; private set; }
        public BinOpType Type { get; private set; }

        public static Dictionary<BinOpType, ASTTypeCompatibility> OpMap = new()
        {
            [BinOpType.Add] = ASTTypeCompatibility.Arithmetic,
            [BinOpType.Subtract] = ASTTypeCompatibility.Arithmetic,
            [BinOpType.Multiply] = ASTTypeCompatibility.Arithmetic,
            [BinOpType.Divide] = ASTTypeCompatibility.Arithmetic,

            [BinOpType.Greater] = ASTTypeCompatibility.Relational,
            [BinOpType.GreaterEqual] = ASTTypeCompatibility.Relational,
            [BinOpType.Less] = ASTTypeCompatibility.Relational,
            [BinOpType.LessEqual] = ASTTypeCompatibility.Relational,
            [BinOpType.Equal] = ASTTypeCompatibility.Relational,
            [BinOpType.NotEqual] = ASTTypeCompatibility.Relational,

            [BinOpType.CondAnd] = ASTTypeCompatibility.Logical,
            [BinOpType.CondOr] = ASTTypeCompatibility.Logical,
        };

        public BinOpNode(ExpressionNode left, ExpressionNode right, BinOpType type)
        {
            Left = left;
            Right = right;
            Type = type;
        }

        public static T Add<T>(T a, T b) where T : INumber<T> => a + b;
        public static T Subtract<T>(T a, T b) where T : INumber<T> => a - b;
        public static T Multiply<T>(T a, T b) where T : INumber<T> => a * b;
        public static T Divide<T>(T a, T b) where T : INumber<T> => a / b;

        public static bool Greater<T>(T a, T b) where T : INumber<T> => a > b;
        public static bool GreaterEqual<T>(T a, T b) where T : INumber<T> => a >= b;
        public static bool Less<T>(T a, T b) where T : INumber<T> => a < b;
        public static bool LessEqual<T>(T a, T b) where T : INumber<T> => a <= b;
        public static bool Equal<T>(T a, T b) where T : INumber<T> => a == b;
        public static bool NotEqual<T>(T a, T b) where T : INumber<T> => a != b;

        public static bool Greater(String a, String b) => StringComparer.Ordinal.Compare(a, b) < 0;
        public static bool GreaterEqual(String a, String b) => StringComparer.Ordinal.Compare(a, b) <= 0;
        public static bool Less(String a, String b) => StringComparer.Ordinal.Compare(a, b) > 0;
        public static bool LessEqual(String a, String b) => StringComparer.Ordinal.Compare(a, b) >= 0;
        public static bool Equal(String a, String b) => StringComparer.Ordinal.Compare(a, b) == 0;
        public static bool NotEqual(String a, String b) => StringComparer.Ordinal.Compare(a, b) != 0;

        public static bool CondAnd<T>(T a, T b) where T : INumber<T> => a != T.AdditiveIdentity && b != T.AdditiveIdentity;
        public static bool CondOr<T>(T a, T b) where T : INumber<T> => a != T.AdditiveIdentity || b != T.AdditiveIdentity;

        public override void Visit(IExpressionVisitorAST visitor) => visitor.Accept(this);
    }
}
