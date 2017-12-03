using System;
using System.Collections.Generic;
using System.Linq;

namespace DependencyInversion
{
    // an interface that abstracts the lower level details, all the user cares about is this interface
    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // an enum describing genealogical relationship
    public enum Relationship
    {
        Parent, Child, Sibling
    }

    // simple person class, could have more data
    public class Person
    {
        public string Name;
    }

    // implements the IRelationshipBrowser
    public class Relationships : IRelationshipBrowser
    {
        // for all implementers of IRelationshipBrowser, they could use whatever underlying store they desired, not tied to collection type
        private List<(Person, Relationship, Person)> relations = new List<(Person, Relationship, Person)>();

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child));
            relations.Add((child, Relationship.Child, parent));
        }

        // implemented method
        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(x => x.Item1.Name == "John" && x.Item2 == Relationship.Parent).Select(r => r.Item3);
        }
    }

    public class Research
    {
        public Research(IRelationshipBrowser browser)
        {
            foreach(var r in browser.FindAllChildrenOf("John"))
            {
                Console.WriteLine($"John has a child named {r.Name}.");
            }
        }

        static void Main(string[] args)
        {
            var parent = new Person { Name = "John" };
            var child1 = new Person { Name = "Chris" };
            var child2 = new Person { Name = "Mary" };
            var relationships = new Relationships();
            relationships.AddParentAndChild(parent, child1);
            relationships.AddParentAndChild(parent, child2);
            new Research(relationships);
        }
    }
}
