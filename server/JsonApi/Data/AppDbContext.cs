using System;
using JsonApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace JsonApi.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
  public DbSet<User> Users { get; set;}

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<User>().HasData(
      new User { Id = 1, Ime = "Janez", Starost = 25 },
      new User { Id = 2, Ime = "Micka", Starost = 24 },
      new User { Id = 3, Ime = "Polde", Starost = 27 }
    );
  }
}
