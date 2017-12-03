using System;
using System.Collections.Generic;

namespace AbstractFactory
{
    // let the IHotDrink and implementations of this interface be internal
    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Yum tea.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("Coffee is sensational!");
        }
    }


    // a hot drink factory interface and implement for the available drinks
    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine("Some tea is ready.");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine("Coffee has been made!");
            return new Coffee();
        }
    }

    // the hot drink machine lets maintains a dictionary of availale drinks to factories
    public class HotDrinkMachine
    {
        public enum AvailableDrink
        {
            Coffee, Tea
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> factories = new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                // construct a string of the fully qualified type name of the factory class then create an instance from it
                var factory = (IHotDrinkFactory)Activator.CreateInstance(
                    Type.GetType("AbstractFactory." + Enum.GetName(typeof(AvailableDrink), drink) + "Factory")
                    );
                factories.Add(drink, factory);
            }
        }

        // one can get a hot drink by calling the method with the specified drink enum and the amount
        // will call the prepare method on the correect factory
        public IHotDrink PrepareDrink(AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            HotDrinkMachine machine = new HotDrinkMachine();
            machine.PrepareDrink(HotDrinkMachine.AvailableDrink.Coffee, 1);
        }
    }
}
