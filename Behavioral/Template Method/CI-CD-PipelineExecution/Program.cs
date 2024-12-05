namespace CI_CD_PipelineExecution
{
    //Abstract Class(Template)
    public abstract class CICDPipeline
    {
        // Template method
        public void ExecutePipeline()
        {
            CheckoutCode();
            RunTests();
            BuildArtifacts();
            Deploy();
        }

        private void CheckoutCode()
        {
            Console.WriteLine("Checking out code from version control...");
        }

        protected abstract void RunTests();

        protected abstract void BuildArtifacts();

        protected abstract void Deploy();
    }

    //Concrete Implementations:
    //For Web Application
    public class WebAppPipeline : CICDPipeline
    {
        protected override void RunTests()
        {
            Console.WriteLine("Running web app unit and integration tests...");
        }

        protected override void BuildArtifacts()
        {
            Console.WriteLine("Building web application binaries and assets...");
        }

        protected override void Deploy()
        {
            Console.WriteLine("Deploying web application to server...");
        }
    }

    //For Library Package
    public class LibraryPackagePipeline : CICDPipeline
    {
        protected override void RunTests()
        {
            Console.WriteLine("Running library unit tests...");
        }

        protected override void BuildArtifacts()
        {
            Console.WriteLine("Building library binaries...");
        }

        protected override void Deploy()
        {
            Console.WriteLine("Publishing library to package repository...");
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Executing CI/CD for Web Application:");
            CICDPipeline webAppPipeline = new WebAppPipeline();
            webAppPipeline.ExecutePipeline();

            Console.WriteLine("\nExecuting CI/CD for Library Package:");
            CICDPipeline libraryPipeline = new LibraryPackagePipeline();
            libraryPipeline.ExecutePipeline();

            Console.ReadKey();
        }
    }
}
