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
                    .Where(h => h.IsPlayable)
                    .Select(h => new PlayableHeroDto
                    {
                        Id = h.Id,
                        Name = h.Name,
                        AccuracyRate = h.AccuracyRate,
                        Armor = GetRandomValue(h.Armor.DamageReducitonMinimum, h.Armor.DamageReducitonMaximum),
                        HP = GetRandomValue(h.HPMin, h.HPMax),
                        Mana = GetRandomValue(h.ManaMin, h.ManaMax),
                        Attack = GetRandomValue(h.AttackMinimum, h.AttackMaximum),
                        Description= h.Description,
                        HeroClass = h.HeroClass.ClassName,
                        Rating= h.Rating,
                        SpellModifier = h.SpellModifier
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
            var heroes = _context.Heroes
                    .Where(h => h.IsPlayable)
                    .Select(h => new PlayableHeroDto
                    {
                        Id = h.Id,
                        Name = h.Name,
                        AccuracyRate = h.AccuracyRate,
                        Armor = GetRandomValue(h.Armor.DamageReducitonMinimum, h.Armor.DamageReducitonMaximum),
                        HP = GetRandomValue(h.HPMin, h.HPMax),
                        Mana = GetRandomValue(h.ManaMin, h.ManaMax),
                        Attack = GetRandomValue(h.AttackMinimum, h.AttackMaximum),
                        Description = h.Description,
                        HeroClass = h.HeroClass.ClassName,
                        Rating = h.Rating,
                        SpellModifier = h.SpellModifier
                    })
                    .ToList();

            try
            {
                for(int i = 0; i<2; i++)
                {
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

        // GET: api/Heroes/5
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

        // PUT: api/Heroes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
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

        // POST: api/Heroes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hero>> PostHero(Hero hero)
        {
            _context.Heroes.Add(hero);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHero", new { id = hero.Id }, hero);
        }

        // DELETE: api/Heroes/5
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
    }
}
