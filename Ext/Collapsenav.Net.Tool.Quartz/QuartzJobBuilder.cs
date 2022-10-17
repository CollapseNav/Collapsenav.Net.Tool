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
    public List<IQuartzJsonConfig> ConfigNodes { get; set; }
    private List<JobItem> JobItems { get; set; }
    public QuartzJobBuilder()
    {
        CronJobs = new();
        SimpleJobs = new();
        XmlConfig = new();
        ConfigNodes = new();
        JobItems = new();
    }
    /// <summary>
    /// 添加经典的xml配置
    /// </summary>
    public void AddXmlConfig(string path)
    {
        if (path.ToLower().EndsWith(".xml"))
            XmlConfig.Add(path);
    }
    /// <summary>
    /// 添加CronJob
    /// </summary>
    public void AddJob<Job>(string cron) where Job : IJob
    {
        CronJobs.Add(new CronJob
        {
            JobType = typeof(Job),
            Cron = cron,
        });
    }
    /// <summary>
    /// 添加SimpleJob
    /// </summary>
    public void AddJob<Job>(int len) where Job : IJob
    {
        SimpleJobs.Add(new SimpleJob
        {
            JobType = typeof(Job),
            Len = len,
        });
    }
    /// <summary>
    /// 添加CronJob
    /// </summary>
    public void AddJob(Type type, string cron)
    {
        CronJobs.Add(new CronJob
        {
            JobType = type,
            Cron = cron,
        });
    }
    /// <summary>
    /// 添加SimpleJob
    /// </summary>
    public void AddJob(Type type, int len)
    {
        SimpleJobs.Add(new SimpleJob
        {
            JobType = type,
            Len = len,
        });
    }
    /// <summary>
    /// 添加SimpleJob
    /// </summary>
    public void AddJob<Job>(params int[] len) where Job : IJob
    {
        AddJob(typeof(Job), len);
    }
    /// <summary>
    /// 添加SimpleJob
    /// </summary>
    public void AddJob(Type type, params int[] len)
    {
        if (len.IsEmpty())
            return;
        var keys = QuartzTool.CreateJobKeyAndTriggerKey(len.Length, type).ToArray();
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
    /// <summary>
    /// 添加CronJob
    /// </summary>
    public void AddJob<Job>(IEnumerable<string> crons) where Job : IJob
    {
        AddJob(typeof(Job), crons);
    }
    /// <summary>
    /// 添加CronJob
    /// </summary>
    public void AddJob(Type type, IEnumerable<string> crons)
    {
        if (crons.IsEmpty())
            return;
        var list = crons.ToArray();
        var keys = QuartzTool.CreateJobKeyAndTriggerKey(list.Length, type).ToArray();
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

    public void AddQuartzJsonConfig(IEnumerable<IQuartzJsonConfig> nodes)
    {
        ConfigNodes ??= new();
        ConfigNodes.AddRange(nodes);
    }
    /// <summary>
    /// 构建 scheduler 的job
    /// </summary>
    /// <remarks>默认情况下会构建 QuartzNode.Scheduler</remarks>
    public async Task Build(IScheduler scheduler)
    {
        var sch = scheduler ?? QuartzNode.Scheduler;
        if (CronJobs.NotEmpty())
            JobItems.AddRange(CronJobs);
        if (SimpleJobs.NotEmpty())
            JobItems.AddRange(SimpleJobs);
        var configs = ConfigNodes?.Select(item => item.ToJobItem()).ToList();
        if (configs.NotEmpty())
            JobItems.AddRange(configs);

        foreach (var item in JobItems)
            await sch?.ScheduleJob(item.GetJobDetail(), item.GetTrigger());
        foreach (var path in XmlConfig)
            await sch?.LoadXmlConfig(path);
    }
}