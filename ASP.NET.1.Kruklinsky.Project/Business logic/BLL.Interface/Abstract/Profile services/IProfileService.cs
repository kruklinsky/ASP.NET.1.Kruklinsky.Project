using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IProfileService
    {
        Profile GetUserProfile(string id);
        void UpdateUserProfile(string id, Profile profile);
    }
}
