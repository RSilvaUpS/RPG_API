using TBRPGF_API.Dto.HeroesDto;

namespace TBRPGF_API.Services.Heroes.Interfaces
{
    public interface IHeroService
    {
        Task<List<HeroSampleDto>> GetHeroes();
        Task<List<PlayableHeroDto>> GetRandomPlayableHeroes();
        Hero GetHero(int id);
        PlayableHeroDto GetEnemy(int id);
    }
}
