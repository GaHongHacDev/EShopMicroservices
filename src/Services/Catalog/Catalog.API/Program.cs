using BuildingBlocks.Exceptions.Handler;

using JasperFx;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string
var connectionString = builder.Configuration.GetConnectionString("MartenDatabase");

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

app.Run();
