using Quartz;

namespace Collapsenav.Net.Tool.Ext;
public class QuartzTool
{
    public static ITrigger CreateTrigger(TriggerKey tkey, string cron)
    {
        return TriggerBuilder.Create()
        .StartNow()
        .WithIdentity(tkey)
        .WithCronSchedule(cron)
        .Build();
    }
    /// <summary>
    /// 获取一个trigger
    /// </summary>
    /// <param name="name">name 和 group 都是 key</param>
    /// <param name="cron"></param>
    public static ITrigger CreateTrigger(string name, string cron)
    {
        return CreateTrigger(new TriggerKey(name, name), cron);
    }
    /// <summary>
    /// 获取一个trigger
    /// </summary>
    public static ITrigger CreateTrigger(string name, string group, string cron)
    {
        return CreateTrigger(new TriggerKey(name, group), cron);
    }


    public static IJobDetail CreateJob(Type type, JobKey jkey)
    {
        return JobBuilder.Create(type)
        .WithIdentity(jkey)
        .Build();
    }
    public static IJobDetail CreateJob<Job>(JobKey key) where Job : IJob
    {
        return CreateJob(typeof(Job), key);
    }
    public static IJobDetail CreateJob<Job>(string name) where Job : IJob
    {
        return CreateJob(typeof(Job), new JobKey(name, name));
    }
    public static IJobDetail CreateJob<Job>(string name, string group) where Job : IJob
    {
        return CreateJob(typeof(Job), new JobKey(name, group));
    }
    public static IJobDetail CreateJob(Type type, string name)
    {
        return CreateJob(type, new JobKey(name, name));
    }
    public static IJobDetail CreateJob(Type type, string name, string group)
    {
        return CreateJob(type, new JobKey(name, group));
    }


    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    /// <remarks>
    /// <para>同时生成多个 triggerkey</para>
    /// <para>如: 传入 name,group</para>
    /// <para>则会生成 name_1.group, name_2.group, name_3.group ... 这样的key</para>
    /// </remarks>
    public static IEnumerable<ITrigger> CreateTriggers(string name, string group, IEnumerable<string> crons)
    {
        return crons.Select((item, index) =>
        {
            return CreateTrigger(new TriggerKey($"{name}_{index}", $"{group}"), item);
        });
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    /// <remarks>
    /// <para>同时生成多个 triggerkey</para>
    /// <para>如: 传入 name</para>
    /// <para>则会生成 name_1.name, name_2.name, name_3.name ... 这样的key</para>
    /// </remarks>
    public static IEnumerable<ITrigger> CreateTriggers(string name, IEnumerable<string> crons)
    {
        return CreateTriggers(name, name, crons);
    }



    public static JobAndTriggerItem CreateJobAndTrigger<Job>(string name, string cron) where Job : IJob
    {
        return CreateJobAndTrigger<Job>(new JobKey(name, name), new TriggerKey(name, name), cron);
    }
    public static JobAndTriggerItem CreateJobAndTrigger<Job>(string name, string group, string cron) where Job : IJob
    {
        return CreateJobAndTrigger<Job>(new JobKey(name, group), new TriggerKey(name, group), cron);
    }
    public static JobAndTriggerItem CreateJobAndTrigger<Job>(JobKey jobKey, TriggerKey triggerKey, string cron) where Job : IJob
    {
        return new JobAndTriggerItem
        {
            Trigger = CreateTrigger(triggerKey, cron),
            Job = CreateJob<Job>(jobKey)
        };
    }


    public static IEnumerable<JobAndTriggerItem> CreateJobAndTriggers<Job>(string name, IEnumerable<string> crons) where Job : IJob
    {
        return CreateJobAndTriggers<Job>(name, name, crons);
    }
    public static IEnumerable<JobAndTriggerItem> CreateJobAndTriggers<Job>(string name, string group, IEnumerable<string> crons) where Job : IJob
    {
        return CreateTriggers(name, group, crons).Select(item =>
        {
            return new JobAndTriggerItem
            {
                Trigger = item,
                Job = CreateJob<Job>(item.Key.Name, item.Key.Group)
            };
        });
    }
}