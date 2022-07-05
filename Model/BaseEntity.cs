using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Model
{
    public class BaseEntity
    {
        public BaseEntity()=>
            InsertedDateTime=UpdatedDateTime=DateTime.Now;
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        public DateTime InsertedDateTime { get; set; }
        public DateTime UpdatedDateTime { get; set; }
        public bool? IsDeleted { get; set; }
    }
}