using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFMAMVCDemo.Data.Models
{
    public class Payment
    {
        //Add entity properties here. The fields are Id, PaymentDate, MemberId (foreign key to Members), Amount
        [Key]
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Payment Date")]
        public DateTime PaymentDate { get; set; }
        [DisplayName("Member")]
        [Required]
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        
        public virtual Member Member { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Amount { get; set; }
        
    }
}
