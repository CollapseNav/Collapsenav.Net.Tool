using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    /// <summary>
    /// 根据间隔的时长(秒位单位)创建 simpletrigger
    /// </summary>
    public static ITrigger CreateTrigger(TriggerKey tkey, int len)
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
    public static ITrigger CreateTrigger(TriggerKey tkey, string cron)
    {
        return TriggerBuilder.Create()
        .StartNow()
        .WithIdentity(tkey)
        .WithCronSchedule(cron)
        .Build();
    }


    /// <summary>
    /// 获取一个trigger, 使用name作为triggerkey
    /// </summary>
    public static ITrigger CreateTrigger(string name, string cron)
    {
        return CreateTrigger(new TriggerKey(name, name), cron);
    }
    /// <summary>
    /// 获取一个simple trigger, 使用name作为triggerkey
    /// </summary>
    public static ITrigger CreateTrigger(string name, int len)
    {
        return CreateTrigger(new TriggerKey(name, name), len);
    }
    /// <summary>
    /// 获取一个trigger
    /// </summary>
    public static ITrigger CreateTrigger(string name, string group, string cron)
    {
        return CreateTrigger(new TriggerKey(name, group), cron);
    }
    /// <summary>
    /// 获取一个trigger
    /// </summary>
    public static ITrigger CreateTrigger(string name, string group, int len)
    {
        return CreateTrigger(new TriggerKey(name, group), len);
    }

    /// <summary>
    /// 使用type的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger(Type type, string cron)
    {
        return CreateTrigger(type.Name, cron);
    }
    /// <summary>
    /// 使用type的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger(Type type, int len)
    {
        return CreateTrigger(type.Name, len);
    }
    /// <summary>
    /// 使用泛型类型的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger<Job>(string cron) where Job : IJob
    {
        return CreateTrigger(typeof(Job), cron);
    }
    /// <summary>
    /// 使用泛型类型的name作为triggerkey创建trigger
    /// </summary>
    public static ITrigger CreateTrigger<Job>(int len) where Job : IJob
    {
        return CreateTrigger(typeof(Job), len);
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
    public static IEnumerable<ITrigger> CreateTriggers(string name, IEnumerable<string> crons)
    {
        return CreateTriggers(name, name, crons);
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers(Type type, IEnumerable<string> crons)
    {
        return CreateTriggers(type.Name, crons);
    }
    /// <summary>
    /// 根据cron的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers<Job>(IEnumerable<string> crons) where Job : IJob
    {
        return CreateTriggers(typeof(Job), crons);
    }


    /// <summary>
    /// 根据lens的数量生成多个trigger
    /// </summary>
    /// <remarks>
    /// <para>同时生成多个 triggerkey</para>
    /// <para>如: 传入 name,group</para>
    /// <para>则会生成 name_1.group, name_2.group, name_3.group ... 这样的key</para>
    /// </remarks>
    public static IEnumerable<ITrigger> CreateTriggers(string name, string group, IEnumerable<int> lens)
    {
        return lens.Select((item, index) =>
        {
            return CreateTrigger(new TriggerKey($"{name}_{index}", $"{group}"), item);
        });
    }
    /// <summary>
    /// 根据lens的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers(string name, IEnumerable<int> lens)
    {
        return CreateTriggers(name, name, lens);
    }
    /// <summary>
    /// 根据lens的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers(Type type, IEnumerable<int> lens)
    {
        return CreateTriggers(type.Name, lens);
    }
    /// <summary>
    /// 根据lens的数量生成多个trigger
    /// </summary>
    public static IEnumerable<ITrigger> CreateTriggers<Job>(IEnumerable<int> lens) where Job : IJob
    {
        return CreateTriggers(typeof(Job), lens);
    }

    public static IEnumerable<TriggerKey> CreateTriggerKeys(string name, string group, int count)
    {
        return count <= 0 ? null : Enumerable.Range(0, count).Select(item => new TriggerKey($"{name}_{item}", $"{group}"));
    }
    public static IEnumerable<TriggerKey> CreateTriggerKeys(string name, int count)
    {
        return CreateTriggerKeys(name, name, count);
    }
    public static IEnumerable<TriggerKey> CreateTriggerKeys(Type type, int count)
    {
        return CreateTriggerKeys(type.Name, count);
    }
    public static IEnumerable<TriggerKey> CreateTriggerKeys<Job>(int count) where Job : IJob
    {
        return CreateTriggerKeys(typeof(Job).Name, count);
    }
}