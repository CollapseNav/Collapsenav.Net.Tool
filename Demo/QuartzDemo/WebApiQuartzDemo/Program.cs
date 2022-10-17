using Collapsenav.Net.Tool.Ext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer()
.AddSwaggerGen();
builder.Services
.AddAllJob()
// 注册 QuartzModel
// .AddJob<SecondJob>("0/5 * * * * ?")
// .AddJobs<MutJob>(CronTool.CreateCrons(70))
// .AddJob<ReJob>(10)
// .AddDefaultQuartzService(builder => builder.AddJob<UseBuilderJob>(1))
.AddQuartzJsonConfig(builder.Configuration.GetSection("Quartz1"))
.AddQuartzJsonConfig(builder.Configuration.GetSection("Quartz2"))
.AddDefaultQuartzService()
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
