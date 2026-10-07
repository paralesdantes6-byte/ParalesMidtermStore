using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParalesMidtermStore.Models;

namespace ParalesMidtermStore.Data;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options
) : IdentityDbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public DbSet<CartItem> CartItems { get; set; }
}