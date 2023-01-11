using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Data.Context;
using TBRPGF_API.Heroes;
using TBRPGF_API.Dto;
using TBRPGF_API.Enums;

namespace TBRPGF_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly TBRPGDBContext _context;

        public HeroesController(TBRPGDBContext context)
        {
            _context = context;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayableHeroDto>>> GetHeroes()
        {
            try
            {
                var heroList = _context.Heroes
                    .Where(h => h.IsPlayable).Join(_context.HeroPortrait, h => h.Id, p => p.HeroId, (h, p) => new { hero = h, portrait = p })
                    .Select(h => new PlayableHeroDto
                    {
                        Id = h.hero.Id,
                        Name = h.hero.Name,
                        AccuracyRate = h.hero.AccuracyRate,
                        Armor = GetRandomValue(h.hero.Armor.DamageReducitonMinimum, h.hero.Armor.DamageReducitonMaximum),
                        HP = GetRandomValue(h.hero.HPMin, h.hero.HPMax),
                        Mana = GetRandomValue(h.hero.ManaMin, h.hero.ManaMax),
                        Attack = GetRandomValue(h.hero.AttackMinimum, h.hero.AttackMaximum),
                        Description= h.hero.Description,
                        HeroClass = h.hero.HeroClass.ClassName,
                        Rating= h.hero.Rating,
                        SpellModifier = h.hero.SpellModifier,
                        Portrait = h.portrait.HeroImage
                    }) 
                    .ToList();

                return heroList;
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Playable")]
        public async Task<ActionResult<IEnumerable<PlayableHeroDto>>> GetRandomPlayableHeroes()
        {
            Random rnd = new Random();
            var heroList = new List<PlayableHeroDto>();
            var heroes = _context.Heroes.Join(_context.HeroPortrait, h => h.Id, p => p.HeroId, (h, p) => new {hero = h, portrait = p})
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
                        Portrait = h.portrait.HeroImage
                    })
                    .ToList();

            try
            {
                for(int i = 0; i<2; i++)
                {
                    var rank = rnd.Next(1,100);
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
                throw new Exception("Not enough heroes to play");
            }

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);

            if (hero == null)
            {
                return NotFound();
            }

            return hero;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHero(int id, Hero hero)
        {
            if (id != hero.Id)
            {
                return BadRequest();
            }

            _context.Entry(hero).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HeroExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHero(int id)
        {
            var hero = await _context.Heroes.FindAsync(id);
            if (hero == null)
            {
                return NotFound();
            }

            _context.Heroes.Remove(hero);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HeroExists(int id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }

        private int GetRandomValue(int minValue, int maxValue)
        {
            Random random= new Random();
            return random.Next(minValue, maxValue);
        }

        private Rating GetRandomRating(int randomChance)
        {
            switch (randomChance)
            {
                case< 40:
                    return Rating.R;
                case< 65:
                    return Rating.SR;
                case< 96:
                    return Rating.SSR;
                case < 98:
                    return Rating.UR;
                default: return Rating.R;
            }
        }
    }
}
