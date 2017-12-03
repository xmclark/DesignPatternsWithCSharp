using System;

namespace FactoryExercise
{

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}";
        }
    }

    public class PersonFactory
    {
        private int id = 0;
        public Person CreatePerson(string name)
        {
            return new Person { Id = id++, Name = name };
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            var factory = new PersonFactory();

            var john = factory.CreatePerson("John");
            var rachel = factory.CreatePerson("Rachel");

            var anotherFactory = new PersonFactory();
            var jose = anotherFactory.CreatePerson("Jose");

            Console.WriteLine(john);
            Console.WriteLine(rachel);

            Console.WriteLine(jose);


        }
    }
}
