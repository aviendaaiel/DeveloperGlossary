using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DeveloperGlossary.Models
{
    public class DeveloperGlossaryContext : DbContext
    {
        public DeveloperGlossaryContext (DbContextOptions<DeveloperGlossaryContext> options)
            : base(options)
        {
        }

        public DbSet<DeveloperGlossary.Models.Glossary> Glossary { get; set; }
    }
}
