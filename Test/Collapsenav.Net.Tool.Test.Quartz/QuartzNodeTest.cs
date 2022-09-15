using Collapsenav.Net.Tool.Ext;
using Quartz;
using Quartz.Impl.Triggers;

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

    [Theory]
    [InlineData(1, 10)]
    [InlineData(15, 5)]
    public async Task RescheduleSimpleJobTest(int old, int newc)
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob<TestJob>(old);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "TestJob");
        Assert.True(triggers.First().Key.Group == "TestJob");
        Assert.True((triggers.First() as SimpleTriggerImpl).RepeatInterval.Seconds == old);

        await QuartzNode.Scheduler.RescheduleJob<TestJob>(newc);
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True((triggers.First() as SimpleTriggerImpl).RepeatInterval.Seconds == newc);
    }


    [Theory]
    [InlineData("0/5 * * * * ?", "0/10 * * * * ?")]
    public async Task RescheduleCronJobTest(string old, string newc)
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob<TestJob>(old);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "TestJob");
        Assert.True(triggers.First().Key.Group == "TestJob");
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == old);

        await QuartzNode.Scheduler.RescheduleJob<TestJob>(newc);
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == newc);
    }


    [Theory]
    [InlineData(1, 10)]
    [InlineData(15, 5)]
    public async Task RescheduleSimpleJobWithTriggerTest(int old, int newc)
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob<TestJob>(old);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "TestJob");
        Assert.True(triggers.First().Key.Group == "TestJob");
        Assert.True(condition: (triggers.First() as SimpleTriggerImpl).RepeatInterval.Seconds == old);

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.RescheduleJob<TestJob>(QuartzTool.CreateTrigger<TestJob>(newc));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(condition: (triggers.First() as SimpleTriggerImpl).RepeatInterval.Seconds == newc);

        await QuartzNode.Scheduler.DeleteAllJobs();
        // 如果没有对应 TriggerKey 的任务，则新建一个
        await QuartzNode.Scheduler.RescheduleJob<TestJob>(old, new TriggerKey("1", "1"));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "1");
        Assert.True(triggers.First().Key.Group == "1");
        Assert.True((triggers.First() as SimpleTriggerImpl).RepeatInterval.Seconds == old);
    }


    [Theory]
    [InlineData("0/5 * * * * ?", "0/10 * * * * ?")]
    [InlineData("0/15 * * * * ?", "0/25 * * * * ?")]
    public async Task RescheduleCronJobWithTriggerTest(string old, string newc)
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob<TestJob>(old);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "TestJob");
        Assert.True(triggers.First().Key.Group == "TestJob");
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == old);

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.RescheduleJob<TestJob>(QuartzTool.CreateTrigger<TestJob>(newc));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == newc);

        await QuartzNode.Scheduler.DeleteAllJobs();
        // 如果没有对应 TriggerKey 的任务，则新建一个
        await QuartzNode.Scheduler.RescheduleJob<TestJob>(old, new TriggerKey("1", "1"));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "1");
        Assert.True(triggers.First().Key.Group == "1");
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == old);
    }
    [Theory]
    [InlineData(1, 10)]
    [InlineData(15, 5)]
    public async Task RescheduleSimpleJobWithTypeTest(int old, int newc)
    {
        var type = typeof(TestJob);
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob(type, old);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "TestJob");
        Assert.True(triggers.First().Key.Group == "TestJob");
        Assert.True((triggers.First() as SimpleTriggerImpl).RepeatInterval.Seconds == old);

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.RescheduleJob(type, QuartzTool.CreateTrigger(newc, type));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True((triggers.First() as SimpleTriggerImpl).RepeatInterval.Seconds == newc);

        await QuartzNode.Scheduler.DeleteAllJobs();
        // 如果没有对应 TriggerKey 的任务，则新建一个
        await QuartzNode.Scheduler.RescheduleJob(type, old, new TriggerKey("1", "1"));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "1");
        Assert.True(triggers.First().Key.Group == "1");
        Assert.True((triggers.First() as SimpleTriggerImpl).RepeatInterval.Seconds == old);
    }
    [Theory]
    [InlineData("0/5 * * * * ?", "0/10 * * * * ?")]
    [InlineData("0/15 * * * * ?", "0/25 * * * * ?")]
    public async Task RescheduleCronJobWithTypeTest(string old, string newc)
    {
        var type = typeof(TestJob);
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob(type, old);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "TestJob");
        Assert.True(triggers.First().Key.Group == "TestJob");
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == old);

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.RescheduleJob(type, QuartzTool.CreateTrigger(newc, type));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == newc);

        await QuartzNode.Scheduler.DeleteAllJobs();
        // 如果没有对应 TriggerKey 的任务，则新建一个
        await QuartzNode.Scheduler.RescheduleJob(type, old, new TriggerKey("1", "1"));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "1");
        Assert.True(triggers.First().Key.Group == "1");
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == old);

        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.RescheduleJob(type, newc, "2", "2");
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "2");
        Assert.True(triggers.First().Key.Group == "2");
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == newc);
    }


    [Theory]
    [InlineData("0/5 * * * * ?", "0/10 * * * * ?")]
    [InlineData("0/15 * * * * ?", "0/25 * * * * ?")]
    public async Task RescheduleJobWithoutTypeTest(string old, string newc)
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJob<TestJob>(old);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "TestJob");
        Assert.True(triggers.First().Key.Group == "TestJob");
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == old);

        await QuartzNode.Scheduler.RescheduleJob(QuartzTool.CreateTrigger<TestJob>(newc));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == newc);

        await QuartzNode.Scheduler.RescheduleJob(old, new TriggerKey("1", "1"));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True(triggers.First().Key.Name == "TestJob");
        Assert.True(triggers.First().Key.Group == "TestJob");
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == newc);

        await QuartzNode.Scheduler.RescheduleJob(old, "TestJob", "TestJob");
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == old);


        await QuartzNode.Scheduler.RescheduleJob(QuartzTool.CreateTrigger(old, "1", "1"), "TestJob");
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == 1);
        Assert.True((triggers.First() as CronTriggerImpl).CronExpressionString == old);
    }


    [Theory]
    [InlineData("0/5 * * * * ?", "0/10 * * * * ?")]
    [InlineData("0/15 * * * * ?", "0/25 * * * * ?")]
    public async Task RescheduleJobsTest(params object[] objs)
    {
        await QuartzNode.Scheduler.DeleteAllJobs();
        await QuartzNode.Scheduler.ScheduleJobs<TestJob>(objs);
        var triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == objs.Length);

        await QuartzNode.Scheduler.RescheduleJobs<TestJob>(objs);
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == objs.Length);

        await QuartzNode.Scheduler.RescheduleJobs(typeof(TestJob), objs);
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == objs.Length);

        await QuartzNode.Scheduler.RescheduleJobs(QuartzTool.CreateTriggers<TestJob>(objs));
        triggers = await QuartzNode.Scheduler.GetTriggers();
        Assert.True(triggers.Count == objs.Length);
    }
}