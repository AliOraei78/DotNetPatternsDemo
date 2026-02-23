using AdvancedDotNetPatternsDemo.Application.Features.Todo.Commands;
using AdvancedDotNetPatternsDemo.Application.Features.Todo.Queries;
using AdvancedDotNetPatternsDemo.Application.Patterns;
using AdvancedDotNetPatternsDemo.Infrastructure.Persistence;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString, b =>
        /* English comment: 
           Explicitly set the assembly where migrations will be generated.
           Replace "DotNetPatternsDemo.Application" with your actual Application project name. 
        */
        b.MigrationsAssembly("DotNetPatternsDemo.Application")));
builder.Services.AddHangfire(config => config
    .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
    .UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(connectionString));
// Registering services
builder.Services.AddHangfireServer();
builder.Services.AddScoped<OutboxProcessorJob>();
builder.Services.AddLogging();
builder.Services.AddScoped<IOrderRepository, SqlOrderRepository>();
builder.Services.AddScoped<ILoggerService, ConsoleLogger>();
builder.Services.AddScoped<OrderService>();  // Automatic constructor injection
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(
    typeof(CreateTodoCommand).Assembly,
    typeof(GetTodoByIdQuery).Assembly
));
builder.Services.AddSingleton<InMemoryTodoRepository>();
builder.Services.AddSingleton<ITodoRepository>(sp => sp.GetRequiredService<InMemoryTodoRepository>());

builder.Services.TryAddSingleton<ITodoReadRepository, InMemoryTodoReadRepository>();

// For simple testing
builder.Services.AddSingleton<IOrderRepository, SqlOrderRepository>(); // Or Scoped
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHangfireDashboard();
RecurringJob.AddOrUpdate<OutboxProcessorJob>(
    "outbox-processor",
    job => job.ProcessPendingMessages(),
    "*/10 * * * * *");

// In the Main method or somewhere for testing
// ------------------Singleton Pattern------------------------
Console.WriteLine("------------------Singleton Pattern------------------------");
var logger1 = LoggerSingleton.Instance;
logger1.Log("First test");

var logger2 = LoggerSingleton.Instance;
logger2.Log("Second test");

Console.WriteLine(ReferenceEquals(logger1, logger2)
    ? "Same instance"
    : "Different instances");
Console.WriteLine("\n\n");

// ------------------Factory Method Pattern------------------------
Console.WriteLine("------------------Factory Method Pattern------------------------");
NotificationFactory factory = new EmailNotificationFactory();
factory.SendNotification("user@example.com", "Hello, your order has been registered.");

factory = new SmsNotificationFactory();
factory.SendNotification("09123456789", "Your order has been confirmed.");
Console.WriteLine("\n\n");

// ------------------Abstract Factory Pattern------------------------
Console.WriteLine("------------------Abstract Factory Pattern------------------------");
IUiFactory factoryUi = new DarkUiFactory();
factoryUi.CreateButton().Render();
factoryUi.CreateCheckbox().Render();
Console.WriteLine("\n\n");

// ------------------Builder Pattern------------------------
Console.WriteLine("------------------Builder Pattern------------------------");
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

// ------------------Prototype Pattern------------------------
Console.WriteLine("------------------Prototype Pattern------------------------");
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

// ------------------Adapter Pattern------------------------
Console.WriteLine("------------------Adapter Pattern------------------------");
var legacy = new LegacyPaymentGateway();
var adapter = new LegacyPaymentAdapter(legacy);

bool success = adapter.ProcessPayment(1500000m, "1234-5678-9012-3456");
Console.WriteLine($"Payment is success: {success}");
Console.WriteLine("\n\n");

// ------------------Bridge Pattern------------------------
Console.WriteLine("------------------Bridge Pattern------------------------");
IDevice tv = new Tv();
AdvancedRemoteControl remote = new AdvancedRemoteControl(tv);

remote.TurnOn();
remote.SetVolume(25);
remote.Mute();
remote.TurnOff();
Console.WriteLine("\n\n");

