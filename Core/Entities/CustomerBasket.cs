using System.Collections.Generic;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket() { } //Used by Redis
        public CustomerBasket(string id)
        {
            Id = id;
        }
        public string Id { get; set; } // Will be initialized in the Client side


        public List<BasketItem> Items { get; set; } = new List<BasketItem>();

    }
}