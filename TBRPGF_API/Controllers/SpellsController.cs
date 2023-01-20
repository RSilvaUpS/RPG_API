using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TBRPGF_API.Data.Context;
using TBRPGF_API.Heroes;
using TBRPGF_API.Services.Spells.Interface;

namespace TBRPGF_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpellsController : ControllerBase
    {
        private readonly ISpellService _service;

        public SpellsController(ISpellService service)
        {
            _service = service;
        }

        // GET: api/Spells
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Spell>>> GetSpells()
        {
            var spells = await _service.GetSpells();
            if (spells == null)
                return BadRequest("No available spells");
            if (spells.Count() == 0)
                return NotFound("No spells were found");
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Spell>> GetSpell(int id)
        {
            var spell= await _service.GetSpell(id);
            if (spell == null)
                return NotFound("Spell not Found");

            return spell;
        }

    }
}
