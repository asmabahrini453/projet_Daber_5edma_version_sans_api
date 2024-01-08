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
    public class CandidatsController : Controller
    {
        private readonly AppDbContext _context;

        public CandidatsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Candidats
        public async Task<IActionResult> Index()
        {
            return _context.Candidats != null ?
                        View(await _context.Candidats.ToListAsync()) :
                        Problem("Entity set 'AppDbContext.Candidats'  is null.");
        }

        // GET: Candidats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Candidats == null)
            {
                return NotFound();
            }

            var candidat = await _context.Candidats
                .FirstOrDefaultAsync(m => m.Id == id);
            if (candidat == null)
            {
                return NotFound();
            }

            return View(candidat);
        }

        // GET: Candidats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Candidats/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,Tel,Password,DateNaiss,Speciality,Experience,Education")] Candidat candidat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(candidat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(candidat);
        }

        // GET: Candidats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Candidats == null)
            {
                return NotFound();
            }

            var candidat = await _context.Candidats.FindAsync(id);
            if (candidat == null)
            {
                return NotFound();
            }
            return View(candidat);
        }

        // POST: Candidats/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Tel,Password,,Speciality,Experience,Education")] Candidat candidat)
        {
            if (id != candidat.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(candidat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CandidatExists(candidat.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index","Home");
            }
            return View(candidat);
        }

       
        

        //******************************************************************
        // Add this action for displaying the login form
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            var candidat = _context.Candidats.SingleOrDefault(u => u.Email == email && u.Password == password);
            if (candidat != null)
            {
                HttpContext.Session.SetInt32("Candidat", candidat.Id);
                return RedirectToAction("Index","Home");
            }
            else
            {
                return BadRequest(new { message = "Username or password is incorrect" });
            }
        }
        //******************************************************************
        //***********************************************************************
        [HttpPost]
        public async Task<IActionResult> Offer_app_list(int id)
        {
            if (id == null || _context.JobOffers == null)
            {
                return NotFound();
            }

            var l = from ja in _context.JobApplications
                    join jo in _context.JobOffers
                    on ja.JobOfferId equals jo.Id
                    join c in _context.Companies
                    on jo.Id equals c.Id
                    where ja.CandidatId == id
                    select new Candidat_Application
                    {
                        Title = jo.Title,
                        Speciality = jo.Speciality,
                        NameCompany = c.Name,
                        TelCompany = c.Tel,
                        EmailCompany = c.Email,
                        Status = ja.Status
                    };

            if (l == null || !l.Any())
            {
                return NotFound();
            }

            return View(await l.ToListAsync());
        }

        //***********************************************************************
        private bool CandidatExists(int id)
        {
          return (_context.Candidats?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
