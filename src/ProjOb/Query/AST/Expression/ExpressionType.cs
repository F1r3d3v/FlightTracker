namespace ProjOb.Query.AST.Expression
{
    public class ExpressionType
    {
        private double _doubleValue;
        public double DoubleValue
        {
            get
            {
                if (_type != ASTType.Number) throw new ArgumentException("Data type mismatch");
                return _doubleValue;
            }

            set
            {
                if (_type != ASTType.Number) throw new ArgumentException("Data type mismatch");
                _doubleValue = value;
            }
        }

        private String _stringValue = String.Empty;
        public String StringValue
        {
            get
            {
                if (_type != ASTType.String) throw new ArgumentException("Data type mismatch");
                return _stringValue;
            }

            set
            {
                if (_type != ASTType.String) throw new ArgumentException("Data type mismatch");
                _stringValue = value;
            }
        }

        private ASTType _type;

        public ExpressionType(ASTType type)
        {
            _type = type;
        }
    }
}
