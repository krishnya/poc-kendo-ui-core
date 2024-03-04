using System.ComponentModel.DataAnnotations;

namespace IFMAMVCDemo.Models
{
    public class ValidMemberIdAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is int memberId && memberId <= 0)
            {
                ErrorMessage = "Please select a member.";
                return false;
            }
            return true;
        }
    }
}
