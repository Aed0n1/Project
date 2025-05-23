namespace Project.Models
{
    public class ListProduct
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string Type { get; set; }
        public int Quantity { get; set; }
        public double Calories { get; set; }
        public double TotalCalories => Calories * Quantity;
    }
} 