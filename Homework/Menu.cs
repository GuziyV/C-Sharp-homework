using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    static class Menu
    {
        public static bool ExitProgram { get; private set; } = false;
        public static void ChooseCommande()
        {
            Console.WriteLine("Choose commande");
            Console.WriteLine("1. Add car");
            Console.WriteLine("2. Add car balance(by id)");
            Console.WriteLine("3. Delete car(by id)");
            Console.WriteLine("4. Output last minute transaction history");
            Console.WriteLine("5. Show parking profit");
            Console.WriteLine("6. Show last minute profit");
            Console.WriteLine("7. Output Transactions.log");
            Console.WriteLine("8. Show car balance(by id)");
            Console.WriteLine("9. Show number of free places");
            Console.WriteLine("10. Show number of cars in a parking lot");
            Console.WriteLine("11. Exit");
            int commande = Int32.Parse(Console.ReadLine());
            switch (commande)
            {
                case 1:
                    AddCar();
                    break;
                case 2:
                    AddBalance();
                    break;
                case 3:
                    RemoveCar();
                    break;
                case 4:
                    ShowLastMinuteTransactions();
                    break;
                case 5:
                    ShowParkingBalance();
                    break;
                case 6:
                    ShowLastMinuteEarnings();
                    break;
                case 7:
                    OutputTransactions();
                    break;
                case 8:
                    ShowCarBalance();
                    break;
                case 9:
                    ShowFreeSpaces(); 
                    break;
                case 10:
                    ShowNumberOfCars();
                    break;
                case 11:
                    Exit();
                    break;
                default:
                    throw new WrongTypeOfCarException("Unknown command");
            }
        }
        private static void AddCar()
        {
            Console.WriteLine("enter Car Id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            if (Parking.Instance.IsIdOfCarExist(id))
            {
                throw new WrongCommandException("Id is already exists");
            }
            Console.WriteLine("enter type of car(Passenger/Truck/Bus/Motorcycle): ");
            string type = Console.ReadLine();
            CarType cType;

            switch (type.ToLower())
            {
                case "passenger":
                    cType = CarType.Passenger;
                    break;
                case "truck":
                    cType = CarType.Truck;
                    break;
                case "bus":
                    cType = CarType.Bus;
                    break;
                case "motorcycle":
                    cType = CarType.Motorcycle;
                    break;
                default:
                    throw new WrongTypeOfCarException("Wrong type of car");
            }
            Console.WriteLine("Enter car balance: ");
            decimal balance = Decimal.Parse(Console.ReadLine());
            Parking.Instance.AddCar(new Car(id, cType, balance));
        }
        private static void AddBalance()
        {
            Console.WriteLine("enter car id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            if (!Parking.Instance.IsIdOfCarExist(id))
            {
                throw new WrongCommandException("Can' t find a car with such id");
            }
            Console.WriteLine("enter amount: ");
            decimal amount = Decimal.Parse(Console.ReadLine());
            Parking.Instance.AddCarMoney(id, amount);
            Console.WriteLine("**Added**");

        }

        private static void RemoveCar()
        {
            Console.WriteLine("enter car id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            if (Parking.Instance.GetCarBalance(id) < 0)
            {
                throw new NotEnoughMoneyException("You havent enough money");
            }
            Parking.Instance.DeleteCar(id);
            Console.WriteLine("**Removed**");

        }

        private static void ShowLastMinuteTransactions()
        {
            var lastMinuteTransactins = Parking.Instance.GetLastMinuteTransactions();
            foreach (var transaction in lastMinuteTransactins)
            {
                Console.WriteLine(transaction);
            }
        }
        private static void Exit()
        {
            ExitProgram = true;
            Parking.Instance.Dispose();
        }

        private static void ShowFreeSpaces()
        {
            Console.WriteLine("**{0} free**", Settings.ParkingSpace - Parking.Instance.GetNumberOfCars());
        }

        private static void ShowCarBalance()
        {
            Console.WriteLine("enter id: ");
            uint id = UInt32.Parse(Console.ReadLine());
            Console.WriteLine("**Balance: {0}**", Parking.Instance.GetCarBalance(id));
        }

        private static void OutputTransactions()
        {
            using (StreamReader sr = new StreamReader("Transactions.log", System.Text.Encoding.Default))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                }
            }
        }
        private static void ShowLastMinuteEarnings()
        {
            var lastMinuteTransactins = Parking.Instance.GetLastMinuteTransactions();
            decimal sum = 0;
            foreach (var transaction in lastMinuteTransactins)
            {
                sum += transaction.Withdraw;
            }
            Console.WriteLine("**{0}**", sum);
        }
        private static void ShowNumberOfCars() => Console.WriteLine("**{0}**", Parking.Instance.GetNumberOfCars());

        private static void ShowParkingBalance() => Console.WriteLine("**{0}**", Parking.Instance.Balance);
    }
}
