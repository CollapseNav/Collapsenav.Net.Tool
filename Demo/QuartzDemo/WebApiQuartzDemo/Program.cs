using Collapsenav.Net.Tool;
using Collapsenav.Net.Tool.Demo.Quartz;
using Collapsenav.Net.Tool.Ext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer()
.AddSwaggerGen();

builder.Services
// 注册 QuartzModel
.AddSingleton(new DIModel
{
    Name = "New DI Model"
})
.AddJob<FirstJob>(10)
.AddJob<SecondJob>("0/5 * * * * ?")
.AddJobs<MutJob>(CronTool.CreateCrons(70))
.AddJob<DIJob>(3)
.AddJob<ReJob>(10)
.AddDefaultQuartzService(builder => builder.AddJob<UseBuilderJob>(1))
// .AddDefaultQuartzService()
;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapControllers();
app.Run();
