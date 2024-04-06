namespace Bridge
{
    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Bridge pattern is simply the concept of connecting different components through abstractions

            IRenderer renderer = new RasterRenderer();
            var circle = new Circle(renderer, 5);
            circle.Draw();
            circle.Resize(2);
            circle.Draw();
        }
    }

    public class VectorRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing a pixels of circle with radius {radius}");
        }
    }

    public abstract class Shape
    {
        //Here is where the Bridge comes in
        protected IRenderer renderer; //the bridge between the specific shape and whoever is drawing it

        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        //some additional methods
        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    //an actual shape
    public class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer, float radius)
            : base(renderer)
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
}
