namespace MOBY_API_Core6.Data_View_Model
{
    public class ItemVM
    {
        public int ItemId { get; set; }
        public int UserId { get; set; }
        public string ItemTitle { get; set; } = null!;
        public bool? ItemSponsoredOrderShippingFee { get; set; }
        public double? ItemSalePrice { get; set; }
        public int ItemShareAmount { get; set; }
        public string Image { get; set; } = null!;
        public bool ItemStatus { get; set; }



        public static ItemVM ItemToViewModel(Models.Item item)
        {

            var ItemVM = new ItemVM()
            {
                ItemId = item.ItemId,
                UserId = item.UserId,
                ItemTitle = item.ItemTitle,
                ItemSponsoredOrderShippingFee = item.ItemSponsoredOrderShippingFee,
                ItemSalePrice = item.ItemSalePrice,
                ItemShareAmount = item.ItemShareAmount,
                Image = item.Image,
                ItemStatus = item.ItemStatus,
            };

            return ItemVM;
        }
    }
}
