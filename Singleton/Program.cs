using MoreLinq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string cityName);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> capitals = new Dictionary<string, int>();

        // explicitly private constructor
        private SingletonDatabase()
        {
            Console.WriteLine("Initializing database.");
            capitals = File.ReadAllLines("capitals.txt").Batch(2).ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1))
                );
        }

        public int GetPopulation(string cityName)
        {
            return capitals[cityName];
        }

        // a private static member
        //private static SingletonDatabase instance = new SingletonDatabase();

        // also can be lazy!
        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());

        // access member by reference
        //public static SingletonDatabase Instance => instance;
        public static SingletonDatabase Instance => instance.Value;
    }


    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("Started database application.");

            var db = SingletonDatabase.Instance;
            var city = "Jakarta";

            Console.WriteLine($"Getting {city} population...");
            var pop = db.GetPopulation(city);


            Console.WriteLine($"{city} has population of {pop/10} million.");
        }
    }
}
