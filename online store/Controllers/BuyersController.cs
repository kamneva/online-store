using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laba4.Data;
using Laba4.Models;

namespace Laba4.Controllers
{
    public class BuyersController : Controller
    {
        private readonly Laba4Context _context;

        public BuyersController(Laba4Context context)
        {
            _context = context;
        }

        // GET: Buyers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Buyers.ToListAsync());
        }

        // GET: Buyers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyers.Include(m => m.Orders).FirstOrDefaultAsync(m => m.BuyerID == id);
            if (buyer == null)
            {
                return NotFound();
            }
            ViewData["Orders"] = new MultiSelectList(buyer.Orders, "OrderID", "Number");
            return View(buyer);
        }

        // GET: Buyers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Buyers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BuyerID,Surname,Name,Patronymic,Adress,Mail,Phone")] Buyer buyer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(buyer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(buyer);
        }

        // GET: Buyers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyers.Include(m => m.Orders).FirstOrDefaultAsync(m => m.BuyerID == id);
            if (buyer == null)
            {
                return NotFound();
            }
            var kek = await _context.Orders.ToListAsync();
            ViewData["Orders"] = new MultiSelectList(kek, "OrderID", "Number");
            return View(buyer);
        }

        // POST: Buyers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BuyerID,Surname,Name,Patronymic,Adress,Mail,Phone")] Buyer buyer, int[] Orders)
        {
            if (id != buyer.BuyerID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(buyer);
                    List<Order> order = new List<Order>();
                    for (int i = 0; i < Orders.Length; i++)
                    {
                        order.Add(_context.Orders.FirstOrDefault(m => m.OrderID == Orders[i]));
                        Console.WriteLine(i + ") " + Orders[i]);
                    }
                    for (int i = 0; i < order.Count; i++)
                    {
                        if (buyer.Orders.Contains(order[i]))
                        {
                            buyer.Orders.Remove(order[i]);
                        }
                        else
                        {
                            buyer.Orders.Add(order[i]);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BuyersExists(buyer.BuyerID))
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
            return View(buyer);
        }

        // GET: Buyers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var buyer = await _context.Buyers
                .FirstOrDefaultAsync(m => m.BuyerID == id);
            if (buyer == null)
            {
                return NotFound();
            }

            return View(buyer);
        }

        // POST: Buyers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var buyer = await _context.Buyers.Include(m => m.Orders).FirstOrDefaultAsync(m => m.BuyerID == id);
            buyer.Orders.Clear();
            await _context.SaveChangesAsync();
            _context.Buyers.Remove(buyer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BuyersExists(int id)
        {
            return _context.Buyers.Any(e => e.BuyerID == id);
        }
    }
}
