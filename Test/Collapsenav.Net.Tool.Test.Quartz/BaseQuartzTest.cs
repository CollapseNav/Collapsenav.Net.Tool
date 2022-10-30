using Collapsenav.Net.Tool.Ext;
using Quartz;
using Quartz.Impl.Triggers;

namespace Collapsenav.Net.Tool.Test.Quartz;

public class BaseQuartzTest
{
    public BaseQuartzTest()
    {
    }
    [Theory]
    [InlineData(3, "TestJob_", "TestTest")]
    [InlineData(3, "TestJob_", "ASD")]
    [InlineData(3, "TestJob_", "WTFWTF")]
    public void CreateJobKeyTest(int count, string pre, string group)
    {
        var jobKeys = QuartzTool.CreateJobKeys<TestJob>(count);
        Assert.Equal(count, jobKeys.Count());
        foreach (var (key, index) in jobKeys.SelectWithIndex())
        {
            Assert.True(key.Name == $"{pre}{index}");
            Assert.True(key.Group == $"{nameof(TestJob)}");
        }
        jobKeys = QuartzTool.CreateJobKeys(count, typeof(TestJob));
        Assert.Equal(count, jobKeys.Count());
        foreach (var (key, index) in jobKeys.SelectWithIndex())
        {
            Assert.True(key.Name == $"{pre}{index}");
            Assert.True(key.Group == $"{nameof(TestJob)}");
        }
        jobKeys = QuartzTool.CreateJobKeys<TestJob>(count, group);
        Assert.Equal(count, jobKeys.Count());
        foreach (var (key, index) in jobKeys.SelectWithIndex())
        {
            Assert.True(key.Name == $"{pre}{index}");
            Assert.True(key.Group == $"{group}");
        }
    }

    [Theory]
    [InlineData(3, "TestJob_", "TestTest")]
    [InlineData(3, "TestJob_", "ASD")]
    [InlineData(3, "TestJob_", "WTFWTF")]
    public void CreateTriggerKeyTest(int count, string pre, string group)
    {
        var jobKeys = QuartzTool.CreateTriggerKeys<TestJob>(count);
        Assert.Equal(count, jobKeys.Count());
        foreach (var (key, index) in jobKeys.SelectWithIndex())
        {
            Assert.True(key.Name == $"{pre}{index}");
            Assert.True(key.Group == $"{nameof(TestJob)}");
        }
        jobKeys = QuartzTool.CreateTriggerKeys(count, typeof(TestJob));
        Assert.Equal(count, jobKeys.Count());
        foreach (var (key, index) in jobKeys.SelectWithIndex())
        {
            Assert.True(key.Name == $"{pre}{index}");
            Assert.True(key.Group == $"{nameof(TestJob)}");
        }
        jobKeys = QuartzTool.CreateTriggerKeys<TestJob>(count, group);
        Assert.Equal(count, jobKeys.Count());
        foreach (var (key, index) in jobKeys.SelectWithIndex())
        {
            Assert.True(key.Name == $"{pre}{index}");
            Assert.True(key.Group == $"{group}");
        }
    }

