using System.Security.Principal;
using OrdersEntities.Entities;

namespace OrdersService
{
    public class MembershipContext
    {
        public IPrincipal Principal { get; set; }

        public User User { get; set; }

        public bool IsValid()

        {
            return this.Principal != null;
        }
    }
}