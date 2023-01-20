using TBRPGF_API.Enums;

namespace TBRPGF_API.Dto.HeroesDto
{
    public class HeroSampleDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Rating Rating { get; set; }
        public string HeroClass { get; set; }
    }
}
