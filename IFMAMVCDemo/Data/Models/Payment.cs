using IFMAMVCDemo.Models;
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
        [Required(ErrorMessage = "Please select a member")]
        [ValidMemberId]
        [ForeignKey("Member")]
        public int MemberId { get; set; }
        
        public virtual Member Member { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Amount { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        [NotMapped]
        public double? Balance
        {
            get
            {
                if (Member?.Title?.Category != null)
                {
                    return Member.Title.Category.Amount - Amount;
                }
                return null;
            }
        }

        public string? Description { get; set; }
        
    }
}
