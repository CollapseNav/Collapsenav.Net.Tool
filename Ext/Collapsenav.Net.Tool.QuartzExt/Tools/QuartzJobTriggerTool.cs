using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey(string name, string group, int count)
    {
        return count <= 0 ? null : Enumerable.Range(0, count).Select(item => new JobTriggerKey
        {
            JKey = new JobKey($"{name}_{item}", $"{group}"),
            TKey = new TriggerKey($"{name}_{item}", $"{group}"),
        });
    }
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey(string name, int count)
    {
        return CreateJobKeyAndTriggerKey(name, name, count);
    }
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey(Type type, int count)
    {
        return CreateJobKeyAndTriggerKey(type.Name, count);
    }
    public static IEnumerable<JobTriggerKey> CreateJobKeyAndTriggerKey<Job>(int count) where Job : IJob
    {
        return CreateJobKeyAndTriggerKey(typeof(Job).Name, count);
    }


    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, string name, string group, IEnumerable<string> crons)
    {
        return crons.Select((item, index) =>
        {
            return new JobAndTrigger
            {
                Job = CreateJob(type, $"{name}_{index}", $"{group}"),
                Trigger = CreateTrigger($"{name}_{index}", $"{group}", item),
            };
        });
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, string name, IEnumerable<string> crons)
    {
        return CreateJobAndTrigger(type, name, name, crons);
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, IEnumerable<string> crons)
    {
        return CreateJobAndTrigger(type, type.Name, crons);
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(IEnumerable<string> crons) where Job : IJob
    {
        return CreateJobAndTrigger(typeof(Job), crons);
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(string name, string group, IEnumerable<string> crons) where Job : IJob
    {
        return CreateJobAndTrigger(typeof(Job), name, group, crons);
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(string name, IEnumerable<string> crons) where Job : IJob
    {
        return CreateJobAndTrigger(typeof(Job), name, crons);
    }


    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, string name, string group, IEnumerable<int> lens)
    {
        return lens.Select((item, index) =>
        {
            return new JobAndTrigger
            {
                Job = CreateJob(type, $"{name}_{index}", $"{group}"),
                Trigger = CreateTrigger($"{name}_{index}", $"{group}", item),
            };
        });
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, string name, IEnumerable<int> lens)
    {
        return CreateJobAndTrigger(type, name, name, lens);
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger(Type type, IEnumerable<int> lens)
    {
        return CreateJobAndTrigger(type, type.Name, lens);
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(IEnumerable<int> lens) where Job : IJob
    {
        return CreateJobAndTrigger(typeof(Job), lens);
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(string name, string group, IEnumerable<int> lens) where Job : IJob
    {
        return CreateJobAndTrigger(typeof(Job), name, group, lens);
    }
    public static IEnumerable<JobAndTrigger> CreateJobAndTrigger<Job>(string name, IEnumerable<int> lens) where Job : IJob
    {
        return CreateJobAndTrigger(typeof(Job), name, lens);
    }
}