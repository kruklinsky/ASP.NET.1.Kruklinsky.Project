using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface ISubjectCreationService
    {
        void CreateSubject(string name, string description);
        bool DeleteSubject(int id);
        void UpdateSubject(Subject subjects);
    }
}
