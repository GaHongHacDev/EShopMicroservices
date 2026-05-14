using JasperFx;

var builder = WebApplication.CreateBuilder(args);

// Retrieve the connection string
var connectionString = builder.Configuration.GetConnectionString("MartenDatabase");

// Add Marten services
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


//Add services to the container.
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapCarter();


app.Run();
