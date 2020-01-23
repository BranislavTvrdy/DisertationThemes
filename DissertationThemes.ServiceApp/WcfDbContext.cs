using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DissertationThemes.Database.Model;
using Microsoft.EntityFrameworkCore;
using DbContext = System.Data.Entity.DbContext;
using ModelBuilder = System.Data.Entity.DbModelBuilder;


namespace DissertationThemes.ServiceApp
{
    class WcfDbContext : DbContext
    {
        
        public virtual System.Data.Entity.DbSet<StProgram> StPrograms { get; set; }
        public virtual System.Data.Entity.DbSet<Supervisor> Supervisors { get; set; }
        public virtual System.Data.Entity.DbSet<Theme> Themes { get; set; }

        public WcfDbContext() : base("name=BaliContext")
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            System.Data.Entity.Database.SetInitializer<WcfDbContext>(null);

            modelBuilder.Entity<StProgram>().ToTable("StProgram");

            modelBuilder.Entity<Supervisor>().ToTable("Supervisor");

            modelBuilder.Entity<Theme>().ToTable("Theme");

            modelBuilder.Entity<StProgram>().HasMany(theme => theme.Themes);

            modelBuilder.Entity<Supervisor>().HasMany(theme => theme.Themes);

        }

        public IQueryable<Theme> GetThemes()
        {
            return this.Themes.AsQueryable();
        }
    }
}
