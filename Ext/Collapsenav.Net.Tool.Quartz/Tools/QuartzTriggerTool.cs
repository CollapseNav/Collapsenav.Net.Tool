using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    public static ITrigger CreateTrigger(object obj, TriggerKey tkey)
    {
        if (obj is string cron)
        {
            return TriggerBuilder.Create()
            .StartNow()
            .WithIdentity(tkey)
            .WithCronSchedule(cron)
            .Build();
        }
        else if (obj is int len)
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
        return null;
    }
    /// <summary>
    /// 获取一个trigger
    /// </summary>
    public static ITrigger CreateTrigger(object cron, string name, string group = null)
    {
        return CreateTrigger(cron, new TriggerKey(name, group ?? name));
    }

    /// <summary>
    /// 使用type的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger(object cron, Type type)
    {
        return CreateTrigger(cron, type.Name);
    }
    /// <summary>
    /// 使用泛型类型的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger<Job>(object cron) where Job : IJob
    {
        return CreateTrigger(cron, typeof(Job));
    }

    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    /// <remarks>
    /// <para>同时生成多个 triggerkey</para>
    /// <para>如: 传入 name,group</para>
    /// <para>则会生成 name_1.group, name_2.group, name_3.group ... 这样的key</para>
    /// </remarks>
    public static IEnumerable<ITrigger> CreateTriggers(IEnumerable<int> crons, string name, string group = null)
    {
        return crons.Select((item, index) =>
        {
            return CreateTrigger(item, new TriggerKey($"{name}_{index}", $"{group ?? name}"));
        }).ToList();
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    /// <remarks>
    /// <para>同时生成多个 triggerkey</para>
    /// <para>如: 传入 name,group</para>
    /// <para>则会生成 name_1.group, name_2.group, name_3.group ... 这样的key</para>
    /// </remarks>
    public static IEnumerable<ITrigger> CreateTriggers(IEnumerable<object> crons, string name, string group = null)
    {
        return crons.Select((item, index) =>
        {
            return CreateTrigger(item, new TriggerKey($"{name}_{index}", $"{group ?? name}"));
        }).ToList();
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers(Type type, params int[] crons)
    {
        return CreateTriggers(crons, type.Name);
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers(Type type, params object[] crons)
    {
        return CreateTriggers(crons, type.Name);
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers<Job>(params object[] crons) where Job : IJob
    {
        return CreateTriggers(typeof(Job), crons);
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers<Job>(params int[] crons) where Job : IJob
    {
        return CreateTriggers(typeof(Job), crons);
    }
    public static IEnumerable<TriggerKey> CreateTriggerKeys(int count, string name, string group = null)
    {
        return count <= 0 ? null : Enumerable.Range(0, count).Select(item => new TriggerKey($"{name}_{item}", $"{group ?? name}"));
    }
    public static IEnumerable<TriggerKey> CreateTriggerKeys(int count, Type type, string group = null)
    {
        return CreateTriggerKeys(count, type.Name, group);
    }
    public static IEnumerable<TriggerKey> CreateTriggerKeys<Job>(int count, string group = null) where Job : IJob
    {
        return CreateTriggerKeys(count, typeof(Job).Name, group);
    }
}