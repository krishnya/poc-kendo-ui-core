using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.Data;
using AdminDashboard.Data.Models.Members;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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

        public async Task<IActionResult> OnPostCreateAsync([DataSourceRequest] DataSourceRequest request, Member member, List<IFormFile> files)
        {
            if (ModelState.IsValid)
            {
                if (files != null && files.Count > 0)
                {
                    foreach (var file in files)
                    {
                        var fileName = Path.GetFileName(file.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }

                        member.Documents.Add(new Document { FileName = "/documents/" + fileName });
                    }
                }
                _context.Members.Add(member);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { member }.ToDataSourceResult(request, ModelState));
        }


        public async Task<IActionResult> OnPostUpdate([DataSourceRequest] DataSourceRequest request, Member member, List<IFormFile> files)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    if (files != null && files.Count > 0)
                    {
                        foreach (var file in files)
                        {
                            var fileName = Path.GetFileName(file.FileName);
                            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\documents", fileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            member.Documents.Add(new Document { FileName = "/documents/" + fileName });
                        }
                    }

                    _context.Members.Update(member);

                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return new JsonResult(new[] { member }.ToDataSourceResult(request, ModelState));
        }



        public Task<IActionResult> OnPostDestroy([DataSourceRequest] DataSourceRequest request, Member member)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    var documentsToRemove = _context.Documents.Where(d => d.MemberId == member.Id);
                    _context.Documents.RemoveRange(documentsToRemove);

                    if (ModelState.IsValid)
                    {
                        _context.Members.Remove(member);
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }

            return Task.FromResult<IActionResult>(new JsonResult(new[] { member }.ToDataSourceResult(request, ModelState)));
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
