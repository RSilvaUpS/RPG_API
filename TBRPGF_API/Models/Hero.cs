using TBRPGF_API.Enums;

namespace TBRPGF_API.Heroes
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HPMin { get; set; }
        public int HPMax { get; set; }
        public int ManaMin { get; set; }
        public int ManaMax { get; set; }
        public int AttackMinimum { get; set; }
        public int AttackMaximum { get; set; }
        public float SpellModifier { get; set; }
        public int? ArmorId { get; set; }
        public Armor Armor { get; set; }
        public int HeroClassId { get; set; }
        public HeroClass HeroClass { get; set; }
        public int AccuracyRate { get; set; }
        public Rating Rating { get; set; }
        public bool IsPlayable { get; set; }

    }
}
