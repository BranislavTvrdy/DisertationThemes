using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
//using System.Data.Entity;
using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
using System.Xml;
//using DbContext = Microsoft.EntityFrameworkCore.DbContext;
//using DbContext = System.Data.Entity.DbContext;


namespace DissertationThemes.Database.Model
{
    public partial class NorthwindContext : DbContext
    {
        public NorthwindContext()
        {
        }

        public NorthwindContext(DbContextOptions<NorthwindContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StProgram> StPrograms { get; set; }
        public virtual DbSet<Supervisor> Supervisors { get; set; }
        public virtual DbSet<Theme> Themes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=sharp.kst.fri.uniza.sk;" +
                                            "Initial Catalog=problem2019_DissertationThemes_TvrdyBranislavDb;" +
                                            "Persist Security Info=True;" +
                                            "User ID=problem2019_DissertationThemes_TvrdyBranislav;" +
                                            "Password=558884");
                
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StProgram>().ToTable("StProgram");

            modelBuilder.Entity<Supervisor>().ToTable("Supervisor");

            modelBuilder.Entity<Theme>().ToTable("Theme");

            modelBuilder.Entity<StProgram>().HasMany(theme => theme.Themes);

            modelBuilder.Entity<Supervisor>().HasMany(theme => theme.Themes);

            //OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
