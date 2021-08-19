using System;
using Microsoft.EntityFrameworkCore;
using System.IO;


namespace CP380_PubsLab.Models
{
    public class PubsDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbpath = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\pubs.mdf"));
            optionsBuilder.UseSqlServer($"Server=(localdb)\\mssqllocaldb;Integrated Security=true;AttachDbFilename={dbpath}");
        }

        public DbSet<Employee> Employee { get; set; }   //Employee table
        public DbSet<Jobs> Jobs { get; set; }
        public DbSet<Sales> Sales { get; set; }//Job table
        public DbSet<Titles> Titles { get; set; }       //Titles table
        public DbSet<Stores> Stores { get; set; }       // Stores table
      

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Sales>()
          .HasOne(ho => ho.Store)
          .Withmany(wm => wm.Store)
          .HasForeignKey<Stores>(hfk => ho.Id);

            modelBuilder.Entity<Stores>()
        .HasOne(a => a.Sales)
        .WithOne(b => b.Storesli)
        .HasForeignKey<Sales>(b => b.SId);
            // TODO
        }
    }


    public class Titles
    {
       
        public string title { get; set; }
       
        public Sales Sales { get; set; }

    }


    public class Stores
    {
        public char stor_id { get; set; }
        public string namestore { get; set; }
        public Sales Sales { get; set; }
    }


    public class Sales
    {
        public string stor_id { get; set; }
        public string  storename { get; set; }
        public string stor_address { get; set; }
    }

    public class Employee
    {
        // TODO
        public string emp_id { get; set; }
        public string fname { get; set; }
        public string lname { get; set; }

        public Jobs Job { get; set; }
    }

    public class Jobs
    {
        // TODO
        public Int16 Id { get; set; }
      
        public string Desc { get; set; }
    }
}
