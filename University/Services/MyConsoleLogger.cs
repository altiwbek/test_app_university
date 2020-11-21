using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace University.Services
{
    public class MyConsoleLogger : ILog
    {
        private string SomeField;
        public void info(string str)
        {
            if (SomeField == null)
                SomeField = "";
            SomeField += "\n" + str;

            
            Console.WriteLine(str);
            Console.WriteLine("**********Begining of SomeField*****************");
            

            Console.WriteLine(SomeField);

            Console.WriteLine("**********End of SomeField*****************");
        }
    }

}
