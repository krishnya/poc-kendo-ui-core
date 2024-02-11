using System;
using System.Collections.Generic;
using AdminDashboard.Data.Models.Payments;

namespace AdminDashboard.Data.Models.Members
{
    public class MemberViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string TitleName { get; set; }
        public DateTime DateOfJoin { get; set; }
        public List<Payment> Payments{ get; set; }
    }
}
