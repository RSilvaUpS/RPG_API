using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Data.Context;
using TBRPGF_API.Dto.HeroesDto;
using TBRPGF_API.Enums;
using TBRPGF_API.Services.Heroes.Interfaces;

namespace TBRPGF_API.Services.Heroes.Requests
{
    public class HeroRequests : IHeroService
    {
        private readonly TBRPGDBContext _context;

        public HeroRequests(TBRPGDBContext context)
        {
            _context = context;
        }

        public async Task<Hero>? GetHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return null;
            }
                return hero;
        }

        public async Task<List<HeroSampleDto>> GetHeroes()
        {
            try
            {
                var heroList = await _context.Heroes
                    .Select(h => new HeroSampleDto
                    {
                        Id = h.Id,
                        Name = h.Name,
                        Description = h.Description,
                        HeroClass = h.HeroClass.ClassName,
                        Rating = h.Rating,                       
                    })
                    .ToListAsync();

                return heroList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PlayableHeroDto>> GetRandomPlayableHeroesAsync()
        {
            Random rnd = new Random();
            var heroList = new List<PlayableHeroDto>();
            var heroes = await _context.Heroes.Join(_context.HeroPortrait, h => h.Id, p => p.HeroId, (h, p) => new { hero = h, portrait = p })
                    .Where(h => h.hero.IsPlayable)
                    .Select(h => new PlayableHeroDto
                    {
                        Id = h.hero.Id,
                        Name = h.hero.Name,
                        AccuracyRate = h.hero.AccuracyRate,
                        Armor = GetRandomValue(h.hero.Armor.DamageReducitonMinimum, h.hero.Armor.DamageReducitonMaximum),
                        HP = GetRandomValue(h.hero.HPMin, h.hero.HPMax),
                        Mana = GetRandomValue(h.hero.ManaMin, h.hero.ManaMax),
                        Attack = GetRandomValue(h.hero.AttackMinimum, h.hero.AttackMaximum),
                        Description = h.hero.Description,
                        HeroClass = h.hero.HeroClass.ClassName,
                        Rating = h.hero.Rating,
                        SpellModifier = h.hero.SpellModifier,
                        Portrait = h.portrait.HeroImage,
                        CastableSpells = _context.HeroSpellList.Where(s => s.HeroId == h.hero.Id).Select(s => s.Spell).ToList()
                    })
                    .ToListAsync();

            try
            {
                for (int i = 0; i < 2; i++)
                {
                    var rank = rnd.Next(1, 100);
                    Rating rating = GetRandomRating(rank);
                    IEnumerable<PlayableHeroDto> heroRanked = heroes.Where(hr => hr.Rating == rating);
                    int index = rnd.Next(heroes.Count);
                    heroList.Add(heroes[index]);
                    heroes.RemoveAt(index);
                }

                return heroList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        private static int GetRandomValue(int minValue, int maxValue)
        {
            Random random = new Random();
            return random.Next(minValue, maxValue);
        }

        private static Rating GetRandomRating(int randomChance)
        {
            switch (randomChance)
            {
                case < 40:
                    return Rating.R;
                case < 65:
                    return Rating.SR;
                case < 96:
                    return Rating.SSR;
                case < 98:
                    return Rating.UR;
                default: return Rating.R;
            }
        }
    }

}
