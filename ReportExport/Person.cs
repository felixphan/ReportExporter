using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportExport
{
    class Person
    {
        public int age { get; set; }

        public String name { get; set; }

        public String address { get; set; }

        public List<Child> children { get; set; }
        public class Child
        {
            public String name { get; set; }
            public int age { get; set; }
        }
    }
}
