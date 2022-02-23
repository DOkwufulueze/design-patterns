using System.Collections.Generic;

namespace DesignPatterns.SolidPrinciple
{
    interface ISizable
    {
        public Size GetSize();
    }

    interface IColourable
    {
        public Colour GetColour();
    }

    interface IFilterable : IColourable, ISizable { }

    interface ISpecification<T>
    {
        public bool IsSatisfied(T t);
    }

    interface IFilter<T>
    {
        public IEnumerable<T> Search(IEnumerable<T> items, ISpecification<T> specification);
    }

    interface IPrinter
    {
        public void Print(Document document);
    }

    interface IScanner
    {
        public void Scan(Document document);
    }

    interface ICopier
    {
        public void Copy(Document document);
    }

    interface IFaxer
    {
        public void Fax(Document document);
    }

    interface IMultiFunctionMachine : IPrinter, IScanner, ICopier, IFaxer { }

    interface IRelationshipsBrowser<IRelationshipable>
    {
        public IEnumerable<IRelationshipable> FindAllChildrenOf(IRelationshipable parent);
        public IEnumerable<IRelationshipable> FindAllParentsOf(IRelationshipable child);
        public IEnumerable<IRelationshipable> FindAllSiblingsOf(IRelationshipable sibling);
        public IRelationshipable FindSpouseOf(IRelationshipable spouse);
    }

    interface IRelationshipable
    {
        public string GetName();
    }
}
