using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdminDashboard.Data;
using AdminDashboard.Data.Models.Categories;
using AdminDashboard.Data.Models.Payments;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminDashboard.Pages
{
    [Authorize]
    public class PaymentsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public PaymentsModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
        }

        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            var data = _context.Payments.ToDataSourceResult(request);
            return new JsonResult(data);
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { payment}.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Update(payment);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { payment }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Payments.Remove(payment);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { payment}.ToDataSourceResult(request, ModelState));
        }



    }
}
