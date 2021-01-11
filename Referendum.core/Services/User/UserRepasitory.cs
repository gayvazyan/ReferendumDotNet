using Referendum.core.Entities;

namespace Referendum.core
{
    public class UserRepasitory : Repositories<UserDb>, IUserRepasitory
    {
        public UserRepasitory(ReferendumContext dbContext) : base(dbContext) { }
    }
}
