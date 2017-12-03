using System;
using System.Collections.Generic;
using System.Linq;

namespace AbstractFactoryWithReflection
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
        private List<Tuple<string, IHotDrinkFactory>> factories = new List<Tuple<string, IHotDrinkFactory>>();
        public HotDrinkMachine()
        {
            foreach (var t in typeof(HotDrinkMachine).Assembly.GetTypes())
            {
                // check if is an IHotDrinkFactory and is NOT an Interface
                if (typeof(IHotDrinkFactory).IsAssignableFrom(t) && !t.IsInterface)
                {
                    // create an instance with Activator
                    factories.Add(Tuple.Create(
                        t.Name.Replace("Factory", string.Empty),
                        (IHotDrinkFactory)Activator.CreateInstance(t)
                        )
                        );
                }
            }
        }

        public void MakeDrink(string drink, int amount)
        {
            // find the factory and call Prepare
            factories.Where((Tuple<string, IHotDrinkFactory> pair) => (pair.Item1 == drink)).First().Item2.Prepare(amount);
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            HotDrinkMachine machine = new HotDrinkMachine();
            machine.MakeDrink("Tea", 1);
        }
    }
}
