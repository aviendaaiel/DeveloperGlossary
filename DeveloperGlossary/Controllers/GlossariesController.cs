using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DeveloperGlossary.Models;

namespace DeveloperGlossary.Controllers
{
    public class GlossariesController : Controller
    {
        private readonly DeveloperGlossaryContext _context;

        public GlossariesController(DeveloperGlossaryContext context)
        {
            _context = context;
        }

        // GET: Glossaries
        public async Task<IActionResult> Index(string glossaryLanguage, string searchString)
        {
            //IQueryable<string> languageQuery = from m in _context.Glossary
            //                                   orderby m.Language
            //                                   select m.Language;

            IQueryable<string> languageQuery = _context.Glossary.Select(x => x.Language).OrderBy(x => x);

            var glossary = _context.Glossary.Select(x => x);

            // TODO: Load state from previously suspended application  

            if (!String.IsNullOrEmpty(searchString))
            {
                glossary = glossary.Where(s => s.Term.Contains(searchString));
            }

            if (!String.IsNullOrEmpty(glossaryLanguage))
            {
                glossary = glossary.Where(x => x.Language == glossaryLanguage);
            }

            var glossaryLanguageVM = new GlossaryLanguageViewModel();
            glossaryLanguageVM.languages = new SelectList(await languageQuery.Distinct().ToListAsync());
            glossaryLanguageVM.glossary = await glossary.ToListAsync();

            return View(glossaryLanguageVM);
        }

        // GET: Glossaries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossary = await _context.Glossary
                .SingleOrDefaultAsync(m => m.ID == id);
            if (glossary == null)
            {
                return NotFound();
            }

            return View(glossary);
        }

        // GET: Glossaries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Glossaries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Term,Language,Syntax,Definition")] Glossary glossary)
        {
            if (ModelState.IsValid)
            {
                _context.Add(glossary);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(glossary);
        }

        // GET: Glossaries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossary = await _context.Glossary.SingleOrDefaultAsync(m => m.ID == id);
            if (glossary == null)
            {
                return NotFound();
            }
            return View(glossary);
        }

        // POST: Glossaries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Term,Language,Syntax,Definition")] Glossary glossary)
        {
            if (id != glossary.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(glossary);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlossaryExists(glossary.ID))
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
            return View(glossary);
        }

        // GET: Glossaries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var glossary = await _context.Glossary
                .SingleOrDefaultAsync(m => m.ID == id);
            if (glossary == null)
            {
                return NotFound();
            }

            return View(glossary);
        }

        // POST: Glossaries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var glossary = await _context.Glossary.SingleOrDefaultAsync(m => m.ID == id);
            _context.Glossary.Remove(glossary);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlossaryExists(int id)
        {
            return _context.Glossary.Any(e => e.ID == id);
        }
    }
}
