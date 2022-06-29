using Quartz;

namespace Collapsenav.Net.Tool.Ext;
public class QuartzTool
{
    public static ITrigger CreateCronTrigger(TriggerKey tkey, string cron)
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
    public static ITrigger CreateCronTrigger(string name, string cron)
    {
        return CreateCronTrigger(new TriggerKey(name, name), cron);
    }
    /// <summary>
    /// 获取一个trigger
    /// </summary>
    public static ITrigger CreateCronTrigger(string name, string group, string cron)
    {
        return CreateCronTrigger(new TriggerKey(name, group), cron);
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
    public static IEnumerable<ITrigger> CreateCronTriggers(string name, string group, IEnumerable<string> crons)
    {
        return crons.Select((item, index) =>
        {
            return CreateCronTrigger(new TriggerKey($"{name}_{index}", $"{group}"), item);
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
    public static IEnumerable<ITrigger> CreateCronTriggers(string name, IEnumerable<string> crons)
    {
        return CreateCronTriggers(name, name, crons);
    }



    public static JobAndTriggerItem CreateJobAndCronTrigger<Job>(string name, string cron) where Job : IJob
    {
        return CreateJobAndCronTrigger<Job>(new JobKey(name, name), new TriggerKey(name, name), cron);
    }
    public static JobAndTriggerItem CreateJobAndCronTrigger<Job>(string name, string group, string cron) where Job : IJob
    {
        return CreateJobAndCronTrigger<Job>(new JobKey(name, group), new TriggerKey(name, group), cron);
    }
    public static JobAndTriggerItem CreateJobAndCronTrigger<Job>(JobKey jobKey, TriggerKey triggerKey, string cron) where Job : IJob
    {
        return new JobAndTriggerItem
        {
            Trigger = CreateCronTrigger(triggerKey, cron),
            Job = CreateJob<Job>(jobKey)
        };
    }


    public static IEnumerable<JobAndTriggerItem> CreateJobAndCronTriggers<Job>(string name, IEnumerable<string> crons) where Job : IJob
    {
        return CreateJobAndCronTriggers<Job>(name, name, crons);
    }
    public static IEnumerable<JobAndTriggerItem> CreateJobAndCronTriggers<Job>(string name, string group, IEnumerable<string> crons) where Job : IJob
    {
        return CreateCronTriggers(name, group, crons).Select(item =>
        {
            return new JobAndTriggerItem
            {
                Trigger = item,
                Job = CreateJob<Job>(item.Key.Name, item.Key.Group)
            };
        });
    }
    public static IEnumerable<JobAndTriggerItem> CreateJobAndCronTriggers(Type type, string name, IEnumerable<string> crons)
    {
        return CreateJobAndCronTriggers(type, name, name, crons);
    }
    public static IEnumerable<JobAndTriggerItem> CreateJobAndCronTriggers(Type type, string name, string group, IEnumerable<string> crons)
    {
        return CreateCronTriggers(name, group, crons).Select(item =>
        {
            return new JobAndTriggerItem
            {
                Trigger = item,
                Job = CreateJob(type, item.Key.Name, item.Key.Group)
            };
        });
    }
}