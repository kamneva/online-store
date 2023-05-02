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
    public class DeliveriesController : Controller
    {
        private readonly Laba4Context _context;

        public DeliveriesController(Laba4Context context)
        {
            _context = context;
        }

        // GET: Delivery
        public async Task<IActionResult> Index()
        {
            return View(await _context.Deliveries.ToListAsync());
        }

        // GET: Delivery/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.Include(m => m.Orders).FirstOrDefaultAsync(m => m.DeliveryID == id);
            if (delivery == null)
            {
                return NotFound();
            }
    
            ViewData["Orders"] = new MultiSelectList(delivery.Orders, "OrderID", "Buyer");
            return View(delivery);
            
        }

        // GET: Delivery/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Delivery/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("DeliveryID,Order,DeliveryService,DeliveryDate")] Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                _context.Add(delivery);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(delivery);
        }

        // GET: Delivery/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries.Include(m => m.Orders).FirstOrDefaultAsync(m => m.DeliveryID == id);
            if (delivery == null)
            {
                return NotFound();
            }
            var kek = await _context.Orders.ToListAsync();
            ViewData["Orders"] = new MultiSelectList(kek, "OrderID", "Buyer");
            return View(delivery);
        }

        // POST: Delivery/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("DeliveryID,Order,DeliveryService,DeliveryDate")] Delivery delivery, int[] Orders)
        {
            if (id != delivery.DeliveryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(delivery);
                    List<Order> order = new List<Order>();
                    for (int i = 0; i < Orders.Length; i++)
                    {
                        order.Add(_context.Orders.FirstOrDefault(m => m.OrderID == Orders[i]));
                        Console.WriteLine(i + ") " + Orders[i]);
                    }
                    for (int i = 0; i < order.Count; i++)
                    {
                        if (delivery.Orders.Contains(order[i]))
                        {
                            delivery.Orders.Remove(order[i]);
                        }
                        else
                        {
                            delivery.Orders.Add(order[i]);
                        }
                    }
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryExists(delivery.DeliveryID))
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
            return View(delivery);
        }

        // GET: Delivery/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var delivery = await _context.Deliveries
                .FirstOrDefaultAsync(m => m.DeliveryID == id);
            if (delivery == null)
            {
                return NotFound();
            }

            return View(delivery);
        }

        // POST: Delivery/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var delivery = await _context.Deliveries.Include(m => m.Orders).FirstOrDefaultAsync(m => m.DeliveryID == id);
            delivery.Orders.Clear();
            await _context.SaveChangesAsync();
            _context.Deliveries.Remove(delivery);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryExists(int id)
        {
            return _context.Deliveries.Any(e => e.DeliveryID == id);
        }
    }
}
