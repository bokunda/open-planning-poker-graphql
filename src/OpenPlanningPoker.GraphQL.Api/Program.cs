var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddServices(new AppSettings());
builder.Services.AddGraphQlWithSchema();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.Map("/api", builder =>
{
    builder
        .UseSwagger()
        .UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/api/swagger/v1/swagger.json", "Open Planning Poker GraphQL v1");
        });

    builder
        .UseRouting()
        .UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQLSchema();
            endpoints.MapBananaCakePop();
            endpoints.MapGraphQLHttp();
            endpoints.MapControllers();
        });
});


app.Run();
