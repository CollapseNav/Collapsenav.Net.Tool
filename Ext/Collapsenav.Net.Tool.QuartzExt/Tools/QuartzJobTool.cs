using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public partial class QuartzTool
{
    /// <summary>
    /// 根据 type 创建 job
    /// </summary>
    public static IJobDetail CreateJob(Type type, JobKey jkey)
    {
        return JobBuilder.Create(type)
        .WithIdentity(jkey)
        .Build();
    }
    /// <summary>
    /// 根据 type 创建 job, 使用type的名称作为jobkey
    /// </summary>
    public static IJobDetail CreateJob(Type type)
    {
        return JobBuilder.Create(type)
        .WithIdentity(new JobKey(type.Name, type.Name))
        .Build();
    }
    /// <summary>
    /// 根据 type 创建 job, 使用传入的 name 作为jobkey
    /// </summary>
    public static IJobDetail CreateJob(Type type, string name)
    {
        return CreateJob(type, new JobKey(name, name));
    }
    /// <summary>
    /// 根据 type 创建 job
    /// </summary>
    public static IJobDetail CreateJob(Type type, string name, string group)
    {
        return CreateJob(type, new JobKey(name, group));
    }

    /// <summary>
    /// 使用传入的泛型类型创建 job
    /// </summary>
    public static IJobDetail CreateJob<Job>(JobKey key) where Job : IJob
    {
        return CreateJob(typeof(Job), key);
    }
    /// <summary>
    /// 使用传入的泛型类型创建 job, 使用泛型类型的名称作为jobkey
    /// </summary>
    public static IJobDetail CreateJob<Job>() where Job : IJob
    {
        return CreateJob(typeof(Job));
    }
    /// <summary>
    /// 使用传入的泛型类型创建 job, 使用传入的 name 作为jobkey
    /// </summary>
    public static IJobDetail CreateJob<Job>(string name) where Job : IJob
    {
        return CreateJob(typeof(Job), new JobKey(name, name));
    }
    /// <summary>
    /// 使用传入的泛型类型创建 job
    /// </summary>
    public static IJobDetail CreateJob<Job>(string name, string group) where Job : IJob
    {
        return CreateJob(typeof(Job), new JobKey(name, group));
    }
}