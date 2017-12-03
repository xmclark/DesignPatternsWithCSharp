using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace PrototypeExercise
{
    public static class ExtensionMethods
    {
        // an extension method for all objects
        public static T DeepCopy<T>(this T self)
        {
            // serialize using the binary serializer
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }
    }

    // all types have to have this attribute
    [Serializable]
    public class Point
    {
        public int X, Y;
        public override string ToString()
        {
            return $"({X},{Y})";
        }
    }

    [Serializable]
    public class Line
    {
        public Point Start, End;

        public Line DeepCopy()
        {
            return this.DeepCopy<Line>();
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
