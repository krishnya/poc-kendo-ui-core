using IFMAMVCDemo.Data;
using IFMAMVCDemo.Data.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IFMAMVCDemo.Models
{
    //public class ValidMemberIdAttribute : ValidationAttribute
    //{
    //    public override bool IsValid(object value)
    //    {
    //        if (value is int memberId && memberId <= 0)
    //        {
    //            ErrorMessage = "Please select a member.";
    //            return false;
    //        }
    //        return true;
    //    }
    //}

    //public class MaxAmountAttribute : ValidationAttribute
    //{
    //    private readonly ApplicationDbContext _context;

    //    public MaxAmountAttribute(ApplicationDbContext context)
    //    {
    //        _context = context;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var payment = (Payment)validationContext.ObjectInstance;
    //        var amount = (double)value;

    //        var member = _context.Members.Include(m => m.Title.Category).FirstOrDefault(m => m.Id == payment.MemberId);

    //        if (member?.Title?.Category != null && amount > member.Title.Category.Amount)
    //        {
    //            return new ValidationResult("Amount cannot exceed Category Amount");
    //        }

    //        return ValidationResult.Success;
    //    }
    //}


}
