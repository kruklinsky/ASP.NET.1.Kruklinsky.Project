using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface ISubjectQueryService
    {
        Subject GetSubject(int id);
        IEnumerable<Subject> GetAllSubjects();
    }
}
