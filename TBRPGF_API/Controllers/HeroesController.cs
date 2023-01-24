using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Data.Context;
using TBRPGF_API.Heroes;
using TBRPGF_API.Enums;
using TBRPGF_API.Services.Heroes.Interfaces;
using TBRPGF_API.Dto.HeroesDto;

namespace TBRPGF_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeroesController : ControllerBase
    {
        private readonly IHeroService _service;

        public HeroesController(IHeroService service)
        {
            _service = service;
        }

        // GET: api/Heroes
        [HttpGet]
        public async Task<ActionResult<List<HeroSampleDto>>> GetHeroes()
        {
            var heroes = await _service.GetHeroes();
            if (heroes == null)
                return BadRequest("No available heroes");
            if (heroes.Count == 0)
                return NotFound("No heroes were found");
            return Ok(heroes);
        }


        [HttpGet]
        [Route("playable")]
        public async Task<ActionResult<List<PlayableHeroDto>>> GetRandomPlayableHeroes()
        {
            var heroes = await _service.GetRandomPlayableHeroes();
            if (heroes == null)
                return BadRequest("No available playable heroes");
            if(heroes.Count == 0)
                return NotFound("No heroes were found");
            
            return Ok(heroes);

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Hero>> GetHero(int id)
        {
            var hero = _service.GetHero(id);
            if(hero == null)
                return NotFound("Hero not found");
            
            return Ok(hero);
        }
        [HttpGet]
        [Route("get-enemy")]
        public async Task<ActionResult<PlayableHeroDto>> GetEnemy(int id)
        {
            var enemy = _service.GetEnemy(id);
            if (enemy == null)
                return NotFound("Hero not found");

            return Ok(enemy);
        }

    }
}
