using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MvcUI.Models
{
    public class Subjects
    {
        public IEnumerable<Subject> Data { get; set; }
    }

    public class Subject
    {
        public BLL.Interface.Entities.Subject subject { get; set; }
        public IEnumerable<BLL.Interface.Entities.Test> Tests { get; set; }
    }
}