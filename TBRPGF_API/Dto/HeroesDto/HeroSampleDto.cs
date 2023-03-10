using TBRPGF_API.Enums;

namespace TBRPGF_API.Dto.HeroesDto
{
    public class HeroSampleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Rating { get; set; }
        public string HeroClass { get; set; }
        public string? Portrait { get; set;}
        public bool isPlayable { get; set; }
    }
}
