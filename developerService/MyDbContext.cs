using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace developerService
{
  class MyDbContext : DbContext
  {
    public DbSet<Developer> Developers { get; set; }

    public MyDbContext()
    {
      //needed for the migrations only
    }

    public MyDbContext(DbContextOptions<MyDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Developer>()
        .Property(b => b.Name)
        .IsRequired();
    }

    //used for the migrations only
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      //
      var cs = Program.Configuration.GetConnectionString("MyDbContext");
      if (cs == null)
      {
        //we are in dev so use a local db
        cs = @"Server = (localdb)\mssqllocaldb; Database = demo; Trusted_Connection = True; ConnectRetryCount = 0";
        Console.WriteLine("In dev so using the local dev db...");
      }
      Console.WriteLine("trying to migrate the db using cs: "+cs);
      optionsBuilder.UseSqlServer(cs);
    }
  }


  public class Developer
  {
    public int DeveloperId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string HackerName { get; set; }

    [Required]
    public string Location { get; set; }

    public string Bio { get; set; }
  }
}
