using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using projet_Daber_5edma_version_sans_api.Models;

namespace projet_Daber_5edma_version_sans_api.Controllers
{
    public class JobOffersController : Controller
    {
        private readonly AppDbContext _context;

        public JobOffersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: JobOffers
        public async Task<IActionResult> Index()
        {
            var companies_id = HttpContext.Session.GetInt32("Companie");
            ViewBag.Companie = companies_id;

            if (companies_id != null) {
                var appDbContext1 = _context.JobOffers.Include(j => j.Company).Where(j => j.CompanyId == companies_id);
                return View(await appDbContext1.ToListAsync());
            }

            // Filtrer les JobOffers par CompanyId
            var appDbContext = _context.JobOffers.Include(j => j.Company);
                return View(await appDbContext.ToListAsync());
            
            
        }
    

        // GET: JobOffers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.JobOffers == null)
            {
                return NotFound();
            }

            ViewBag.Companie= HttpContext.Session.GetInt32("Companie");
            ViewBag.Candidat = HttpContext.Session.GetInt32("Candidat");

            var jobOffer = await _context.JobOffers
                .Include(j => j.Company)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (jobOffer == null)
            {
                return NotFound();
            }

            return View(jobOffer);
        }


        //***********************************************************************
        [HttpPost]
        public async Task<IActionResult> Candidate_list(int id )
        {    
            if (id == null || _context.JobOffers == null)
            {

                return NotFound();
            }

            var l = from c in _context.Candidats
                    join ja in _context.JobApplications on c.Id equals ja.CandidatId
                    join jo in _context.JobOffers on ja.JobOfferId equals jo.Id
                    where jo.Id == id
                    select new Candidat_JobOffer
                    {
                        cName = c.Name,
                        cSpeciality = c.Speciality,
                        cExperience = c.Experience,
                        cEducation = c.Education,
                        cTel = c.Tel,
                        cEmail = c.Email,
                        cDateNaiss = c.DateNaiss,
                        jaStatus = ja.Status,
                        jaId = ja.Id
                    };

            if (l == null)
            {
                return NotFound();
            }

            return View(l);
        }
        //***********************************************************************
        // GET: JobOffers/Create
        public IActionResult Create()
        {
           // ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description");
            ViewBag.companie_id = HttpContext.Session.GetInt32("Companie");
            return View();
        }

        // POST: JobOffers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,PostedDate,Speciality,Location")] JobOffer jobOffer)
        {
            if (HttpContext.Session.GetInt32("Companie") != null)
            {
                jobOffer.CompanyId = (int)HttpContext.Session.GetInt32("Companie");
            }

            if (ModelState.IsValid)
            {
                _context.Add(jobOffer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(jobOffer);
           
        }

        // GET: JobOffers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.JobOffers == null)
            {
                return NotFound();
            }

            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description", jobOffer.CompanyId);
            return View(jobOffer);
        }

        // POST: JobOffers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,PostedDate,Speciality,Location")] JobOffer jobOffer)
        {
            if (id != jobOffer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    jobOffer.CompanyId = (int)HttpContext.Session.GetInt32("Companie");
                    _context.Update(jobOffer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobOfferExists(jobOffer.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.Companies, "Id", "Description", jobOffer.CompanyId);
            return View(jobOffer);
        }

        // GET: JobOffers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.JobOffers == null)
            {
                return NotFound();
            }

            var jobOffer = await _context.JobOffers
                .Include(j => j.Company)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (jobOffer == null)
            {
                return NotFound();
            }

            return View(jobOffer);
        }

        // POST: JobOffers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.JobOffers == null)
            {
                return Problem("Entity set 'AppDbContext.JobOffers'  is null.");
            }
            var jobOffer = await _context.JobOffers.FindAsync(id);
            if (jobOffer != null)
            {
                _context.JobOffers.Remove(jobOffer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobOfferExists(int id)
        {
          return (_context.JobOffers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}