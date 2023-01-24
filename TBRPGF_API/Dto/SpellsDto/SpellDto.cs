using TBRPGF_API.Enums;

namespace TBRPGF_API.Dto.SpellsDto
{
    public class SpellDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int DamageMin { get; set; }
        public int DamageMax { get; set; }
        public string? SpellType { get; set; }
        public int ManaCost { get; set; }
        public string? Portrait { get; set; } 
    }
}
