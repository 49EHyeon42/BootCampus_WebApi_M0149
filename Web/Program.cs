using Biz.Configs;
using Biz.Services;
using Contract.Biz;
using Contract.Dac;
using Dac.Daos;
using Web;
using Web.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<UserStorage>();

builder.Services.AddSingleton<IDatabaseConfig>(provider => new MsSqlConfig(builder.Configuration.GetConnectionString("Default")!));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IWorkExperienceService, WorkExperienceService>();
builder.Services.AddScoped<IEmployeeDao, MsSqlEmployeeDao>();
builder.Services.AddScoped<IWorkExperienceDao, MsSqlWorkExperienceDao>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<RequestLoggingMiddleware>();

app.MapControllers();

app.Run();
