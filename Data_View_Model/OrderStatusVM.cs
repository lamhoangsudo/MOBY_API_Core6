using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class OrderStatusVM
    {
        [DefaultValue(0)]
        public int? OrderStatus { get; set; }
    }
}
