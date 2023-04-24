using MOBY_API_Core6.Models;

namespace MOBY_API_Core6.Data_View_Model
{
    public class TransationLogVM
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public double Value { get; set; }
        public bool TransactionStatus { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateUpdate { get; set; }

        public UserVM? UserVM { get; set; }

        public static TransationLogVM TransactionToVewModel(TransationLog log)
        {

            var transactionLog = new TransationLogVM
            {
                TransactionId = log.TransactionId,
                Value = log.Value,
                TransactionStatus = log.TransactionStatus,
                DateCreate = log.DateCreate,
                DateUpdate = log.DateUpdate

            };
            var user = log.User;
            transactionLog.UserVM = UserVM.UserAccountToVewModel(user);

            return transactionLog;
        }
    }
}
