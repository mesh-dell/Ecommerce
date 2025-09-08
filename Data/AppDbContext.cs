using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext(options)
{

  public DbSet<AppUser> AppUsers { get; set; }
  public DbSet<Product> Products { get; set; }
  public DbSet<Cart> Carts { get; set; }
  public DbSet<CartItem> CartItems { get; set; }
  public DbSet<Category> Categories { get; set; }
  public DbSet<Order> Orders { get; set; }
  public DbSet<OrderItem> OrderItems { get; set; }

  protected override void OnModelCreating(ModelBuilder builder)
  {
    base.OnModelCreating(builder);

    builder.Entity<Product>()
    .HasOne<Category>()
    .WithMany()
    .HasForeignKey(p => p.CategoryId);

    builder.Entity<Cart>()
    .HasMany(c => c.CartItems)
    .WithOne()
    .HasForeignKey(p => p.CartId)
    .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<Order>()
    .HasMany(o => o.OrderItems)
    .WithOne()
    .HasForeignKey(p => p.OrderId)
    .OnDelete(DeleteBehavior.Cascade);

    builder.Entity<CartItem>()
    .HasOne<Product>()
    .WithMany()
    .HasForeignKey(ci => ci.ProductId);

    builder.Entity<OrderItem>()
    .HasOne<Product>()
    .WithMany()
    .HasForeignKey(oi => oi.ProductId);

    builder.Entity<AppUser>()
    .HasOne<Cart>()
    .WithOne()
    .HasForeignKey<Cart>(c => c.UserId);

    builder.Entity<AppUser>()
    .HasMany<Order>()
    .WithOne()
    .HasForeignKey(c => c.UserId);


    List<IdentityRole> roles = [
      new() { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
      new() { Id = "2", Name = "User", NormalizedName = "USER"}
    ];

    builder.Entity<IdentityRole>().HasData(roles);
  }

}
