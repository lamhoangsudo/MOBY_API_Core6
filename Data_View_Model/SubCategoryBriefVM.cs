using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class SubCategoryBriefVM
    {
        public int SubCategoryId { get; set; }
        public string SubCategoryName { get; set; } = null!;

        public static SubCategoryBriefVM SubCategorToViewModel(SubCategory subcate)
        {

            return new SubCategoryBriefVM
            {
                SubCategoryId = subcate.SubCategoryId,
                SubCategoryName = subcate.SubCategoryName,
            };
        }
    }
}
