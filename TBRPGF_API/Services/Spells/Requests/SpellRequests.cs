using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Data.Context;
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

        public async Task<List<Spell>> GetSpells()
        {
            return await _context.Spells.ToListAsync();
        }

        public async Task<Spell> GetSpell(int id) {
        var spell = await _context.Spells.FindAsync(id);
            if (spell == null)
                return null;
            return spell;
        }
    }
}
