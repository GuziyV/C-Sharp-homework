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
            if (Settings.Parking.Cars.Count >= Settings.ParkingSpace)
            {
                throw new NotEnoughSpaceException("Not enough space in the parking lot, please wait");
            }
            else
            {
                Cars.Add(car);
            }
        }

        public void AddTransaction(Transaction transaction)
        {
            Transactions.Add(transaction);
        }

        public void AddMoney(decimal amount)
        {
            Balance += amount;
        }

        public bool AddCarMoney(uint carId, decimal amount)
        {
            Car car = Cars.First<Car>(c => c.Id == carId);
            if (car == null)
            {
                return false;
            }
            car.AddMoney(amount);
            return true;
        }
        public void DeleteCar(uint carId)
        {
            Car car = Cars.First<Car>(c => c.Id == carId);
            Cars.Remove(car);
        }
        public decimal GetCarBalance(uint id)
        {
            Car car = Cars.First<Car>(c => c.Id == id);
            return car.Balance;     
        }

        public bool isIdOfCarExist(uint id)
        {
            Car car = Cars.FirstOrDefault<Car>(c => c.Id == id);
            if(car == null)
            {
                return false;
            }
            return true;
        }
    }
}
