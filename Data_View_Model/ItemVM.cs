namespace MOBY_API_Core6.Data_View_Model
{
    public class ItemVM
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string ItemTitle { get; set; } = null!;
        public double? ItemSalePrice { get; set; }
        public int ItemShareAmount { get; set; }
        public string Image { get; set; } = null!;
        public bool? ItemStatus { get; set; }
        public SubCategoryBriefVM? SubCategoryBriefVM { get; set; }
        public ItemOwnerVM? ItemOwnerVM { get; set; }


        public static ItemVM ItemToViewModel(Models.Item item)
        {

            var ItemVM = new ItemVM()
            {
                ItemId = item.ItemId,
                UserId = item.UserId,
                ItemTitle = item.ItemTitle,

                ItemSalePrice = item.ItemSalePrice,
                ItemShareAmount = item.ItemShareAmount,
                Image = item.Image,
                ItemStatus = item.ItemStatus,
            };
            if (item.User != null)
            {
                var user = item.User;
                ItemVM.ItemOwnerVM = ItemOwnerVM.ItemOwnerToVewModel(user);
            }
            if (item.SubCategory != null)
            {
                var subcate = item.SubCategory;
                ItemVM.SubCategoryBriefVM = SubCategoryBriefVM.SubCategorToViewModel(subcate);
            }

            return ItemVM;
        }

    }
}
