﻿using ProjOb.Query.AST;

namespace ProjOb.Query
{
    public class QueryStatement
    {
        private readonly String _query;
        private readonly Database _db;

        public QueryStatement(String query, Database db)
        {
            _query = query;
            _db = db;
        }

        public QueryResult? Execute()
        {
            Lexer l = new Lexer(_query);
            Parser p = new Parser(l);
            QueryNode root = p.Parse();

            QueryVisitor visitor = new QueryVisitor(_db);
            root.Visit(visitor);

            return visitor.Result;
        }
    }
}
