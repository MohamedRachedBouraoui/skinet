namespace API.Dtos
{
    public class OrderDto
    {
        public string BasketId { get; set; }
        public AddressDto ShipToAddress { get; set; }
        public int DeliveryMethodId { get; set; }
    }
}