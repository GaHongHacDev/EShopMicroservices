using BuildingBlocks.Exceptions.Handler;

using Catalog.API.Data;

using HealthChecks.UI.Client;

using JasperFx;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string
var connectionString = builder.Configuration.GetConnectionString("CatalogDatabase");

#region Add services to the container. 

#region Add Marten (Session) Dependency Injection
builder.Services.AddMarten(options =>
{
    // Establish the connection string
    options.Connection(connectionString!);

    // Optional: Set the schema name (default is "public")
    options.DatabaseSchemaName = "CatalogSchema";

    // Optional: Automatically create tables in development
    options.AutoCreateSchemaObjects = AutoCreate.All;
})
.UseLightweightSessions(); // Recommended for most apps
#endregion

#region Add Fluent Validate DI 
builder.Services.AddValidatorsFromAssembly(
    typeof(Program).Assembly,
    includeInternalTypes: true
);
#endregion

#region Add Carter DI
builder.Services.AddCarter();
#endregion

#region Add MediatR DI
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<InitialCatalogData>();
}
#endregion

#region Add Health Check DI
builder.Services.AddHealthChecks().AddNpgSql(connectionString!);
#endregion

#region 
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
#endregion

#endregion

var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapCarter();

#region Exception handler 
app.UseExceptionHandler(options => { });
#endregion

#region Health Check Endpoint
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
#endregion

app.Run();
