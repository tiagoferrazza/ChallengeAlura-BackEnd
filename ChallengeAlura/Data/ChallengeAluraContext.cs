using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ChallengeAlura.Models;

namespace ChallengeAlura.Data
{
    public class ChallengeAluraContext : DbContext
    {
        public ChallengeAluraContext (DbContextOptions<ChallengeAluraContext> options)
            : base(options)
        {
        }

        public DbSet<ChallengeAlura.Models.Despesas> Despesas { get; set; } = default!;

        public DbSet<ChallengeAlura.Models.Receitas>? Receitas { get; set; }
    }
}
