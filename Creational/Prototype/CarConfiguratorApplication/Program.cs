namespace CarConfiguratorApplication
{
    //Prototype - ICarPrototype Interface
    public interface ICarPrototype
    {
        ICarPrototype Clone();
    }

    //Concrete Prototype - Car Class
    public class Car : ICarPrototype
    {
        public string Model { get; set; }
        public string Color { get; set; }
        public string Engine { get; set; }
        public bool Sunroof { get; set; }

        public ICarPrototype Clone()
        {
            // Using MemberwiseClone for simplicity, which is a shallow copy.
            // For complex objects, you might need to implement a deep copy.
            return (ICarPrototype)this.MemberwiseClone();
        }

        public override string ToString()
        {
            return $"{Model} | Color: {Color} | Engine: {Engine} | Sunroof: {Sunroof}";
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Create an initial car configuration
            Car prototypeCar = new Car
            {
                Model = "Sedan",
                Color = "Blue",
                Engine = "V6",
                Sunroof = true
            };

            Console.WriteLine("Original Car Configuration:");
            Console.WriteLine(prototypeCar);

            // Now clone the prototype and make modifications for a new configuration
            Car clonedCar = (Car)prototypeCar.Clone();
            clonedCar.Color = "Red";
            clonedCar.Sunroof = false;
            clonedCar.Engine = "V8";

            Console.WriteLine("\nCloned and Modified Car Configuration:");
            Console.WriteLine(clonedCar);

            Console.ReadKey();
        }
    }
}
