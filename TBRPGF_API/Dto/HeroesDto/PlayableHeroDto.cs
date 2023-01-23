using TBRPGF_API.Dto.SpellsDto;
using TBRPGF_API.Enums;
using TBRPGF_API.Heroes;

namespace TBRPGF_API.Dto.HeroesDto
{
    public class PlayableHeroDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public int Mana { get; set; }
        public int AttackMinimum { get; set; }
        public int AttackMaximum { get; set; }
        public float SpellModifier { get; set; }
        public int Armor { get; set; }
        public string HeroClass { get; set; }
        public int AccuracyRate { get; set; }
        public string Rating { get; set; }
        public string? Portrait { get; set; }

        public List<SpellDto> CastableSpells { get; set; }
    }
}
