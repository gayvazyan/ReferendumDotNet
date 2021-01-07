using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Referendum.core.Entities
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

        public virtual DbSet<CitizenDb> CitizenDb { get; set; }
        public virtual DbSet<ReferendumDb> ReferendumDb { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {}
    }
}
