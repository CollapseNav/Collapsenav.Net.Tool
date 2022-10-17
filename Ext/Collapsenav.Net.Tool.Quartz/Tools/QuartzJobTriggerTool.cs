using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey(int count, string name, string group = null)
        => count <= 0 ? null : Enumerable.Range(0, count).Select(item => new JobTriggerKey
        {
            JKey = new JobKey($"{name}_{item}", $"{group.IsEmpty(name)}"),
            TKey = new TriggerKey($"{name}_{item}", $"{group.IsEmpty(name)}"),
        });
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey(int count, Type type, string group = null) => CreateJobKeyAndTriggerKey(count, type.Name, group);
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey<Job>(int count, string group = null) where Job : IJob => CreateJobKeyAndTriggerKey(count, typeof(Job).Name, group);

    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, IEnumerable<string> crons, string name = null, string group = null)
        => crons.Select((item, index) =>
        {
            return new JobAndTrigger
            {
                Job = CreateJob(type, $"{name.IsEmpty(type.Name)}_{index}", $"{group.IsEmpty(type.Name)}"),
                Trigger = CreateTrigger($"{name.IsEmpty(type.Name)}_{index}", $"{group.IsEmpty(type.Name)}", item),
            };
        });
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(IEnumerable<string> crons, string name = null, string group = null) where Job : IJob
        => CreateJobAndTrigger(typeof(Job), crons, name, group);
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, IEnumerable<int> lens, string name = null, string group = null)
        => lens.Select((item, index) =>
        {
            return new JobAndTrigger
            {
                Job = CreateJob(type, $"{name.IsEmpty(type.Name)}_{index}", $"{group.IsEmpty(type.Name)}"),
                Trigger = CreateTrigger(item, $"{name.IsEmpty(type.Name)}_{index}", $"{group.IsEmpty(type.Name)}"),
            };
        });
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(IEnumerable<int> lens, string name = null, string group = null) where Job : IJob
        => CreateJobAndTrigger(typeof(Job), lens, name, group);
}