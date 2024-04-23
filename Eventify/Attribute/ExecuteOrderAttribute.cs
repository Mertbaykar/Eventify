using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eventify.Attribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class ExecuteOrderAttribute : System.Attribute
    {
        public ExecuteOrderAttribute(int order)
        {
            Order = order;
        }

        public int Order { get; set; }
    }
}
