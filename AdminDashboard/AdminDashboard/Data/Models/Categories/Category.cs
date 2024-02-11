using System.ComponentModel.DataAnnotations;
using System;

namespace AdminDashboard.Data.Models.Categories
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName{ get; set; }        
        public string Code { get; set; }
        [Required]
        public double Amount { get; set; }        
    }
}
