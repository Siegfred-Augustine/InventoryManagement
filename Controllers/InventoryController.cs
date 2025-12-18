using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Data;
using InventoryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Controllers
{
    public class InventoryController : Controller
    {
      private readonly ILogger<HomeController> _logger;
      private readonly ApplicationDbContext _context;

      public InventoryController(ILogger<HomeController> logger, ApplicationDbContext context)
      {
        _context = context;
        _logger = logger;
      }
      [HttpGet]
      public IActionResult AddItem(){
        return View();
      }
      [HttpPost]
      public async Task<IActionResult> AddItem(Item item){
        Console.WriteLine(item.itemName + item.itemDesc);
        await _context.Items.AddAsync(item);
        await _context.SaveChangesAsync();
        Console.WriteLine(_context.Database.GetConnectionString());
        return View("DisplayItem", item);
      }
      public IActionResult DisplayItem(Item item){
        return View(item);
      }
      [HttpPost]
      public async Task<IActionResult> Delete(Guid id){
        var item = await _context.Items.FindAsync(id);

        if(item == null){
          return NotFound();
        }  
        _context.Items.Remove(item);
        await _context.SaveChangesAsync();

        return RedirectToAction("TableDisplay");
      }
      [HttpGet]
      public async Task<IActionResult> TableDisplay(){
        var items = await _context.Items.ToListAsync();

        return View(items);
      }
      [HttpPost]
      public async Task<IActionResult> Edit(Guid id){
        var item = await _context.Items.FindAsync(id);
        Console.WriteLine("Action Confirmed");

        if(item == null){
          return NotFound();
        }  

        return View("Modify", item);
      }
      [HttpPost]
      public async Task<IActionResult> Modify(Item item){
        if(!ModelState.IsValid)
          return View(item);

        var existing = await _context.Items.FindAsync(item.id);
        if(existing == null)
          return NotFound();

        existing.itemName = item.itemName;
        existing.itemDesc = item.itemDesc;
        existing.itemVal = item.itemVal;
        existing.itemCount = item.itemCount;

        await _context.SaveChangesAsync();
        
        return RedirectToAction("TableDisplay");
      }
    }
}
