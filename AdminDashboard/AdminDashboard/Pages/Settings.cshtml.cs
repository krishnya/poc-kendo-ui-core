using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminDashboard.Data;
using AdminDashboard.Data.Models;
using AdminDashboard.Data.Models.Categories;
using AdminDashboard.Data.Models.Titles;
using Azure.Core;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace AdminDashboard.Pages
{
    [Authorize]
    public class SettingsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public SettingsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Category> Categories { get; set; }
        public List<Title> Titles{ get; set; }
        public void OnGet()
        {
            Categories = _context.Categories.ToList();
            Titles = _context.Titles.Include(t => t.Category).ToList();
        }
        public JsonResult OnGetGetCategories([DataSourceRequest] DataSourceRequest request)
        {
            var categories = _context.Categories.Select(c => new
            {
                c.Id,
                c.CategoryName
            });

            return new JsonResult(categories);
        }
        public JsonResult OnGetGetTitles([DataSourceRequest] DataSourceRequest request)
        {
            var categories = _context.Titles.Select(t => new
            {
                t.Id,
                t.TitleName
            });

            return new JsonResult(categories);
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

   

        public JsonResult OnPostReadTitle([DataSourceRequest] DataSourceRequest request)
        {
            var titles = _context.Titles.Include(t => t.Category).ToDataSourceResult(request);
            return new JsonResult(titles);
        }

        public JsonResult OnGetReadTitle([DataSourceRequest] DataSourceRequest request)
        {
            var titles = _context.Titles.Include(t => t.Category).ToDataSourceResult(request);
            return new JsonResult(titles);
        }

        public JsonResult OnPostCreateTitle([DataSourceRequest] DataSourceRequest request, Title title)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                _context.Titles.Add(title);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { title }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostUpdateTitle([DataSourceRequest] DataSourceRequest request, Title title)
        {
            ModelState.Clear();
            if (ModelState.IsValid)
            {
                _context.Titles.Update(title);
                _context.SaveChanges();
            }

            return new JsonResult(new[] { title }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult OnPostDestroyTitle([DataSourceRequest] DataSourceRequest request, Title title)
        {
            _context.Titles.Remove(title);
            _context.SaveChanges();

            return new JsonResult(new[] { title }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult OnPostSave(string contentType, string base64, string fileName)
        {
            var fileContents = Convert.FromBase64String(base64);

            return File(fileContents, contentType, fileName);
        }
    }
}
