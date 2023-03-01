using System.ComponentModel;

namespace MOBY_API_Core6.Data_View_Model
{
    public class CreateCommentVM
    {
        [DefaultValue(null)]
        public int? ItemId { get; set; }
        [DefaultValue(null)]
        public int? BlogId { get; set; }
        public string CommentContent { get; set; } = null!;

    }
}
