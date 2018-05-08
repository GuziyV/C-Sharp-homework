using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Parking
    {
        private List<Car> cars;
        private List<Transaction> transactions;
        public decimal Balance { get; private set; }

        public void AddMoney(decimal amount)
        {
            Balance += amount;
        }
    }
}
