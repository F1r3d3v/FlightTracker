using ProjOb.Accessors;
using ProjOb.Exceptions;

namespace ProjOb.Query.AST.Expression
{
    public class TypeCheckVisitor : IExpressionVisitorAST
    {
        private readonly Object _obj;
        private ASTType _type;
        public readonly Dictionary<ExpressionNode, ASTType> TypeTable = [];
        public readonly Dictionary<ExpressionNode, ExpressionType> ValueTable = [];

        public TypeCheckVisitor(Object obj)
        {
            _obj = obj;
        }

        internal class ASTOpTypeComparer : IEqualityComparer<(BinOpType, ASTType, ASTType)>
        {
            public bool Equals((BinOpType, ASTType, ASTType) tuple1, (BinOpType, ASTType, ASTType) tuple2) =>
            (Equals(tuple1.Item1, tuple2.Item1) && Equals(tuple1.Item2, tuple2.Item2) && Equals(tuple1.Item3, tuple2.Item3)) ||
            (Equals(tuple1.Item1, tuple2.Item1) && Equals(tuple1.Item2, tuple2.Item3) && Equals(tuple1.Item2, tuple2.Item2));

            public int GetHashCode((BinOpType, ASTType, ASTType) obj)
            {
                return obj.Item1.GetHashCode() ^ obj.Item2.GetHashCode() ^ obj.Item3.GetHashCode();
            }
        }

        public void Accept(IdentifierNode node)
        {
            IQueryAccessor accessor = _obj.Apply(new AccessorVisitor());
            String val = accessor.GetValue(node.Value)!;

            ExpressionType expType;
            if (double.TryParse(val, out double res))
            {
                _type = ASTType.Number;
                expType = new ExpressionType(ASTType.Number);
                expType.DoubleValue = res;
            }
            else
            {
                _type = ASTType.String;
                expType = new ExpressionType(ASTType.String);
                expType.StringValue = val;
            }

            TypeTable.Add(node, _type);
            ValueTable.Add(node, expType);
        }

        public void Accept(NumberNode node)
        {
            var expType = new ExpressionType(ASTType.Number);
            expType.DoubleValue = node.Value;
            _type = ASTType.Number;
            TypeTable.Add(node, _type);
            ValueTable.Add(node, expType);
        }

        public void Accept(StringNode node)
        {
            var expType = new ExpressionType(ASTType.String);
            expType.StringValue = node.Value;
            _type = ASTType.String;
            TypeTable.Add(node, _type);
            ValueTable.Add(node, expType);
        }

        public void Accept(BinOpNode node)
        {
            node.Left.Visit(this);
            ASTType type1 = _type;

            node.Right.Visit(this);
            ASTType type2 = _type;

            if (!type1.isCompatible(type2, BinOpNode.OpMap[node.Type]))
            {
                throw new QueryExecutionException("Incompatible types for binary operation");
            }

            _type = ASTType.Number;
            TypeTable.Add(node, _type);
        }

        public void Accept(UnOpNode node)
        {
            node.Arg.Visit(this);
            ASTType type1 = _type;

            if (type1 == ASTType.String)
            {
                throw new QueryExecutionException("Incompatible type for unary operation");
            }

            TypeTable.Add(node, _type);
        }
    }
}
