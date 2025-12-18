using Microsoft.EntityFrameworkCore;
namespace InventoryManagement.Data;

public class ApplicationDbContext : DbContext
{
  public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options){
  }
  DbSet<Item> Items{get;set;}
}
