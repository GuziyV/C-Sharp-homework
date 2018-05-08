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
            var parking = Parking.Instance;
            while(true)
            {
                Console.WriteLine("enter");
                int i = Int32.Parse(Console.ReadLine());
                if(i == 1)
                {
                    parking.transactions.Add(new Transaction(2, 20));
                }
                else
                {
                    parking.ShowLastMinuteTransaction();
                }

            }
        }

    }
}
