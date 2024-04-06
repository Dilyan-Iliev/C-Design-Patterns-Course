namespace EndModuleTask
{
    public interface IRenderer
    {
        string WhatToRenderAs { get; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public abstract class Shape
    {
        private IRenderer rendering;

        protected Shape(IRenderer rendering)
        {
            this.rendering = rendering;
        }

        public string Name { get; set; }

        public override string ToString()
        {
            return $"Drawing {Name} as {rendering.WhatToRenderAs}";
        }
    }

    public class Triangle : Shape
    {
        public Triangle(IRenderer strategy) : base(strategy)
        {
            Name = "Triangle";
        }
    }

    public class Square : Shape
    {
        public Square(IRenderer rendering) : base(rendering)
        {
            Name = "Square";
        }
    }

    public class RasterRenderer : IRenderer
    {
        public string WhatToRenderAs
        {
            get => "pixels";
        }
    }

    public class VectorRenderer : IRenderer
    {
        public string WhatToRenderAs
        {
            get => "lines";
        }
    }
}
