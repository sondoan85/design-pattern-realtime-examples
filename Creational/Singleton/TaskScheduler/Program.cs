namespace TaskScheduler
{
    public class TaskScheduler
    {
        // Static instance for Singleton
        private static readonly TaskScheduler _instance = new TaskScheduler();
        // Dictionary to hold tasks and their timers
        private Dictionary<string, System.Timers.Timer> scheduledTasks;
        // Private constructor to prevent external instantiation
        private TaskScheduler()
        {
            scheduledTasks = new Dictionary<string, System.Timers.Timer>();
        }
        // Public method to schedule a task
        public void ScheduleTask(string taskId, Action taskAction, double intervalInMilliseconds)
        {
            if (scheduledTasks.ContainsKey(taskId))
            {
                throw new InvalidOperationException($"Task with ID {taskId} is already scheduled.");
            }
            var timer = new System.Timers.Timer(intervalInMilliseconds);
            timer.Elapsed += (sender, e) => taskAction();
            timer.Start();
            scheduledTasks[taskId] = timer;
        }
        // Public method to stop a scheduled task
        public void StopScheduledTask(string taskId)
        {
            if (scheduledTasks.TryGetValue(taskId, out var timer))
            {
                timer.Stop();
                scheduledTasks.Remove(taskId);
            }
        }
        // Public property to access the Singleton instance
        public static TaskScheduler Instance => _instance;
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            // Schedule a task to run every 5 seconds
            TaskScheduler.Instance.ScheduleTask("SampleTask", () => Console.WriteLine("Task executed!"), 5000);

            // Later, stop the task if needed
            TaskScheduler.Instance.StopScheduledTask("SampleTask");

            Console.ReadKey();
        }
    }
}
