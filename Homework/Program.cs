using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Program
    {
        static void Main(string[] args)
        {
            while (Menu.Exit == false)
            {
                Menu.ChooseCommande();
                Console.WriteLine(new string('_', 10));
            }

        }
    }
}
