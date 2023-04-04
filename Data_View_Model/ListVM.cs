

using Microsoft.AspNetCore.Http;
using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class ListVM<T>
    {
        [DefaultValue(0)]
        public int Total { get; set; }
        [DefaultValue(0)]
        public int TotalPage { get; set; }
        public List<T>? List { get; set; }

        public ListVM(int total, int totalPage, IEnumerable<T> list)
        {
            this.Total = total;
            this.TotalPage = totalPage;
            this.List = new List<T>(list);
        }
    }
}
