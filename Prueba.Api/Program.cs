using Microsoft.EntityFrameworkCore;
using Prueba.Api.DataAccess;
using Prueba.Api.Domains;
using Prueba.Api.Domains.State;
using Prueba.Api.Domains.Task;
using Prueba.Api.Services.Files;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DbContexts>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region Add Layout
builder.Services.AddTransient<IStateDomainGetStateById, StateDomainGetStateById>();
builder.Services.AddTransient<IStateDomainGetStateList, StateDomainGetStateList>();
builder.Services.AddTransient<ITaskDomainCreate, TaskDomainCreate>();
builder.Services.AddTransient<ITaskDomainDelete, TaskDomainDelete>();
builder.Services.AddTransient<ITaskDomainUpdate, TaskDomainUpdate>();
builder.Services.AddTransient<ITaskDomainGet, TaskDomainGet>();
builder.Services.AddTransient<ITaskDomainGetSumTasksFilterState, TaskDomainGetSumTasksFilterState>();
builder.Services.AddTransient<ISaveFileService, SaveFileService>();
#endregion

#region Cors
builder.Services.AddCors( options =>
{
    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
