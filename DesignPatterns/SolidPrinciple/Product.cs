using System;
using System.Collections.Generic;

// SOLID Principle: Open-Close
namespace DesignPatterns.SolidPrinciple
{
    class Product : IFilterable
    {
        public readonly string Name;
        public readonly Colour Colour;
        public readonly Size Size;

        public Product(string name, Colour colour, Size size)
        {
            if (name == null)
                throw new ArgumentNullException(paramName: nameof(name));

            Name = name;
            Colour = colour;
            Size = size;
        }

        public Colour GetColour()
        {
            return Colour;
        }

        public Size GetSize()
        {
            return Size;
        }
    }

    class Filter<T> : IFilter<T>
    {
        public IEnumerable<T> Search(IEnumerable<T> items, ISpecification<T> specification)
        {
            foreach (T item in items)
                if (specification.IsSatisfied(item))
                    yield return item;
        }
    }

    // Specifications
    class SizeSpecification : ISpecification<IFilterable>
    {
        private readonly Size Size;
        public SizeSpecification(Size size)
        {
            Size = size;
        }

        public bool IsSatisfied(IFilterable t)
        {
            return t.GetSize() == Size;
        }
    }

    class ColourSpecification : ISpecification<IFilterable>
    {
        private readonly Colour Colour;

        public ColourSpecification(Colour colour)
        {
            Colour = colour;
        }

        public bool IsSatisfied(IFilterable t)
        {
            return t.GetColour() == Colour;
        }
    }

    class AndSpecification<T> : ISpecification<T>
    {
        private readonly ISpecification<T> First;
        private readonly ISpecification<T> Second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            First = first;
            Second = second;
        }

        public bool IsSatisfied(T t)
        {
            return First.IsSatisfied(t) && Second.IsSatisfied(t);
        }
    }

    class ProductDemo
    {
        public static void Run()
        {
            IEnumerable<Product> products = new List<Product>()
            {
                {new Product(name: "Product 1", colour: Colour.Red, size: Size.Small)},
                {new Product(name: "Product 1", colour: Colour.Green, size: Size.Small)},
                {new Product(name: "Product 1", colour: Colour.Blue, size: Size.Small)},

                new Product(name: "Product 1", colour: Colour.Red, size: Size.Medium),
                new Product(name: "Product 1", colour: Colour.Green, size: Size.Medium),
                new Product(name: "Product 1", colour: Colour.Blue, size: Size.Medium),

                new Product(name: "Product 1", colour: Colour.Red, size: Size.Large),
                new Product(name: "Product 1", colour: Colour.Green, size: Size.Large),
                new Product(name: "Product 1", colour: Colour.Blue, size: Size.Large),

                new Product(name: "Product 1", colour: Colour.Red, size: Size.Huge),
                new Product(name: "Product 1", colour: Colour.Green, size: Size.Huge),
                new Product(name: "Product 1", colour: Colour.Blue, size: Size.Huge),
            };

            // Filter by colour
            Colour colour = Colour.Red;
            ISpecification<IFilterable> colourSpecification = new ColourSpecification(colour);
            IEnumerable<IFilterable> colourFilteredProducts = new Filter<IFilterable>().Search(items: products, specification: colourSpecification);
            Console.WriteLine("Filtered Products by colour: {0}", colour);
            foreach (Product filteredProduct in colourFilteredProducts)
            {
                Console.WriteLine("\tName: {0}\n\tColour: {1}\n\tSize: {2}\n\n", filteredProduct.Name, filteredProduct.Colour, filteredProduct.Size);
            }

            // Filter by size
            Size size = Size.Huge;
            ISpecification<IFilterable> sizeSpecification = new SizeSpecification(size);
            IEnumerable<IFilterable> sizeFilteredProducts = new Filter<IFilterable>().Search(items: products, specification: sizeSpecification);
            Console.WriteLine("Filtered Products by size: {0}", size);
            foreach (Product filteredProduct in sizeFilteredProducts)
            {
                Console.WriteLine("\tName: {0}\n\tColour: {1}\n\tSize: {2}\n\n", filteredProduct.Name, filteredProduct.Colour, filteredProduct.Size);
            }

            // Filter by both colour and size
            ISpecification<IFilterable> andSpecification =
                new AndSpecification<IFilterable>(colourSpecification, sizeSpecification);
            IEnumerable<IFilterable> colourAndSizeFilteredProducts = new Filter<IFilterable>().Search(items: products, specification: andSpecification);
            Console.WriteLine("Filtered Products by colour and size: {0} and {1}", colour, size);
            foreach (Product filteredProduct in colourAndSizeFilteredProducts)
            {
                Console.WriteLine("\tName: {0}\n\tColour: {1}\n\tSize: {2}\n\n", filteredProduct.Name, filteredProduct.Colour, filteredProduct.Size);
            }
        }
    }
}
