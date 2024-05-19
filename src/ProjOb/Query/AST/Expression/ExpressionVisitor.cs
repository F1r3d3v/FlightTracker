using ProjOb.Exceptions;
using ProjOb.Query.AST.Expression;
using ProjOb.Query.Wrappers;

namespace ProjOb.Query.AST
{
    public class ExpressionVisitor : IExpressionVisitorAST
    {
        private readonly Object _object;
        private readonly AccessorVisitor accessorComponent = new AccessorVisitor();
        private readonly TypeCheckVisitor _typeCheck;
        private ASTType _type;

        public ExpressionType? Result { get; private set; }

        public ExpressionVisitor(Object obj, TypeCheckVisitor TypeCheck)
        {
            _object = obj;
            _typeCheck = TypeCheck;
        }

        public void Accept(IdentifierNode node)
        {
            _type = _typeCheck.TypeTable[node];
            Result = _typeCheck.ValueTable[node];
        }

        public void Accept(NumberNode node)
        {
            _type = _typeCheck.TypeTable[node];
            Result = _typeCheck.ValueTable[node];
        }

        public void Accept(StringNode node)
        {
            _type = _typeCheck.TypeTable[node];
            Result = _typeCheck.ValueTable[node];
        }

        public void Accept(BinOpNode node)
        {
            node.Left.Visit(this);
            ASTType type1 = _type;
            ExpressionType expType1 = Result!;

            node.Right.Visit(this);
            ASTType type2 = _type;
            ExpressionType expType2 = Result!;

            ASTTypeCompatibility type = BinOpNode.OpMap[node.Type];
            _type = ASTType.Number;
            Result = new ExpressionType(ASTType.Number);

            if (type == ASTTypeCompatibility.Arithmetic)
            {
                Result.DoubleValue = node.Type switch
                {
                    BinOpType.Add => BinOpNode.Add(expType1.DoubleValue, expType2.DoubleValue),
                    BinOpType.Subtract => BinOpNode.Subtract(expType1.DoubleValue, expType2.DoubleValue),
                    BinOpType.Multiply => BinOpNode.Multiply(expType1.DoubleValue, expType2.DoubleValue),
                    BinOpType.Divide => BinOpNode.Divide(expType1.DoubleValue, expType2.DoubleValue),
                    _ => throw new QueryExecutionException("Invalid Operation")
                };
            }
            else if (type == ASTTypeCompatibility.Relational)
            {
                if (type1 == ASTType.Number)
                {
                    bool res = node.Type switch
                    {
                        BinOpType.Greater => BinOpNode.Greater(expType1.DoubleValue, expType2.DoubleValue),
                        BinOpType.GreaterEqual => BinOpNode.GreaterEqual(expType1.DoubleValue, expType2.DoubleValue),
                        BinOpType.Less => BinOpNode.Less(expType1.DoubleValue, expType2.DoubleValue),
                        BinOpType.LessEqual => BinOpNode.LessEqual(expType1.DoubleValue, expType2.DoubleValue),
                        BinOpType.Equal => BinOpNode.Equal(expType1.DoubleValue, expType2.DoubleValue),
                        BinOpType.NotEqual => BinOpNode.NotEqual(expType1.DoubleValue, expType2.DoubleValue),
                        _ => throw new QueryExecutionException("Invalid Operation")
                    };
                    Result.DoubleValue = res ? 1.0 : 0.0;
                }
                else if (type1 == ASTType.String)
                {
                    bool res = node.Type switch
                    {
                        BinOpType.Greater => BinOpNode.Greater(expType1.StringValue, expType2.StringValue),
                        BinOpType.GreaterEqual => BinOpNode.GreaterEqual(expType1.StringValue, expType2.StringValue),
                        BinOpType.Less => BinOpNode.Less(expType1.StringValue, expType2.StringValue),
                        BinOpType.LessEqual => BinOpNode.LessEqual(expType1.StringValue, expType2.StringValue),
                        BinOpType.Equal => BinOpNode.Equal(expType1.StringValue, expType2.StringValue),
                        BinOpType.NotEqual => BinOpNode.NotEqual(expType1.StringValue, expType2.StringValue),
                        _ => throw new QueryExecutionException("Invalid Operation")
                    };
                    Result.DoubleValue = res ? 1.0 : 0.0;
                }
            }
            else if (type == ASTTypeCompatibility.Logical)
            {
                if (node.Type == BinOpType.CondAnd)
                {
                    Result.DoubleValue = BinOpNode.CondAnd(expType1.DoubleValue, expType2.DoubleValue) ? 1.0 : 0.0;
                }
                else if (node.Type == BinOpType.CondOr)
                {
                    Result.DoubleValue = BinOpNode.CondOr(expType1.DoubleValue, expType2.DoubleValue) ? 1.0 : 0.0;
                }
            }
            else
            {
                throw new QueryExecutionException("Invalid Operation");
            }
        }

        public void Accept(UnOpNode node)
        {
            node.Arg.Visit(this);
            ASTType type = _type;
            ExpressionType expType = Result!;

            if (node.Type == UnOpType.Negation)
            {
                expType.DoubleValue = -expType.DoubleValue;
                Result = expType;
            }
            else if (node.Type == UnOpType.CondNot)
            {
                expType.DoubleValue = (expType.DoubleValue != 0) ? 0.0 : 1.0;
            }
        }
    }
}
