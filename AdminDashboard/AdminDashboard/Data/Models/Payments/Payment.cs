using AdminDashboard.Data.Models.Members;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdminDashboard.Data.Models.Payments
{
    public class Payment
    {
        //Add entity properties here. The fields are Id, PaymentDate, MemberId (foreign key to Members), Amount
        [Key]
        public int Id { get; set; }
        public DateTime PaymentDate { get; set; }
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        public virtual Member Member { get; set; }
        [Required]
        public int Amount { get; set; }
        public int Balance { get; set; }              
    }
}
