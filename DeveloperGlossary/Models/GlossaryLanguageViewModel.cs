using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace DeveloperGlossary.Models
{
    public class GlossaryLanguageViewModel
    {
        public List<Glossary> glossary;
        public SelectList languages;
        public string glossaryLanguage { get; set; }
    }
}
