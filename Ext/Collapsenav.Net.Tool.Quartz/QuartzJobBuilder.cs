using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public class QuartzJobBuilder
{
    /// <summary>
    /// 使用cron的job
    /// </summary>
    public List<CronJob> CronJobs { get; set; }
    /// <summary>
    /// 使用一般时间间隔的job
    /// </summary>
    public List<SimpleJob> SimpleJobs { get; set; }
    /// <summary>
    /// xml配置文件路径
    /// </summary>
    public List<string> XmlConfig { get; set; }
    public QuartzJobBuilder()
    {
        CronJobs = new();
        SimpleJobs = new();
        XmlConfig = new();
    }
    public void AddXmlConfig(string path)
    {
        if (path.ToLower().EndsWith(".xml"))
            XmlConfig.Add(path);
    }
    public void AddJob<Job>(string cron) where Job : IJob
    {
        CronJobs.Add(new CronJob
        {
            JobType = typeof(Job),
            Cron = cron,
        });
    }
    public void AddJob<Job>(int len) where Job : IJob
    {
        SimpleJobs.Add(new SimpleJob
        {
            JobType = typeof(Job),
            Len = len,
        });
    }
    public void AddJob(Type type, string cron)
    {
        CronJobs.Add(new CronJob
        {
            JobType = type,
            Cron = cron,
        });
    }
    public void AddJob(Type type, int len)
    {
        SimpleJobs.Add(new SimpleJob
        {
            JobType = type,
            Len = len,
        });
    }
    public void AddJob<Job>(params int[] len) where Job : IJob
    {
        AddJob(typeof(Job), len);
    }
    public void AddJob(Type type, params int[] len)
    {
        if (len.IsEmpty())
            return;
        var keys = QuartzTool.CreateJobKeyAndTriggerKey(type, len.Length).ToArray();
        for (var i = 0; i < len.Length; i++)
        {
            SimpleJobs.Add(new SimpleJob
            {
                JobType = type,
                Len = len[i],
                JKey = keys[i].JKey,
                TKey = keys[i].TKey,
            });
        }
    }
    public void AddJob<Job>(IEnumerable<string> crons) where Job : IJob
    {
        AddJob(typeof(Job), crons);
    }
    public void AddJob(Type type, IEnumerable<string> crons)
    {
        if (crons.IsEmpty())
            return;
        var list = crons.ToArray();
        var keys = QuartzTool.CreateJobKeyAndTriggerKey(type, list.Length).ToArray();
        for (var i = 0; i < list.Length; i++)
        {
            CronJobs.Add(new CronJob
            {
                JobType = type,
                Cron = list[i],
                JKey = keys[i].JKey,
                TKey = keys[i].TKey,
            });
        }
    }
    /// <summary>
    /// 构建 scheduler 的job
    /// </summary>
    /// <remarks>默认情况下会构建 QuartzNode.Scheduler</remarks>
    public async Task Build(IScheduler scheduler)
    {
        var sch = scheduler ?? QuartzNode.Scheduler;
        foreach (var job in CronJobs)
            await sch?.ScheduleJob(job.GetJobDetail(), job.GetTrigger());
        foreach (var job in SimpleJobs)
            await sch?.ScheduleJob(job.GetJobDetail(), job.GetTrigger());
        foreach (var path in XmlConfig)
            await sch?.LoadXmlConfig(path);
    }
}