using System.Collections.Generic;
using System.Linq;
using static System.Console;

// SOLID Principle: Dependency Inversion
namespace DesignPatterns.SolidPrinciple
{
    // Low level module
    class Relationships<IRelationshipable> : IRelationshipsBrowser<IRelationshipable>
    {
        private readonly List<(IRelationshipable, Relationship, IRelationshipable)> _relations = new ();

        public void AddParentAndChild(IRelationshipable parent, IRelationshipable child)
        {
            _relations.Add((parent, Relationship.Parent, child));
            _relations.Add((child, Relationship.Child, parent));
        }

        public void AddSibling(IRelationshipable referenceSibling, IRelationshipable sibling)
        {
            _relations.Add((referenceSibling, Relationship.Sibling, sibling));
            _relations.Add((sibling, Relationship.Sibling, referenceSibling));
        }

        public void AddSpouse(IRelationshipable referenceSpouse, IRelationshipable spouse)
        {
            _relations.Add((referenceSpouse, Relationship.Spouse, spouse));
            _relations.Add((spouse, Relationship.Spouse, referenceSpouse));
        }

        public IEnumerable<IRelationshipable> FindAllChildrenOf(IRelationshipable parent)
        {
            foreach (var relation in _relations)
            {
                if (relation.Item1.Equals(parent) && relation.Item2 == Relationship.Parent)
                    yield return relation.Item3;
            }
        }

        public IEnumerable<IRelationshipable> FindAllParentsOf(IRelationshipable child)
        {
            foreach (var relation in _relations)
            {
                if (relation.Item1.Equals(child) && relation.Item2 == Relationship.Child)
                    yield return relation.Item3;
            }
        }

        public IEnumerable<IRelationshipable> FindAllSiblingsOf(IRelationshipable sibling)
        {
            foreach (var relation in _relations)
            {
                if (relation.Item1.Equals(sibling) && relation.Item2 == Relationship.Sibling)
                    yield return relation.Item3;
            }
        }

        public IRelationshipable FindSpouseOf(IRelationshipable spouse)
        {
            return _relations
                .First(relation => relation.Item1.Equals(spouse) && relation.Item2 == Relationship.Spouse)
                .Item3;
        }
    }

    // Another low level module
    class Person : IRelationshipable
    {
        private readonly string Name;

        public Person(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return Name;
        }
    }

    // Yet another low level module
    class DOM : IRelationshipable
    {
        private readonly string Name;

        public DOM(string name)
        {
            Name = name;
        }

        public string GetName()
        {
            return $"Dom Element {Name}";
        }
    }

    // High level
    class Research
    {
        // Depends on low-level Relationships only via the
        // IRelationshpsBrowser interface's implemented methods.
        private readonly IRelationshipsBrowser<IRelationshipable> _relationshipsBrowser;

        public Research(IRelationshipsBrowser<IRelationshipable> relationshipsBrowser)
        {
            _relationshipsBrowser = relationshipsBrowser;
        }

        public void GetChildren(IRelationshipable parent)
        {
            foreach (var child in _relationshipsBrowser.FindAllChildrenOf(parent))
            {
                WriteLine("{0} has a child called {1}", parent.GetName(), child.GetName());
            }
        }

        public void GetParents(IRelationshipable child)
        {
            foreach (var parent in _relationshipsBrowser.FindAllParentsOf(child))
            {
                WriteLine("{0} has a parent called {1}", child.GetName(), parent.GetName());
            }
        }

        public void GetSiblings(IRelationshipable referenceSibling)
        {
            foreach (var sibling in _relationshipsBrowser.FindAllSiblingsOf(referenceSibling))
            {
                WriteLine("{0} has a sibling called {1}", referenceSibling.GetName(), sibling.GetName());
            }
        }

        public void GetSpouse(IRelationshipable referenceSpouse)
        {
            IRelationshipable spouse = _relationshipsBrowser.FindSpouseOf(referenceSpouse);
            WriteLine("{0} has a spouse called {1}", referenceSpouse.GetName(), spouse.GetName());
        }
    }

    class RelationshipsDemo
    {
        public static void Run()
        {
            Relationships<IRelationshipable> relationships = new Relationships<IRelationshipable>();
            Research research = new Research(relationships);

            // A low level module that implements IRelationshipable: Person
            Person amy = new Person(name: "Amy");
            Person barbara = new Person(name: "Barbara");
            Person daniel = new Person(name: "Daniel");
            relationships.AddParentAndChild(barbara, amy);
            relationships.AddParentAndChild(daniel, amy);
            relationships.AddSpouse(barbara, daniel);

            research.GetParents(amy);
            research.GetSiblings(amy);
            research.GetChildren(barbara);
            research.GetChildren(daniel);
            research.GetSpouse(barbara);
            research.GetSpouse(daniel);

            // Another low level module that implements IRelationshipable: DOM
            DOM html = new DOM(name: "HTML");
            DOM head = new DOM(name: "HEAD");
            DOM title = new DOM(name: "TITLE");
            DOM body = new DOM(name: "BODY");
            relationships.AddParentAndChild(html, head);
            relationships.AddParentAndChild(head, title);
            relationships.AddParentAndChild(html, body);
            relationships.AddSibling(head, body);

            research.GetParents(body);
            research.GetSiblings(head);
            research.GetSiblings(body);
            research.GetChildren(html);
            research.GetChildren(head);
        }
    }
}
