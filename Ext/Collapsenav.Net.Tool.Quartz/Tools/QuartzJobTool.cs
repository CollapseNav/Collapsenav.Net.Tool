using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    /// <summary>
    /// 根据 type 创建 job
    /// </summary>
    public static IJobDetail CreateJob(Type type, JobKey jkey = null)
    {
        jkey ??= new JobKey(type.Name, type.Name);
        return JobBuilder.Create(type)
        .WithIdentity(jkey)
        .Build();
    }
    /// <summary>
    /// 根据 type 创建 job, 使用传入的 name 作为jobkey
    /// </summary>
    public static IJobDetail CreateJob(Type type, string name, string group = null)
    {
        group ??= name;
        return CreateJob(type, new JobKey(name, group));
    }



    /// <summary>
    /// 使用传入的泛型类型创建 job
    /// </summary>
    public static IJobDetail CreateJob<Job>(JobKey key = null) where Job : IJob
    {
        return CreateJob(typeof(Job), key);
    }
    /// <summary>
    /// 使用传入的泛型类型创建 job, 使用传入的 name 作为jobkey
    /// </summary>
    public static IJobDetail CreateJob<Job>(string name, string group = null) where Job : IJob
    {
        return CreateJob(typeof(Job), name, group);
    }



    public static IEnumerable<JobKey> CreateJobKeys(int count, string name = null, string group = null)
    {
        name ??= typeof(JobKey).Name;
        group ??= name;
        return count <= 0 ? null : Enumerable.Range(0, count).Select(item => new JobKey($"{name}_{item}", $"{group}"));
    }
    public static IEnumerable<JobKey> CreateJobKeys(int count, Type type)
    {
        return CreateJobKeys(count, type.Name);
    }
    public static IEnumerable<JobKey> CreateJobKeys<Job>(int count) where Job : IJob
    {
        return CreateJobKeys(count, typeof(Job).Name);
    }
}