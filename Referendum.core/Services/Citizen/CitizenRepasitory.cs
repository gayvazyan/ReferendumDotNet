using Referendum.core.Entities;

namespace Referendum.core
{
    public class CitizenRepasitory : Repositories<CitizenDb>, ICitizenRepasitory
    {
        public CitizenRepasitory(ReferendumContext dbContext) : base(dbContext) { }
    }
}
