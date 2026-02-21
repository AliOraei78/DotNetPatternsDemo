using System;

namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Builder interface (optional – for a fuller GoF version)
    public interface IOrderBuilder
    {
        IOrderBuilder WithCustomerName(string name);
        IOrderBuilder WithItem(string item);
        IOrderBuilder WithTotalAmount(decimal amount);
        IOrderBuilder WithShippingAddress(string address);
        IOrderBuilder WithPaymentMethod(string method);
        Order Build();
    }

    // Concrete Builder – often fluent
    public class OrderBuilder : IOrderBuilder
    {
        private readonly Order _order = new Order();

        public OrderBuilder()
        {
            _order.OrderId = Guid.NewGuid().ToString("N").Substring(0, 10).ToUpper();
            _order.OrderDate = DateTime.UtcNow;
        }

        public IOrderBuilder WithCustomerName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Customer name is required.");
            _order.CustomerName = name;
            return this;
        }

        public IOrderBuilder WithItem(string item)
        {
            _order.Items.Add(item);
            return this;
        }

        public IOrderBuilder WithTotalAmount(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be positive.");
            _order.TotalAmount = amount;
            return this;
        }

        public IOrderBuilder WithShippingAddress(string address)
        {
            _order.ShippingAddress = address;
            return this;
        }

        public IOrderBuilder WithPaymentMethod(string method)
        {
            _order.PaymentMethod = method;
            return this;
        }

        public Order Build()
        {
            // Final validation (optional)
            if (string.IsNullOrEmpty(_order.CustomerName))
                throw new InvalidOperationException("Customer name must be set.");
            if (_order.Items.Count == 0)
                throw new InvalidOperationException("At least one item is required.");

            return _order;
        }
    }
}