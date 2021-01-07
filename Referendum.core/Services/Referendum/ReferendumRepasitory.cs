using Referendum.core.Entities;

namespace Referendum.core
{
    public class ReferendumRepasitory : Repositories<ReferendumDb>, IReferendumRepasitory
    {
        public ReferendumRepasitory(ReferendumContext dbContext) : base(dbContext) { }
    }
}
