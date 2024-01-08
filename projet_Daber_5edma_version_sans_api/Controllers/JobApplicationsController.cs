using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projet_Daber_5edma_version_sans_api.Models;

namespace projet_Daber_5edma_version_sans_api.Controllers
{
    public class JobApplicationsController : Controller
    {
        private readonly AppDbContext _context;

        public JobApplicationsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: JobApplications
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.JobApplications.Include(j => j.Candidat).Include(j => j.JobOffer);
            return View(await appDbContext.ToListAsync());
        }

        // GET: JobApplications/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobApplications == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .Include(j => j.Candidat)
                .Include(j => j.JobOffer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // GET: JobApplications/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: JobApplications/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int JobOfferId, JobApplication jobApplication)
        {
            if (HttpContext.Session.GetInt32("Candidat") != null)
            {
                jobApplication.CandidatId = (int)HttpContext.Session.GetInt32("Candidat");
                
            }
            
            jobApplication.JobOfferId = JobOfferId;
            jobApplication.Status = "Processing";

                _context.Add(jobApplication);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Home");
  
        }

        // GET: JobApplications/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobApplications == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication == null)
            {
                return NotFound();
            }
            ViewData["CandidatId"] = new SelectList(_context.Candidats, "Id", "Education", jobApplication.CandidatId);
            ViewData["JobOfferId"] = new SelectList(_context.JobOffers, "Id", "Description", jobApplication.JobOfferId);
            return View(jobApplication);
        }

        // POST: JobApplications/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Status,CandidatId,JobOfferId")] JobApplication jobApplication)
        {
            if (id != jobApplication.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobApplication);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobApplicationExists(jobApplication.Id))
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
            ViewData["CandidatId"] = new SelectList(_context.Candidats, "Id", "Education", jobApplication.CandidatId);
            ViewData["JobOfferId"] = new SelectList(_context.JobOffers, "Id", "Description", jobApplication.JobOfferId);
            return View(jobApplication);
        }

        // GET: JobApplications/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobApplications == null)
            {
                return NotFound();
            }

            var jobApplication = await _context.JobApplications
                .Include(j => j.Candidat)
                .Include(j => j.JobOffer)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobApplication == null)
            {
                return NotFound();
            }

            return View(jobApplication);
        }

        // POST: JobApplications/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobApplications == null)
            {
                return Problem("Entity set 'AppDbContext.JobApplications'  is null.");
            }
            var jobApplication = await _context.JobApplications.FindAsync(id);
            if (jobApplication != null)
            {
                _context.JobApplications.Remove(jobApplication);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        //***************************
        [HttpPost]
        public async Task<IActionResult> ModifSatut(int JobOfferId, string result)
        {
            var jobApplication = await _context.JobApplications.FindAsync(JobOfferId);


            if (jobApplication == null)
            {
                return NotFound();
            }

            jobApplication.Status = result;
            _context.JobApplications.Update(jobApplication);
            await _context.SaveChangesAsync();


            return RedirectToAction("Index", "JobOffers");
        }

        //*******************************



        private bool JobApplicationExists(int id)
        {
          return (_context.JobApplications?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