// ------------------Composite Pattern------------------------
Console.WriteLine("------------------Composite Pattern------------------------");
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

// ------------------Decorator Pattern------------------------
Console.WriteLine("------------------Decorator Pattern------------------------");
INotificationService service = new BasicNotificationService();

service = new LoggingNotificationDecorator(service);
service = new TimestampNotificationDecorator(service);

service.Send("user@example.com", "Test decorator");
Console.WriteLine("\n\n");

// ------------------Facade Pattern------------------------
Console.WriteLine("------------------Facade Pattern------------------------");
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

// ------------------Flyweight Pattern------------------------
Console.WriteLine("------------------Flyweight Pattern------------------------");
var factoryProduct = new ProductTypeFactory();

var laptopType = factoryProduct.GetProductType("Electronics", "Gaming Laptop", 45000000m);
var phoneType = factoryProduct.GetProductType("Mobile", "Smartphone", 25000000m);

laptopType.Display("Dell XPS 13", 5);
phoneType.Display("iPhone 14", 10);
laptopType.Display("Lenovo Legion", 3);  // Reuses the same previous Flyweight
Console.WriteLine("\n\n");

// ------------------Proxy Pattern------------------------
Console.WriteLine("------------------Proxy Pattern------------------------");
IExpensiveService serviceP = new CachingProxy(new RealExpensiveService());

Console.WriteLine(serviceP.GetData("Product 1"));  // actual request
Console.WriteLine(serviceP.GetData("Product 1"));  // from cache
Console.WriteLine(serviceP.GetData("Product 2"));  // actual request
Console.WriteLine("\n\n");

// ------------------Chain of Responsibility Pattern------------------------
Console.WriteLine("------------------Chain of Responsibility Pattern------------------------");
var supervisor = new Supervisor();
var manager = new Manager();
var hr = new HRDirector();

supervisor.SetNext(manager);
manager.SetNext(hr);

var request = new LeaveRequest { Days = 5, Employee = "Sam Smith" };
supervisor.ProcessRequest(request);  // Manager will handle
Console.WriteLine("\n\n");

// ------------------Command Pattern------------------------
Console.WriteLine("------------------Command Pattern------------------------");
Console.WriteLine("Command Pattern");
var light = new Light();
var remoteC = new RemoteController();

remoteC.SetCommand(new LightOnCommand(light));
remoteC.PressButton();   // On
remoteC.PressUndo();     // Off
Console.WriteLine("\n\n");

// ------------------Interpreter Pattern------------------------
Console.WriteLine("------------------Interpreter Pattern------------------------");
var context = new Dictionary<string, int> { { "x", 10 }, { "y", 3 } };

var expression = new Minus(
    new Plus(new Variable("x"), new Number(5)),
    new Variable("y")
);

int result = expression.Interpret(context);
Console.WriteLine($"Result: {result}");  // 12
Console.WriteLine("\n\n");

// ------------------Iterator Pattern------------------------
Console.WriteLine("------------------Iterator Pattern------------------------");
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

// ------------------Mediator Pattern------------------------
Console.WriteLine("------------------Mediator Pattern------------------------");
var chat = new ChatRoom();

var john = new ConcreteUser(chat, "John");
var sarah = new ConcreteUser(chat, "Sarah");
var laura = new ConcreteUser(chat, "Laura");

chat.RegisterUser(john);
chat.RegisterUser(sarah);
chat.RegisterUser(laura);

john.Send("Hi everyone!");
sarah.Send("Hi John, how are you?");
Console.WriteLine("\n\n");

// ------------------Memento Pattern------------------------
Console.WriteLine("------------------Memento Pattern------------------------");
var editor = new TextEditor();
var history = new History();

editor.Type("Hello");
history.Save(editor);

editor.Type(" World!");
history.Save(editor);

history.Undo(editor);  // Returns to "Hello"
history.Undo(editor);  // Returns to ""
Console.WriteLine("\n\n");

// ------------------Observer Pattern------------------------
Console.WriteLine("------------------Observer Pattern------------------------");
var publisher = new NewsPublisher();

