namespace ProjOb.Query.AST.Expression
{
    public enum ASTType
    {
        Number,
        String
    }

    public enum ASTTypeCompatibility
    {
        Arithmetic,
        Relational,
        Logical,
        Assignment
    }

    internal class ASTTypeComparer : IEqualityComparer<(ASTType, ASTType)>
    {
        public bool Equals((ASTType, ASTType) tuple1, (ASTType, ASTType) tuple2) =>
        (Equals(tuple1.Item1, tuple2.Item1) && Equals(tuple1.Item2, tuple2.Item2)) ||
        (Equals(tuple1.Item1, tuple2.Item2) && Equals(tuple1.Item2, tuple2.Item1));

        public int GetHashCode((ASTType, ASTType) obj)
        {
            return obj.Item1.GetHashCode() ^ obj.Item2.GetHashCode();
        }
    }

    public static class ASTTypeExtension
    {
        private static readonly Dictionary<(ASTType, ASTType), bool> arithmeticCompatibility = new(new ASTTypeComparer())
        {
            {(ASTType.Number,ASTType.Number), true },
            {(ASTType.Number,ASTType.String), false },
            {(ASTType.String,ASTType.String), false }
        };
        private static readonly Dictionary<(ASTType, ASTType), bool> relationalCompatibility = new(new ASTTypeComparer())
        {
            {(ASTType.Number,ASTType.Number), true },
            {(ASTType.Number,ASTType.String), false },
            {(ASTType.String,ASTType.String), true }
        };
        private static readonly Dictionary<(ASTType, ASTType), bool> logicalCompatibility = new(new ASTTypeComparer())
        {
            {(ASTType.Number,ASTType.Number), true },
            {(ASTType.Number,ASTType.String), false },
            {(ASTType.String,ASTType.String), false }
        };
        private static readonly Dictionary<(ASTType, ASTType), bool> assignmentCompatibility = new(new ASTTypeComparer())
        {
            {(ASTType.Number,ASTType.Number), true },
            {(ASTType.Number,ASTType.String), false },
            {(ASTType.String,ASTType.String), true }
        };

        private static readonly Dictionary<ASTTypeCompatibility, Dictionary<(ASTType, ASTType), bool>> compatibilityMap = new()
        {
            [ASTTypeCompatibility.Arithmetic] = arithmeticCompatibility,
            [ASTTypeCompatibility.Relational] = relationalCompatibility,
            [ASTTypeCompatibility.Logical] = logicalCompatibility,
            [ASTTypeCompatibility.Assignment] = assignmentCompatibility
        };

        public static bool isCompatible(this ASTType type1, ASTType type2, ASTTypeCompatibility compatibility) => compatibilityMap[compatibility][(type1, type2)];
    }
}
