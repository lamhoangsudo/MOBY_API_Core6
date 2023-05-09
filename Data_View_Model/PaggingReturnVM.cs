namespace MOBY_API_Core6.Data_View_Model
{
    public class PaggingReturnVM<T>
    {
        public int TotalRecord { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }

        public List<T>? ListModel { get; set; }
        public PaggingReturnVM(IEnumerable<T> results, PaggingVM pagging, int totalRecords)
        {
            ListModel = new List<T>(results);
            PageNumber = pagging.PageNumber;
            PageSize = pagging.PageSize;
            TotalRecord = totalRecords;
        }


    }
}
