using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DeveloperGlossary.Models
{
    public class Glossary
    {
        public int ID { get; set; }

        [StringLength(100, MinimumLength = 2)]
        [Required]
        public string Term { get; set; }
        [StringLength(20, MinimumLength = 1)]
        [Required]
        public string Language { get; set; }
        [StringLength(100)]
        public string Syntax { get; set; }
        [Required]
        public string Definition { get; set; }
    }
}
