using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ClinicWebAPI.Data;
using ClinicWebAPI.Models;

namespace ClinicWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AilmentsController : ControllerBase
    {
        private readonly ClinicContext _context;

        public AilmentsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: api/Ailments
        [HttpGet]
        public IEnumerable<Ailment> GetAilments()
        {
            return _context.Ailments;
        }

        // GET: api/Ailments/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAilment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ailment = await _context.Ailments.FindAsync(id);

            if (ailment == null)
            {
                return NotFound();
            }

            return Ok(ailment);
        }

        // PUT: api/Ailments/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAilment([FromRoute] int id, [FromBody] Ailment ailment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ailment.Id)
            {
                return BadRequest();
            }

            _context.Entry(ailment).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AilmentExists(id))
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

        // POST: api/Ailments
        [HttpPost]
        public async Task<IActionResult> PostAilment([FromBody] Ailment ailment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Ailments.Add(ailment);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAilment", new { id = ailment.Id }, ailment);
        }

        // DELETE: api/Ailments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAilment([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ailment = await _context.Ailments.FindAsync(id);
            if (ailment == null)
            {
                return NotFound();
            }

            _context.Ailments.Remove(ailment);
            await _context.SaveChangesAsync();

            return Ok(ailment);
        }

        private bool AilmentExists(int id)
        {
            return _context.Ailments.Any(e => e.Id == id);
        }
    }
}