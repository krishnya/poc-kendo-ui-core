using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFMAMVCDemo.Data.Models
{
    public class Title
    {
        [Key]
        public int Id { get; set; }        
        [ForeignKey("Category")]
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        [Required]
        [DisplayName("Title Name")]
        public string TitleName { get; set; }

        public virtual Category Category { get; set; }
    }
}
