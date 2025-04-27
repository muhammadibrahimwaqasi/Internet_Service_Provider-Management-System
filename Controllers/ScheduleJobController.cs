using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ISPMs.Models;

namespace ISPMs.Controllers
{
    public class ScheduleJobController : Controller
    {
        private readonly ManagementSystemContext _context;

        public ScheduleJobController(ManagementSystemContext context)
        {
            _context = context;
        }

        // GET: ScheduleJob
        public async Task<IActionResult> Index()
        {
            var managementSystemContext = _context.ScheduleJobs.Include(s => s.Customer).Include(s => s.Package);
            return View(await managementSystemContext.ToListAsync());
        }

        // GET: ScheduleJob/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJob = await _context.ScheduleJobs
                .Include(s => s.Customer)
                .Include(s => s.Package)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (scheduleJob == null)
            {
                return NotFound();
            }

            return View(scheduleJob);
        }

        // GET: ScheduleJob/Create
        public IActionResult Create()
        {
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId");
            ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PackageId");
            return View();
        }

        // POST: ScheduleJob/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,TaskType,PackageId,CustomerId,ScheduleOn,Status")] ScheduleJob scheduleJob)
        {
            if (ModelState.IsValid)
            {
                _context.Add(scheduleJob);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", scheduleJob.CustomerId);
            ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PackageId", scheduleJob.PackageId);
            return View(scheduleJob);
        }

        // GET: ScheduleJob/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJob = await _context.ScheduleJobs.FindAsync(id);
            if (scheduleJob == null)
            {
                return NotFound();
            }
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", scheduleJob.CustomerId);
            ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PackageId", scheduleJob.PackageId);
            return View(scheduleJob);
        }

        // POST: ScheduleJob/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ScheduleId,TaskType,PackageId,CustomerId,ScheduleOn,Status")] ScheduleJob scheduleJob)
        {
            if (id != scheduleJob.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(scheduleJob);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleJobExists(scheduleJob.ScheduleId))
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
            ViewData["CustomerId"] = new SelectList(_context.Customers, "CustomerId", "CustomerId", scheduleJob.CustomerId);
            ViewData["PackageId"] = new SelectList(_context.Packages, "PackageId", "PackageId", scheduleJob.PackageId);
            return View(scheduleJob);
        }

        // GET: ScheduleJob/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scheduleJob = await _context.ScheduleJobs
                .Include(s => s.Customer)
                .Include(s => s.Package)
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (scheduleJob == null)
            {
                return NotFound();
            }

            return View(scheduleJob);
        }

        // POST: ScheduleJob/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var scheduleJob = await _context.ScheduleJobs.FindAsync(id);
            if (scheduleJob != null)
            {
                _context.ScheduleJobs.Remove(scheduleJob);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ScheduleJobExists(int id)
        {
            return _context.ScheduleJobs.Any(e => e.ScheduleId == id);
        }
    }
}
