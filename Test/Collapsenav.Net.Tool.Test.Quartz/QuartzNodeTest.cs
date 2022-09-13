using Collapsenav.Net.Tool.Ext;
using Quartz;

namespace Collapsenav.Net.Tool.Test.Quartz;

public class QuartzNodeTest
{
    public QuartzNodeTest()
    {
        if (QuartzNode.Scheduler == null)
            QuartzNode.InitSchedulerAsync().Wait();
    }

    internal static void AssertJobAndTrigger<Job>(IJobDetail job, ITrigger trigger, string name = null, string group = null)
    {
        Assert.True(trigger.Key.Name == (name ?? typeof(Job).Name));
        Assert.True(trigger.Key.Group == (group ?? typeof(Job).Name));
        Assert.True(job.Key.Name == (name ?? typeof(Job).Name));
        Assert.True(job.Key.Group == (group ?? typeof(Job).Name));
    }

    [Fact]
    public async Task ScheduleJobTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob<TestJob>(5);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        var jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 1);
        Assert.True(jobs.Count == 1);
        AssertJobAndTrigger<TestJob>(jobs.First(), triggers.First());
    }
    [Fact]
    public async Task ScheduleJobWithTypeTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob(typeof(TestJob), 5);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        var jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 1);
        Assert.True(jobs.Count == 1);
        AssertJobAndTrigger<TestJob>(jobs.First(), triggers.First());
    }
    [Fact]
    public async Task ScheduleJobWithTriggerTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob<TestJob>(QuartzTool.CreateTrigger<TestJob>(5));
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        var jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 1);
        Assert.True(jobs.Count == 1);
        AssertJobAndTrigger<TestJob>(jobs.First(), triggers.First());
    }
    [Fact]
    public async Task ScheduleJobWithTriggerAndTypeTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob(typeof(TestJob), QuartzTool.CreateTrigger<TestJob>(5));
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        var jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 1);
        Assert.True(jobs.Count == 1);
        AssertJobAndTrigger<TestJob>(jobs.First(), triggers.First());
    }
    [Fact]
    public async Task ScheduleJobWithNameAndGroupTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob<TestJob>(5, "TestJob", "TestJob");
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        var jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 1);
        Assert.True(jobs.Count == 1);
        AssertJobAndTrigger<TestJob>(jobs.First(), triggers.First());
    }
    [Fact]
    public async Task ScheduleJobWithNameAndGroupAndTypeTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob(typeof(TestJob), 5, "TestJob", "TestJob");
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        var jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 1);
        Assert.True(jobs.Count == 1);
        AssertJobAndTrigger<TestJob>(jobs.First(), triggers.First());
    }
    [Fact]
    public async Task ScheduleJobsWithNameAndGroupTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJobs<TestJob>(new[] { 1, 2, 3 });
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        var jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 3);
        Assert.True(jobs.Count == 3);
        foreach (var (trigger, index) in triggers.SelectWithIndex())
        {
            Assert.True(trigger.Key.Name == $"TestJob_{index}");
            Assert.True(trigger.Key.Group == nameof(TestJob));
        }
        foreach (var (job, index) in jobs.SelectWithIndex())
        {
            Assert.True(job.Key.Name == $"TestJob_{index}");
            Assert.True(job.Key.Group == nameof(TestJob));
        }

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJobs<TestJob>(new[] { 1, 2, 3 }, "TestJob", "TestJob");
        triggers = await QuartzNode.Scheduler.GetTriggers();
        jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 3);
        Assert.True(jobs.Count == 3);
        foreach (var (trigger, index) in triggers.SelectWithIndex())
        {
            Assert.True(trigger.Key.Name == $"TestJob_{index}");
            Assert.True(trigger.Key.Group == nameof(TestJob));
        }
        foreach (var (job, index) in jobs.SelectWithIndex())
        {
            Assert.True(job.Key.Name == $"TestJob_{index}");
            Assert.True(job.Key.Group == nameof(TestJob));
        }

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJobs<TestJob>(new[] { "0/5 * * * * ?", "0/10 * * * * ?", "0/15 * * * * ?" }, "TestJob", "TestJob");
        triggers = await QuartzNode.Scheduler.GetTriggers();
        jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 3);
        Assert.True(jobs.Count == 3);
        foreach (var (trigger, index) in triggers.SelectWithIndex())
        {
            Assert.True(trigger.Key.Name == $"TestJob_{index}");
            Assert.True(trigger.Key.Group == nameof(TestJob));
        }
        foreach (var (job, index) in jobs.SelectWithIndex())
        {
            Assert.True(job.Key.Name == $"TestJob_{index}");
            Assert.True(job.Key.Group == nameof(TestJob));
        }
    }
    [Fact]
    public async Task ScheduleJobsWithNameAndGroupAndTypeTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJobs(typeof(TestJob), new[] { 1, 2, 3 });
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        var jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 3);
        Assert.True(jobs.Count == 3);
        foreach (var (trigger, index) in triggers.SelectWithIndex())
        {
            Assert.True(trigger.Key.Name == $"TestJob_{index}");
            Assert.True(trigger.Key.Group == nameof(TestJob));
        }
        foreach (var (job, index) in jobs.SelectWithIndex())
        {
            Assert.True(job.Key.Name == $"TestJob_{index}");
            Assert.True(job.Key.Group == nameof(TestJob));
        }

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJobs(typeof(TestJob), new[] { 1, 2, 3 }, "TestJob", "TestJob");
        triggers = await QuartzNode.Scheduler.GetTriggers();
        jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 3);
        Assert.True(jobs.Count == 3);
        foreach (var (trigger, index) in triggers.SelectWithIndex())
        {
            Assert.True(trigger.Key.Name == $"TestJob_{index}");
            Assert.True(trigger.Key.Group == nameof(TestJob));
        }
        foreach (var (job, index) in jobs.SelectWithIndex())
        {
            Assert.True(job.Key.Name == $"TestJob_{index}");
            Assert.True(job.Key.Group == nameof(TestJob));
        }

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJobs(typeof(TestJob), new[] { "0/5 * * * * ?", "0/10 * * * * ?", "0/15 * * * * ?" }, "TestJob", "TestJob");
        triggers = await QuartzNode.Scheduler.GetTriggers();
        jobs = await QuartzNode.Scheduler.GetJobDetails();
        Assert.True(triggers.Count == 3);
        Assert.True(jobs.Count == 3);
        foreach (var (trigger, index) in triggers.SelectWithIndex())
        {
            Assert.True(trigger.Key.Name == $"TestJob_{index}");
            Assert.True(trigger.Key.Group == nameof(TestJob));
        }
        foreach (var (job, index) in jobs.SelectWithIndex())
        {
            Assert.True(job.Key.Name == $"TestJob_{index}");
            Assert.True(job.Key.Group == nameof(TestJob));
        }
    }
    [Fact]
    public async Task DeleteJobByGroupTest()
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJobs<TestJob>(new[] { 1, 2, 3 });
        await QuartzNode.Scheduler.DeleteJobs("TestJob");
        var jobKeys = await QuartzNode.Scheduler.GetJobKeys();
        Assert.True(jobKeys.IsEmpty());
        await QuartzNode.Scheduler.DeleteJobs("TestJob123");
    }
}