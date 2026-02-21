using System;
using System.Collections.Generic;

namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    public class OrderTemplate
    {
        public string TemplateName { get; set; } = string.Empty;
        public string CustomerCategory { get; set; } = string.Empty;
        public List<string> DefaultItems { get; set; } = new List<string>();
        public decimal DefaultDiscount { get; set; }

        // Clone method – Deep Copy
        public OrderTemplate Clone()
        {
            // Initial shallow copy
            var clone = (OrderTemplate)this.MemberwiseClone();

            // Deep copy for the list (reference type)
            clone.DefaultItems = new List<string>(this.DefaultItems);

            return clone;
        }

        public override string ToString()
        {
            return $"Template: {TemplateName} - Customer Category: {CustomerCategory} - Items: {string.Join(", ", DefaultItems)} - Discount: {DefaultDiscount}%";
        }
    }
}