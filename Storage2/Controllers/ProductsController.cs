using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Storage2.Data;
using Storage2.Models;

namespace Storage2.Controllers
{
    public class ProductsController : Controller
    {
        private readonly Storage2Context _context;

        public ProductsController(Storage2Context context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        //[HttpGet]
        //public async Task<IActionResult> Filter()
        //{
        //    IEnumerable<ProductViewModel> model = await _context.Product.Select(p => new ProductViewModel
        //    {
        //        Id = p.Id,
        //        Name = p.Name,
        //        Price = p.Price,
        //        Count = p.Count,
        //        InventoryValue = p.Count * p.Price
        //    })
        //        .ToListAsync();

        //    return View(model);
        //}

    

        [HttpGet]
        public async Task<IActionResult> Filter(string category)
        {
            ViewData["CurrentFilter"] = category;

            IQueryable<ProductViewModel> model = _context.Product
                .Where(p => string.IsNullOrEmpty(category) || p.Category.ToUpper().Contains(category.ToUpper()))
                .Select(p => new ProductViewModel
                {
                    Name = p.Name,
                    Price = p.Price,
                    Count = p.Count,
                    InventoryValue = p.Count * p.Price
                });

            var resultList = await model.ToListAsync();
            return View(resultList);
        }


        //public async Task<IActionResult> Filter(string category)
        //{
        //    IEnumerable<ProductViewModel> model;

        //    if (string.IsNullOrEmpty(category))
        //    {
        //        model = await _context.Product.Select(p => new ProductViewModel
        //        {
        //            Id = p.Id,
        //            Name = p.Name,
        //            Price = p.Price,
        //            Count = p.Count,
        //            InventoryValue = p.Count * p.Price
        //        }).ToListAsync();
        //    }
        //    else
        //    {
        //        model = await _context.Product
        //            .Where(p => p.Category.Equals(category, StringComparison.OrdinalIgnoreCase))
        //            .Select(p => new ProductViewModel
        //            {
        //                Id = p.Id,
        //                Name = p.Name,
        //                Price = p.Price,
        //                Count = p.Count,
        //                InventoryValue = p.Count * p.Price
        //            })
        //            .ToListAsync();
        //    }

        //    return View(model);
        //}



        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Orderdate,Category,Shelf,Count,Description")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
