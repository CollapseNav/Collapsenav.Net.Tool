using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    /// <summary>
    /// 根据间隔的时长(秒位单位)创建 simpletrigger
    /// </summary>
    public static ITrigger CreateTrigger(int len, TriggerKey tkey)
    {
        return TriggerBuilder.Create()
        .WithSimpleSchedule(builder =>
        {
            builder.WithIntervalInSeconds(len).RepeatForever();
        })
        .StartNow()
        .WithIdentity(tkey)
        .Build();
    }
    /// <summary>
    /// 根据cron表达式创建常见的trigger
    /// </summary>
    public static ITrigger CreateTrigger(string cron, TriggerKey tkey)
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
    public static ITrigger CreateTrigger(string cron, string name, string group = null)
    {
        return CreateTrigger(cron, new TriggerKey(name, group ?? name));
    }
    /// <summary>
    /// 获取一个trigger
    /// </summary>
    public static ITrigger CreateTrigger(int len, string name, string group = null)
    {
        return CreateTrigger(len, new TriggerKey(name, group ?? name));
    }

    /// <summary>
    /// 使用type的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger(string cron, Type type)
    {
        return CreateTrigger(cron, type.Name);
    }
    /// <summary>
    /// 使用type的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger(int len, Type type)
    {
        return CreateTrigger(len, type.Name);
    }
    /// <summary>
    /// 使用泛型类型的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger<Job>(string cron) where Job : IJob
    {
        return CreateTrigger(cron, typeof(Job));
    }
    /// <summary>
    /// 使用泛型类型的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger<Job>(int len) where Job : IJob
    {
        return CreateTrigger(len, typeof(Job));
    }

    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    /// <remarks>
    /// <para>同时生成多个 triggerkey</para>
    /// <para>如: 传入 name,group</para>
    /// <para>则会生成 name_1.group, name_2.group, name_3.group ... 这样的key</para>
    /// </remarks>
    public static IEnumerable<ITrigger> CreateTriggers(IEnumerable<string> crons, string name, string group = null)
    {
        return crons.Select((item, index) =>
        {
            return CreateTrigger(item, new TriggerKey($"{name}_{index}", $"{group ?? name}"));
        });
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers(IEnumerable<string> crons, Type type)
    {
        return CreateTriggers(crons, type.Name);
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers<Job>(IEnumerable<string> crons) where Job : IJob
    {
        return CreateTriggers(crons, typeof(Job));
    }


    /// <summary>
    /// 根据lens的数量生成多个trigger
    /// </summary>
    /// <remarks>
    /// <para>同时生成多个 triggerkey</para>
    /// <para>如: 传入 name,group</para>
    /// <para>则会生成 name_1.group, name_2.group, name_3.group ... 这样的key</para>
    /// </remarks>
    public static IEnumerable<ITrigger> CreateTriggers(IEnumerable<int> lens, string name, string group = null)
    {
        return lens.Select((item, index) =>
        {
            return CreateTrigger(item, new TriggerKey($"{name}_{index}", $"{group ?? name}"));
        });
    }
    /// <summary>
    /// 根据lens的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers(IEnumerable<int> lens, Type type)
    {
        return CreateTriggers(lens, type.Name);
    }
    /// <summary>
    /// 根据lens的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers<Job>(IEnumerable<int> lens) where Job : IJob
    {
        return CreateTriggers(lens, typeof(Job));
    }

    public static IEnumerable<TriggerKey> CreateTriggerKeys(int count, string name, string group = null)
    {
        return count <= 0 ? null : Enumerable.Range(0, count).Select(item => new TriggerKey($"{name}_{item}", $"{group ?? name}"));
    }
    public static IEnumerable<TriggerKey> CreateTriggerKeys(int count, Type type)
    {
        return CreateTriggerKeys(count, type.Name);
    }
    public static IEnumerable<TriggerKey> CreateTriggerKeys<Job>(int count) where Job : IJob
    {
        return CreateTriggerKeys(count, typeof(Job).Name);
    }
}