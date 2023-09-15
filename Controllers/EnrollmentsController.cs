using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SchoolManagementApp.MVC.Data;

namespace SchoolManagementApp.MVC.Controllers
{
    public class EnrollmentsController : Controller
    {
        private readonly SchoolManagementDbContext _context;

        public EnrollmentsController(SchoolManagementDbContext context)
        {
            _context = context;
        }

        // GET: Enrollments
        public async Task<IActionResult> Index()
        {
            var schoolManagementDbContext = _context.Enrolllments.Include(e => e.Class).Include(e => e.Student);
            return View(await schoolManagementDbContext.ToListAsync());
        }

        // GET: Enrollments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Enrolllments == null)
            {
                return NotFound();
            }

            var enrolllment = await _context.Enrolllments
                .Include(e => e.Class)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrolllment == null)
            {
                return NotFound();
            }

            return View(enrolllment);
        }

        // GET: Enrollments/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id");
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudentId,ClassId,Grade")] Enrolllment enrolllment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(enrolllment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", enrolllment.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", enrolllment.StudentId);
            return View(enrolllment);
        }

        // GET: Enrollments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Enrolllments == null)
            {
                return NotFound();
            }

            var enrolllment = await _context.Enrolllments.FindAsync(id);
            if (enrolllment == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", enrolllment.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", enrolllment.StudentId);
            return View(enrolllment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudentId,ClassId,Grade")] Enrolllment enrolllment)
        {
            if (id != enrolllment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(enrolllment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EnrolllmentExists(enrolllment.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Classes, "Id", "Id", enrolllment.ClassId);
            ViewData["StudentId"] = new SelectList(_context.Students, "Id", "Id", enrolllment.StudentId);
            return View(enrolllment);
        }

        // GET: Enrollments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Enrolllments == null)
            {
                return NotFound();
            }

            var enrolllment = await _context.Enrolllments
                .Include(e => e.Class)
                .Include(e => e.Student)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (enrolllment == null)
            {
                return NotFound();
            }

            return View(enrolllment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Enrolllments == null)
            {
                return Problem("Entity set 'SchoolManagementDbContext.Enrolllments'  is null.");
            }
            var enrolllment = await _context.Enrolllments.FindAsync(id);
            if (enrolllment != null)
            {
                _context.Enrolllments.Remove(enrolllment);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EnrolllmentExists(int id)
        {
          return (_context.Enrolllments?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
