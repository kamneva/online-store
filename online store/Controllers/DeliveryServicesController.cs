using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba4.Data;
using Laba4.Models;
using Microsoft.VisualBasic;

namespace Laba4.Controllers {
    public class DeliveryServicesController : Controller 
    {
        private readonly Laba4Context _context;

        public DeliveryServicesController(Laba4Context context)
        {
            _context = context;
        }

        // GET: DeliveryService
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryServices.ToListAsync());
        }

        // GET: DeliveryService/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _DeliveryService = await _context.DeliveryServices.Include(m => m.Deliveries).FirstOrDefaultAsync(m => m.DeliveryServiceID == id);
            if (_DeliveryService == null)
            {
                return NotFound();
            }
            ViewData["Deliveries"] = new MultiSelectList(_DeliveryService.Deliveries, "DeliveryID", "Order");
            return View(_DeliveryService);
        }

        // GET: DeliveryService/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryService/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryServiceID,Name")] DeliveryService _DeliveryService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(_DeliveryService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(_DeliveryService);
        }

        // GET: DeliveryService/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _DeliveryService = await _context.DeliveryServices.Include(m => m.Deliveries).FirstOrDefaultAsync(m => m.DeliveryServiceID == id);
            if (_DeliveryService == null)
            {
                return NotFound();
            }
            var kek = await _context.Deliveries.ToListAsync();
            ViewData["Deliveries"] = new MultiSelectList(kek, "DeliveryID", "Order");
            return View(_DeliveryService);
        }

        // POST: DeliveryService/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryServiceID,Name")] DeliveryService _DeliveryService, int[] Deliveries)
        {
            if (id != _DeliveryService.DeliveryServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_DeliveryService);
                    List<Delivery> delivery = new List<Delivery>();
                    for (int i = 0; i < Deliveries.Length; i++)
                    {
                        delivery.Add(_context.Deliveries.FirstOrDefault(m => m.DeliveryID == Deliveries[i]));
                        Console.WriteLine(i + ") " + Deliveries[i]);
                    }
                    for (int i = 0; i < delivery.Count; i++)
                    {
                        if (_DeliveryService.Deliveries.Contains(delivery[i]))
                        {
                            _DeliveryService.Deliveries.Remove(delivery[i]);
                        }
                        else
                        {
                            _DeliveryService.Deliveries.Add(delivery[i]);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryServiceExists(_DeliveryService.DeliveryServiceID))
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
            return View(_DeliveryService);
        }

        // GET: DeliveryService/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var _DeliveryService = await _context.DeliveryServices
                .FirstOrDefaultAsync(m => m.DeliveryServiceID == id);
            if (_DeliveryService == null)
            {
                return NotFound();
            }

            return View(_DeliveryService);
        }

        // POST: DeliveryService/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var _DeliveryService = await _context.DeliveryServices.Include(m => m.Deliveries).FirstOrDefaultAsync(m => m.DeliveryServiceID == id);
            _DeliveryService.Deliveries.Clear();
            await _context.SaveChangesAsync();
            _context.DeliveryServices.Remove(_DeliveryService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryServiceExists(int id)
        {
            return _context.DeliveryServices.Any(e => e.DeliveryServiceID == id);
        }

        public IActionResult Search()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Search(int id, [Bind("Name")] DeliveryService _DeliveryService)
        {
            if (id != _DeliveryService.DeliveryServiceID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(_DeliveryService);
                    bool Proverka = _DeliveryService.Name == null || _DeliveryService.Name.Trim().ToLower().Length == 0;
                    List<DeliveryService> Result = await _context.DeliveryServices.Where(m => Proverka || m.Name.Trim().ToLower().Contains(_DeliveryService.Name.Trim().ToLower())).ToListAsync();
                    if (Result.Count == 0)
                    {
                        return View(nameof(Index), null);
                    }
                    return View(nameof(Index), Result);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryServiceExists(_DeliveryService.DeliveryServiceID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(_DeliveryService);
        }
    }
}
