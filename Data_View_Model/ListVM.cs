

using Microsoft.AspNetCore.Http;

namespace MOBY_API_Core6.Data_View_Model
{
    public class ListVM<T>
    {
        public int total { get; set; }
        public int totalPage { get; set; }
        public List<T>? list { get; set; }

        public ListVM(int total, int totalPage, IEnumerable<T> list)
        {
            this.total = total;
            this.totalPage = totalPage;
            this.list = new List<T>(list);
        }
    }
}
