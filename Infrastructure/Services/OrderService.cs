using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;

namespace Infrastructure.Services
{
    public class OrderService : IOrderService
    {
        private readonly IBasketRepository _basketRepo;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork, IBasketRepository basketRepo)
        {
            _unitOfWork = unitOfWork;
            _basketRepo = basketRepo;
        }

        public async Task<Order> CreateOrderAsync(string buyerEmail, int deliveryMethodId, string basketId, Address shippingAddress)
        {
            // Get the basket from repo
            var basket = await _basketRepo.GetBasketAsync(basketId);

            // Get items from prod Repo
            var productItems = await _unitOfWork.Repository<Product>().ListBySpecAsync(new ProductsByIdsSpec(basket.Items.Select(i => i.Id)));

            var items = new List<OrderItem>();
            foreach (var productItem in productItems)
            {
                var itemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PictureUrl); // Snapshot the ordered Item

                var orderItem = new OrderItem(itemOrdered, productItem.Price, basket.Items.First(i => i.Id == productItem.Id).Quantity);
                items.Add(orderItem);
            }

            // Get delivery method from Repo
            var deliveryMethod = await _unitOfWork.Repository<DeliveryMethod>().GetByIdAsync(deliveryMethodId);

            // Calc Subtotals
            var subtotals = items.Sum(item => item.Price * item.Quantity);

            // Create Order 
            var order = new Order(items, buyerEmail, shippingAddress, deliveryMethod, subtotals);
            _unitOfWork.Repository<Order>().Add(order);

            // TODO: Save to DB

            var result = await _unitOfWork.Complete();
            if (result <= 0)
            {
                return null;
            }

            // Delete basket
            await _basketRepo.DeleteBasketAsync(basketId);

            return order;


        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryMethodsAsync()
        {
            return await _unitOfWork.Repository<DeliveryMethod>().ListAllAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int orderId, string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpec(orderId, buyerEmail);
            return await _unitOfWork.Repository<Order>().GetBySpecAsync(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string buyerEmail)
        {
            var spec = new OrdersWithItemsAndOrderingSpec(buyerEmail);
            return await _unitOfWork.Repository<Order>().ListBySpecAsync(spec);
        }
    }
}