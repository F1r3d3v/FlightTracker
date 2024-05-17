using Avalonia;
using ProjOb.Query.Invoker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query.AST
{
    public class ASTQueryVisitor : IQueryVisitorAST
    {
        public QueryResult? Result { get; private set; }

        private readonly QueryReceiver _receiver;
        private readonly QueryInvoker _invoker;

        public ASTQueryVisitor(Database db)
        {
            _receiver = new QueryReceiver(db);
            _invoker = new QueryInvoker();
        }

        public void Accept(DisplayNode node)
        {
            var command = new DisplayCommand(_receiver, node);
            _invoker.SetCommand(command);
            _invoker.InvokeCommand();
            Result = command.getResult();
        }
    }
}
