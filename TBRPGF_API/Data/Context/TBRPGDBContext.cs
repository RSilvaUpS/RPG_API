using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Heroes;

namespace TBRPGF_API.Data.Context
{
    public class TBRPGDBContext : DbContext
    {
        public TBRPGDBContext(DbContextOptions<TBRPGDBContext> options) : base(options)
        {

        }

        public DbSet<Armor> Armors { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<HeroClass> HeroClasses { get; set; }
        public DbSet<HeroSpellList> HeroSpellList { get; set; }
        public DbSet<Spell> Spells { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HeroSpellList>()
                .HasKey(o => new { o.HeroId, o.SpellId });
        }
    }
}
