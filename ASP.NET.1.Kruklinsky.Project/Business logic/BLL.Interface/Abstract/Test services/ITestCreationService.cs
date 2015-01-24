using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface ITestCreationService
    {
        void CreateTest(int subjectId, string name, string topic, string description);
        bool DeleteTest(int id);
        void UpdateTest(Test test);
    }
}
