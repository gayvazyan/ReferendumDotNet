using Microsoft.EntityFrameworkCore;

namespace Referendum.core.Entities
{
    public class ReferendumContext : DbContext
    {

        public ReferendumContext(DbContextOptions<ReferendumContext> options)
            : base(options)
        {
        }

        public DbSet<CitizenDb> CitizenDb { get; set; }
        public DbSet<ReferendumDb> ReferendumDb { get; set; }
        public DbSet<CommunitiesDb> CommunitiesDb { get; set; }
        public DbSet<UserDb> UserDb { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserDb>().HasData(

                new UserDb
                {
                    Id = 1,
                    Login = "Garegin",
                    Password = "Sa123456!",
                    FirstName = "Garegin",
                    LastName = "Ayvazyan"
                },
                 new UserDb
                 {
                     Id = 2,
                     Login = "Yelena",
                     Password = "Sa123456",
                     FirstName = "Yelena",
                     LastName = "Ayvazyan"
                 },
                  new UserDb
                  {
                      Id = 3,
                      Login = "Meline",
                      Password = "Sa123456",
                      FirstName = "Meline",
                      LastName = "Davtyan"
                  }

                );
        }

    }
}
