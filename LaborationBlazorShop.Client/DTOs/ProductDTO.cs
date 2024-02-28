namespace LaborationBlazorShop.Client.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal ExchangeRate { get; set; }
        public string SelectedCurrency { get; set; }
    }
}
