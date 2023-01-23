using TBRPGF_API.Dto.SpellsDto;

namespace TBRPGF_API.Services.Spells.Interface
{
    public interface ISpellService
    {
        Task<List<SpellDto>> GetSpells();
        Task<SpellDto> GetSpell(int id);
    }
}
