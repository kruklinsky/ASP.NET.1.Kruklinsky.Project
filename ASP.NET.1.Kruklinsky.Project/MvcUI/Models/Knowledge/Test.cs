using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MvcUI.Models
{
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
}
