using IFMAMVCDemo.Data.Models;
using System.ComponentModel;

namespace IFMAMVCDemo.Data.Models
{
    public class MemberViewModel : Member
    {
        [DisplayName("Title")]
        public string TitleName { get; set; }
        [DisplayName("Category Amount")]
        public double CategoryAmount { get; set; }
        public double Paid { get; set; }
        public double Balance { get; set; }
    }


}
