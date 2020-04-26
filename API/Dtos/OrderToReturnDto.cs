using System;
using System.Collections.Generic;
using Core.Entities.OrderAggregate;

namespace API.Dtos
{
    public class OrderToReturnDto
    {
        public int Id { get; set; }
        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public Address ShipToAddress { get; set; }
        public decimal ShippingPrice { get; set; }
        public string DeliveryMethod { get; set; }
        public IReadOnlyList<OrderItemDto> OrderItems { get; set; }
        public decimal Subtotal { get; set; }
        public string Status { get; set; }
        public decimal Total { get; set; } // Automapper Use the 'GetTotal()' method inside 'Order' to map this value 'Great'
    }
}