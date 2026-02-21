using AdvancedDotNetPatternsDemo.Application.Patterns;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// In the Main method or somewhere for testing
var logger1 = LoggerSingleton.Instance;
logger1.Log("First test");

var logger2 = LoggerSingleton.Instance;
logger2.Log("Second test");

Console.WriteLine(ReferenceEquals(logger1, logger2)
    ? "Same instance"
    : "Different instances");

NotificationFactory factory = new EmailNotificationFactory();
factory.SendNotification("user@example.com", "Hello, your order has been registered.");

factory = new SmsNotificationFactory();
factory.SendNotification("09123456789", "Your order has been confirmed.");

IUiFactory factoryUi = new DarkUiFactory();
factoryUi.CreateButton().Render();
factoryUi.CreateCheckbox().Render();

app.Run();


