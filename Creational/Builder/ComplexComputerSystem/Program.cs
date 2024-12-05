namespace ComplexComputerSystem
{
    public class Computer
    {
        public string CPU { get; set; }
        public string RAM { get; set; }
        public string HardDrive { get; set; }
        public string GraphicsCard { get; set; }
        public string SoundCard { get; set; }

        public void DisplaySpecifications()
        {
            Console.WriteLine($"CPU: {CPU}");
            Console.WriteLine($"RAM: {RAM}");
            Console.WriteLine($"HardDrive: {HardDrive}");
            Console.WriteLine($"GraphicsCard: {GraphicsCard ?? "Not present"}");
            Console.WriteLine($"SoundCard: {SoundCard ?? "Not present"}");
        }
    }

    // Builder (Abstract).
    public abstract class ComputerBuilder
    {
        protected Computer Computer { get; private set; } = new Computer();

        public abstract void SetCPU();
        public abstract void SetRAM();
        public abstract void SetHardDrive();
        public virtual void SetGraphicsCard() { }
        public virtual void SetSoundCard() { }

        public Computer GetComputer() => Computer;
    }

    // Concrete Builder.
    public class GamingComputerBuilder : ComputerBuilder
    {
        public override void SetCPU()
        {
            Computer.CPU = "High Performance CPU";
        }
        public override void SetRAM()
        {
            Computer.RAM = "32 GB DDR4";
        }
        public override void SetHardDrive()
        {
            Computer.HardDrive = "2 TB SSD";
        }
        public override void SetGraphicsCard()
        {
            Computer.GraphicsCard = "High-end Graphics Card Gen 2";
        }
        public override void SetSoundCard()
        {
            Computer.SoundCard = "7.1 Surround Sound Card";
        }
    }

    // Concrete Builder.
    public class WorkingComputerBuilder : ComputerBuilder
    {
        public override void SetCPU()
        {
            Computer.CPU = "High Performance CPU";
        }
        public override void SetRAM()
        {
            Computer.RAM = "16 GB DDR4";
        }
        public override void SetHardDrive()
        {
            Computer.HardDrive = "1 TB SSD";
        }
        public override void SetGraphicsCard()
        {
            Computer.GraphicsCard = "High-end Graphics Card Gen 1";
        }
        public override void SetSoundCard()
        {
            Computer.SoundCard = "5.1 Surround Sound Card";
        }
    }

    public class ComputerShop
    {
        public void ConstructComputer(ComputerBuilder builder)
        {
            builder.SetCPU();
            builder.SetRAM();
            builder.SetHardDrive();
            builder.SetGraphicsCard();
            builder.SetSoundCard();
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var shop = new ComputerShop();

            Console.WriteLine("Gaming Computer Specifications:");
            ComputerBuilder builder = new GamingComputerBuilder();
            shop.ConstructComputer(builder);
            Computer computer = builder.GetComputer();
            computer.DisplaySpecifications();

            Console.WriteLine("\nWorking Computer Specifications:");
            builder = new WorkingComputerBuilder();
            shop.ConstructComputer(builder);
            computer = builder.GetComputer();
            computer.DisplaySpecifications();

            Console.ReadLine();
        }
    }
}
