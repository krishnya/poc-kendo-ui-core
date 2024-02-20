using System.Linq;
using AdminDashboard.Data;
using AdminDashboard.Data.Models.Members;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdminDashboard.Pages
{
    [Authorize]
    public class MembersModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MembersModel> _logger;

        public MembersModel(ApplicationDbContext context, ILogger<MembersModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult OnGetRead([DataSourceRequest] DataSourceRequest request)
        {
            var members = _context.Members.ToDataSourceResult(request);
            return new JsonResult(members);
        }

        public JsonResult OnGetGetMembers()
        {
            var members = _context.Members.Select(m => new {
                Id = m.Id,
                FirstName = m.FirstName,
                LastName = m.LastName,
                Gender = m.Gender,
                FullName = m.FirstName + " " + m.LastName
            }).ToList();
            members.Insert(0, new { Id = 0, FirstName = "Select Member..", LastName = "", Gender="", FullName = "Select Member.." });
            return new JsonResult(members);
        }

        public IActionResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            var members = _context.Members
         .Include(m => m.Payments) // Load the Payments related to each Member
         .Include(m => m.Title) // Load the Title related to each Member
             .ThenInclude(t => t.Category) // Load the Category related to each Title
         .Select(m => new {
             Id = m.Id,
             FirstName = m.FirstName,
             LastName = m.LastName,
             Gender = m.Gender,
             FullName = m.FirstName + " " + m.LastName,
             TitleId = m.TitleId,
             DateOfJoin = m.DateOfJoin,
             TotalPaid = m.TotalPaid,
             CategoryAmount = m.CategoryAmount
         })
         .ToDataSourceResult(request);

            return new JsonResult(members);
        }

        public IActionResult OnPostCreate([DataSourceRequest] DataSourceRequest request, Member member)
        {            
            if (ModelState.IsValid)
            {
                _context.Members.Add(member);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { member }.ToDataSourceResult(request, ModelState));
        }

        public IActionResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Members.Update(member);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { member }.ToDataSourceResult(request, ModelState));
        }

        public IActionResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, Member member)
        {
            if (ModelState.IsValid)
            {
                _context.Members.Remove(member);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { member }.ToDataSourceResult(request, ModelState));
        }

        public IActionResult OnGetGetPayments(int memberId)
        {
            var payments = _context.Payments
                .Where(p => p.MemberId == memberId)
                .ToList();

            return new JsonResult(payments);
        }

        public IActionResult OnPostGetPayments(int memberId)
        {
            var payments = _context.Payments
                .Where(p => p.MemberId == memberId)
                .ToList();

            return new JsonResult(payments);
        }

    }
}
