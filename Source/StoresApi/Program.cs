using StoresApi.Middleware;
using StoresApi.Services;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IRepository, StoresRepository>();

WebApplication app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseMiddleware<CountryCodeMiddleware>();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
