using Collapsenav.Net.Tool.Demo.Quartz;
using Collapsenav.Net.Tool.Ext;

QuartzNode.Builder.AddJob<FirstJob>(3);
QuartzNode.Builder.AddJob<SecondJob>("0/5 * * * * ?");
QuartzNode.Builder.AddJob<MutJob>(new[] { "0/5 * * * * ?", "0/20 * * * * ?" });

await QuartzNode.InitSchedulerAsync();
await QuartzNode.Build();
await QuartzNode.Scheduler.Start();

Console.ReadKey();