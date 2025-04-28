using PS.OrderService.API;
using PS.OrderService.Infrastructure;



var builder = WebApplication.CreateBuilder(args);


builder.Services
    .AddPresentation()
    .AddInfrastructure(builder.Configuration);


var app = builder.Build();
{

    if (app.Environment.IsDevelopment())
    {
        app.MapOpenApi();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}