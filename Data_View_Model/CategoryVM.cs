namespace Category.Data_View_Model
{
    public class CategoryVM
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
        public string? CategoryImage { get; set; }
        public bool CategoryStatus { get; set; }
        public List<SubCategoryVM>? subCategoryVMs { get; set; }

        public CategoryVM(int categoryId, string categoryName, string? categoryImage, bool categoryStatus, List<SubCategoryVM>? subCategoryVMs)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryImage = categoryImage;
            CategoryStatus = categoryStatus;
            this.subCategoryVMs = subCategoryVMs;
        }

        public CategoryVM(int categoryId, string categoryName, string? categoryImage, bool categoryStatus)
        {
            CategoryId = categoryId;
            CategoryName = categoryName;
            CategoryImage = categoryImage;
            CategoryStatus = categoryStatus;
        }
    }
}
