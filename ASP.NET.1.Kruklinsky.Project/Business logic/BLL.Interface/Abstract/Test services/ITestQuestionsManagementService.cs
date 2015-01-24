using BLL.Interface.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Abstract
{
    public interface ITestQuestionsManagementService
    {
        int AddTestQuestions(int id, IEnumerable<int> questionsId);
        int AddNewTestQuestions(int id, IEnumerable<Question> questions);
        int DeleteTestQuestions(int id, IEnumerable<int> questionsId);
    }
}
