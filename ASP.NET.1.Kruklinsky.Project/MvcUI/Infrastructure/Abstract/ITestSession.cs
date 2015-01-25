using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcUI.Infrastructure.Abstract
{
    public interface ITestSession
    {
        bool IsStarted { get; }
        int ResultId { get; }
        Testing Test { get; }

        void Start(Testing test, int resultId);
        IEnumerable<BLL.Interface.Entities.UserAnswer> Finish(List<Answers> answers);
    }
}
