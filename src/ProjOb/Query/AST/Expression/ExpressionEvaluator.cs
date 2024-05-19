using ProjOb.Exceptions;

namespace ProjOb.Query.AST.Expression
{
    public static class ExpressionEvaluator
    {
        public static double Evaluate(ExpressionNode node, Object obj)
        {
            TypeCheckVisitor typeChecker = new TypeCheckVisitor(obj);
            node.Visit(typeChecker);

            if (typeChecker.TypeTable[node] != ASTType.Number)
            {
                throw new QueryExecutionException("Where condition must be a bool");
            }

            ExpressionVisitor visitor = new ExpressionVisitor(obj, typeChecker);
            node.Visit(visitor);

            return visitor.Result!.DoubleValue;
        }
    }
}
