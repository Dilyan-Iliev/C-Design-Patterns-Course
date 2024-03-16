namespace StepwiseBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var car = Builder
                .Create()
                //.Create returns ISpecificyCarType and this interface has only OfType method
                //which returns ISpecifyWheelSize with method .WithWheels
                //and WithWheels method reutnrs ICarBuilder interface with method .Build
                .OfType(CarType.Crossover)
                .WithWheels(17)
                .Build();

            //and this way we dont allow some memthod to be invoked before another method
        }
    }

    public enum CarType
    {
        Sedan, Crossover
    }

    public class Car
    {
        public CarType Type;
        public int WheelSize;
        //depending on the cartype we can only select certain wheelsize
        //that means that if we want to use Builder we would want to build the object in specific order
        //1st select the CarType and based on the CarType - select specific 
    }

    public interface ICarBuilder
    {
        Car Build();
    }
    public interface ISpecifyWheelSize
    {
        ICarBuilder WithWheels(int size);
    }
    public interface ISpecificyCarType
    {
        ISpecifyWheelSize OfType(CarType type);
    }

    public class Builder
    {
        private class Impl : ISpecificyCarType, ISpecifyWheelSize, ICarBuilder
        {
            private Car car = new();

            public Car Build()
            {
                return car;
            }

            public ISpecifyWheelSize OfType(CarType type)
            {
                car.Type = type;
                return this;
            }

            public ICarBuilder WithWheels(int size)
            {
                switch (car.Type)
                {
                    case CarType.Crossover when size < 17 || size > 20:
                    case CarType.Sedan when size < 15 || size > 17:
                        throw new ArgumentException($"Wrong size of wheels for {car.Type}");
                }

                car.WheelSize = size;
                return this;
            }
        }

        public static ISpecificyCarType Create()
        {
            return new Impl();
        }
    }
}
