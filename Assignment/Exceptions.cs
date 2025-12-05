using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    public class OutofRange : Exception
    {
        public OutofRange(string message) : base(message)
        {
        }
    }

    public class InvalidInput : Exception
    {
        public InvalidInput(string message) : base(message)
        {
        }
    }
}
