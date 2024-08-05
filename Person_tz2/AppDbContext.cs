using Person_tz2;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

public class AppDbContext : DbContext
{
    public AppDbContext() : base("name=db") // Имя строки подключения
    {
    }
    public DbSet<Person> People { get; set; }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>()
            .Property(p => p.PersonalId)
            .HasMaxLength(20);

        modelBuilder.Entity<Person>()
            .Property(p => p.LastName)
            .HasMaxLength(50);

        modelBuilder.Entity<Person>()
            .Property(p => p.FirstName)
            .HasMaxLength(50);

        modelBuilder.Entity<Person>()
            .Property(p => p.MiddleName)
            .HasMaxLength(50);

        modelBuilder.Entity<Person>()
            .Property(p => p.Email)
            .HasMaxLength(100);

        modelBuilder.Entity<Person>()
            .Property(p => p.PhoneNumber)
            .HasMaxLength(100);
    }
}


