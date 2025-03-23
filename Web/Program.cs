using Biz.Configs;
using Biz.Services;
using Contract.Biz;
using Contract.Dac;
using Dac.Daos;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<UserStorage>();

builder.Services.AddSingleton(builder.Configuration.GetSection("ConnectionStrings").Get<DatabaseConfig>()!);

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

app.MapControllers();

app.Run();
