using System;
using System.IO;

namespace PrototypeExerciseWithExplicitInterface
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }

    public class Point : IPrototype<Point>
    {
        public int X, Y;

        public Point DeepCopy()
        {
            return new Point { X = this.X, Y = this.Y };
        }

        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            return new Line { Start = this.Start.DeepCopy(), End = this.End.DeepCopy() };
        }

        public override string ToString()
        {
            return $"{Start.ToString()} -> {End.ToString()}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var line = new Line { Start = new Point { X = 10, Y = 20 }, End = new Point { X = 800, Y = 600 } };
            var line2 = line.DeepCopy();

            line.Start = new Point { X = 24, Y = 32 };
            line.End = new Point { X = 24, Y = 32 };

            Console.WriteLine(line);
            Console.WriteLine(line2);
        }
    }
}
