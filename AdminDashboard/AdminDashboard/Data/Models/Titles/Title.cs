using AdminDashboard.Data.Models.Categories;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDashboard.Data.Models.Titles
{
    public class Title
    {
        [Key]
        public int Id { get; set; }
        //[UIHint("CategoryEditor")]
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        [Required]
        public string TitleName { get; set; }

        public virtual Category Category { get; set; }
    }
}
