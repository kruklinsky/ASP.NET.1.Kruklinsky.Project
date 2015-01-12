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
        Subject GetSubject(int id, out IEnumerable<Test> tests);

        IEnumerable<Test> GetSubjectTests(int id);
    }
}
