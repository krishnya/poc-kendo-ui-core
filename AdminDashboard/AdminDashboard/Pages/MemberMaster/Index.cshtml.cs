using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AdminDashboard.Data;
using AdminDashboard.Data.Models.Members;

namespace AdminDashboard.Pages.MemberMaster
{
    public class IndexModel : PageModel
    {
        private readonly AdminDashboard.Data.ApplicationDbContext _context;

        public IndexModel(AdminDashboard.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Member> Member { get;set; }

        public async Task OnGetAsync()
        {
            Member = await _context.Members
                .Include(m => m.Title).ToListAsync();
        }
    }
}
