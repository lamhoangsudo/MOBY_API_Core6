using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateCartDetailVM
    {
        [DefaultValue("1")]
        public int CartId { get; set; }
        [DefaultValue("1")]
        public int ItemId { get; set; }
        [DefaultValue("1")]
        public int CartDetailItemQuantity { get; set; }
    }
}
