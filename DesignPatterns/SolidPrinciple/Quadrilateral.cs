using static System.Console;

// SOLID Principle: Liskov Substitution
namespace DesignPatterns.SolidPrinciple
{
    class Quadrilateral
    {
        public int TopSide { get; set; }
        public int RightSide { get; set; }
        public int BottomSide { get; set; }
        public int LeftSide { get; set; }
        public virtual string QuadrilateralType { get; set; } = "Quadrilateral";
        public Quadrilateral() {}

        public Quadrilateral(int topSide, int rightSide, int bottomSide, int leftSide)
        {
            TopSide = topSide;
            RightSide = rightSide;
            BottomSide = bottomSide;
            LeftSide = leftSide;
        }
    }

    class Rectangle : Quadrilateral
    {
        public int Length { get; set; }
        public int Breadth { get; set; }
        public override string QuadrilateralType { get; set; } = "Rectangle";

        public Rectangle() { }

        public Rectangle(int length, int breadth)
        {
            TopSide = BottomSide = Length = length;
            RightSide = LeftSide = Breadth = breadth;
        }

        public override string ToString()
        {
            return $"Dimension:\n\t\t{nameof(Length)}: {Length}\n\t\t{nameof(Breadth)}: {Breadth}\n\t{nameof(Area)}: {Area()}";
        }

        public int Area() => Length * Breadth;
    }

    class Square : Rectangle
    {
        public override string QuadrilateralType { get; set; } = "Square";
        public Square(int length)
        {
            Length = length;
            Breadth = length;
        }
    }

    public class RectangleDemo
    {
        public static void Run()
        {
            Quadrilateral rectangle = new Rectangle(20, 30);
            Rectangle smallSquare = new Square(10);
            Quadrilateral bigSquare = new Square(50);
            WriteLine("This is a {1} with the following properties:\n\t{0}", rectangle, rectangle.QuadrilateralType);
            WriteLine("This is a {1} with the following properties:\n\t{0}", smallSquare, smallSquare.QuadrilateralType);
            WriteLine("This is a {1} with the following properties:\n\t{0}", bigSquare, bigSquare.QuadrilateralType);
        }
    }
}
