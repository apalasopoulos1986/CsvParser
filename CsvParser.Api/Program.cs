using CsvParser.Api.Configuration;
using CsvParser.Common.HelperMethods;
using CsvParser.Db.Context;
using CsvParser.Db.Interfaces;
using CsvParser.Db.Repository;

var builder = WebApplication.CreateBuilder(args);
// Configure logging
builder.Logging.ClearProviders();
builder.Logging.AddConsole();
builder.Logging.AddLoggingFile(builder.Configuration.GetSection("Logging:File"));

builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<ITransactionsRepository,TransactionsRepository>();
builder.Services.AddServices();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new CustomDateTimeConverter());
        options.JsonSerializerOptions.Converters.Add(new EmptyStringToGuidConverter());
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
