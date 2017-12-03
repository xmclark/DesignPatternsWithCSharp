using System;

namespace InnerFactory
{
    public class Point
    {
        private double x, y;

        public static Point Origin = new Point(0, 0);

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }

        public static class Factory
        {
            public static Point NewCartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point NewPolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Point.Factory.NewCartesianPoint(100.0, 200.0);
            Point.Factory.NewPolarPoint(Math.PI / 2.0, Math.PI / 3.0);
        }
    }
}
