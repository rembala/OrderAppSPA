using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrdersEntities.Entities
{
    public class Role
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)] 
        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public Role()
        {
            this.UserRoles = new List<UserRole>();
        }
    }
}
