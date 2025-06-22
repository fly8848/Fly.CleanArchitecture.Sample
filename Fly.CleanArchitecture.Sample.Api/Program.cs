using Fly.CleanArchitecture.Sample.Api;
using Fly.CleanArchitecture.Sample.Application;
using Fly.CleanArchitecture.Sample.Domain;
using Fly.CleanArchitecture.Sample.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;
var configuration = builder.Configuration;

services.AddApi();
services.AddInfrastructure(configuration);
services.AddApplication();
services.AddDomain();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();