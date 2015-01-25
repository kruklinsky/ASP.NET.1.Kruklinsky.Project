using MvcUI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcUI.Infrastructure.Abstract
{
    public interface ITestSessionFactory
    {
        ITestSession GetTestSession();
    }
}
