using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Transaction
    {
        public Transaction(uint carId, decimal withdraw)
        {
            TransactionTime = DateTime.Now;
            CarId = carId;
            Withdraw = withdraw;
        }

        public override string ToString()
        {
            return String.Format("id{0,-3}, Withdraw {1,-5} time {2,-10}", CarId, Withdraw, TransactionTime);
        }

        public readonly DateTime TransactionTime;

        public readonly uint CarId;

        public readonly decimal Withdraw;
    }
}
