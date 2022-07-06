using Quartz;

namespace Collapsenav.Net.Tool.Ext;

public class QuartzJobBuilder
{
    public List<CronJob> CronJobs { get; set; }
    public List<SimpleJob> SimpleJobs { get; set; }
    public QuartzJobBuilder()
    {
        CronJobs = new();
        SimpleJobs = new();
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
    public async Task Build(IScheduler scheduler)
    {
        foreach (var job in CronJobs)
            await scheduler.ScheduleJob(job.JobType, job.JKey, job.TKey, job.Cron);
        foreach (var job in SimpleJobs)
            await scheduler.ScheduleJob(job.JobType, job.JKey, job.TKey, job.Len);
    }
}