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
    public class Subject
    {
        [Display(Name = "Subject id")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Subject name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }


    public class TestEditor
    {
        public Test Test { get; set; }

        public IEnumerable<Question> Questions { get; set; }
    }
    public class Test
    {
        [Display(Name = "Test id")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Subject id")]
        [HiddenInput(DisplayValue = false)]
        public int SubjectId { get; set; }

        [Required]
        [Display(Name = "Test name")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Topic")]
        public string Topic { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }


    public class QuestionEditor
    {
        public Question Question { get; set; }
        public IEnumerable<Answer> Answers { get; set; }
        public IEnumerable<Fake> Fakes { get; set; }
    }
    public class Question
    {
        [Display(Name = "Question id")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Subject id")]
        [HiddenInput(DisplayValue = false)]
        public int SubjectId { get; set; }

        [Display(Name = "Test id")]
        [HiddenInput(DisplayValue = false)]
        public int TestId { get; set; }

        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }

        [Required]
        [Display(Name = "Topic")]
        public string Topic { get; set; }

        [Required]
        [Display(Name = "Question level")]
        public int Level { get; set; }

        [Required]
        [Display(Name = "Example")]
        public string Example { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

    }


    public class Answer
    {
        [Display(Name = "Answer id")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Question id")]
        [HiddenInput(DisplayValue = false)]
        public int QuestionId { get; set; }

        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }
    }
    public class Fake
    {
        [Display(Name = "Fake id")]
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Display(Name = "Question id")]
        [HiddenInput(DisplayValue = false)]
        public int QuestionId { get; set; }

        [Required]
        [Display(Name = "Text")]
        public string Text { get; set; }
    }

}