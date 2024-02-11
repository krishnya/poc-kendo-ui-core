using System.Linq;
using AdminDashboard.Data;
using AdminDashboard.Data.Models.Members;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

        public IActionResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            var members = _context.Members.ToDataSourceResult(request);
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
            var payments = _context.Payments.Where(p => p.MemberId == memberId);
            return new JsonResult(payments);
        }
    }
}
