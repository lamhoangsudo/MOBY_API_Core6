namespace Category.Data_View_Model
{
    public class SubCategoryVM
    {
        public int SubCategoryId { get; set; }
        public int CategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;
        public bool SubCategoryStatus { get; set; }

        public SubCategoryVM(int subCategoryId, int categoryId, string subCategoryName, bool subCategoryStatus)
        {
            SubCategoryId = subCategoryId;
            CategoryId = categoryId;
            SubCategoryName = subCategoryName;
            SubCategoryStatus = subCategoryStatus;
        }

        public SubCategoryVM(int subCategoryId, string subCategoryName, bool subCategoryStatus)
        {
            SubCategoryId = subCategoryId;
            SubCategoryName = subCategoryName;
            SubCategoryStatus = subCategoryStatus;
        }


    }
}
