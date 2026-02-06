using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using asdasd.Data;
using asdasd.Models;

namespace asdasd.Controllers;

public class ProductsController : Controller
{
    private readonly ApplicationDbContext _context;

    public ProductsController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null) return NotFound();
        var product = await _context.Products.FindAsync(id);
        if (product == null) return NotFound();
        return View(product);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, Product product)
    {
        if (id != product.Id) return NotFound();
        if (ModelState.IsValid)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(product);
    }

    [HttpGet]
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }
        
        var product = await _context.Products
            .FirstOrDefaultAsync(m => m.Id == id);

        if (product == null)
        {
            return NotFound();
        }
        
        return View(product); // Търсим delete.cshtml
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (id == null) return NotFound();
        
        var product = await _context.Products.FindAsync(id);
        if (product != null)
        {
            _context.Products.Remove(product);
        }
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Index(string? searchString)
    {
        var productsQuery = from p in _context.Products
            select p;
        if (!string.IsNullOrEmpty(searchString))
        {
            productsQuery = productsQuery.Where(s => s.Name!.ToLower().Contains(searchString.ToLower()));
        }
        return View(await productsQuery.ToListAsync());
    }
   
}