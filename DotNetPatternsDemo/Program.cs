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

var order = new OrderBuilder()
    .WithCustomerName("Joe Jones")
    .WithItem("Dell XPS Laptop")
    .WithItem("Logitech Mouse")
    .WithTotalAmount(45000000m)
    .WithShippingAddress("NYC, Times Sq")
    .WithPaymentMethod("Card-to-Card")
    .Build();

Console.WriteLine(order);

var baseTemplate = new OrderTemplate
{
    TemplateName = "VIP Template",
    CustomerCategory = "VIP",
    DefaultItems = { "Special Product 1", "Special Product 2" },
    DefaultDiscount = 15m
};

var newOrderTemplate = baseTemplate.Clone();
newOrderTemplate.TemplateName = "New VIP Template";
newOrderTemplate.DefaultItems.Add("Special Product 3"); // Should not affect base

Console.WriteLine("Base Template:");
Console.WriteLine(baseTemplate);

Console.WriteLine("\nCloned Template:");
Console.WriteLine(newOrderTemplate);

app.Run();


