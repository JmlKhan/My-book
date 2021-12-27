using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using My_books.Data;
using My_books.Data.Services;
using My_books.Exceptions;
using Serilog;

Log.Logger = new LoggerConfiguration().CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.
builder.Services.AddControllers();

Log.CloseAndFlush();

//db setting
builder.Services.AddDbContext<AppDbContext>(options => 
            options.UseSqlServer(
                builder.Configuration.GetConnectionString("DefaultConnectionString"))
            );

builder.Services.AddTransient<AuthorService>();
builder.Services.AddTransient<PublisherService>();
builder.Services.AddTransient<BookService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//api versioning
builder.Services.AddApiVersioning(config =>
{
    config.DefaultApiVersion = new ApiVersion(1, 0);
    config.AssumeDefaultVersionWhenUnspecified = true;

    config.ApiVersionReader = new HeaderApiVersionReader("custom-version-header");
});


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

app.ConfigureBuildInExceptionHandler();

app.MapControllers();

//exception handling
AppDbInitializer.Seed(app);
//app.ConfigureCustomExceptionMiddleware();

app.Run();
