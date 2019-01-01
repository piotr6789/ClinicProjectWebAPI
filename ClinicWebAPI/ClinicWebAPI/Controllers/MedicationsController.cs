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
    public class MedicationsController : ControllerBase
    {
        private readonly ClinicContext _context;

        public MedicationsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: api/Medications
        [HttpGet]
        public IEnumerable<Medication> GetMedications()
        {
            return _context.Medications;
        }

        // GET: api/Medications/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMedication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medication = await _context.Medications.FindAsync(id);

            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        // PUT: api/Medications/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMedication([FromRoute] int id, [FromBody] Medication medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != medication.Id)
            {
                return BadRequest();
            }

            _context.Entry(medication).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MedicationExists(id))
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

        // POST: api/Medications
        [HttpPost]
        public async Task<IActionResult> PostMedication([FromBody] Medication medication)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Medications.Add(medication);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMedication", new { id = medication.Id }, medication);
        }

        // DELETE: api/Medications/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMedication([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var medication = await _context.Medications.FindAsync(id);
            if (medication == null)
            {
                return NotFound();
            }

            _context.Medications.Remove(medication);
            await _context.SaveChangesAsync();

            return Ok(medication);
        }

        private bool MedicationExists(int id)
        {
            return _context.Medications.Any(e => e.Id == id);
        }
    }
}