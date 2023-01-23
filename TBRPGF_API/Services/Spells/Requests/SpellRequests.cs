using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Data.Context;
using TBRPGF_API.Dto.SpellsDto;
using TBRPGF_API.Services.Spells.Interface;

namespace TBRPGF_API.Services.Spells.Requests
{
    public class SpellRequests : ISpellService
    {
        private readonly TBRPGDBContext _context;

        public SpellRequests(TBRPGDBContext context)
        {
            _context = context;
        }

        public async Task<List<SpellDto>> GetSpells()
        {
            return await _context.Spells.Select( s => new SpellDto
            {
                Id = s.Id,
                Name = s.Name,
                SpellType= s.SpellType.ToString(),
                DamageMax= s.DamageMax,
                DamageMin= s.DamageMin,
                Description= s.Description,
                ManaCost = s.ManaCost
            }).ToListAsync();
        }

        public async Task<SpellDto> GetSpell(int id) {
            var s = await _context.Spells.FindAsync(id);
            var spell = new SpellDto
            {
            Id = s.Id,
            Name = s.Name,
            SpellType= s.SpellType.ToString(),
            DamageMax= s.DamageMax,
            DamageMin= s.DamageMin,
            Description= s.Description,
            ManaCost= s.ManaCost
        };
            if (spell == null)
                return null;
            spell.SpellType = spell.SpellType.ToString();
            return spell;
        }
    }
}
