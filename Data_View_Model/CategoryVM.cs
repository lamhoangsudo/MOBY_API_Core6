namespace Category.Data_View_Model
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryImage { get; set; }
        public bool CategoryStatus { get; set; }
    }
}
