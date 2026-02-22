// Flyweight Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Flyweight (intrinsic state – shared)
    public class ProductType
    {
        public string Category { get; }
        public string Description { get; }
        public decimal BasePrice { get; }

        public ProductType(string category, string description, decimal basePrice)
        {
            Category = category;
            Description = description;
            BasePrice = basePrice;
        }

        public void Display(string name, int quantity)
        {
            Console.WriteLine($"{name} ({Category}): {Description} - Unit Price: {BasePrice:C} - Quantity: {quantity}");
        }
    }

    // Flyweight Factory
    public class ProductTypeFactory
    {
        private readonly Dictionary<string, ProductType> _flyweights = new();

        public ProductType GetProductType(string category, string description, decimal basePrice)
        {
            string key = $"{category}:{description}:{basePrice}";

            if (!_flyweights.TryGetValue(key, out var flyweight))
            {
                flyweight = new ProductType(category, description, basePrice);
                _flyweights[key] = flyweight;
                Console.WriteLine($"Created new Flyweight: {key}");
            }

            return flyweight;
        }
    }
}