namespace AdSyst.Advertisments.Application.Tests.Infrastructure
{
    public static class TaskExtensions
    {
        public static T Await<T>(this Task<T> task) => task.GetAwaiter().GetResult();
    }
}