    [Theory]
    [InlineData(3, "TestJob_", "TestTest")]
    [InlineData(3, "TestJob_", "ASD")]
    [InlineData(3, "TestJob_", "WTFWTF")]
    public void CreateJobTriggerKeyTest(int count, string pre, string group)
    {
        var jatKey = QuartzTool.CreateJobKeyAndTriggerKey<TestJob>(count);
        Assert.Equal(count, jatKey.Count());
        foreach (var (key, index) in jatKey.SelectWithIndex())
        {
            Assert.True(key.JKey.Name == $"{pre}{index}");
            Assert.True(key.JKey.Group == $"{nameof(TestJob)}");
            Assert.True(key.TKey.Name == $"{pre}{index}");
            Assert.True(key.TKey.Group == $"{nameof(TestJob)}");
        }
        jatKey = QuartzTool.CreateJobKeyAndTriggerKey(count, typeof(TestJob));
        Assert.Equal(count, jatKey.Count());
        foreach (var (key, index) in jatKey.SelectWithIndex())
        {
            Assert.True(key.JKey.Name == $"{pre}{index}");
            Assert.True(key.JKey.Group == $"{nameof(TestJob)}");
            Assert.True(key.TKey.Name == $"{pre}{index}");
            Assert.True(key.TKey.Group == $"{nameof(TestJob)}");
        }
        jatKey = QuartzTool.CreateJobKeyAndTriggerKey<TestJob>(count, group);
        Assert.Equal(count, jatKey.Count());
        foreach (var (key, index) in jatKey.SelectWithIndex())
        {
            Assert.True(key.JKey.Name == $"{pre}{index}");
            Assert.True(key.JKey.Group == $"{group}");
            Assert.True(key.TKey.Name == $"{pre}{index}");
            Assert.True(key.TKey.Group == $"{group}");
        }
    }
    [Theory]
    [InlineData("1", "1")]
    [InlineData("1", "2")]
    public void CreateJobTest(string name, string group)
    {
        var job = QuartzTool.CreateJob<TestJob>();
        Assert.True(job.Key.Name == $"{nameof(TestJob)}");
        Assert.True(job.Key.Group == $"{nameof(TestJob)}");
        job = QuartzTool.CreateJob<TestJob>(new JobKey(name, group));
        Assert.True(job.Key.Name == name);
        Assert.True(job.Key.Group == group);
        job = QuartzTool.CreateJob<TestJob>(name, group);
        Assert.True(job.Key.Name == name);
        Assert.True(job.Key.Group == group);
    }
    [Theory]
    [InlineData(10, "1", "1")]
    [InlineData(20, "1", "2")]
    [InlineData(30, "1", "2")]
    public void CreateSimpleTriggerTest(object cron, string name, string group)
    {
        var trigger = QuartzTool.CreateTrigger<TestJob>(cron);
        Assert.True(trigger is SimpleTriggerImpl);
        Assert.True((trigger as SimpleTriggerImpl).RepeatInterval.Seconds == (int)cron);
        Assert.True(trigger.Key.Name == $"{nameof(TestJob)}");
        Assert.True(trigger.Key.Group == $"{nameof(TestJob)}");
        trigger = QuartzTool.CreateTrigger(cron, new TriggerKey(name, group));
        Assert.True(trigger is SimpleTriggerImpl);
        Assert.True((trigger as SimpleTriggerImpl).RepeatInterval.Seconds == (int)cron);
        Assert.True(trigger.Key.Name == name);
        Assert.True(trigger.Key.Group == group);
        trigger = QuartzTool.CreateTrigger(cron, name, group);
        Assert.True(trigger is SimpleTriggerImpl);
        Assert.True((trigger as SimpleTriggerImpl).RepeatInterval.Seconds == (int)cron);
        Assert.True(trigger.Key.Name == name);
        Assert.True(trigger.Key.Group == group);
    }
    [Theory]
    [InlineData("0/15 * * * * ?", "1", "1")]
    [InlineData("0/25 * * * * ?", "1", "2")]
    [InlineData("0/35 * * * * ?", "1", "2")]
    public void CreateCronTriggerTest(object cron, string name, string group)
    {
        var trigger = QuartzTool.CreateTrigger<TestJob>(cron);
        Assert.True(trigger is CronTriggerImpl);
        Assert.True((trigger as CronTriggerImpl).CronExpressionString == cron.ToString());
        Assert.True(trigger.Key.Name == $"{nameof(TestJob)}");
        Assert.True(trigger.Key.Group == $"{nameof(TestJob)}");
        trigger = QuartzTool.CreateTrigger(cron, new TriggerKey(name, group));
        Assert.True(trigger is CronTriggerImpl);
        Assert.True((trigger as CronTriggerImpl).CronExpressionString == cron.ToString());
        Assert.True(trigger.Key.Name == name);
        Assert.True(trigger.Key.Group == group);
        trigger = QuartzTool.CreateTrigger(cron, name, group);
        Assert.True(trigger is CronTriggerImpl);
        Assert.True((trigger as CronTriggerImpl).CronExpressionString == cron.ToString());
        Assert.True(trigger.Key.Name == name);
        Assert.True(trigger.Key.Group == group);
    }

    [Theory]
    [InlineData(1, 2, 3)]
    public void CreateSimpleTriggersTest(params int[] objs)
    {
        var triggers = QuartzTool.CreateTriggers<TestJob>(objs);
        Assert.True(objs.Length == triggers.Count());
        foreach (var (trigger, index) in triggers.SelectWithIndex())
        {
            Assert.True((trigger as SimpleTriggerImpl).RepeatInterval.Seconds == (int)objs[index]);
            Assert.True(trigger.Key.Name == $"{nameof(TestJob)}_{index}");
            Assert.True(trigger.Key.Group == $"{nameof(TestJob)}");
        }
    }
    [Theory]
    [InlineData("0/5 * * * * ?", "0/10 * * * * ?", "0/15 * * * * ?")]
    public void CreateCronTriggersTest(params string[] objs)
    {
        var triggers = QuartzTool.CreateTriggers<TestJob>(objs);
        Assert.True(objs.Length == triggers.Count());
        foreach (var (trigger, index) in triggers.SelectWithIndex())
        {
            Assert.True((trigger as CronTriggerImpl).CronExpressionString == objs[index]);
            Assert.True(trigger.Key.Name == $"{nameof(TestJob)}_{index}");
            Assert.True(trigger.Key.Group == $"{nameof(TestJob)}");
        }
    }
}