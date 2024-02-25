using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using AdminDashboard.Data.Models.Categories;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using System.Linq;
using AdminDashboard.Data;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;

namespace AdminDashboard.Pages
{
    [Authorize]
    public class CategoryModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        
        public CategoryModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; }
        public void OnGet()
        {
            Categories = _context.Categories?.ToList();
        }
        public JsonResult OnPostRead([DataSourceRequest] DataSourceRequest request)
        {
            var data = _context.Categories.ToDataSourceResult(request);
            return new JsonResult(data);
        }

        public JsonResult OnPostCreate([DataSourceRequest] DataSourceRequest request, Category category)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                _context.Categories.Add(category);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdate([DataSourceRequest] DataSourceRequest request, Category category)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                _context.Categories.Update(category);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroy([DataSourceRequest] DataSourceRequest request, Category category)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                _context.Categories.Remove(category);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { category }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult OnPostSave(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }

    }
}
