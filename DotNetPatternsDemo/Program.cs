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

Console.WriteLine("\n\n");

NotificationFactory factory = new EmailNotificationFactory();
factory.SendNotification("user@example.com", "Hello, your order has been registered.");

factory = new SmsNotificationFactory();
factory.SendNotification("09123456789", "Your order has been confirmed.");

Console.WriteLine("\n\n");

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

Console.WriteLine("\n\n");

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

Console.WriteLine("\n\n");

var legacy = new LegacyPaymentGateway();
var adapter = new LegacyPaymentAdapter(legacy);

bool success = adapter.ProcessPayment(1500000m, "1234-5678-9012-3456");
Console.WriteLine($"Payment is success: {success}");

Console.WriteLine("\n\n");

IDevice tv = new Tv();
AdvancedRemoteControl remote = new AdvancedRemoteControl(tv);

remote.TurnOn();
remote.SetVolume(25);
remote.Mute();
remote.TurnOff();

Console.WriteLine("\n\n");

var mainMenu = new MenuGroup("Main Menu");

var fileMenu = new MenuGroup("File");
fileMenu.Add(new MenuItem("New"));
fileMenu.Add(new MenuItem("Open"));
fileMenu.Add(new MenuItem("Save"));

var editMenu = new MenuGroup("Edit");
editMenu.Add(new MenuItem("Cut"));
editMenu.Add(new MenuItem("Copy"));

mainMenu.Add(fileMenu);
mainMenu.Add(editMenu);
mainMenu.Add(new MenuItem("Exit"));

mainMenu.Display();

Console.WriteLine("\n\n");

INotificationService service = new BasicNotificationService();

service = new LoggingNotificationDecorator(service);
service = new TimestampNotificationDecorator(service);

service.Send("user@example.com", "Test decorator");

Console.WriteLine("\n\n");

var facade = new OrderProcessingFacade();
var orderB = new OrderBuilder()
    .WithCustomerName("Joe Jonas")
    .WithItem("Design Patterns Book")         // ← added this line
    .WithTotalAmount(1200000m)
    .WithShippingAddress("NYC, Book Sq") // optional
    .Build();

bool successB = facade.ProcessOrder(orderB);
Console.WriteLine($"Order processed successfully: {successB}");

Console.WriteLine("\n\n");

var factoryProduct = new ProductTypeFactory();

var laptopType = factoryProduct.GetProductType("Electronics", "Gaming Laptop", 45000000m);
var phoneType = factoryProduct.GetProductType("Mobile", "Smartphone", 25000000m);

laptopType.Display("Dell XPS 13", 5);
phoneType.Display("iPhone 14", 10);
laptopType.Display("Lenovo Legion", 3);  // Reuses the same previous Flyweight
Console.WriteLine("\n\n");

IExpensiveService serviceP = new CachingProxy(new RealExpensiveService());

Console.WriteLine(serviceP.GetData("Product 1"));  // actual request
Console.WriteLine(serviceP.GetData("Product 1"));  // from cache
Console.WriteLine(serviceP.GetData("Product 2"));  // actual request
Console.WriteLine("\n\n");

var supervisor = new Supervisor();
var manager = new Manager();
var hr = new HRDirector();

supervisor.SetNext(manager);
manager.SetNext(hr);

var request = new LeaveRequest { Days = 5, Employee = "Sam Smith" };
supervisor.ProcessRequest(request);  // Manager will handle
Console.WriteLine("\n\n");

var light = new Light();
var remoteC = new RemoteController();

remoteC.SetCommand(new LightOnCommand(light));
remoteC.PressButton();   // On
remoteC.PressUndo();     // Off
Console.WriteLine("\n\n");

var context = new Dictionary<string, int> { { "x", 10 }, { "y", 3 } };

var expression = new Minus(
    new Plus(new Variable("x"), new Number(5)),
    new Variable("y")
);

int result = expression.Interpret(context);
Console.WriteLine($"Result: {result}");  // 12
Console.WriteLine("\n\n");

var root = new TreeNode(1);
var child1 = new TreeNode(2); root.Add(child1);
var child2 = new TreeNode(3); root.Add(child2);
child1.Add(new TreeNode(4));

var iterator = new DepthFirstIterator(root);
while (iterator.MoveNext())
{
    Console.WriteLine(iterator.Current.Value);  // 1 → 3 → 2 → 4
}
Console.WriteLine("\n\n");



app.Run();


