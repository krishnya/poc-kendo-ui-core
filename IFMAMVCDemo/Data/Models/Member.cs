using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace IFMAMVCDemo.Data.Models
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }
        [Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }
        [DisplayName("Middle Name")]
        public string? MiddleName { get; set; }
        [Required]
        public string Gender { get; set; }                
        public string? Phone { get; set; }
        public string? Address { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date of Birth")]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [ForeignKey("Title")]
        [DisplayName("Title")]
        public int TitleId { get; set; }
        [DataType(DataType.Date)]
        [DisplayName("Date of Join")]
        public DateTime DateOfJoin { get; set; }
        [DisplayName("Passport No")]
        public string? PassportNo { get; set; }
        [DisplayName("Aadhar No")]
        public string? AadharNo { get; set; }
        [DisplayName("Driving License No")]
        public string? DrivingLicenseNo { get; set; }
        public virtual Title Title { get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        [NotMapped]
        public List<IFormFile> Files { get; set; }        
    }
}
