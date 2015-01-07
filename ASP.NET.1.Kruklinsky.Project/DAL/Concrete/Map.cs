﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interface.Entities;

namespace DAL.Concrete
{
    public static class Map
    {
        public static ORM.Model.User ToOrm(this User item)
        {
            return new ORM.Model.User()
            {
                UserId = item.Id != null ? new Guid(item.Id) : new Guid(),
                Email = item.Email,
                Password = item.Password,
                IsApproved = item.IsApproved,
                CreateDate = item.CreateDate
            };
        }

        public static User ToDal(this ORM.Model.User item)
        {
            return new User()
            {
                Id = item.UserId.ToString(),
                Email = item.Email,
                Password = item.Password,
                IsApproved = item.IsApproved,
                CreateDate = item.CreateDate
            };
        }

        public static ORM.Model.Profile ToOrm(this Profile item)
        {
            return new ORM.Model.Profile()
            {
                ProfileId = item.Id,
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Birthday = item.Birthday
            };
        }

        public static Profile ToDal(this ORM.Model.Profile item)
        {
            return new Profile()
            {
                Id = item.ProfileId,
                FirstName = item.FirstName,
                SecondName = item.SecondName,
                Birthday = item.Birthday
            };
        }

        public static Role ToDal(this ORM.Model.Role item)
        {
            return new Role()
            {
                Id = item.RoleId,
                Name = item.RoleName,
                Description = item.Description
            };
        }

        public static Guid ToGuid (this string item)
        {
            Guid result;
            if (Guid.TryParse(item, out result))
            {
                return result;
            }
            return new Guid();
        }
    }
}
