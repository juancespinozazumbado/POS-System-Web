namespace Doamin.Products;

    public class Product
    {
        
        public string Name { get; set; }
        public Category Category { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
    }