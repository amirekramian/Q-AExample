using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public  class Post : BaseEntity
    {
        public string? Title { get; set; }
        public string? desciption { get; set; }
        public bool IsArchive { get; set; }
        public bool IsDeleted { get; set; }
        public int LikeCount { get; set; }
        //public virtual User? User { get; set; }
        public User? User { get; set; }
        [ForeignKey("User")]
        public int UserID { get; set; }
        public virtual Comment? Comment { get; set; }


    }
}
