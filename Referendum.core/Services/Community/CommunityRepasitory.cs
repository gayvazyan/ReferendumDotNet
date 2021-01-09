using Referendum.core.Entities;

namespace Referendum.core
{
    public class CommunityRepasitory : Repositories<CommunitiesDb>, ICommunityRepasitory
    {
        public CommunityRepasitory(ReferendumContext dbContext) : base(dbContext) { }
    }
}
