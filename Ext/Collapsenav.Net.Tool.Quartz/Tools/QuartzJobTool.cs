using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public static partial class QuartzTool
{
    /// <summary>
    /// 根据 type 创建 job
    /// </summary>
    public static IJobDetail CreateJob(Type type, JobKey jkey = null)
        => JobBuilder.Create(type)
        .WithIdentity(jkey ?? new JobKey(type.Name, type.Name))
        .Build();
    /// <summary>
    /// 根据 type 创建 job, 使用传入的 name 作为jobkey
    /// </summary>
    public static IJobDetail CreateJob(Type type, string name, string group = null) => CreateJob(type, new JobKey(name, group.IsEmpty(name)));

    /// <summary>
    /// 使用传入的泛型类型创建 job
    /// </summary>
    public static IJobDetail CreateJob<Job>(JobKey key = null) where Job : IJob => CreateJob(typeof(Job), key);

    /// <summary>
    /// 使用传入的泛型类型创建 job, 使用传入的 name 作为jobkey
    /// </summary>
    public static IJobDetail CreateJob<Job>(string name, string group = null) where Job : IJob => CreateJob(typeof(Job), name, group);

    public static IEnumerable<JobKey> CreateJobKeys(int count, string name, string group = null)
        => count <= 0 ? null : Enumerable.Range(0, count).Select(item => new JobKey($"{name}_{item}", $"{group.IsEmpty(name)}"));
    public static IEnumerable<JobKey> CreateJobKeys(int count, Type type, string group = null) => CreateJobKeys(count, type.Name, group);
    public static IEnumerable<JobKey> CreateJobKeys<Job>(int count, string group = null) where Job : IJob => CreateJobKeys(count, typeof(Job).Name, group);
}