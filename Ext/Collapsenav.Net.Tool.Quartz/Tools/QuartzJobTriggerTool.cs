using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey(int count, string name, string group = null)
    {
        group ??= name;
        return count <= 0 ? null : Enumerable.Range(0, count).Select(item => new JobTriggerKey
        {
            JKey = new JobKey($"{name}_{item}", $"{group}"),
            TKey = new TriggerKey($"{name}_{item}", $"{group}"),
        });
    }
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey(Type type, int count)
    {
        return CreateJobKeyAndTriggerKey(count, type.Name);
    }
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey<Job>(int count) where Job : IJob
    {
        return CreateJobKeyAndTriggerKey(count, typeof(Job).Name);
    }


    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, IEnumerable<string> crons, string name = null, string group = null)
    {
        name ??= type.Name;
        group ??= name;
        return crons.Select((item, index) =>
        {
            return new JobAndTrigger
            {
                Job = CreateJob(type, $"{name}_{index}", $"{group}"),
                Trigger = CreateTrigger($"{name}_{index}", $"{group}", item),
            };
        });
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(IEnumerable<string> crons, string name = null, string group = null) where Job : IJob
    {
        return CreateJobAndTrigger(typeof(Job), crons, name, group);
    }

    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, IEnumerable<int> lens, string name = null, string group = null)
    {
        name ??= type.Name;
        group ??= name;
        return lens.Select((item, index) =>
        {
            return new JobAndTrigger
            {
                Job = CreateJob(type, $"{name}_{index}", $"{group}"),
                Trigger = CreateTrigger(item, $"{name}_{index}", $"{group}"),
            };
        });
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(IEnumerable<int> lens, string name = null, string group = null) where Job : IJob
    {
        return CreateJobAndTrigger(typeof(Job), lens, name, group);
    }
}