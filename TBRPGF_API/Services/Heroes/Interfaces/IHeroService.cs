using TBRPGF_API.Dto.HeroesDto;

namespace TBRPGF_API.Services.Heroes.Interfaces
{
    public interface IHeroService
    {
        Task<List<HeroSampleDto>> GetHeroes();
        Task<List<PlayableHeroDto>> GetRandomPlayableHeroesAsync();
        Task<Hero>? GetHero(int id);
    }
}
