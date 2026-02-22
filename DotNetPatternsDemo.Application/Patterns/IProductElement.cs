// Visitor Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Element interface
    public interface IProductElement
    {
        void Accept(IProductVisitor visitor);
    }

    // Concrete Elements
    public class PhysicalProduct : IProductElement
    {
        public string Name { get; }
        public decimal Price { get; }
        public decimal Weight { get; }

        public PhysicalProduct(string name, decimal price, decimal weight)
        {
            Name = name;
            Price = price;
            Weight = weight;
        }

        public void Accept(IProductVisitor visitor) => visitor.Visit(this);
    }

    public class DigitalProduct : IProductElement
    {
        public string Name { get; }
        public decimal Price { get; }
        public string DownloadLink { get; }

        public DigitalProduct(string name, decimal price, string downloadLink)
        {
            Name = name;
            Price = price;
            DownloadLink = downloadLink;
        }

        public void Accept(IProductVisitor visitor) => visitor.Visit(this);
    }

    // Visitor interface
    public interface IProductVisitor
    {
        void Visit(PhysicalProduct product);
        void Visit(DigitalProduct product);
    }

    // Concrete Visitor
    public class ReportVisitor : IProductVisitor
    {
        public decimal TotalPrice { get; private set; }
        public int PhysicalCount { get; private set; }
        public int DigitalCount { get; private set; }

        public void Visit(PhysicalProduct product)
        {
            TotalPrice += product.Price;
            PhysicalCount++;
            Console.WriteLine(
                $"Physical: {product.Name} - Price: {product.Price:C} - Weight: {product.Weight} kg");
        }

        public void Visit(DigitalProduct product)
        {
            TotalPrice += product.Price;
            DigitalCount++;
            Console.WriteLine(
                $"Digital: {product.Name} - Price: {product.Price:C} - Link: {product.DownloadLink}");
        }
    }
}