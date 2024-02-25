using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IFMAMVCDemo.Data;
using IFMAMVCDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace IFMAMVCDemo.Controllers
{
    [Authorize]
    public class MembersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MembersController> _logger;

        public MembersController(ApplicationDbContext context, ILogger<MembersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: Members
        public async Task<IActionResult> Index()
        {
            try
            {
                var membersWithTitlesAndCategories = _context.Members
                .Include(m => m.Title)
                    .ThenInclude(t => t.Category)
                .Include(m => m.Payments);

                var membersWithBalances = membersWithTitlesAndCategories
                    .Select(m => new MemberViewModel
                    {
                        // Copy properties from member to viewModel
                        Id = m.Id,
                        FirstName = m.FirstName,
                        LastName = m.LastName,
                        MiddleName = m.MiddleName,
                        Gender = m.Gender,
                        Phone = m.Phone,
                        Address = m.Address,
                        DateOfBirth = m.DateOfBirth,
                        TitleId = m.TitleId,
                        DateOfJoin = m.DateOfJoin,
                        PassportNo = m.PassportNo,
                        AadharNo = m.AadharNo,
                        DrivingLicenseNo = m.DrivingLicenseNo,
                        TitleName = m.Title.TitleName,
                        Documents = m.Documents,
                        // Calculate category amount and balance
                        CategoryAmount = m.Title.Category.Amount,
                        Paid = m.Payments.Sum(p => p.Amount),
                        Balance = Math.Round(m.Title.Category.Amount - m.Payments.Sum(p => p.Amount), 2)
                    });

                return View(await membersWithBalances.ToListAsync());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting members");
                throw;
            }

        }




        // GET: Members/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var member = await _context.Members.Include(m => m.Documents).FirstOrDefaultAsync(m => m.Id == id);
                if (member == null)
                {
                    return NotFound();
                }

                var title = await _context.Titles.FindAsync(member.TitleId);
                if (title == null)
                {
                    return NotFound();
                }

                var viewModel = new MemberViewModel
                {
                    // Copy properties from member to viewModel
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    MiddleName = member.MiddleName,
                    Gender = member.Gender,
                    Phone = member.Phone,
                    Address = member.Address,
                    DateOfBirth = member.DateOfBirth,
                    TitleId = member.TitleId,
                    DateOfJoin = member.DateOfJoin,
                    PassportNo = member.PassportNo,
                    AadharNo = member.AadharNo,
                    DrivingLicenseNo = member.DrivingLicenseNo,
                    Documents = member.Documents,
                    // Set TitleName from title
                    TitleName = title.TitleName,
                    Payments = member.Payments != null ? member.Payments.OrderByDescending(p => p.PaymentDate).ToList() : new List<Payment>()

                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting members");
                throw;
            }
        }



        // GET: Members/Create
        public IActionResult Create()
        {
            try
            {
                ViewData["TitleId"] = new SelectList(_context.Titles, "Id", "TitleName");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating member");
                throw;
            }
        }

        // POST: Members/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Gender,DateOfBirth,TitleId,DateOfJoin")] Member member, List<IFormFile> files)
        {
            ModelState.Remove("Title");
            ModelState.Remove("Payments");
            ModelState.Remove("Documents");
            ModelState.Remove("Files");
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Add(member);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Member created with id {Id}", member.Id);

                        // Handle file uploads
                        if (files != null && files.Count > 0)
                        {
                            foreach (var file in files)
                            {
                                var fileName = $"{member.Id}_{Path.GetFileName(file.FileName)}";
                                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\files", fileName);
                                using (var fileStream = new FileStream(filePath, FileMode.Create))
                                {
                                    await file.CopyToAsync(fileStream);
                                }

                                var document = new Document
                                {
                                    FileName = fileName,
                                    MemberId = member.Id
                                };
                                _context.Documents.Add(document);
                            }
                            await _context.SaveChangesAsync();
                            _logger.LogInformation("Documents added for member with id {Id}", member.Id);
                        }

                        transaction.Commit();
                        _logger.LogInformation("Transaction committed");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error creating member with id {Id}", member.Id);
                        transaction.Rollback();
                        throw;
                    }
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["TitleId"] = new SelectList(_context.Titles, "Id", "TitleName", member.TitleId);
            return View(member);
        }

        // GET: Members/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var member = await _context.Members
                                .Include(m => m.Documents)
                                .FirstOrDefaultAsync(m => m.Id == id);
                if (member == null)
                {
                    return NotFound();
                }
                // Initialize Documents to an empty list if it's null
                member.Documents = member.Documents ?? new List<Document>();

                ViewData["TitleId"] = new SelectList(_context.Titles, "Id", "TitleName", member.TitleId);
                return View(member);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error editing member with id {Id}", id);
                throw;
            }
        }

        // POST: Members/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Gender,DateOfBirth,TitleId,DateOfJoin")] Member member, List<IFormFile> files)
        {
            if (id != member.Id)
            {
                return NotFound();
            }

            ModelState.Remove("Title");
            ModelState.Remove("Payments");
            ModelState.Remove("Documents");
            ModelState.Remove("Files");
            if (ModelState.IsValid)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        _context.Update(member);
                        await _context.SaveChangesAsync();
                        _logger.LogInformation("Member with id {Id} updated", member.Id);

                        // Handle file uploads
                        if (files != null && files.Count > 0)
                        {
                            foreach (var file in files)
                            {
                                try
                                {
                                    var fileName = $"{member.Id}_{Path.GetFileName(file.FileName)}";
                                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", fileName);
                                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                                    {
                                        await file.CopyToAsync(fileStream);
                                    }

                                    var document = new Document
                                    {
                                        FileName = fileName,
                                        MemberId = member.Id
                                    };
                                    _context.Documents.Add(document);
                                    await _context.SaveChangesAsync(); // Save changes after each document creation
                                    _logger.LogInformation("Document added for member with id {Id}", member.Id);
                                }
                                catch (Exception ex)
                                {
                                    _logger.LogError(ex, "Error uploading file for member with id {Id}", member.Id);
                                    throw;
                                }
                            }
                        }

                        transaction.Commit();
                        _logger.LogInformation("Transaction committed");
                        return RedirectToAction(nameof(Index));
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error updating member with id {Id}", member.Id);
                        transaction.Rollback();
                        throw;
                    }
                }
            }
            ViewData["TitleId"] = new SelectList(_context.Titles, "Id", "TitleName", member.TitleId);
            return View(member);
        }



        // GET: Members/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var member = await _context.Members
                                    .Include(m => m.Documents)
                                    .FirstOrDefaultAsync(m => m.Id == id);
                if (member == null)
                {
                    return NotFound();
                }
                // Initialize Documents to an empty list if it's null
                member.Documents = member.Documents ?? new List<Document>();

                var title = await _context.Titles.FindAsync(member.TitleId);
                if (title == null)
                {
                    return NotFound();
                }

                var viewModel = new MemberViewModel
                {
                    // Copy properties from member to viewModel
                    Id = member.Id,
                    FirstName = member.FirstName,
                    LastName = member.LastName,
                    MiddleName = member.MiddleName,
                    Gender = member.Gender,
                    Phone = member.Phone,
                    Address = member.Address,
                    DateOfBirth = member.DateOfBirth,
                    TitleId = member.TitleId,
                    DateOfJoin = member.DateOfJoin,
                    PassportNo = member.PassportNo,
                    AadharNo = member.AadharNo,
                    DrivingLicenseNo = member.DrivingLicenseNo,
                    Documents = member.Documents,
                    // Set TitleName from title
                    TitleName = title.TitleName
                };

                return View(viewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting member with id {Id}", id);
                throw;
            }
        }


        // POST: Members/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var member = await _context.Members.FindAsync(id);
                if (member != null)
                {
                    _context.Members.Remove(member);
                }

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting member with id {Id}", id);
                throw;
            }
        }

        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.Id == id);
        }
    }
}
