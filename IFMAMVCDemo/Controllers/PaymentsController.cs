using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using IFMAMVCDemo.Data;
using IFMAMVCDemo.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace IFMAMVCDemo.Controllers
{
    [Authorize]
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payments
        public async Task<IActionResult> Index()
        {
            //var applicationDbContext = _context.Payments.Include(p => p.Member);
            //return View(await applicationDbContext.ToListAsync());
            var applicationDbContext = _context.Payments.Include(p => p.Member)
                                                .ThenInclude(m => m.Title)
                                                .ThenInclude(t => t.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                            .Include(p => p.Member)
                            .ThenInclude(m => m.Title)
                            .ThenInclude(t => t.Category)
                            .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // GET: Payments/Create
        public IActionResult Create(int? memberId)
        {
            var emptyItem = new { Id = 0, FullName = "Select Member..." };
            var memberList = _context.Members.ToList().Concat(new List<object>() { emptyItem });

            ViewData["MemberId"] = new SelectList(memberList, "Id", "FullName", memberId);
            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PaymentDate,MemberId,Amount,Description")] Payment payment)
        {            
            var member = _context.Members.Include(m => m.Title.Category).FirstOrDefault(m => m.Id == payment.MemberId);
                        
            if(payment.MemberId <= 0)
            {
                ModelState.AddModelError("MemberId", "Please select a member.");
            }

            if(payment.PaymentDate > DateTime.Today)
            {
                ModelState.AddModelError("PaymentDate", "Payment date cannot be in the future.");
            }
            
            if(payment.Amount <= 0)
            {
                ModelState.AddModelError("Amount", "Payment amount must be greater than 0.");                                
            }
            else 
            {
                var totalPayments = _context.Payments.Where(p => p.MemberId == payment.MemberId).Sum(p => p.Amount);

                if (member?.Title?.Category != null && payment.Amount > (member.Title.Category.Amount - totalPayments))
                {
                    var balance = member.Title.Category.Amount - totalPayments;
                    //var message = $"Total payments so far: {totalPayments}, Balance: {balance}, Attempted Payment: {payment.Amount}, Category Amount: {member.Title.Category.Amount}. Total payments cannot exceed Category Amount.";
                    var message = $"Payment amount can't be more than outstanding balance ({balance}).";                   
                    ModelState.AddModelError("Amount", message);
                }               
            } 
            
            if(payment.MemberId >0)
                ModelState.Remove("Member");

            if (!ModelState.IsValid)
            {
                var emptyItem = new { Id = 0, FullName = "Select Member..." };
                var memberList = _context.Members.ToList().Concat(new List<object>() { emptyItem });
                ViewData["MemberId"] = new SelectList(memberList, "Id", "FullName", payment.MemberId);
                return View(payment);
            }

            ModelState.Remove("Member");
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                //return RedirectToAction("Index", "Members", new { id = payment.MemberId });
            }
            ViewData["MemberId"] = new SelectList(_context.Members, "Id", "FullName", payment.MemberId);
            //return View(payment);
            return RedirectToAction("Details", "Members", new { id = payment.MemberId });
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
            {
                return NotFound();
            }
            var emptyItem = new { Id = 0, FullName = "Select Member..." };
            var memberList = _context.Members.ToList().Concat(new List<object>() { emptyItem });
            ViewData["MemberId"] = new SelectList(memberList, "Id", "FullName", payment.MemberId);
            return View(payment);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PaymentDate,MemberId,Amount,Balance,Description")] Payment payment)
        {
            if (id != payment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentExists(payment.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            var emptyItem = new { Id = 0, FullName = "Select Member..." };
            var memberList = _context.Members.ToList().Concat(new List<object>() { emptyItem });
            ViewData["MemberId"] = new SelectList(memberList, "Id", "FullName", payment.MemberId);
            return View(payment);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var payment = await _context.Payments
                .Include(p => p.Member)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payment == null)
            {
                return NotFound();
            }

            return View(payment);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment != null)
            {
                _context.Payments.Remove(payment);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
