using System;
using System.Linq.Expressions;
using Core.Entities.OrderAggregate;

namespace Core.Specifications
{
    public class OrdersWithItemsAndOrderingSpec : BaseSpecification<Order>
    {
        /// <summary>
        /// Return ALL ORDERS for the specified User
        /// </summary>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public OrdersWithItemsAndOrderingSpec(string userEmail) : base(o => o.BuyerEmail == userEmail)
        {
            AddIncludes(o => o.OrderItems);
            AddIncludes(o => o.DeliveryMethod);
            AddOrderByDescending(o => o.OrderDate);
        }

        /// <summary>
        /// Return ONE ORDER for the specified User
        /// </summary>
        /// <param name="id"></param>
        /// <param name="userEmail"></param>
        /// <returns></returns>
        public OrdersWithItemsAndOrderingSpec(int id, string userEmail) : base(o => o.Id == id && o.BuyerEmail == userEmail)
        {
            AddIncludes(o => o.OrderItems);
            AddIncludes(o => o.DeliveryMethod);
        }
    }
}