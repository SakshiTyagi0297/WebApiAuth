﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using newProj.Models;
using WebApiAuth.Models;

namespace WebApiAuth.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {
                
        }
        public DbSet<TblUser> TblUsers { get; set; }

        public DbSet<NewModels> NewModels { get; set; }
        public DbSet<Department> departments { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Project> projects { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the AccessToken property to have a default value
            modelBuilder.Entity<TblUser>()
                .Property(u => u.AccessToken)
                .HasDefaultValue("default_value");
            modelBuilder.Entity<TblUser>()
                .Property(x => x.UserMessage)
                .HasDefaultValue("default_value");

            // Other model configurations go here

            base.OnModelCreating(modelBuilder);
        }
    }
}
