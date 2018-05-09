using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Menu
    {//TODO wrong parse exception
        public static void ChooseCommande()
        {
            Console.WriteLine("Choose commande: ");
            Console.WriteLine("1. Add car");
            Console.WriteLine("2. Add car balance(by id)");
            Console.WriteLine("3. Delete car");
            Console.WriteLine("4. Output last minute transaction history");
            Console.WriteLine("5. Get earnings");
            Console.WriteLine("6. Get free places");
            Console.WriteLine("7. Output Transactions.log");
            int commande = Int32.Parse(Console.ReadLine());
            switch(commande)
            {
                case 1:
                    addCar();
                    break;
                case 2:
                    addBalance();
                    break;
                case 3:
                    deleteCar();
                    break;
                case 4:
                    showLastMinuteTransactions();
                    break;

            }

        }
        private static void addCar()
        {
            Console.WriteLine("enter Car Id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            Console.WriteLine("enter type of car(Passenger/Truck/Bus/Motorcycle): ");
            string type = Console.ReadLine();
            Console.WriteLine("Enter car balance: ");
            decimal balance = Decimal.Parse(Console.ReadLine());
            switch (type)
            {
                case "Passenger":
                    Settings.Parking.AddCar(new Car(id, CarType.Passenger, balance));
                    break;
                case "Truck":
                    Settings.Parking.AddCar(new Car(id, CarType.Truck, balance));
                    break;
                case "Bus":
                    Settings.Parking.AddCar(new Car(id, CarType.Bus, balance));
                    break;
                case "Motorcycle":
                    Settings.Parking.AddCar(new Car(id, CarType.Motorcycle, balance));
                    break;
                default:
                    throw new Exception(); //TODO my own exception
            }
        }
        private static void addBalance()
        {
            Console.WriteLine("enter car id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            Console.WriteLine("enter amount: ");
            decimal amount = Decimal.Parse(Console.ReadLine());
            if(Settings.Parking.AddCarMoney(id, amount))
            {
                Console.WriteLine("Added");
            }
            else
            {
                Console.WriteLine("Not found such car");
            }
        }

        private static void deleteCar()
        {
            Console.WriteLine("enter car id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            if (Settings.Parking.DeleteCar(id))
            {
                Console.WriteLine("Deleted");
            }
            else
            {
                Console.WriteLine("Not found such car");
            }
        }

        public void showParkingBalance() => Console.WriteLine(Settings.Parking.Balance);

        public static void showLastMinuteTransactions()
        {
            var lastMinuteTransactins = Settings.Parking.Transactions.
                Where<Transaction>(t => DateTime.Now - t.TransactionTime < new TimeSpan(0, 1, 0));
            foreach (var transaction in lastMinuteTransactins)
            {
                Console.WriteLine(transaction);
            }
        }
        
    }
}
