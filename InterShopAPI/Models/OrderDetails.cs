namespace InterShopAPI.Models
{
    public class OrderDetails
    {
        public int OrderId { get; set;}
        public int ProductVariantId { get; set;}
        public int Count { get; set;}
        public int Price { get; set;}

        public virtual Order Order { get; set;}
        public virtual ProductVariant ProductVariant { get; set;}
    }
}