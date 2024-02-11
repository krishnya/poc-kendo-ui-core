using AdminDashboard.Data.Models.Payments;
using AdminDashboard.Data.Models.Titles;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDashboard.Data.Models.Members
{
    public class Member
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }        
        public DateTime DateOfBirth { get; set; }        
        [ForeignKey("Title")]
        public int TitleId { get; set; }        
        public DateTime DateOfJoin { get; set; }
        public string PassportNo { get; set; }
        public string AadharNo { get; set; }
        public string DrivingLicenseNo { get; set; }        
        public virtual Title Title{ get; set; }
        public virtual ICollection<Payment> Payments { get; set; }
    }
}
