namespace API.Apps.AdminApi.DTOs.ProductDTOs
{
    public class ProductDTO
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SalePrice { get; set; }
    }
}
