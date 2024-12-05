namespace ComputerSystemComponentHierarchy
{
    // Element Interface
    public interface IComputerComponent
    {
        void Accept(IComputerVisitor visitor);
    }

    // Concrete Elements
    public class Monitor : IComputerComponent
    {
        public void Accept(IComputerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class Keyboard : IComputerComponent
    {
        public void Accept(IComputerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    public class CPU : IComputerComponent
    {
        public void Accept(IComputerVisitor visitor)
        {
            visitor.Visit(this);
        }
    }

    // Visitor Interface
    public interface IComputerVisitor
    {
        void Visit(Monitor monitor);
        void Visit(Keyboard keyboard);
        void Visit(CPU cpu);
    }

    // Concrete Visitor for Status
    public class StatusVisitor : IComputerVisitor
    {
        public void Visit(Monitor monitor)
        {
            Console.WriteLine("Checking the monitor's status...");
        }

        public void Visit(Keyboard keyboard)
        {
            Console.WriteLine("Checking the keyboard's status...");
        }

        public void Visit(CPU cpu)
        {
            Console.WriteLine("Checking the CPU's status...");
        }
    }

    // Concrete Visitor for Diagnostics
    public class DiagnosticVisitor : IComputerVisitor
    {
        public void Visit(Monitor monitor)
        {
            Console.WriteLine("Running diagnostics on the monitor...");
        }

        public void Visit(Keyboard keyboard)
        {
            Console.WriteLine("Running diagnostics on the keyboard...");
        }

        public void Visit(CPU cpu)
        {
            Console.WriteLine("Running diagnostics on the CPU...");
        }
    }

    // Testing the Visitor Design Pattern
    // Client Code
    public class Program
    {
        public static void Main()
        {
            IComputerComponent[] components = new IComputerComponent[] {
                new Monitor(),
                new Keyboard(),
                new CPU()
            };

            IComputerVisitor statusVisitor = new StatusVisitor();
            IComputerVisitor diagnosticVisitor = new DiagnosticVisitor();

            Console.WriteLine("=== Status Operation ===");
            foreach (var component in components)
            {
                component.Accept(statusVisitor);
            }

            Console.WriteLine("\n=== Diagnostic Operation ===");
            foreach (var component in components)
            {
                component.Accept(diagnosticVisitor);
            }

            Console.ReadKey();
        }
    }
}
