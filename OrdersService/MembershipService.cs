using OrdersData.Infrastructure;
using OrdersData.Repository;
using OrdersEntities.Entities;
using OrdersService.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrdersService.Extensions;
using System.Security.Principal;

namespace OrdersService
{
    public class MembershipService : IMembership
    {
        private readonly IEntityBaseRepository<User> _userRepository;
        private readonly IEntityBaseRepository<Role> _roleRepository;
        private readonly IEntityBaseRepository<UserRole> _userRoleRepository;
        private readonly IEncryptionService _encryptionService;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipService(IEntityBaseRepository<User> userRepository, IEntityBaseRepository<Role> roleRepository,
         IEntityBaseRepository<UserRole> userRoleRepository, IEncryptionService encryptionService, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _userRoleRepository = userRoleRepository;
            _encryptionService = encryptionService;
            _unitOfWork = unitOfWork;
        }

        public MembershipContext ValidateUser(string UserName, string Password)
        {
            var membershictx = new MembershipContext();
            User User = this._userRepository.GetSingleByUsername(UserName);
            if (User != null && isUserValid(User, Password))
            {
                var userRoles = GetUserRoles(User.UserName);
                membershictx.User = User;

                var identity = new GenericIdentity(User.UserName);
                membershictx.Principal = new GenericPrincipal(identity, userRoles.Select(g => g.RoleName).ToArray());
            }
            return membershictx;
        }

        private bool isUserValid(User User, string password)
        {
            if (IsPasswordIsValid(User, password))
            {
                return !User.IsLocked;
            }
            return false;
        }

        private bool IsPasswordIsValid(User User, string password)
        {
            return string.Equals(this._encryptionService.EncryptPassword(password, User.Salt), User.HashedPassword);
        }

        public List<Role> GetUserRoles(string username)
        {
            List<Role> _result = new List<Role>();
            User _existingUser = this._userRepository.GetSingleByUsername(username);
            if (_existingUser != null)
            {
                foreach (var userRole in _existingUser.UserRole)
                {
                    _result.Add(userRole.Role);
                }
            }
            return _result.Distinct().ToList();
        }

        public User CreateUser(string username, string FirstName, string LastName, string email, string password, int[] roles)
        {
            var existingUser = this._userRepository.GetSingleByUsername(username);
            if (existingUser != null)
            {
                throw new Exception("UserName is already in use");
            }
            var passwordSalt = this._encryptionService.CreateSalt();
            var user = new User()
            {
                UserName = username,
                FirstName = FirstName,
                LastName = LastName,
                Salt = passwordSalt,
                Email = email,
                IsLocked = false,
                HashedPassword = this._encryptionService.EncryptPassword(password, passwordSalt),
                DateCreated = DateTime.Now
            };
            this._userRepository.Add(user);
            this._unitOfWork.Commit();
            if (roles != null && roles.Length > 0)
            {
                foreach (var role in roles)
                {
                    this.addUserToRole(user, role);
                }
            }
            this._unitOfWork.Commit();
            return user;
        }

        public User GetUser(int userId)
        {
            return this._userRepository.GetAll().FirstOrDefault(g => g.UserID == userId);
        }

        private void addUserToRole(User user, int roleId)
        {
            var role = this._roleRepository.GetAll().FirstOrDefault(g => g.RoleID == roleId);
            if (role == null)
            {
                throw new Exception("Role doesn't exist");
            }
            var userRole = new UserRole()
            {
                RoleID = role.RoleID,
                UserID = user.UserID
            };
            this._userRoleRepository.Add(userRole);
        }

    }
}
