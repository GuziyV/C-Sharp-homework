using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Parking
    {
        private static readonly Lazy<Parking> lazy = new Lazy<Parking>(() => new Parking());

        public static Parking Instance { get { return lazy.Value; } }

        private Parking()
        {
            Cars = new List<Car>();
            Transactions = new List<Transaction>();
        }

        public List<Car> Cars { get; private set; }
        public List<Transaction> Transactions { get; private set; }
        public decimal Balance { get; private set; }

        public void AddCar(Car car)
        {
            Cars.Add(car);
        }

        public void addTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public void AddMoney(decimal amount)
        {
            Balance += amount;
        }

        public bool AddCarMoney(uint carId, decimal value)
        {
            Car car = Cars.First<Car>(c => c.Id == carId);
            if (car == null)
            {
                return false;
            }
            car.AddMoney(value);
            return true;
        }
        public bool DeleteCar(uint carId)
        {
            Car car = Cars.First<Car>(c => c.Id == carId);
            if(car == null)
            {
                return false;
            }
            Cars.Remove(car);
            return true;
        }
        public decimal GetCarBalance(uint id)
        {
            Car car = Cars.First<Car>(c => c.Id == id);
            if(car == null)
            {
                throw new Exception("Car wasnt found");
            }
            else
            {
                return car.Balance;
            }
            
        }
    }
}
