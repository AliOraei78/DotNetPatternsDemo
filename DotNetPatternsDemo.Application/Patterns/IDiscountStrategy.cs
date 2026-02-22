// Strategy Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Strategy interface
    public interface IDiscountStrategy
    {
        decimal ApplyDiscount(decimal originalPrice);
    }

    // Concrete Strategies
    public class NoDiscountStrategy : IDiscountStrategy
    {
        public decimal ApplyDiscount(decimal originalPrice) => originalPrice;
    }

    public class PercentageDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _percentage;

        public PercentageDiscountStrategy(decimal percentage)
        {
            _percentage = percentage;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            return originalPrice * (1 - _percentage / 100);
        }
    }

    public class FixedAmountDiscountStrategy : IDiscountStrategy
    {
        private readonly decimal _fixedAmount;

        public FixedAmountDiscountStrategy(decimal fixedAmount)
        {
            _fixedAmount = fixedAmount;
        }

        public decimal ApplyDiscount(decimal originalPrice)
        {
            return Math.Max(0, originalPrice - _fixedAmount);
        }
    }

    // Context
    public class OrderWithDiscount
    {
        public decimal OriginalPrice { get; }
        private IDiscountStrategy _discountStrategy;

        public OrderWithDiscount(decimal originalPrice, IDiscountStrategy initialStrategy)
        {
            OriginalPrice = originalPrice;
            _discountStrategy = initialStrategy;
        }

        public void SetDiscountStrategy(IDiscountStrategy strategy)
        {
            _discountStrategy = strategy;
        }

        public decimal GetFinalPrice()
        {
            return _discountStrategy.ApplyDiscount(OriginalPrice);
        }
    }
}