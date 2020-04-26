namespace Core.Entities.OrderAggregate
{
    /// <summary>
    /// Used as a snapshot of the purchased Item
    /// so we keep track of the original name/picture etc.
    /// </summary>
    public class ProductItemOrdered
    {
        public ProductItemOrdered() // Needed by EFCOre migrations
        {

        }
        public ProductItemOrdered(int productItemId, string productName, string pictureUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            PictureUrl = pictureUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
    }
}