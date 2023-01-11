using TBRPGF_API.Enums;
using TBRPGF_API.Heroes;

namespace TBRPGF_API.Dto
{
    public class PlayableHeroDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public int Mana { get; set; }
        public int Attack { get; set; }
        public float SpellModifier { get; set; }
        public int Armor { get; set; }
        public string HeroClass { get; set; }
        public int AccuracyRate { get; set; }
        public Rating Rating { get; set; }
        public byte[] Portrait { get; set; }

        public List<Spell> CastableSpells { get; set; }
    }
}
