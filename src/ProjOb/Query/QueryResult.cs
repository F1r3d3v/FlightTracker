using ProjOb.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjOb.Query
{
    public class QueryResult
    {
        private readonly List<Dictionary<String, String>> _result;

        public QueryResult(List<Dictionary<string, string>> result)
        {
            _result = result;
        }

        public void Display()
        {
            ITable table = new TableDecorator(new Table());
            if (_result.Count == 0)
            {
                Console.WriteLine("Output is empty");
                return;
            }

            foreach (var key in _result[0].Keys)
            {
                table.AddColumn(key);
            }

            foreach (var rec in _result)
            {
                table.AddRow([.. rec.Values]);
            }

            Console.WriteLine("\nQuery Output:");
            table.Display();
        }
    }
}
