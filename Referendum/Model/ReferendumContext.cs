using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Referendum.Model
{
    public partial class ReferendumContext : DbContext
    {
        public ReferendumContext()
        {
        }

        public ReferendumContext(DbContextOptions<ReferendumContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Table1> Table1 { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Server= DESKTOP-9GM7NOP\\SQLEXPRESS;Database=referendumDB;Trusted_Connection=True;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Table1>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();
            });
        }
    }
}
