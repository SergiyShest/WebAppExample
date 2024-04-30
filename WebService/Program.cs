using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Добавление сервисов в контейнер
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());


// Конфигурация контейнера Autofac
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
     containerBuilder.RegisterBuildCallback(c => Console.WriteLine("Container built successfully!"));
    var config = new ConfigurationBuilder();
    config.AddJsonFile("autofac.json");
    var module = new ConfigurationModule(config.Build());
    containerBuilder.RegisterModule(module);

});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();


