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
    public class PatientsController : ControllerBase
    {
        private readonly ClinicContext _context;

        public PatientsController(ClinicContext context)
        {
            _context = context;
        }

        // GET: api/Patients
        [HttpGet]
        public IEnumerable<Patient> GetPatients()
        {
            return _context.Patients
                .Include(a => a.Ailments)
                .Include(m => m.Medications)
                .Include(v => v.Visits)
                .Include(d => d.Doctor);
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPatient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await _context.Patients
                .Include(a => a.Ailments)
                .Include(m => m.Medications)
                .Include(v => v.Visits)
                .Include(d => d.Doctor)
                .FirstOrDefaultAsync(i => i.Id == id);

            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPatient([FromRoute] int id, [FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != patient.Id)
            {
                return BadRequest();
            }

            _context.Entry(patient).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        [HttpPost]
        public async Task<IActionResult> PostPatient([FromBody] Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPatient", new { id = patient.Id }, patient);
        }

        // DELETE: api/Patients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePatient([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();

            return Ok(patient);
        }

        private bool PatientExists(int id)
        {
            return _context.Patients.Any(e => e.Id == id);
        }

        // GET api/patients/3/medication
        [HttpGet("{id:int}/medication")]
        public async Task<IActionResult> GetMedications(int id)
        {
            var patient = await _context.Patients
              .Include(m => m.Medications)
              .FirstOrDefaultAsync(i => i.Id == id);

            if (patient == null)
                return NotFound();

            return Ok(patient.Medications);
        }

        // GET api/patients/3/visit
        [HttpGet("{id:int}/visit")]
        public async Task<IActionResult> GetVisits(int id)
        {
            var patient = await _context.Patients
              .Include(m => m.Visits)
              .FirstOrDefaultAsync(i => i.Id == id);

            if (patient == null)
                return NotFound();

            return Ok(patient.Visits);
        }

        // GET api/patients/3/ailment
        [HttpGet("{id:int}/ailment")]
        public async Task<IActionResult> GetAilments(int id)
        {
            var patient = await _context.Patients
              .Include(m => m.Ailments)
              .FirstOrDefaultAsync(i => i.Id == id);

            if (patient == null)
                return NotFound();

            return Ok(patient.Ailments);
        }

        // GET api/patients/3/doctor
        [HttpGet("{id:int}/doctor")]
        public async Task<IActionResult> GetDoctors(int id)
        {
            var patient = await _context.Patients
              .Include(m => m.Doctor)
              .FirstOrDefaultAsync(i => i.Id == id);

            if (patient == null)
                return NotFound();

            return Ok(patient.Doctor);
        }
    }
}