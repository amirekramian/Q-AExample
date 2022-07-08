using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Comment : BaseEntity
    {
        public string? Text { get; set; }
        public int CommentLikeCount { get; set; }
        public virtual Post? Post { get; set; }
        [ForeignKey("Post")]
        public int PostID { get; set; }
    }
}
