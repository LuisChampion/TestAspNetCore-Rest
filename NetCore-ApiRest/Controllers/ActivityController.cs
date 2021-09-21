using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Data;
using Entities;
using BusinessLogic;
using Entities.Helper;

namespace PruebaAspNetCore_Rest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly ApplicationDbContextActivities _context;
        private ActivityBusinessLogic _ActivityBusinessLogic;

        //public ActivityController(ApplicationDbContextActivities context)
        //{
        //    _context = context;
        //}

        public ActivityController()
        {
            _ActivityBusinessLogic = new ActivityBusinessLogic();
        }

        // GET: api/Activity
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Activity>>> GetActivity()
        {
            
            return await _ActivityBusinessLogic.GetAsync(); 
        }

        // GET: api/Activity/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Activity>> GetActivity(int id)
        {
            var activity = await _ActivityBusinessLogic.GetAsync(id);
            if (activity == null)
            {
                return NotFound();
            }
            return activity;
        }

        // PUT: api/Activity/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutActivity(int id, Activity activity)
        {
            if (id != activity.Id)
            {
                return BadRequest();
            }

            _context.Entry(activity).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ActivityExists(id))
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

        // POST: api/Activity
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Message<Activity>>> PostActivity(Activity activity)
        {            
            var newActivity = await _ActivityBusinessLogic.AddAsync(activity);
            if (newActivity == null)
            {
                return NotFound();
            }
            //_context.Activity.Add(activity);
            //await _context.SaveChangesAsync();
            //return newActivity; // CreatedAtAction("GetActivity", new { id = newActivity.Id }, newActivity);

            return newActivity;
            //return NoContent();
        }

        // DELETE: api/Activity/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteActivity(int id)
        {
            var activity = await _context.Activity.FindAsync(id);
            if (activity == null)
            {
                return NotFound();
            }

            _context.Activity.Remove(activity);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ActivityExists(int id)
        {
            return _context.Activity.Any(e => e.Id == id);
        }

        [HttpPut("Cancel/{id}")]
        public async Task<ActionResult<Message<Activity>>> CancelActivity(int id)
        {
            var modififyActivity = await _ActivityBusinessLogic.CancelAsync(id);
            return modififyActivity;
        }

        [HttpPost("Reagenda")]
        public async Task<ActionResult<Message<Activity>>> ReagendaActivity(Activity activity)
        {
            var modififyActivity = await _ActivityBusinessLogic.ReagendarAsync(activity);
            return modififyActivity;
        }

    }
}
