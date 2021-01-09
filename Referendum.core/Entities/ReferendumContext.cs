using Microsoft.EntityFrameworkCore;

namespace Referendum.core.Entities
{
    public  class ReferendumContext : DbContext
    {

        public ReferendumContext(DbContextOptions<ReferendumContext> options)
            : base(options)
        {
        }

        public DbSet<CitizenDb> CitizenDb { get; set; }
        public DbSet<ReferendumDb> ReferendumDb { get; set; }
        public DbSet<CommunitiesDb> CommunitiesDb { get; set; }

    }
}
