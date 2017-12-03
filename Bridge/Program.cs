using System;

namespace Bridge
{ 
    public interface IRenderer
    {
        void RenderCircle(double radius);
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(double radius)
        {
            Console.WriteLine($"Rendering a vector circle with radius {radius}");
        }
    }

    public class PixelRenderer : IRenderer
    {
        public void RenderCircle(double radius)
        {
            Console.WriteLine($"Rendering a pixel circle with radius {radius}");
        }
    }

    public abstract class Shape
    {
        protected IRenderer renderer;
        protected Shape(IRenderer renderer)
        {
            if (renderer == null)
            {
                throw new ArgumentNullException();
            }

            this.renderer = renderer;
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private double radius;

        public Circle(IRenderer renderer, double radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var renderer = new VectorRenderer();
            var otherRenderer = new PixelRenderer();
            var circle = new Circle(renderer, 10.0);
            circle.Draw();
            var circle2 = new Circle(otherRenderer, 20.0);
            circle2.Draw();
        }
    }
}
