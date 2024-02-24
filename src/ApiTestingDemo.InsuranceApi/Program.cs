using System.Text.Json.Serialization;
using ApiTestingDemo.InsuranceApi.Bureau;
using ApiTestingDemo.InsuranceApi.Quotation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(o
    => o.EnableAnnotations());
builder.Services.ConfigureHttpJsonOptions(options =>
    options.SerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddSingleton<TimeProvider>(TimeProvider.System);

builder.Services.AddBureau();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapQuotes();

app.Run();



// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program;
