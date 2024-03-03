using IFMAMVCDemo.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IFMAMVCDemo.Data.Models
{
    public class MemberViewModel : Member
    {
        [DisplayName("Membership ID")]
        public string MembershipId => $"IFMA0{Id}";
        [DisplayName("Title")]
        public string? TitleName { get; set; }
        [DisplayName("Category Amount")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double CategoryAmount { get; set; }
        [DisplayName("Category Name")]
        public string? CategoryName { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Paid { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public double Balance { get; set; }
        public IList<Payment> Payments { get; set; }
    }


}
