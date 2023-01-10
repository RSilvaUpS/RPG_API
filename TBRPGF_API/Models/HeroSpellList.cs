namespace TBRPGF_API.Heroes
{
    public class HeroSpellList
    {
        public int Id { get; set; }
        public int HeroId { get; set; }
        public Hero Hero { get; set; }
        public int SpellId { get; set; }
        public Spell Spell { get; set; }
    }
}
