using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcUI.Models
{
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

        [Display(Name = "Example")]
        public string Example { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}
