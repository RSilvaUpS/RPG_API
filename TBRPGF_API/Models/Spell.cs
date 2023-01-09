using Microsoft.Identity.Client;
using TBRPGF_API.Enums;

namespace TBRPGF_API.Heroes
{
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DamageMin { get; set; }
        public int DamageMax { get; set; }
        public SpellType SpellType { get; set; }
        public int ManaCost { get; set; }
    }
}
