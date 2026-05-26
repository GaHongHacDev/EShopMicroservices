var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

#region Add services to the container. 
var Assembly = typeof(Program).Assembly;

#region Add Marten (Session) Dependency Injection
var connectionString = builder.Configuration.GetConnectionString("BasketDatabase");
builder.Services.AddMarten(options =>
{
    // Establish the connection string
    options.Connection(connectionString!);

    //Config Identity - Kiểu ID để nhận dạng trong Document PostgreSQL mà ko dùng Guid,
    //mà dùng string (Username) để nhận dạng luôn
    //Hoặc có thể dùng Attribute [Identity] trong Property Username của class ShoppingCart
    options.Schema.For<ShoppingCart>().Identity(x => x.Username);

    // Optional: Set the schema name (default is "public")
    // options.DatabaseSchemaName = "BasketSchema";

    // Optional: Automatically create tables in development
    //options.AutoCreateSchemaObjects = AutoCreate.All;
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
#endregion

var app = builder.Build();

//Configure the HTTP request pipeline.
app.MapCarter();

app.Run();
