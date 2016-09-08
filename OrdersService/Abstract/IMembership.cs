using OrdersEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersService.Abstract
{
    public interface IMembership
    {
        MembershipContext ValidateUser(string username, string password);
        User CreateUser(string username, string FirstName, string LastName, string email, string password, int[] roles);
        User GetUser(int userId);
        List<Role> GetUserRoles(string username);
    }
}