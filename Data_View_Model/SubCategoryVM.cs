namespace Category.Data_View_Model
{
    public class SubCategoryVM
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public bool SubCategoryStatus { get; set; }
    }
}
