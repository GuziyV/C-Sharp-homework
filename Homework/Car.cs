using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Car
    {
        public Car(uint id, CarType type, int balance = 0)
        {
            Id = id;
            Type = type;
            Balance = balance;

        }
        public uint Id { get; private set; }

        public readonly CarType Type;

        public int Balance { get; private set; }
    }

    enum CarType
    {
        Passenger,
        Truck,
        Bus,
        Motorcycle
    }
}
