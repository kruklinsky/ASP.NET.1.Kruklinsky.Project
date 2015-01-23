using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Concrete
{
    public static class MembershipMap
    {
        public static DAL.Interface.Entities.User ToDal (this User item, string password)
        {
            return new DAL.Interface.Entities.User
            {
                 Id = item.Id,
                 Email = item.Email,
                 Password = password,
                 IsApproved = item.IsApproved,
                 CreateDate = item.CreateDate,
            };
        }
        public static DAL.Interface.Entities.User ToDal(this User item)
        {
            return new DAL.Interface.Entities.User
            {
                Id = item.Id,
                Email = item.Email,
                IsApproved = item.IsApproved,
                CreateDate = item.CreateDate,
            };
        }
        public static User ToBll(this DAL.Interface.Entities.User item)
        {
            return new User
            {
                Id = item.Id,
                Email = item.Email,
                IsApproved = item.IsApproved,
                CreateDate = item.CreateDate,
                Profile = item == null ? null : item.Profile.Value.ToBll(),
                Roles = item.Roles == null ? new List<Role>() : item.Roles.Value.Select(r => r.ToBll()).ToList()
            };
        }

        public static Role ToBll(this DAL.Interface.Entities.Role item)
        {
            return new Role
            {
                 Id = item.Id,
                 Name = item.Name,
                 Description = item.Description
            };
        }

        public static DAL.Interface.Entities.Profile ToDal(this Profile item)
        {
            return new DAL.Interface.Entities.Profile
            {
                 Id = item.Id,
                 FirstName = item.FirstName,
                 SecondName = item.SecondName,
                 Birthday = item.Birthday
            };
        }
        public static Profile ToBll (this DAL.Interface.Entities.Profile item)
        {
            return new Profile
            {
                Id = item.Id,
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Birthday = item.Birthday
            };
        }
    }
}
