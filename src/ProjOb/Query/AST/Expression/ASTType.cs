using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public enum ASTType
    {
        Double,
        Integer,
        String
    }

    static class TypeExtensions
    {
        public static bool isCompatible(this ASTType type1, ASTType type2)
        {
            return false;
        }
    }
}
