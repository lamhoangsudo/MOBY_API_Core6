namespace MOBY_API_Core6.Data_View_Model
{
    public class PaggingReturnVM<T>
    {
        public int totalRecord { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }

        public List<T>? ListModel { get; set; }
        public PaggingReturnVM(IEnumerable<T> results, PaggingVM pagging, int totalRecords)
        {
            ListModel = new List<T>(results);
            pageNumber = pagging.pageNumber;
            pageSize = pagging.pageSize;
            totalRecord = totalRecords;
        }


    }
}
