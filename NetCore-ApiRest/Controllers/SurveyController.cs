using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Entities;

namespace PruebaAspNetCore_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SurveyController : ControllerBase
    {
        private readonly ApplicationDbContextActivities _context;

        public SurveyController(ApplicationDbContextActivities context)
        {
            _context = context;
        }

        // GET: api/Survey
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Survey>>> GetSurvey()
        {
            return await _context.Survey.ToListAsync();
        }

        // GET: api/Survey/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Survey>> GetSurvey(int id)
        {
            var survey = await _context.Survey.FindAsync(id);

            if (survey == null)
            {
                return NotFound();
            }

            return survey;
        }

        // PUT: api/Survey/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSurvey(int id, Survey survey)
        {
            if (id != survey.Id)
            {
                return BadRequest();
            }

            _context.Entry(survey).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SurveyExists(id))
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

        // POST: api/Survey
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Survey>> PostSurvey(Survey survey)
        {
            _context.Survey.Add(survey);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSurvey", new { id = survey.Id }, survey);
        }

        // DELETE: api/Survey/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSurvey(int id)
        {
            var survey = await _context.Survey.FindAsync(id);
            if (survey == null)
            {
                return NotFound();
            }

            _context.Survey.Remove(survey);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SurveyExists(int id)
        {
            return _context.Survey.Any(e => e.Id == id);
        }
    }
}