var sub1 = new NewsSubscriber("Jack");
var sub2 = new NewsSubscriber("Katy");

publisher.Attach(sub1);
publisher.Attach(sub2);

publisher.PublishNews("Yesterday, an earthquake occurred in ...");
Console.WriteLine("\n\n");

// ------------------State Pattern------------------------
Console.WriteLine("------------------State Pattern------------------------");
var machine = new VendingMachine();

machine.InsertCoin(3000);
machine.SelectProduct("Soda");     // Not enough balance
machine.InsertCoin(3000);
machine.SelectProduct("Soda");
machine.Dispense();
Console.WriteLine("\n\n");

// ------------------Strategy Pattern------------------------
Console.WriteLine("------------------Strategy Pattern------------------------");
var orderD = new OrderWithDiscount(1000000m, new NoDiscountStrategy());

Console.WriteLine($"Final price (no discount): {orderD.GetFinalPrice():C}");

orderD.SetDiscountStrategy(new PercentageDiscountStrategy(15));
Console.WriteLine($"Final price (15% discount): {orderD.GetFinalPrice():C}");

orderD.SetDiscountStrategy(new FixedAmountDiscountStrategy(200000));
Console.WriteLine($"Final price (fixed 200,000 discount): {orderD.GetFinalPrice():C}");
Console.WriteLine("\n\n");

// ------------------Template Method Pattern------------------------
Console.WriteLine("------------------Template Method Pattern------------------------");
var onlineOrder = new OnlineOrderProcessing();
onlineOrder.ProcessOrder();
Console.WriteLine("\n\n");

// ------------------Visitor Pattern------------------------
Console.WriteLine("------------------Visitor Pattern------------------------");
var products = new List<IProductElement>
{
    new PhysicalProduct("Laptop", 45000000m, 2.1m),
    new DigitalProduct("E-Book", 150000m, "https://..."),
    new PhysicalProduct("Mouse", 800000m, 0.15m)
};

var visitor = new ReportVisitor();

foreach (var product in products)
{
    product.Accept(visitor);
}

Console.WriteLine($"\nTotal price: {visitor.TotalPrice:C}");
Console.WriteLine($"Physical products: {visitor.PhysicalCount} | Digital products: {visitor.DigitalCount}");
Console.WriteLine("\n\n");

// ------------------Ambient Context------------------------
Console.WriteLine("------------------Ambient Context------------------------");
UserContext.CurrentUserId = "user123";
Console.WriteLine(UserContext.CurrentUserId);
Console.WriteLine("\n\n");

// ------------------Method Injection------------------------
app.MapGet("/orders/{id}", (string id, [FromServices] IOrderRepository repo) =>
{
    // Method Injection using [FromServices]
    repo.Save(new OrderBuilder().WithCustomerName("Jack").Build());
    return Results.Ok();
});

// ------------------Constructor injection------------------------
app.MapGet("/test-di", (OrderService service) =>
{
    var order = new OrderBuilder()
        .WithCustomerName("DI Test")
        .WithItem("Test Product")
        .WithTotalAmount(1000000m)
        .Build();

    service.CreateOrder(order);
    return Results.Ok("Order created using DI");
});

app.MapPost("/todos", async (IMediator mediator, CreateTodoCommand cmd) =>
{
    var id = await mediator.Send(cmd);
    return Results.Created($"/todos/{id}", new { Id = id });
});

app.MapGet("/todos/{id:guid}", async (IMediator mediator, Guid id) =>
{
    var todo = await mediator.Send(new GetTodoByIdQuery(id));
    return todo is null ? Results.NotFound() : Results.Ok(todo);
});

app.MapGet("/todos", async (IMediator mediator) =>
{
    var todos = await mediator.Send(new GetAllTodosQuery());
    return Results.Ok(todos);
});

app.MapPut("/todos/{id:guid}/complete", async (IMediator mediator, Guid id) =>
{
    var success = await mediator.Send(new CompleteTodoCommand(id));
    return success ? Results.Ok() : Results.NotFound();
});

app.Run();

