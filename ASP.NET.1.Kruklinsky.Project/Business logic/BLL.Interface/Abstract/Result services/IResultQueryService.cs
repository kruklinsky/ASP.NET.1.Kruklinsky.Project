using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface IResultQueryService
    {
        Result GetResult(int id);
        IEnumerable<Result> GetUserResults(string userId);
    }
}
