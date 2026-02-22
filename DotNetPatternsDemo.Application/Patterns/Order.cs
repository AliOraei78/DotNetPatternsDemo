using System;
using System.Collections.Generic;

namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Product – complex final object
    public class Order
    {
        public string OrderId { get; internal set; }
        public string CustomerName { get; internal set; } = string.Empty;
        public DateTime OrderDate { get; internal set; }
        public List<string> Items { get; internal set; } = new List<string>();
        public decimal TotalAmount { get; internal set; }
        public string ShippingAddress { get; internal set; } = string.Empty;
        public string PaymentMethod { get; internal set; } = string.Empty;

        // For display (in a real project, you might override ToString)
        public override string ToString()
        {
            return $"Order {OrderId} - Customer: {CustomerName} - Amount: {TotalAmount} - Address: {ShippingAddress}";
        }
    }
}