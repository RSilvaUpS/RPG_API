namespace TBRPGF_API.Services.Spells.Interface
{
    public interface ISpellService
    {
        Task<List<Spell>> GetSpells();
        Task<Spell> GetSpell(int id);
    }
}
