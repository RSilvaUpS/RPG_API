using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Data.Context;
using TBRPGF_API.Dto.HeroesDto;
using TBRPGF_API.Dto.SpellsDto;
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

        public Hero GetHero(int id)
        {
            var hero = _context.Heroes
                .Include(hero => hero.Armor)
                .Include(hero => hero.HeroClass)
                .Where(h => h.Id == id).FirstOrDefault();

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
                        Rating = h.Rating.ToString(),
                        Portrait = h.PortraitLink,
                        isPlayable= h.IsPlayable,
                    })
                    .OrderBy(h => h.Name).ToListAsync();

                return heroList;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<PlayableHeroDto>> GetRandomPlayableHeroes()
        {
            Random rnd = new Random();
            var heroList = new List<PlayableHeroDto>();
            var heroes = await _context.Heroes
                    .Where(h => h.IsPlayable)
                    .Select(h => new PlayableHeroDto
                    {
                        Id = h.Id,
                        Name = h.Name,
                        AccuracyRate = h.AccuracyRate,
                        Armor = GetRandomValue(h.Armor.DamageReducitonMinimum, h.Armor.DamageReducitonMaximum),
                        HP = GetRandomValue(h.HPMin, h.HPMax),
                        Mana = GetRandomValue(h.ManaMin, h.ManaMax),
                        AttackMinimum = h.AttackMinimum,
                        AttackMaximum = h.AttackMaximum,
                        Description = h.Description,
                        HeroClass = h.HeroClass.ClassName,
                        Rating = h.Rating.ToString(),
                        SpellModifier = h.SpellModifier,
                        Portrait = h.PortraitLink,
                        CastableSpells = _context.HeroSpellList.Where(s => s.HeroId == h.Id).Select(s=> new SpellDto
                        {
                            Id =s.Spell.Id,
                            Name = s.Spell.Name,
                            DamageMax= s.Spell.DamageMax,
                            DamageMin= s.Spell.DamageMin,
                            Description= s.Spell.Description,
                            ManaCost= s.Spell.ManaCost,
                            SpellType = s.Spell.SpellType.ToString()
                        }).ToList()
                    })
                    .ToListAsync();

            try
            {
                for (int i = 0; i < 2; i++)
                {
                    var rank = rnd.Next(1, 100);
                    Rating rating = GetRandomRating(rank);
                    IEnumerable<PlayableHeroDto> heroRanked = heroes.Where(hr => hr.Rating == rating.ToString());
                    int index = rnd.Next(heroes.Count());
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

        public PlayableHeroDto GetEnemy(int id)
        {
            Random random = new Random();
            var hero = _context.Heroes
                .Include(hero => hero.Armor)
                .Include(hero => hero.HeroClass).Where(h => h.Id != id)
                .Select(h => new PlayableHeroDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    AccuracyRate = h.AccuracyRate,
                    Armor = GetRandomValue(h.Armor.DamageReducitonMinimum, h.Armor.DamageReducitonMaximum),
                    HP = GetRandomValue(h.HPMin, h.HPMax),
                    Mana = GetRandomValue(h.ManaMin, h.ManaMax),
                    AttackMinimum = h.AttackMinimum,
                    AttackMaximum = h.AttackMaximum,
                    Description = h.Description,
                    HeroClass = h.HeroClass.ClassName,
                    Rating = h.Rating.ToString(),
                    SpellModifier = h.SpellModifier,
                    Portrait = h.PortraitLink,
                    CastableSpells = _context.HeroSpellList.Where(s => s.HeroId == h.Id).Select(s => new SpellDto
                    {
                        Id = s.Spell.Id,
                        Name = s.Spell.Name,
                        DamageMax = s.Spell.DamageMax,
                        DamageMin = s.Spell.DamageMin,
                        Description = s.Spell.Description,
                        ManaCost = s.Spell.ManaCost,
                        SpellType = s.Spell.SpellType.ToString()
                    }).ToList()
                }).ToList();
            var skip = (int)(random.NextDouble() * hero.Count());
            var enemy = hero.OrderBy(o => o.Id).Skip(skip).Take(1).First();

            if (enemy == null)
            {
                return null;
            }
            return enemy;
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
