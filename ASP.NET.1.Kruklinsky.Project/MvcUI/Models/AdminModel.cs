using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcUI.Models
{
    public class Subjects
    {
        public IEnumerable<SubjectEditor> Data { get; set; }
    }

    public class SubjectEditor
    {
        public Subject Subject { get; set; }
        public IEnumerable<Test> Tests { get; set; }
    }


    public class TestEditor
    {
        public Test Test { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }

    public class QuestionEditor
    {
        public Question Question { get; set; }
        public List<Answer> Answers { get; set; }
        public List<Fake> Fakes { get; set; }
    }





}