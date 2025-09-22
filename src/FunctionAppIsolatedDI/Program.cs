using FunctionAppIsolatedDI.Implementations;
using FunctionAppIsolatedDI.Interfaces;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services.AddSingleton<ITesteA, TesteA>();
builder.Services.AddTransient<ITesteB, TesteB>();
builder.Services.AddScoped<TesteC>();
builder.Services.AddTransient<TesteInjecao>();

builder.Build().Run();
