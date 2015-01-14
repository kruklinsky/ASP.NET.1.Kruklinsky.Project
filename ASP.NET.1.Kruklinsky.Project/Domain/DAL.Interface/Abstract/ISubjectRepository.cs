using DAL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface.Abstract
{
    public interface ISubjectRepository: IRepository<Subject>
    {
        Subject GetSubject(int id);

        IEnumerable<Test> GetSubjectTests(int id);
    }
}
